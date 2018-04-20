    using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Timesheet_demo.Models;

namespace Timesheet_demo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = BuildWebHost(args);

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<AppDbContext>();
                try
                {
                    
                    DbInititalizer.Seed(context);
                    Employee employee = context.Employees.Single(p=>p.Id==3);
                    // context.Database.ExecuteSqlCommand("UPDATE dbo.Employee SET Name = 'Jane' WHERE PersonId = 3");
                    System.Diagnostics.Debug.WriteLine(employee.Name);
                    employee.Name = "Kaniscsasdasdaddcsdhk";
                    context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    System.Diagnostics.Debug.WriteLine("Someone already updated this data!!");
                    var entry = ex.Entries.Single(); 

                }
            }
            //host.Run();
            
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
