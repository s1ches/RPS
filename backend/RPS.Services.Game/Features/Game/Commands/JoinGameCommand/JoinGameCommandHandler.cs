using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using RPS.Common.Grpc.Clients.Accounts;
using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Game.Data;
using RPS.Services.Game.Hubs.Room;
using RPS.Services.Game.Services.UpdateUserStatusEventSender;

namespace RPS.Services.Game.Features.Game.Commands.JoinGameCommand;

public class JoinGameCommandHandler(
    ILogger<JoinGameCommandHandler> logger,
    GameDbContext dbContext,
    IHubContext<RoomHub> roomHub,
    IUpdateUserStatusEventSender updateUserStatusEventSender,
    IAccountsClient accountsClient) : IRequestHandler<JoinGameCommand>
{
    public Priority Priority { get; set; } = Priority.ExecuteLast;
    
    public async Task HandleAsync(JoinGameCommand request, CancellationToken cancellationToken = default)
    {
        var room = await dbContext.Rooms
            .Include(x => x.Games)
            .SingleAsync(x => x.Id == request.RoomId, cancellationToken);
        
        // var game = room.Games
        //     .Where(x => x).OrderByDescending(x => x.CreateDate)
    }
}