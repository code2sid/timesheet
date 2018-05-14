using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class PendingApproval
    {
        public int TimeSheetId { get; set; }
        public string WeekRange { get; set; }
        public int TotalHours { get; set; }
        public string UserName { get; set; }
    }
}