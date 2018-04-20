using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet_demo.Models
{
    public static class DbInititalizer
    {
        public static void Seed(AppDbContext context)
        {
            if (!context.Employees.Any())
            {
                context.AddRange
                (
                    new Employee { Name = "Lokesh", Employee_id = 1024, Role = "SWE", Client_id = "abc2", Project_id = "CG" },
                    new Employee { Name = "Rahul", Employee_id = 2024, Role = "SE", Client_id = "abc1", Project_id = "BOA" },
                    new Employee { Name = "Hitesh", Employee_id = 1124, Role = "SWE2", Client_id = "abc3", Project_id = "Chase" },
                    new Employee { Name = "Karunakar", Employee_id = 1524, Role = "Lead", Client_id = "abc4", Project_id = "Microsoft" }
                );
                Console.WriteLine("I wil save the data now!");
                context.SaveChanges();
                Console.WriteLine("Data saved!!");
            }
        }
    }
}
