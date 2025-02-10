using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RPS.Common.Exceptions;
using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Common.Options;
using RPS.Services.Auth.Data;
using RPS.Services.Auth.Domain.QueriesExtensions;
using RPS.Services.Auth.Requests.Auth;
using RPS.Services.Auth.Services.PasswordHasher;

namespace RPS.Services.Auth.Features.Commands.LoginCommand;

public class LoginCommandValidator(
    AuthDbContext dbContext,
    IPasswordHasher passwordHasher,
    ILogger<LoginCommandValidator> logger,
    IOptions<AuthOptions> authOptions)
    : IValidator<LoginCommand, AuthResponse>
{
    private readonly AuthOptions _authOptions = authOptions.Value;

    public Priority Priority { get; set; } = Priority.ExecuteFirst;

    public async Task<AuthResponse> HandleAsync(LoginCommand request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.Email))
            throw new ApplicationExceptionBase("Email is required", HttpStatusCode.BadRequest);

        if (string.IsNullOrWhiteSpace(request.Password))
            throw new ApplicationExceptionBase("Password is required", HttpStatusCode.BadRequest);

        if (request.Password.Length < 6)
            throw new ApplicationExceptionBase(
                $"Password must be at least {_authOptions.MinimumPasswordLength} characters long",
                HttpStatusCode.BadRequest);

        var user = await dbContext.Users.GetByEmailAsync(request.Email, cancellationToken: cancellationToken);

        if (user == null || !passwordHasher.IsCorrectPassword(request.Password, user.PasswordHash))
        {
            logger.LogInformation("Email or password is incorrect, Email: {email}, Password: {password}",
                request.Email,
                request.Password);
            throw new ApplicationExceptionBase("Email or password is incorrect", HttpStatusCode.Unauthorized);
        }

        logger.LogInformation("LoginCommand validated for Email {email}", request.Email);
        return new AuthResponse();
    }
}