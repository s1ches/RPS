using MongoDB.Bson.Serialization.Attributes;
using RPS.Common.Domain.Abstractions;

namespace RPS.Services.Accounts.Domain.Entities;

public class UserInfo : AuditableEntityBase
{
    [BsonRequired]
    public string UserName { get; set; } = null!;

    public long Rating { get; set; } = 0;
}