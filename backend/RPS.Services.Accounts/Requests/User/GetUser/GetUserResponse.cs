namespace RPS.Services.Accounts.Requests.User.GetUser;

public class GetUserResponse
{
    public string UserName { get; set; } = null!;
    
    public long Rating { get; set; }
}