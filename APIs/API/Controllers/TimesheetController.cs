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
                new Client { Id = 1, Name = "J&J", Projects = GetProjects(1) },
                new Client { Id = 2, Name = "Jhonson Controls", Projects = GetProjects(2) },
                new Client { Id = 2, Name = "Markel", Projects = GetProjects(1) }
            };
        }

        [Route("GetProjects/{userId}")]
        public List<Project> GetProjects(int userId = 0)
        {

            //implement caching as well
            return (new List<Project> {
                new Project { Id = 1001, Name = "Data Integration", UserId = 101 },
                new Project {Id = 1002, Name = "Data Research", UserId = 101},
                new Project {Id = 1003, Name = "Data Mining", UserId = 202},
                new Project {Id = 1004, Name = "Research & Development", UserId = 202}

            }).Where(p => userId == 0 || p.UserId == userId).ToList();
        }

        [Route("GetTasks")]
        public List<Task> GetTasks(int projectId = 0)
        {
            //implement caching as well
            var tasks = new List<Task> {
                new Task { Id = 1, Name = "Billable Task", TaskTypeId = 1, ProjectId = 1001 },
                new Task { Id = 2, Name = "Leave", TaskTypeId = 2, ProjectId = 1001},
                new Task { Id = 2, Name = "Public Holiday", TaskTypeId = 2, ProjectId = 1001}
            };

            tasks.ForEach(t => t.TaskType = GetTaskTypes(t.TaskTypeId).FirstOrDefault());
            return tasks.Where(t => t.ProjectId == projectId).ToList();
        }

        [Route("GetTasksType")]
        public List<TaskType> GetTaskTypes(int taskTypeId = 0)
        {
            //implement caching as well
            return new List<TaskType> {
                new TaskType { Id = 1, Name = "Billable" },
                new TaskType {Id = 2, Name = "Non-Billable"}
            };
        }

        [Route("InsertEntitiesData/{d}")]
        [HttpPost]
        public bool InsertEntitiesData(DataEntities d = null)
        {
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
                var exitstingProject = appObj.Projects.Where(pro => pro.Name == p.Name && p.UserId == pro.UserId).FirstOrDefault();
                if (exitstingProject == null)
                    appObj.Projects.Add(p);
            });

            //tasks
            d.Tasks.ForEach(t =>
            {
                var exitstingTask = appObj.Tasks.Where(task => task.Name == t.Name && task.TaskTypeId == t.TaskTypeId).FirstOrDefault();
                if (exitstingTask == null)
                    appObj.Tasks.Add(t);
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
                Tasks = appObj.Tasks.ToList(),
                TaskTypes = appObj.TaskTypes.ToList()
            };

        }

    }
}
