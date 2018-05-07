using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class TimeSheetApproval
    {
        public int Id { get; set; }
        public int TimeSheetId { get; set; }
        public int ApproverId { get; set; }
        public DateTime SubmitDate { get; set; }
        public DateTime ApproverDate { get; set; }
        public string Comments { get; set; }
    }
}