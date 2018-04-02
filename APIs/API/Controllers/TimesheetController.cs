using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/TimeSheet")]
    public class TimesheetController : ApiController
    {

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

        [Route("GetProjects")]
        public List<Project> GetProjects(int clientId = 0)
        {

            //implement caching as well
            return new List<Project> {
                new Project { Id = 1001, Name = "Data Integration", ClientId = 1 },
                new Project {Id = 1002, Name = "Data Research", ClientId = 1},
                new Project {Id = 1003, Name = "Data Mining", ClientId = 2},
                new Project {Id = 1004, Name = "Research & Development", ClientId = 2}

            };
        }

        [Route("GetTasks")]
        public List<Task> GetTasks(int projectId = 0)
        {
            //implement caching as well
            var tasks = new List<Task> {
                new Task { Id = 1, Name = "Billable Task", TaskTypeId = 1 },
                new Task { Id = 2, Name = "Leave", TaskTypeId = 2},
                new Task { Id = 2, Name = "Public Holiday", TaskTypeId = 2}
            };

            tasks.ForEach(t => t.TaskType = GetTaskTypes(t.TaskTypeId).FirstOrDefault());
            return tasks;
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
    }
}
