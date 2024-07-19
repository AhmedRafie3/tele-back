using Application.DTO;
using Application.Interfaces;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading;

namespace TeleperformanceTask.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class EmployeesController(IEmployeeService _employeeService) : ControllerBase
    {
        [HttpPost("AddEmployee")]
        
        public Task<bool> AddEmployee([FromForm] AddEmployeeDTO Employee, CancellationToken cancellationToken)
        {
            return _employeeService.AddEmployee(Employee, cancellationToken);
        }

        [HttpDelete("DeleteEmployee")]
        [Authorize(Roles = "Admin")]
        public Task<bool> DeleteEmployee(int id, CancellationToken cancellationToken)
        {
            return _employeeService.DeleteEmployee(id, cancellationToken);
        }

        [HttpGet("GetAllEmployes")]
       
        public Task<List<Employee>> GetAllEmployees()
        {
            return _employeeService.GetAllEmployees();
        }
        [HttpGet("GetById")]

        public Task<Employee> GetById(int id)
        {
            return _employeeService.GetById(id);
        }

        [HttpPut("UpdateEmployee")]
        [Authorize(Roles = "Admin")]
        public Task<bool> UpdateEmployee([FromForm]AddEmployeeDTO Employee, CancellationToken cancellationToken)
        {
            return _employeeService.UpdateEmployee(Employee, cancellationToken);
        }
    }
}
