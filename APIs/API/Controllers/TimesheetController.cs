using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/TimeSheet")]
    public class TimesheetController : ApiController
    {
        readonly AppDbContext appObj = new AppDbContext();

        [Route("GetClients")]
        public List<Client> GetClients()
        {
            //implement caching as well
            return new List<Client> {
                new Client { Id = 1, Name = "J&J", Projects = GetUserProjects(1) },
                new Client { Id = 2, Name = "Jhonson Controls", Projects = GetUserProjects(2) },
                new Client { Id = 2, Name = "Markel", Projects = GetUserProjects(1) }
            };
        }

        [Route("GetProjects/{userId}")]
        public List<Project> GetUserProjects(int userId = 0)
        {
            var projectids = appObj.UserProjects.Where(up => up.UserId == userId).Select(p => p.ProjectId);
            return appObj.Projects.Where(p => projectids.Contains(p.Id)).ToList();
        }

        [Route("GetTasks")]
        public List<Task> GetTasks(int projectId = 0)
        {
            var pt1 = new ProjectTask { Id = 1, ProjectId = 1, TaskId = 1 };
            appObj.ProjectTasks.Add(pt1);
            pt1 = new ProjectTask { Id = 2, ProjectId = 1, TaskId = 2 };
            appObj.ProjectTasks.Add(pt1);
            pt1 = new ProjectTask { Id = 3, ProjectId = 1, TaskId = 3 };
            appObj.ProjectTasks.Add(pt1);

            appObj.SaveChanges();


            var taskIds = appObj.ProjectTasks.Where(pt => pt.ProjectId == projectId).Select(p => p.TaskId);
            return appObj.Tasks.Where(t => taskIds.Contains(t.Id)).ToList();
        }

        [Route("GetTasksType")]
        public List<TaskType> GetTaskTypes(int taskTypeId = 0)
        {

            //implement caching as well
            return appObj.TaskTypes.ToList();
        }

        [Route("SaveTimeSheet")]
        [Route("SubmitTimeSheet")]
        [HttpPost]
        public bool InsertTimeSheetData([FromBody] List<UserTimeSheet> value)
        {
            var tsDateHours = new TimeSheetDateHours();
            var lstDateHours = new List<TimeSheetDateHours>();

            value.ForEach(uts =>
            {
                uts.IsSaved = true;
                appObj.UserTimeSheet.Add(uts);
                int masterId = appObj.UserTimeSheet.Select(ut => ut.Id).Count() == 0 ? 0 : appObj.UserTimeSheet.Select(ut => ut.Id).Max() + 1;
                int i = 0;
                tsDateHours = new TimeSheetDateHours();


                uts.FillDates.ForEach(fd => lstDateHours.Add(new TimeSheetDateHours { Date = fd, TimesheetId = masterId }));
                uts.DatesHrs.ForEach(dh => lstDateHours[i++].Hours = dh);
            });

            lstDateHours.ForEach(dh => appObj.TimeSheetDateHours.Add(dh));

            try
            {
                appObj.SaveChanges();
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        [Route("InsertEntitiesData")]
        [HttpPost]
        public bool InsertEntitiesData(DataEntities d)
        {
            if (d == null)
                return false;
            //users
            d.Users.ForEach(u =>
            {
                var exitstingUsr = appObj.Users.Where(usr => usr.Name == u.Name && usr.Password == u.Password).FirstOrDefault();
                if (exitstingUsr == null)
                    appObj.Users.Add(u);
            });

            //projects
            d.Projects.ForEach(p =>
            {
                var exitstingProject = appObj.Projects.Where(pro => pro.Name == p.Name).FirstOrDefault();
                if (exitstingProject == null)
                    appObj.Projects.Add(p);
            });

            //UserProjects
            d.UserProjects.ForEach(p =>
            {
                var exitstingUserProject = appObj.UserProjects.Where(pro => pro.ProjectId == p.ProjectId && pro.UserId == p.UserId).FirstOrDefault();
                if (exitstingUserProject == null)
                    appObj.UserProjects.Add(p);
            });

            //tasks
            d.Tasks.ForEach(t =>
            {
                var exitstingTask = appObj.Tasks.Where(task => task.Name == t.Name).FirstOrDefault();
                if (exitstingTask == null)
                    appObj.Tasks.Add(t);
            });

            //ProjectTasks
            d.ProjectTasks.ForEach(t =>
            {
                var exitstingProjectTask = appObj.ProjectTasks.Where(task => task.ProjectId == t.ProjectId && task.Id == t.Id).FirstOrDefault();
                if (exitstingProjectTask == null)
                    appObj.ProjectTasks.Add(t);
            });

            //taskTypes
            d.TaskTypes.ForEach(tt =>
            {
                var exitstingTaskType = appObj.TaskTypes.Where(taskType => taskType.Name == tt.Name).FirstOrDefault();
                if (exitstingTaskType == null)
                    appObj.TaskTypes.Add(tt);
            });

            try
            {
                appObj.SaveChanges();
            }
            catch (System.Exception ex)
            {
                return false;
            }

            return true;
        }

        [Route("GetEntitiesData")]
        public DataEntities GetEntitiesData()
        {
            return new DataEntities
            {
                Users = appObj.Users.ToList(),
                Projects = appObj.Projects.ToList(),
                UserProjects = appObj.UserProjects.ToList(),
                Tasks = appObj.Tasks.ToList(),
                ProjectTasks = appObj.ProjectTasks.ToList(),
                TaskTypes = appObj.TaskTypes.ToList()
            };

        }

        [Route("GetPendingApprovals")]
        public List<PendingApproval> GetPendingApprovals(DateTime? from = null, DateTime? to = null)
        {
            var lst = new List<PendingApproval>();
            int[] ids = null;
            var utLst = appObj.UserTimeSheet.Where(ut => ut.IsSubmitted == true && ut.IsApproved == false).ToList();
            if (from != null && to != null)
                ids = appObj.TimeSheetDateHours.Where(dh => dh.Date >= from && dh.Date <= to).Select(dh => dh.TimesheetId).Distinct().ToArray();
            if (ids != null && ids.Count() > 0)
                utLst = utLst.Where(l => ids.Contains(l.Id)).ToList();
            utLst.ForEach(ut =>
                  {
                      var userName = appObj.Users.Where(u => u.Id == ut.UserId).FirstOrDefault().Name;
                      var totalHrs = 0;
                      var tsId = 0;
                      appObj.TimeSheetDateHours.Where(dh => dh.TimesheetId == ut.Id).ToList().ForEach(dh => { totalHrs += dh.Hours; if (tsId == 0) tsId = dh.TimesheetId; });

                      var weekrange = appObj.TimeSheetDateHours.Where(dh => dh.TimesheetId == ut.Id).Select(w => w.Date).Min().Value.ToString("MMM/dd/yyyy") + "-" +
                          appObj.TimeSheetDateHours.Where(dh => dh.TimesheetId == ut.Id).Select(w => w.Date).Max().Value.ToString("MMM/dd/yyyy");
                      lst.Add(new PendingApproval { WeekRange = weekrange, TotalHours = totalHrs, UserName = userName, TimeSheetId = tsId });
                  });

            return lst;

        }

        [Route("ApproveTimeSheets")]
        [HttpPost]
        public bool ApproveTimeSheets(int[] TimeSheetIds)
        {
            foreach (int id in TimeSheetIds)
            {
                appObj.UserTimeSheet.Where(ut => ut.Id == id).FirstOrDefault().IsApproved = true;
            }

            try
            {
                appObj.SaveChanges();
            }
            catch (System.Exception)
            {

                return false;

            }
            return true;
        }

    }
}
