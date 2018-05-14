using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
            : base("AppDbContext")
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DbSet<ProjectTask> ProjectTasks { get; set; }
        public DbSet<TaskType> TaskTypes { get; set; }
        public DbSet<UserTimeSheet> UserTimeSheet { get; set; }
        public DbSet<TimeSheetDateHours> TimeSheetDateHours { get; set; }

    }
}