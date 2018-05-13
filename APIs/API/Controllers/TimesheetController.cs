using API.Models;
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
        [HttpPost]
        public bool InsertTimeSheetData([FromBody] List<UserTimeSheet> value)
        {
            value.ForEach(uts =>
            {
                uts.IsSaved = true;
                appObj.UserTimeSheet.Add(uts);
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
        public List<PendingApproval> GetPendingApprovals()
        {
            var lst = new List<PendingApproval>();
            appObj.UserTimeSheet.Where(uts => uts.IsApproved == false && uts.IsSubmitted == true).ToList().ForEach(ut =>
            {
                var userName = appObj.Users.Where(u => u.Id == ut.UserId).FirstOrDefault().Name;
                var totalHrs = 0;
                ut.DatesHrs.ForEach(h => totalHrs += h);
                var weekrange = ut.FillDates[0].ToString() + "-" + ut.FillDates[6].ToString();
                lst.Add(new PendingApproval { WeekRange = weekrange, TotalHours = totalHrs, UserName = userName });
            });

            return lst;

        }

    }
}
