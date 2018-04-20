using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Timesheet_demo.Models
{
    public class MockEmpRepository : IEmployee
    {
        private List<Employee> _employees;

        public MockEmpRepository()
        {
            if (_employees == null)
            {
                InitializeEmployees();
            }
        }

        public void InitializeEmployees()
        {
            _employees = new List<Employee>
            {
                new Employee{Id=1, Name="Lokesh", Employee_id=1024, Role="SWE", Client_id="abc2", Project_id="CG"},
                new Employee{Id=1, Name="Rahul", Employee_id=2024, Role="SE", Client_id="abc1", Project_id="BOA"},
                new Employee{Id=1, Name="Hitesh", Employee_id=1124, Role="SWE2", Client_id="abc3", Project_id="Chase"},
                new Employee{Id=1, Name="Karunakar", Employee_id=1524, Role="Lead", Client_id="abc4", Project_id="Microsoft"},
            };
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _employees;
        }

        public Employee GetEmpById(int EmpId)
        {
            return _employees.FirstOrDefault(emp => emp.Id == EmpId);
        }

        public void AddEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void EditEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }

        public void DeleteEmployee(Employee employee)
        {
            throw new NotImplementedException();
        }
    }
}
