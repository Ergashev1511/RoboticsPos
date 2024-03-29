using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboticsPos.Common.DTOs
{
    public class UserDTO : BaseDTO
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Fathername { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Address { get; set; }
        public DateTime BornDate { get; set; }
    }
}
