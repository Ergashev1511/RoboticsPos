using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Models;
using RoboticsPos.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboticsPos.Services
{
    public class EmployeeService
    {
        private readonly IEmployeeRepository employeeRepository;

        public EmployeeService(IEmployeeRepository repository)
        {
            this.employeeRepository = repository;
        }

        public async Task<List<EmployeeDTO>> GetAllEmployee()
        {
            var employees = await employeeRepository.GetAllEmployee();
            if (employees.Any())
            {
                var allemployees = employees.Select(a => new EmployeeDTO()
                    {
                        Id = a.Id,
                        JobTitle = a.Title,
                        EmployeeRole = a.EmployeeRole,
                        EnrollNumber = a.EnrollNumber,
                        HireDate = a.HireDate,
                        
                        Username = a.User.UserName,
                        
                        Firstname = a.User.Person.FirstName,
                        Lastname = a.User.Person.LastName,
                        Fathername = a.User.Person.FathersName,
                        Fullname = $"{a.User.Person.LastName} {a.User.Person.FirstName} {a.User.Person.FathersName}",
                        BornDate = a.User.Person.BornDate,
                        Address = a.User.Person.Address,
                        PhoneNumber = a.User.Person.PhoneNumber,
                    }
                ).ToList();

                return allemployees;
            }

            return new List<EmployeeDTO>();
        }



        public async Task CreateEmployee(EmployeeDTO employeeDto)
        {
            if (employeeDto == null) throw new Exception("Employeee is null!");

            Employee newentity = new Employee()
            {
                Title = employeeDto.JobTitle,
                EnrollNumber = employeeDto.EnrollNumber,
                HireDate = employeeDto.HireDate,
                CreateDate = DateTime.Now,
                EmployeeRole = employeeDto.EmployeeRole,
                
                User = new User()
                {
                    UserName = employeeDto.Username,
                    Password = employeeDto.password,
                    PIN = employeeDto.PIN,
                    CreateDate = DateTime.Now,
                    
                    Person = new Person()
                    {
                        FirstName = employeeDto.Firstname,
                        LastName = employeeDto.Lastname,
                        FathersName = employeeDto.Fathername,
                        BornDate = employeeDto.BornDate,
                        Address = employeeDto.Address,
                        PhoneNumber = employeeDto.PhoneNumber,
                        CreateDate = DateTime.Now
                    }
                }
            };

            await employeeRepository.CreateEmployee(newentity);
        }



        public async Task UpdateEmployee(long Id, EmployeeDTO employeeDto)
        {
            var old = await employeeRepository.GetEmployeeById(Id);
            if (old == null) throw new Exception("Eemployee is null!");

            old.Title = employeeDto.JobTitle;
            old.EnrollNumber = employeeDto.EnrollNumber;
            old.HireDate = employeeDto.HireDate;
            old.CreateDate = DateTime.Now;
            old.EmployeeRole = employeeDto.EmployeeRole;
            old.User = new User()
            {
                UserName = employeeDto.Username,
                Password = employeeDto.password,
                PIN = employeeDto.PIN,
                CreateDate = DateTime.Now,
                Person = new Person()
                {
                    FirstName = employeeDto.Firstname,
                    LastName = employeeDto.Lastname,
                    FathersName = employeeDto.Fathername,
                    BornDate = employeeDto.BornDate,
                    Address = employeeDto.Address,
                    PhoneNumber = employeeDto.PhoneNumber,
                    CreateDate = DateTime.Now
                }
            };
            await employeeRepository.Update(old);
        }


        public async Task DeleteEmployee(long Id)
        {
            var employee = await employeeRepository.GetEmployeeById(Id);
            if (employee == null) throw new Exception("Employee not found!");

            await employeeRepository.DeleteEmployeeById(employee);
        }


        public async Task<EmployeeDTO> GetEmployeeById(long Id)
        {
            var data = await employeeRepository.GetEmployeeById(Id);
            if (data == null) throw new Exception("Employee not found!");

            else
            {
                EmployeeDTO employeeDto = new EmployeeDTO()
                {
                    Id = data.Id,
                    JobTitle = data.Title,
                    HireDate = data.HireDate,
                    EnrollNumber = data.EnrollNumber,
                    EmployeeRole = data.EmployeeRole,
                    
                    Username = data.User.UserName,
                    password = data.User.Password,
                    PIN = data.User.PIN,
                    
                    Firstname = data.User.Person.FirstName,
                    Lastname = data.User.Person.LastName,
                    Fathername = data.User.Person.FathersName,
                    BornDate = data.User.Person.BornDate,
                    Address = data.User.Person.Address,
                    PhoneNumber = data.User.Person.PhoneNumber,
                    Fullname = $"{data.User.Person.LastName} {data.User.Person.FirstName} {data.User.Person.FirstName} {data.User.Person.FathersName}"
                };
                return employeeDto;
            }
        }
        
    }

}

