using Microsoft.EntityFrameworkCore;
using RoboticsPos.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboticsPos.Data.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<List<Employee>> GetAllUsers()
        {
            var users = await _context.Employees.Where(a => !a.IsDeleted).Include(s => s.User).ThenInclude(p => p.Person).ToListAsync();
            return users;
        }

        public async Task<User> LoginByPini(string Pin)
        {
            var user = await _context.Users.FirstOrDefaultAsync(p =>!p.IsDeleted && p.PIN == Pin );
            return user ;         
        }

        public async Task<User> LogoutByUsername(string username, string password)
        {
            var users = await _context.Users.FirstOrDefaultAsync(a=>!a.IsDeleted &&  a.UserName == username && a.Password == password);
            return users;
        }
    }

    public interface IUserRepository
    {
        Task<User> LoginByPini(string Pin);
        Task<User> LogoutByUsername(string username, string password);
        Task<List<Employee>> GetAllUsers();

    }

} 
   
   


