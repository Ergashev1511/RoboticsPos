using Microsoft.EntityFrameworkCore;
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
    public class UserService : IUserService
    {

        private readonly IRepository<User> _repository;
        private readonly IUserRepository userrepository;
        private readonly AppDbContext context;

        public UserService(IUserRepository repository, AppDbContext context, IRepository<User> _repository)
        {
            this._repository = _repository;
            this.userrepository = repository;
            this.context = context;
        }
        

        public async Task<List<UserDTO>> GetAllUserDTO()
        {
            List<UserDTO> userDTOs = new List<UserDTO>();

            List<Employee> users = await userrepository.GetAllUsers();

            if (users.Any())
            {
                foreach (var i in users)
                {
                    UserDTO userDTO = new UserDTO()
                    {
                        Id = i.Id,
                        UserName = i.User.UserName,
                        Firstname = i.User.Person.FirstName,
                        Lastname = i.User.Person.LastName,
                        Fathername = i.User.Person.FathersName,
                        FullName = $"{i.User.Person.LastName} {i.User.Person.FirstName} {i.User.Person.FathersName}",
                        Address = i.User.Person.Address,
                        BornDate = i.User.Person.BornDate,
                        PhoneNumber = i.User.Person.PhoneNumber,
                    };
                    userDTOs.Add(userDTO);
                }
                return userDTOs;
            }
            return userDTOs;
        }

        public async Task<bool> LoginByPini(string pini)
        {
            return await _repository.GetAll(a => a.PIN == pini).FirstOrDefaultAsync() != null;
        }

        public async Task<bool> LoginByUsername(string username, string password)
        { 
            //var data = await _repository.GetAll(a => a.UserName == username && a.Password == password).FirstOrDefaultAsync() != null;
           
            var access = await userrepository.LogoutByUsername(username, password); 
            Variablies.StaticVariablies.CurrentUsername = access.UserName;
            return access!=null;
        }
    }

    public interface IUserService
    {
        Task<bool> LoginByPini(string pini);
        Task<bool> LoginByUsername(string username, string password);
        Task<List<UserDTO>> GetAllUserDTO();
    }
}
