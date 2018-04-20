using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet_demo.Models
{
    public interface IFeedback
    {
        void AddFeedback(Feedback feedback);
    }
}
