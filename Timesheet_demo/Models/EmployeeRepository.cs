using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet_demo.Models
{
    public class EmployeeRepository : IEmployee
    {

        private readonly AppDbContext _appDbContext;

        public EmployeeRepository(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _appDbContext.Employees; 
        }

        public Employee GetEmpById(int EmpId)
        {
            return _appDbContext.Employees.FirstOrDefault(emp => emp.Id == EmpId);
        }

        public void AddEmployee(Employee employee)
        {
            _appDbContext.Employees.Add(employee);
            _appDbContext.SaveChanges();
        }

        public void EditEmployee(Employee employee)
        {
            System.Diagnostics.Debug.WriteLine(employee.Id);
            var emp = GetEmpById(employee.Id);
            if (emp == null) System.Diagnostics.Debug.WriteLine("Again null object");
            //emp.Name = employee.Name;
            //emp.Employee_id = employee.Employee_id;
            //emp.Role = employee.Role;
            //emp.Project_id = employee.Project_id;
            //emp.Client_id = employee.Client_id;
            //_appDbContext.SaveChanges();
        }

        public void DeleteEmployee(Employee employee)
        {
            var emp = _appDbContext.Employees.Find(employee.Id);
            _appDbContext.Employees.Remove(emp);
        }
    }
}
