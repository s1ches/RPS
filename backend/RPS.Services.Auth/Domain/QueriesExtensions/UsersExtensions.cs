using Microsoft.EntityFrameworkCore;
using RPS.Services.Auth.Domain.Entities;

namespace RPS.Services.Auth.Domain.QueriesExtensions;

public static class UsersExtensions
{
    public static async Task<User?> GetByEmailAsync(this DbSet<User> set, string email,
        CancellationToken cancellationToken = default) =>
        await set.SingleOrDefaultAsync(u => u.Email == email, cancellationToken: cancellationToken);
}