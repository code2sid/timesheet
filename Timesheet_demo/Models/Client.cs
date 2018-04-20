using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Timesheet_demo.Models
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Project_id { get; set; }
    }
}