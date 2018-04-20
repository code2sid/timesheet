using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet_demo.Models
{
    public class TimeSheet
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int Emp_id { get; set; }
        public int Emp_name { get; set; }
        public string Client_name { get; set; }
        public string Project_name { get; set; } 
        public string Task_type { get; set; }
        public int Hours { get; set; }
        public string Comment { get; set; }
    }
}
