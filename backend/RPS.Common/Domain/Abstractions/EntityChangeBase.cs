using RPS.Common.Domain.Interfaces;

namespace RPS.Common.Domain.Abstractions;

public class EntityChangeBase : EntityBase, IEntityChange
{
    public DateTime CreateDate { get; set; }
}