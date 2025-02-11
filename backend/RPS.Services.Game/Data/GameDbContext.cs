using Microsoft.EntityFrameworkCore;
using RPS.Services.Game.Domain.Entities;
using RPS.Services.Game.Domain.EntitiesChanges;

namespace RPS.Services.Game.Data;

public class GameDbContext : DbContext
{
    public GameDbContext()
    { }
    
    public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
    { }

    public DbSet<Room> Rooms { get; set; }
    
    public DbSet<RoomChange> RoomsChanges { get; set; }
    
    public DbSet<Domain.Entities.Game> Games { get; set; }
    
    public DbSet<GameChange> GamesChanges { get; set; }

    public DbSet<Round> Rounds { get; set; }
    
    public DbSet<RoundChange> RoundsChanges { get; set; }
}