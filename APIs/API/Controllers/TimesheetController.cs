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

        [Route("InsertData")]
        public bool InsertData(object insertData)
        {
            //users
            //var users = insertData.collection["users"];

            var users = new List<User>();
            users.Add(new User { Id = 101, Name = "sid", RoleId = 1, Password = "sid@123" });
            users.Add(new User { Id = 102, Name = "rahul", RoleId = 1, Password = "rahul@123" });
            users.Add(new User { Id = 103, Name = "ruchi", RoleId = 2, Password = "ruchi@123" });
            users.Add(new User { Id = 104, Name = "sugi", RoleId = 2, Password = "sugi@123" });
            users.ForEach(u => appObj.Users.Add(u));

            //projects
            var projects = new List<Project> {
                new Project { Id = 1001, Name = "Data Integration", UserId = 101 },
                new Project {Id = 1002, Name = "Data Research", UserId = 101},
                new Project {Id = 1003, Name = "Data Mining", UserId = 202},
                new Project {Id = 1004, Name = "Research & Development", UserId = 202}};
            projects.ForEach(p => appObj.Projects.Add(p));

            //tasks
            var tasks = new List<Task> {
                new Task { Id = 1, Name = "Billable Task", TaskTypeId = 1, ProjectId = 1001 },
                new Task { Id = 2, Name = "Leave", TaskTypeId = 2, ProjectId = 1001},
                new Task { Id = 2, Name = "Public Holiday", TaskTypeId = 2, ProjectId = 1001}
            };
            tasks.ForEach(t => appObj.Tasks.Add(t));

            var taskTypes = new List<TaskType> {
                new TaskType { Id = 1, Name = "Billable" },
                new TaskType {Id = 2, Name = "Non-Billable"}
            };
            taskTypes.ForEach(tt => appObj.TaskTypes.Add(tt));

            appObj.SaveChanges();
            return true;
        }

    }
}
