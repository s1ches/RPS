using Microsoft.AspNetCore.SignalR;

namespace RPS.Services.Game.Hubs;

public class RoomHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        
    }
}