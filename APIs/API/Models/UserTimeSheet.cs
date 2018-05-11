using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class UserTimeSheet
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int TaskId { get; set; }
        public DateTime FillDate { get; set; }
        public int Hours { get; set; }
        public bool IsSaved { get; set; }
        public bool IsSubmitted { get; set; }
        public string Comments { get; set; }
    }

    public class UserTimeSheetRequest
    {
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int TaskId { get; set; }
        public List<DateTime> FillDates { get; set; }
        public List<int> DatesHrs { get; set; }
    }
}