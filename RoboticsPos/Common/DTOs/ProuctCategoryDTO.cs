namespace RoboticsPos.Common.DTOs;

public class ProuctCategoryDTO : BaseDTO
{
    public string Name { get; set; }
    public string Discription { get; set; }
    
    public long? ParentId { get; set; }
    public List<ProductForSelect> ProductDtos { get; set; }
    public string ParentName { get; set; }
    public string Productnames { get; set; }
}