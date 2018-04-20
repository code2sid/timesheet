using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Timesheet_demo.Models;

namespace Timesheet_demo.Models
{
    public class FeedbackRepository : IFeedback

    {
        private readonly AppDbContext _appDbContext;

        public FeedbackRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public void AddFeedback(Feedback feedback)
        {
            _appDbContext.Feedbacks.Add(feedback);
            _appDbContext.SaveChanges();
        }
    }
}
