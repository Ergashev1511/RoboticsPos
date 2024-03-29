using RoboticsPos.Data.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoboticsPos.Data.Models
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }

        public long DiscountId { get; set; }
        public virtual Discount Discount { get; set; }
    }
}
