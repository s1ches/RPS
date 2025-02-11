namespace RPS.Services.Accounts.Requests.User.GetUsersRatings;

public class GetUsersInfosResponseItem
{
    public long UserId { get; set; }

    public string UserName { get; set; } = null!;
    
    public long Rating { get; set; }
}