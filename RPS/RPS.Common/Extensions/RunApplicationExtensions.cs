using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace RPS.Common.Extensions;

public static class RunApplicationExtensions
{
    public static void PrepareDatabaseState<TDbContext>(this IServiceProvider serviceProvider) where TDbContext : notnull
    {
        var dbContext = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<TDbContext>() as DbContext;
        
        dbContext!.Database.Migrate();
    }
}