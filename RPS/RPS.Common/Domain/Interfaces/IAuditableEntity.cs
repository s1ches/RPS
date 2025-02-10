namespace RPS.Common.Domain.Interfaces;

public interface IAuditableEntity : IEntity
{
    DateTime CreateDate { get; set; }
    
    DateTime UpdateDate { get; set; } 
}