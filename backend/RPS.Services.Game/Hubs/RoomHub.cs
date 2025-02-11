using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RPS.Common.Grpc;
using RPS.Common.Services.ClaimsProvider;
using RPS.Services.Game.Data;
using RPS.Services.Game.Domain.Entities;
using RPS.Services.Game.Services.UpdateUserStatusEventSender;

namespace RPS.Services.Game.Hubs;

[Authorize]
public class RoomHub(
    IUpdateUserStatusEventSender updateUserStatusEventSender,
    IClaimsProvider claimsProvider,
    GameDbContext dbContext) : Hub
{
    public override async Task OnConnectedAsync()
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, "General");

        var userId = claimsProvider.GetUserId(Context.User!);

        var participant = new Participant
        {
            UserId = userId,
            ConnectionId = Context.ConnectionId,
            Groups = ["General"]
        };
        
        await dbContext.Participants.AddAsync(participant);
        await dbContext.SaveChangesAsync();
        
        await updateUserStatusEventSender.SendEventAsync(userId, UserStatus.Online);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var participant = await dbContext.Participants.SingleAsync(x => x.ConnectionId == Context.ConnectionId);
        
        foreach(var group in participant.Groups)
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group);
        
        dbContext.Participants.Remove(participant);
        
        var userId = claimsProvider.GetUserId(Context.User!);
        await updateUserStatusEventSender.SendEventAsync(userId, UserStatus.Offline);
    }
}