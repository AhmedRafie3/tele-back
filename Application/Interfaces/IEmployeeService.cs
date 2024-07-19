using Application.DTO;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IEmployeeService
    {
        public Task<List<Employee>> GetAllEmployees();
        public Task<Employee> GetById(int id);
        public Task<bool> AddEmployee(AddEmployeeDTO Employee, CancellationToken cancellationToken);
        public Task<bool> UpdateEmployee(AddEmployeeDTO Employee, CancellationToken cancellationToken);
        public Task<bool> DeleteEmployee(int id, CancellationToken cancellationToken);
    }
}
