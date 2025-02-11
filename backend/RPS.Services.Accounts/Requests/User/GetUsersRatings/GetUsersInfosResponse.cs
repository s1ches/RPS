namespace RPS.Services.Accounts.Requests.User.GetUsersRatings;

public class GetUsersInfosResponse
{
    public List<GetUsersInfosResponseItem> Users { get; set; } = [];
    
    public int TotalCount { get; set; }
}