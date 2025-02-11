using MassTransit;
using RPS.Common.Configuration;
using RPS.Common.Masstransit.Constansts;
using RPS.Common.Masstransit.Events;
using RPS.Common.MediatR;
using RPS.Common.Middlewares;
using RPS.Common.Options;
using RPS.Services.Accounts.Configuration;
using RPS.Services.Accounts.Masstransit.Consumers;

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

#region MongoDb Configuration
builder.Services.AddMongoDb(builder.Configuration);
#endregion

#region Services Configuration
builder.Services.AddMediatR(typeof(Program).Assembly);
#endregion

#region Masstransit Configuration
var rabbitMqOptions = new RabbitMqOptions();
builder.Configuration.GetSection(nameof(RabbitMqOptions)).Bind(rabbitMqOptions);
builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<RegistrationConsumer>();
    
    x.UsingRabbitMq((ctx, cfg) =>
    {
        cfg.Host($"rabbitmq://{rabbitMqOptions.Host}");

        cfg.ReceiveEndpoint(RabbitMqConstants.RegistrationEventsQueueName, e =>
        { 
            e.ConfigureConsumeTopology = false;
            e.ConfigureConsumer<RegistrationConsumer>(ctx);

            e.Bind<RegistrationEvent>(exchange =>
            {
                exchange.Durable = true;
                exchange.ExchangeType = RabbitMqConstants.DirectExchangeType;
                exchange.RoutingKey = RabbitMqConstants.RegistrationEventsRoutingKey;
            });
        });
        
        cfg.ConfigureEndpoints(ctx);
    });
});
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

app.Run();