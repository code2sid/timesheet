using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet_demo.Models;

namespace Timesheet_demo.ViewModels
{
    public class HomeViewModel
    {
        public string Title { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
