using Application.DTO;
using Application.Interfaces;
using Application.Repository.IBase;
using Domain.Models;
using Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<bool> AddEmployee(AddEmployeeDTO employee, CancellationToken cancellationToken)
        {
            if (employee == null) throw new ArgumentNullException();
            var data = _unitOfWork.Repository<Employee>().FindByCondition(s => s.Email == employee.Email&&s.PhoneNumber==employee.PhoneNumber).ToList();
            if (data.Count > 0) return false;

            var emp = new Employee();
            emp.Name=employee.Name;
            emp.Image= await ConvertToByteArray(employee.Image);
            emp.Grad=employee.Grad;
            emp.Email=employee.Email;
            emp.PhoneNumber=employee.PhoneNumber;
            _unitOfWork.Repository<Employee>().Create(emp);
            var res = await _unitOfWork.CompleteAsync(cancellationToken);
            if (res == 0) return false;
            else return true;
        }

        public async Task<bool> DeleteEmployee(int id, CancellationToken cancellationToken)
        {
            if (id == null) throw new ArgumentNullException();

            var data = _unitOfWork.Repository<Employee>().FindByCondition(s => s.Id == id).FirstOrDefault();
            _unitOfWork.Repository<Employee>().Delete(data);
            var res = await _unitOfWork.CompleteAsync(cancellationToken);
            if (res == 0) return false;
            else return true;

        }

        public Task<List<Employee>> GetAllEmployees()
        {
           return _unitOfWork.Repository<Employee>().FindAll().ToListAsync();

        }

        public async Task<Employee> GetById(int id)
        {
            return  _unitOfWork.Repository<Employee>().FindByCondition(s => s.Id == id).FirstOrDefault();
        }

        public async Task<bool> UpdateEmployee(AddEmployeeDTO employee, CancellationToken cancellationToken)
        {
            if (employee == null) throw new ArgumentNullException();

            var data =  _unitOfWork.Repository<Employee>().FindByCondition(s=>s.Id==employee.Id).FirstOrDefault();
            if (data == null) return false;

            data.Name = employee.Name;
            data.Email = employee.Email;
            data.Image = await ConvertToByteArray(employee.Image);
            data.PhoneNumber = employee.PhoneNumber;
            _unitOfWork.Repository<Employee>().Update(data);
            var res = await _unitOfWork.CompleteAsync(cancellationToken);
            if (res == 0) return false;
            else return true;
        }
        private async Task<byte[]> ConvertToByteArray(IFormFile file)
        {
            using var memoryStream = new MemoryStream();
            await file.CopyToAsync(memoryStream);
            return memoryStream.ToArray();
        }
    }
}
