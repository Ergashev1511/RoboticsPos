using Microsoft.EntityFrameworkCore;
using RoboticsPos.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RoboticsPos.Data.Enum;

namespace RoboticsPos.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {

        private readonly AppDbContext context;
        public EmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }
        public async Task<Employee> CreateEmployee(Employee employee)
        {
            // Employee manager bo'lsa shunda tekshiradi chashier uchun tekshirmaydi oK? ok Ui ni to'g'irlab buni ham to'g'irlaymanOK bo'ldimi savol yo'qmiha bo'ldi rahamt  OK
            var hasscopy = await context.Employees.AnyAsync(a => !a.IsDeleted && (employee.EmployeeRole == EmployeeRole.Manager ? a.User.UserName == employee.User.UserName : false));

            if (hasscopy) throw new Exception("Current username already exist!");

            await context.Employees.AddAsync(employee);
            await context.SaveChangesAsync();
            return employee;
        }

        public async Task DeleteEmployeeById(Employee employee)
        {

            // Delete funksiyasi yozishda ozroq tushunmovchilik bo'ldi mmenda 
            employee.IsDeleted = true;
            context.Employees.Update(employee);
            await context.SaveChangesAsync();
        }

        public async Task<List<Employee>> GetAllEmployee()
        {
            return await context.Employees.Where(a => !a.IsDeleted).Include(s => s.User).ThenInclude(p => p.Person).ToListAsync();
        }

        public async Task<Employee> GetEmployeeById(long id)
        {
            if (id < 0) throw new Exception("Id is from low 0!");
            return await context.Employees.FirstOrDefaultAsync(a => !a.IsDeleted && a.Id == id);
        }

        public async Task<Employee> Update(Employee employee)
        {
            context.Employees.Update(employee);
            await context.SaveChangesAsync();
            return employee;
        }
    }


    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeById(long id);
        Task<Employee> CreateEmployee(Employee employee);
        Task<Employee> Update(Employee employee);
        Task DeleteEmployeeById(Employee employee);
        Task<List<Employee>> GetAllEmployee();
    }
}
