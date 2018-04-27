using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class DataEntities
    {
        public List<User> Users { get; set; }
        public List<Project> Projects { get; set; }
        public List<Task> Tasks { get; set; }
        public List<TaskType> TaskTypes { get; set; }
    }
}