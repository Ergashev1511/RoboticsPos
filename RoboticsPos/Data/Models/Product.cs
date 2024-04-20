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

       
        
        public string BarCode { get; set; }
        public int  AmountInPackage { get; set; }
        public int Amount { get; set; }
        public int ActualPrice { get; set; }
        public int Price { get; set; }
        public int PriceOfPiece { get; set; }
        public bool Selected { get; set; }
        
        
         public long? DiscountId { get; set; }
         public virtual Discount? Discount { get; set; }
                 
         
        public long? CompanyId { get; set; }
        public virtual Company? Companys { get; set; }
        
       
        
        public long? CategoryId { get; set; }
        public virtual ProductCategory? Category { get; set; }
        
    }
}
