namespace Ordering.Domain.Common;

public abstract class EntityBase
{
    public int Id { get; protected set; }
    public string CreatedBy { get; set; } = default!;
    public DateTime CreatedDate { get; set; }
    public string LastModifiedBy { get; set; } = default!;
    public DateTime LastModifiedDate { get; set; }
}
