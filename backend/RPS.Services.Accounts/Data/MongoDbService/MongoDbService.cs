using MongoDB.Driver;
using RPS.Common.Exceptions;
using RPS.Services.Accounts.Domain.Entities;
using RPS.Services.Accounts.Domain.Enums;

namespace RPS.Services.Accounts.Data.MongoDbService;

public class MongoDbService(IMongoDatabase mongoDatabase) : IMongoDbService
{
    private readonly IMongoCollection<UserInfo> _userInfos
        = mongoDatabase.GetCollection<UserInfo>(nameof(UserInfo));

    public async Task<UserInfo> GetUserAsync(long id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<UserInfo>.Filter.Eq(x => x.Id, id);
        return await _userInfos.Find(filter).SingleOrDefaultAsync(cancellationToken);
    }

    public async Task<List<UserInfo>> GetUsersAsync(List<long> ids, CancellationToken cancellationToken = default)
    {
        var filter = Builders<UserInfo>.Filter.In(x => x.Id, ids);
        return await _userInfos.Find(filter).ToListAsync(cancellationToken);
    }

    public async Task<bool> IsUserExistsAsync(long id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<UserInfo>.Filter.Eq(x => x.Id, id);
        return await _userInfos.Find(filter).AnyAsync(cancellationToken);
    }

    public async Task<bool> IsAllUsersExistsAsync(List<long> ids, CancellationToken cancellationToken = default)
    {
        var count = await _userInfos.CountDocumentsAsync(
            Builders<UserInfo>.Filter.In(u => u.Id, ids), 
            cancellationToken: cancellationToken
        );

        return count == ids.Count;
    }

    public async Task<long> AddUserAsync(UserInfo userInfo, CancellationToken cancellationToken = default)
    {
        await _userInfos.InsertOneAsync(userInfo, cancellationToken: cancellationToken);
        return userInfo.Id;
    }

    public async Task<long> UpdateUserRatingAsync(long userId, long rating, CancellationToken cancellationToken = default)
    {
        var user = await GetUserAsync(userId, cancellationToken);
        
        user.Rating = rating;
        var res = 
            await _userInfos.ReplaceOneAsync(x => x.Id == userId, user, cancellationToken: cancellationToken);

        if (res is null || !res.IsAcknowledged)
            throw new InfrastructureExceptionBase($"Cannot update user rating, user id is {userId}");
                
        return res.ModifiedCount;
    }
    public async Task<long> UpdateUserStatusAsync(long userId, UserStatus userStatus, CancellationToken cancellationToken = default)
    {
        var user = await GetUserAsync(userId, cancellationToken);
        
        user.Status = userStatus;
        var res = 
            await _userInfos.ReplaceOneAsync(x => x.Id == userId, user, cancellationToken: cancellationToken);

        if (res is null || !res.IsAcknowledged)
            throw new InfrastructureExceptionBase($"Cannot update user status, user id is {userId}");
                
        return res.ModifiedCount;
    }
}