using RoboticsPos.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboticsPos.Data.Models
{
      public class CashBox : BaseEntity
    {
        public string Title { get; set; }
        public string IPAddress { get; set; }
    }
}
