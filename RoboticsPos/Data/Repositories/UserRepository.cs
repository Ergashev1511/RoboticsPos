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

        public async Task<bool> LoginByPini(string Pin)
        {
            var user = _context.Users.Any(p => p.PIN == Pin);
            return user != null;         
        }

        public async Task<bool> LogoutByUsername(string username, string password)
        {
            var user = _context.Users.FirstOrDefaultAsync(p => p.UserName == username && p.PIN == password);
            return user != null;
        }
    }

    public interface IUserRepository
    {
        Task<bool> LoginByPini(string Pin);
        Task<bool> LogoutByUsername(string username, string password);
        Task<List<Employee>> GetAllUsers();

    }

} 
   
   


