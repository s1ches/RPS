using RPS.Common.Domain.Abstractions;

namespace RPS.Services.Auth.Domain.Entities;

public class User : AuditableEntityBase
{
    public string UserName { get; set; } = null!;
    
    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;
}