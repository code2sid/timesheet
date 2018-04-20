using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet_demo.Models
{
    public class Employee
    {
        public int Id { get; set; }

        [ConcurrencyCheck]
        public string Name { get; set; }

        [ConcurrencyCheck]
        public int Employee_id { get; set; }
        public string Role { get; set; }
        public string Client_id { get; set; }
        public string Project_id { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }


    }
}
