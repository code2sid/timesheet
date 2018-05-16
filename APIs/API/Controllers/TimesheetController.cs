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

        [Route("GetSavedTimeSheets")]
        [Route("GetPendingApprovals")]
        public List<PendingApproval> GetPendingApprovals(RequestData request)
        {
            PendingApproval pa;
            var lst = new List<PendingApproval>();
            int[] ids = null;
            var utLst = appObj.UserTimeSheet.Where(ut => ut.IsSubmitted == request.IsSubmitted).ToList();
            if (!request.IncludeApproved)
                utLst = utLst.Where(ut => ut.IsApproved == false).ToList();
            if (request.From != null && request.To != null)
            {
                ids = appObj.TimeSheetDateHours.Where(dh => dh.Date >= request.From && dh.Date <= request.To).Select(dh => dh.TimesheetId).Distinct().ToArray();
                utLst = utLst.Where(l => ids.Contains(l.Id)).ToList();
            }

            if (request.UserId > 0)
                utLst = utLst.Where(l => l.UserId == request.UserId).ToList();

            utLst.ForEach(ut =>
                  {
                      pa = new PendingApproval
                      {
                          UserName = appObj.Users.Where(u => u.Id == ut.UserId).FirstOrDefault().Name
                      };
                      appObj.TimeSheetDateHours.Where(dh => dh.TimesheetId == ut.Id).ToList().ForEach(dh => { pa.TotalHours += dh.Hours; if (pa.TimeSheetId == 0) pa.TimeSheetId = dh.TimesheetId; });
                      pa.Status = ut.IsApproved ? "Approved" : ut.IsSubmitted ? "Approval Pending" : "Saved and Not Submitted";
                      pa.WeekRange = appObj.TimeSheetDateHours.Where(dh => dh.TimesheetId == ut.Id).Select(w => w.Date).Min().Value.ToString("MMM/dd/yyyy") + "-" +
                          appObj.TimeSheetDateHours.Where(dh => dh.TimesheetId == ut.Id).Select(w => w.Date).Max().Value.ToString("MMM/dd/yyyy");
                      lst.Add(pa);
                  });

            return lst;

        }

        [Route("ApproveTimeSheets")]
        [Route("RejectTimeSheets")]
        [Route("BulkSubmitTimeSheets")]
        [HttpPost]
        public bool ApproveTimeSheets(int[] TimeSheetIds, string actionType = "ApproveTimeSheets")
        {
            switch (actionType)
            {
                case "ApproveTimeSheets":
                    {
                        appObj.UserTimeSheet.Where(ut => TimeSheetIds.Contains(ut.Id)).ToList().ForEach(ut => ut.IsApproved = true);
                        break;
                    }
                case "RejectTimeSheets":
                    {
                        appObj.UserTimeSheet.Where(ut => TimeSheetIds.Contains(ut.Id)).ToList().ForEach(ut =>
                        {
                            ut.IsApproved = false;
                            ut.IsSubmitted = false;
                        });
                        break;
                    }
                case "BulkSubmitTimeSheets":
                    {
                        appObj.UserTimeSheet.Where(ut => TimeSheetIds.Contains(ut.Id)).ToList().ForEach(ut => ut.IsSubmitted = true);
                        break;
                    }

                default:
                    {
                        appObj.UserTimeSheet.Where(ut => TimeSheetIds.Contains(ut.Id)).ToList().ForEach(ut => ut.IsApproved = true);
                        break;
                    }

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
