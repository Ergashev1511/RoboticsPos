using Microsoft.EntityFrameworkCore;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Models;

namespace RoboticsPos.Data.Repositories;

public class ClientRepository : IClientRepository
{
    private readonly AppDbContext _context;

    public ClientRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Client> CreateClient(Client client)
    {
        var hascopy = await _context.Clients.AnyAsync(a => !a.IsDeleted);
        if (hascopy == null) throw new Exception("Current Client already exist!");
        await _context.Clients.AddAsync(client);
         await _context.SaveChangesAsync();
        return client;
    }

    public async Task<Client> UpdateClient(Client client)
    {
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
        return client;
    }

    public async Task<List<Client>> GetAllClient()
    {
        return await _context.Clients.Where(a => !a.IsDeleted).Include(s=>s.Person).ToListAsync();
    }

    public async Task DeleteClient(Client client)
    {
        client.IsDeleted = true;
        _context.Clients.Update(client);
        await _context.SaveChangesAsync();
    }

    public async Task<Client> GetById(long Id)
    {
        if (Id < 0) throw new Exception("Id is low from 0!");
        return await _context.Clients.FirstOrDefaultAsync(a => !a.IsDeleted && a.Id == Id);
    }
    
}

public interface IClientRepository
{
    Task<Client> CreateClient(Client client);
    Task<Client> UpdateClient(Client client);
    Task<List<Client>> GetAllClient();
    Task DeleteClient(Client client);
    Task<Client> GetById(long Id);
  
}