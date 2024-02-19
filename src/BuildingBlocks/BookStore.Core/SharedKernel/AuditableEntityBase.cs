namespace BookStore.Core.SharedKernel;

public class AuditableEntityBase : EntityBase
{
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdateDate { get; set; }
}