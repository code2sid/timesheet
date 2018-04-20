using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet_demo.Models
{
    public interface IEmployee
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmpById(int EmpId);

        void AddEmployee(Employee employee);
        void EditEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
    }
}
