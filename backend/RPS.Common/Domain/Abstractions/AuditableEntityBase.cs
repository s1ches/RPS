using System.ComponentModel.DataAnnotations;
using RPS.Common.Domain.Interfaces;

namespace RPS.Common.Domain.Abstractions;

public class AuditableEntityBase : IAuditableEntity
{
    [Key]
    public long Id { get; set; }
    
    public DateTime CreateDate { get; set; }
    
    public DateTime UpdateDate { get; set; }
}