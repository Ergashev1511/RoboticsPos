using RoboticsPos.Data.Base;
using RoboticsPos.Data.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboticsPos.Data.Models
{
    public  class Employee : BaseEntity
    {
        public string Title { get; set; }
        public long EnrollNumber { get; set; }
        public DateTime HireDate { get; set; }
        public EmployeeRole EmployeeRole { get; set; }

        public long UserId { get; set; }
        public virtual User User { get; set; }
    }
}
