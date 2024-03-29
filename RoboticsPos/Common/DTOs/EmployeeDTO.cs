using RoboticsPos.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboticsPos.Common.DTOs
{
    public class EmployeeDTO : BaseDTO
    {
        public string Username { get; set; }
        public string password { get; set; }
        public string PIN { get; set; }
        public string Fullname { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Fathername { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string JobTitle { get; set; }
        public long EnrollNumber { get; set; }
        public DateTime BornDate { get; set; }
        public DateTime HireDate { get; set; }
        public EmployeeRole EmployeeRole { get; set; }
    }
}
