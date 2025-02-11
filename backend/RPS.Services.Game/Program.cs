using MassTransit;
using RPS.Common.Configuration;
using RPS.Common.Extensions;
using RPS.Common.Grpc;
using RPS.Common.Grpc.Clients;
using RPS.Common.Grpc.Clients.Accounts;
using RPS.Common.Grpc.Options;
using RPS.Common.Masstransit.Constansts;
using RPS.Common.Masstransit.Events;
using RPS.Common.MediatR;
using RPS.Common.Middlewares;
using RPS.Common.Options;
using RPS.Common.Options.KestrelOptions;
using RPS.Common.Services.ClaimsProvider;
using RPS.Services.Game.Data;
using RPS.Services.Game.Hubs.Room;
using RPS.Services.Game.Services.UpdateUserRatingEventSender;
using RPS.Services.Game.Services.UpdateUserStatusEventSender;
using DbContextOptions = RPS.Common.Options.DbContextOptions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSignalR();
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
builder.Services.AddDataContext<GameDbContext>(dbContextOptions);
#endregion

#region Services Configuration
builder.Services.AddMediatR(typeof(Program).Assembly);
builder.Services.AddTransient<IClaimsProvider, ClaimsProvider>();
builder.Services.AddScoped<IUpdateUserRatingEventSender, UpdateUserRatingEventSender>();
builder.Services.AddScoped<IUpdateUserStatusEventSender, UpdateUserStatusEventSender>();
#endregion

#region CORS configuration
var corsOptions = new CorsOptions();
builder.Configuration.GetSection(nameof(CorsOptions)).Bind(corsOptions);
builder.Services.AddCors(corsOptions);
builder.Services.Configure<CorsOptions>(builder.Configuration.GetSection(nameof(CorsOptions)));
#endregion

#region Masstransit Configuration
var rabbitMqOptions = new RabbitMqOptions();
builder.Configuration.GetSection(nameof(RabbitMqOptions)).Bind(rabbitMqOptions);
builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Publish<UpdateUserStatusEvent>(p =>
        {
            p.ExchangeType = RabbitMqConstants.DirectExchangeType;
            p.Durable = true;
        });
        
        cfg.Publish<UpdateUserRatingEvent>(p =>
        {
            p.ExchangeType = RabbitMqConstants.DirectExchangeType;
            p.Durable = true;
        });
        
        cfg.Host($"rabbitmq://{rabbitMqOptions.Host}");
        cfg.ExchangeType = RabbitMqConstants.DirectExchangeType;
        cfg.ConfigureEndpoints(ctx);
    });
});
#endregion

#region Kestrel and gRPC Configuration
var kestrelOptions = builder.Configuration.GetSection(nameof(KestrelOptions)).Get<KestrelOptions>()!;
builder.WebHost.ConfigureKestrel(kestrelOptions);
var grpcOptions = builder.Configuration.GetSection(nameof(GrpcOptions)).Get<GrpcOptions>()!;
var accountsUri = grpcOptions.Services.Single(x => x.ServiceName == "Accounts").Uri;
builder.Services.AddGrpcClientService<AccountsService.AccountsServiceClient>(accountsUri);
builder.Services.AddScoped<IAccountsClient, AccountsClient>();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.MapHub<RoomHub>("/rps-game");
app.Services.PrepareDatabaseState<GameDbContext>();
app.Run();