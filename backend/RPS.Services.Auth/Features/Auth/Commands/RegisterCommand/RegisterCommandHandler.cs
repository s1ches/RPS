using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Services.Auth.Data;
using RPS.Services.Auth.Domain.Entities;
using RPS.Services.Auth.Requests.Auth;
using RPS.Services.Auth.Services.MasstransitService;
using RPS.Services.Auth.Services.PasswordHasher;
using RPS.Services.Auth.Services.TokenProvider;

namespace RPS.Services.Auth.Features.Auth.Commands.RegisterCommand;

public class RegisterCommandHandler(
    ILogger<RegisterCommandHandler> logger,
    IPasswordHasher passwordHasher,
    ITokenProvider tokenProvider,
    AuthDbContext dbContext
    // IRegistrationEventSender registrationEventSender
    ) : IRequestHandler<RegisterCommand, AuthResponse>
{
    public Priority Priority { get; set; } = Priority.ExecuteLast;

    public async Task<AuthResponse> HandleAsync(RegisterCommand request, CancellationToken cancellationToken = default)
    {
        var user = new User
        {
            Email = request.Email,
            UserName = request.UserName,
            CreateDate = DateTime.UtcNow,
            UpdateDate = DateTime.UtcNow,
            PasswordHash = passwordHasher.HashPassword(request.Password),
        };
        
        var entry = await dbContext.Users.AddAsync(user, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        // await registrationEventSender.SendRegistrationEventAsync(entry.Entity, cancellationToken);
        
        logger.LogInformation("User with {email} registered and logged in", user.Email);
        
        return new AuthResponse
        {
            AccessToken = tokenProvider.GenerateToken(entry.Entity)
        };
    }
}