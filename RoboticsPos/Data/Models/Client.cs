using RoboticsPos.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboticsPos.Data.Models
{
     public  class Client : BaseEntity
    {
        public long PersonId { get; set; }
        public virtual Person Person { get; set; }
    }
}
