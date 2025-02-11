using RPS.Common.Domain.Interfaces;

namespace RPS.Common.Domain.Abstractions;

public class AuditableEntityBase : EntityBase, IAuditableEntity
{
    public DateTime CreateDate { get; set; }
    
    public DateTime UpdateDate { get; set; }
}