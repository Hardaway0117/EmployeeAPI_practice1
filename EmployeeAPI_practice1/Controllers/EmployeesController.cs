using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

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

        [HttpPut("{id}")]
        public IActionResult UpdateEmployee(int id, Employee updateEmployee)
        {
            var existingEmployee = Employees.FirstOrDefault(e => e.Id == id);

            if (existingEmployee == null)
            {
                return NotFound(new { message = $"員工 ID {id} 不存在" });
            }
            existingEmployee.Name = updateEmployee.Name;
            existingEmployee.Department = updateEmployee.Department;
            existingEmployee.Salary = updateEmployee.Salary;

            return Ok
                (new
                {
                    message = "員工資料更新成功",
                    data = existingEmployee
                }
                );
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteEmployee(int id)
        {
            var existingEmployee = Employees.FirstOrDefault (e => e.Id == id);

            if (existingEmployee == null)
            {
                return NotFound(new { message = $" ID 為 {id} 的員工不存在" });
            }
            Employees.Remove(existingEmployee);
            return Ok(new { message = $"員工ID : {id}資料刪除成功" });
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
