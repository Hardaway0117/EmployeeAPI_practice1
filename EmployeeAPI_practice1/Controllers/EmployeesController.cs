using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI_practice1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private static readonly List<Employee> Employees = new()
        {
            new Employee { Id = 1, Name = "Tom", Department = "IT", Salary = 60000 },
            new Employee { Id = 2, Name = "Amy", Department = "HR", Salary = 42000 },
            new Employee { Id = 3, Name = "Ken", Department = "IT", Salary = 52000 },
        };

        [HttpGet]
        public ActionResult<IEnumerable<Employee>> GetAllEmployees()
        {
            return Ok(Employees);
        }

        [HttpPost]
        public ActionResult<Employee> AddEmployee(Employee newEmployee)
        {
            int newId = Employees.Any() ? Employees.Max(e => e.Id) + 1 : 1;
            newEmployee.Id = newId;

            Employees.Add(newEmployee);

            return CreatedAtAction(nameof(GetAllEmployees), new { id = newId }, newEmployee);
        }
    }
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Department { get; set; }
        public int Salary { get; set; }
    }
}
