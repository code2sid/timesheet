using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class WeekDates
    {
        public int Id { get; set; }
        public int WeekNumber { get; set; }
        public DateTime TimeSheetDate { get; set; }
        public int Hours { get; set; }
        public bool IsSaved { get; set; }
        public bool IsSubmitted { get; set; }
        public bool IsApproved { get; set; }
        public string Comments { get; set; }

    }
}