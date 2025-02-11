using RPS.Services.Accounts.Domain.Entities;
using RPS.Services.Accounts.Domain.Enums;

namespace RPS.Services.Accounts.Data.MongoDbService;

public interface IMongoDbService
{
    Task<UserInfo> GetUserAsync(long id, CancellationToken cancellationToken = default);

    Task<List<UserInfo>> GetUsersAsync(List<long> ids, CancellationToken cancellationToken = default);

    Task<bool> IsUserExistsAsync(long id, CancellationToken cancellationToken = default);

    Task<bool> IsAllUsersExistsAsync(List<long> ids, CancellationToken cancellationToken = default);

    Task<long> AddUserAsync(UserInfo userInfo, CancellationToken cancellationToken = default);

    Task<long> UpdateUserRatingAsync(long userId, long rating, CancellationToken cancellationToken = default);

    Task<long> UpdateUserStatusAsync(long userId, UserStatus userStatus, CancellationToken cancellationToken = default);
}