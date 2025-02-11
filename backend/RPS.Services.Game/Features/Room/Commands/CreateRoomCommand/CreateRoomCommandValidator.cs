using System.Net;
using RPS.Common.Exceptions;
using RPS.Common.Grpc;
using RPS.Common.Grpc.Clients.Accounts;
using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Game.Requests.Room.CreateRoom;

namespace RPS.Services.Game.Features.Room.Commands.CreateRoomCommand;

public class CreateRoomCommandValidator(ILogger<CreateRoomCommandValidator> logger, IAccountsClient accountsClient)
    : IValidator<CreateRoomCommand, CreateRoomResponse>
{
    public Priority Priority { get; set; } = Priority.ExecuteFirst;

    public async Task<CreateRoomResponse> HandleAsync(CreateRoomCommand request,
        CancellationToken cancellationToken = default)
    {
        if (request.MaxAllowedGameRating < 0)
        {
            logger.LogInformation("Max allowed game rating cannot be negative, request from user with id {id}",
                request.CreatorId);
            throw new ApplicationExceptionBase("Max allowed game rating cannot be negative", HttpStatusCode.BadRequest);
        }
        
        var status = await accountsClient.GetUserStatusAsync(request.CreatorId, cancellationToken);
        if (status == UserStatus.InGame)
        {
            logger.LogInformation("User with id: {id} is already in game", request.CreatorId);
            throw new ApplicationExceptionBase("User can be player only in one game", HttpStatusCode.Forbidden);
        }

        return new CreateRoomResponse();
    }
}