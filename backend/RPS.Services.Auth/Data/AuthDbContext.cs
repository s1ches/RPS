using MassTransit;
using Microsoft.EntityFrameworkCore;
using RPS.Services.Auth.Data.EntitiesConfigurations;
using RPS.Services.Auth.Domain.Entities;

namespace RPS.Services.Auth.Data;

public class AuthDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    
    public AuthDbContext()
    {
    }

    public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        
        modelBuilder.AddInboxStateEntity();
        modelBuilder.AddOutboxMessageEntity();
        modelBuilder.AddOutboxStateEntity();
        
        base.OnModelCreating(modelBuilder);
    }
}