using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using RPS.Common.Domain.Abstractions;

namespace RPS.Services.Accounts.Domain.Entities;

public class UserInfo : AuditableEntityBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public new long Id { get; set; }
    
    [BsonRequired]
    public string UserName { get; set; } = null!;

    public long Rating { get; set; } = 0;
}