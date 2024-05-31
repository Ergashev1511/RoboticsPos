using System;
using RoboticsPos.Data.Enum;

namespace RoboticsPos.Common.DTOs;

public class DiscountDTO : BaseDTO
{
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }           // 0.6 %,         1 + 1,     har biri uchun 10 000
    public DiscountType AmountType { get; set; }  // Percent,       Count,     Amount

    public DiscountStatus DiscountStatus { get; set; }

    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public List<ProductForSelect> ProductDtos { get; set; }
    public string ProductNames { get; set; }
}