using RoboticsPos.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboticsPos.Data.Models
{
      public class CheckPrintingData : BaseEntity
    {
        public string Header { get; set; }
        public string Footer { get; set; }
        public string Tara { get; set; }
        public string Printer { get; set; }
        public string TIN { get; set; }
    }
}
