using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.Models
{
    public class RequestData
    {
        //DateTime? from = null, DateTime? to = null, bool includeApproved = false, bool IsSubmitted = true
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public bool IncludeApproved { get; set; }
        public bool IsSubmitted { get; set; }
        public int UserId { get; set; }

        public RequestData()
        {
            this.IsSubmitted = true;
        }
    }
}