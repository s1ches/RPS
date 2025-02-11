namespace RPS.Services.Accounts.Requests.User.GetUsersInfos;

public class GetUsersInfosResponse
{
    public List<GetUsersInfosResponseItem> Users { get; set; } = [];
    
    public int TotalCount { get; set; }
}