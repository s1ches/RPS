using RPS.Common.Domain.Interfaces;

namespace RPS.Common.Domain.Abstractions;

public class EntityBase : IEntity
{
    public long Id { get; set; }
}