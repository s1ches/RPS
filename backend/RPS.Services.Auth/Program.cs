using RPS.Common.Configuration;
using RPS.Common.Extensions;
using RPS.Common.MediatR;
using RPS.Common.Middlewares;
using RPS.Common.Options;
using RPS.Services.Auth.Data;
using RPS.Services.Auth.Services.ClaimsProvider;
using RPS.Services.Auth.Services.PasswordHasher;
using RPS.Services.Auth.Services.TokenProvider;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddConfiguredSwaggerGen();

#region Auth Configuration
var jwtOptions = new JwtOptions();
builder.Configuration.GetSection(nameof(JwtOptions)).Bind(jwtOptions);
builder.Services.AddAuthentication(jwtOptions);
builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection(nameof(JwtOptions)));
builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection(nameof(AuthOptions)));
#endregion

#region DataContext Configuration
var dbContextOptions = new DbContextOptions();
builder.Configuration.GetSection(nameof(DbContextOptions)).Bind(dbContextOptions);
builder.Services.AddDataContext<AuthDbContext>(dbContextOptions);
#endregion

#region Services Configuration
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddScoped<IClaimsManager, ClaimsManager>();
builder.Services.AddScoped<IPasswordHasher, PasswordHasher>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
#endregion

#region CORS configuration
var corsOptions = new CorsOptions();
builder.Configuration.GetSection(nameof(CorsOptions)).Bind(corsOptions);
builder.Services.AddCors(corsOptions);
builder.Services.Configure<CorsOptions>(builder.Configuration.GetSection(nameof(CorsOptions)));
#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(CorsOptions.CorsPolicyName);

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Services.PrepareDatabaseState<AuthDbContext>();
app.Run();