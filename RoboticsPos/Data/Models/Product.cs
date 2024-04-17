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

        public long? DiscountId { get; set; }
        public virtual Discount Discount { get; set; }
        
        //Bugun qo'shilgan model
        
        public string BarCode { get; set; }
        public int  AmountInPackage { get; set; }
        public int CompanyId { get; set; }
        public int Amount { get; set; }
        public int ActualPrice { get; set; }
        public int Price { get; set; }
        public int PriceOfPiece { get; set; }
        public bool Selected { get; set; }
        
        public virtual List<ProductDiscount> ProductDiscounts { get; set; }
        public virtual List<Discount> Discounts { get; set; }
        
        public long ProductCategoryId { get; set; }
        public ProductCategory ProductCategory { get; set; }
    }
}
