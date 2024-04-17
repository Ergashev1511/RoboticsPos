using RoboticsPos.Data.Models;

namespace RoboticsPos.Common.DTOs;
 
public class ClientDTO : BaseDTO
{
     public long PersonId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string FathersName { get; set; }
    public DateTime BornDate { get; set; }
    public string PhoneNumber { get; set; }
    public string Address { get; set; }
}