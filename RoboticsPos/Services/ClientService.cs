using System.Windows.Documents;
using RoboticsPos.Common.DTOs;
using RoboticsPos.Data.Models;
using RoboticsPos.Data.Repositories;

namespace RoboticsPos.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _repository;

    public ClientService(IClientRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<ClientDTO> Create(ClientDTO clientDto)
    {
        if (clientDto == null) throw new Exception("ClientDto is null");
        Client client = new Client()
        {
            Person = new Person()
            {
                FirstName = clientDto.FirstName,
                LastName = clientDto.LastName,
                FathersName = clientDto.FathersName,
                BornDate = clientDto.BornDate,
                PhoneNumber = clientDto.PhoneNumber,
                Address = clientDto.Address,
            }
        };
        await _repository.CreateClient(client);
        return clientDto;
    }

    public async Task<ClientDTO> Update(long Id, ClientDTO clientDto)
    {
        var oldclient = await _repository.GetById(Id);
        if (oldclient == null) throw new Exception("Client is null");
        oldclient.Person.FirstName = clientDto.FirstName;
        oldclient.Person.LastName = clientDto.LastName;
        oldclient.Person.FathersName = clientDto.FathersName;
        oldclient.Person.BornDate = clientDto.BornDate;
        oldclient.Person.PhoneNumber = clientDto.PhoneNumber;
        oldclient.Person.Address = clientDto.Address;
        await _repository.UpdateClient(oldclient);
        return clientDto;
    }

    public async Task<List<ClientDTO>> GetAll()
    {
        var clients = await _repository.GetAllClient();
        if (clients.Any())
        {
            var client = clients.Select(p => new ClientDTO()
            {
                Id = p.Id,
                PersonId = p.PersonId,
                FirstName = p.Person.FirstName,
                LastName = p.Person.LastName,
                FathersName = p.Person.FathersName,
                PhoneNumber = p.Person.PhoneNumber,
                Address = p.Person.Address,
                BornDate = p.Person.BornDate,
                FullName = $"{p.Person.LastName} {p.Person.FirstName}"
            }).ToList();
            return client;
        }

        return new List<ClientDTO>();

    }

    public async Task Delete(long Id)
    {
        var oldclient = await _repository.GetById(Id);
        if (oldclient == null) throw new Exception("Client is null");

        await _repository.DeleteClient(oldclient);
    }

    public async Task<ClientDTO> GetById(long Id)
    {
        var oldclient = await _repository.GetById(Id);
        if (oldclient == null) throw new Exception("Clinet is null");

        ClientDTO clientDto = new ClientDTO()
        {
            FirstName = oldclient.Person.FirstName,
            LastName = oldclient.Person.LastName,
            FathersName = oldclient.Person.FathersName,
            BornDate = oldclient.Person.BornDate,
            PhoneNumber = oldclient.Person.PhoneNumber,
            Address = oldclient.Person.Address
        };
        return clientDto;
    }

    public async Task<List<ClientSearchDTo>> GetSearchAll(string fullname)
    {
        
        return null;

    }
}


public interface IClientService
{
    Task<ClientDTO> Create(ClientDTO clientDto);
    Task<ClientDTO> Update(long Id,ClientDTO clientDto);
    Task<List<ClientDTO>> GetAll();
    Task Delete(long Id);
    Task<ClientDTO> GetById(long Id);
    Task<List<ClientSearchDTo>> GetSearchAll(string fullname);
}