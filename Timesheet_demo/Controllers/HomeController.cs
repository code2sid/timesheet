using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Timesheet_demo.Models;
using Timesheet_demo.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Timesheet_demo.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEmployee _empRepository;

        public HomeController(IEmployee empRepository)
        {
            _empRepository = empRepository;
        }

        [HttpGet]
        public IActionResult Index()    
        {
            var employees = _empRepository.GetAllEmployees().OrderBy(emp => emp.Name);
            var homeViewModel = new HomeViewModel()
            {
                Title = "Employee List",
                Employees=employees.ToList()
            };
            return View(homeViewModel);
        }

        [HttpDelete]
        public IActionResult DeleteIndex(Employee employee)
        {
            _empRepository.DeleteEmployee(employee);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditIndex(Employee employee)
        {
            System.Diagnostics.Debug.WriteLine(employee.Name, employee.Id);
            _empRepository.EditEmployee(employee);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddIndex(Employee employee)
        {
            _empRepository.AddEmployee(employee);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult CreateNew()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var employees = _empRepository.GetAllEmployees();
            foreach(var emp in employees)
            {
                if (emp.Id == id) return View(emp);
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(Employee employee)
        {
            return View(employee);
        }
    }
}
