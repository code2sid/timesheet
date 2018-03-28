using API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/GeneralLiability")]
    public class TimesheetController : ApiController
    {

        [Route("GetProjects")]
        public List<Project> GetProjects(int clinetID)
        {
            return new List<Project> {
                new Project { Id = 1001, Name = "Data Integration" },
                new Project {Id=1002,Name="Data Research"}
            };
        }
    }
}
