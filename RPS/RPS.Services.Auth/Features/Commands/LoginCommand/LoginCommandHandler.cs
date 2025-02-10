using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Auth.Data;
using RPS.Services.Auth.Domain.QueriesExtensions;
using RPS.Services.Auth.Requests.Auth;
using RPS.Services.Auth.Services.TokenProvider;

namespace RPS.Services.Auth.Features.Commands.LoginCommand;

public class LoginCommandHandler(
    ILogger<LoginCommandHandler> logger,
    ITokenProvider tokenProvider,
    AuthDbContext dbContext)
    : IRequestHandler<LoginCommand, AuthResponse>
{
    public Priority Priority { get; set; } = Priority.ExecuteLast;

    public async Task<AuthResponse> HandleAsync(LoginCommand request, CancellationToken cancellationToken = default)
    {
        var user = await dbContext.Users.GetByEmailAsync(request.Email, cancellationToken: cancellationToken);

        logger.LogInformation("User with email {email} logged in", user!.Email);
        
        return new AuthResponse
        {
            AccessToken = tokenProvider.GenerateToken(user),
        };
    }
}