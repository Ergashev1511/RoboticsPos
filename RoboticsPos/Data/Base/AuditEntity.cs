
namespace RoboticsPos.Data.Base;

public class AuditEntity
{
    public bool IsDeleted { get; set; }
    public DateTime CreatedDate { get; internal set; }
    public DateTime ModifiedDate { get; internal set; }
}