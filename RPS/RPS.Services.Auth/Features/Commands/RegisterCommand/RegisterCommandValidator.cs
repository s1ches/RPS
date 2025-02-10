using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using RPS.Common.Exceptions;
using RPS.Common.MediatR.ModelsAbstractions;
using RPS.Common.MediatR.PipelineItems;
using RPS.Common.Options;
using RPS.Services.Auth.Data;
using RPS.Services.Auth.Requests.Auth;

namespace RPS.Services.Auth.Features.Commands.RegisterCommand;

public class RegisterCommandValidator(
    IOptions<AuthOptions> authOptions,
    AuthDbContext dbContext,
    ILogger<RegisterCommandValidator> logger)
    : IValidator<RegisterCommand, AuthResponse>
{
    private AuthOptions _authOptions = authOptions.Value;

    public Priority Priority { get; set; } = Priority.ExecuteFirst;

    public async Task<AuthResponse> HandleAsync(RegisterCommand request, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(request.UserName))
        {
            throw new ApplicationExceptionBase("UserName is required", HttpStatusCode.BadRequest);
        }

        if (string.IsNullOrWhiteSpace(request.Email))
        {
            throw new ApplicationExceptionBase("Email is required", HttpStatusCode.BadRequest);
        }
        
        var emailValidator = new EmailAddressAttribute();
        if (!emailValidator.IsValid(request.Email))
        {
            throw new ApplicationExceptionBase("Email is invalid", HttpStatusCode.BadRequest);
        }
        
        if (string.IsNullOrWhiteSpace(request.Password))
        {
            throw new ApplicationExceptionBase("Password is required", HttpStatusCode.BadRequest);
        }

        if (request.Password.Length < 6)
        {
            throw new ApplicationExceptionBase(
                $"Password must be at least {_authOptions.MinimumPasswordLength} characters",
                HttpStatusCode.BadRequest);
        }

        if (await dbContext.Users.AnyAsync(x => x.Email == request.Email, cancellationToken))
        {
            throw new ApplicationExceptionBase("Email is already taken", HttpStatusCode.Conflict);
        }

        logger.LogInformation("RegisterCommand validated for Email {email}", request.Email);        
        return new AuthResponse();
    }
}