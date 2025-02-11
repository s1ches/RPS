using Microsoft.AspNetCore.SignalR;

namespace RPS.Services.Game.Hubs.Room;

public static class GroupNameHelper
{
    public static string GetGroupName<THub>(long roomId) where THub: Hub
    {
        return $"{typeof(THub).Name}_{roomId}";
    }
}