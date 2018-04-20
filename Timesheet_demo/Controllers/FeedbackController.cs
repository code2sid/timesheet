using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timesheet_demo.Models;

namespace Timesheet_demo.Controllers
{
    public class FeedbackController : Controller
    {
        private readonly IFeedback _feedbackRepository;
        public FeedbackController(IFeedback feedback)
        {
            _feedbackRepository = feedback;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}