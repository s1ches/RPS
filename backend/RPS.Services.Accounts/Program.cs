using MassTransit;
using RPS.Common.Configuration;
using RPS.Common.MediatR;
using RPS.Common.Middlewares;
using RPS.Common.Options;
using RPS.Services.Accounts.Configuration;
using RPS.Services.Accounts.Masstransit.Consumers;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
// builder.Services.AddMassTransit(cfg =>
// {
//     cfg.SetKebabCaseEndpointNameFormatter();
//     cfg.AddConsumer<RegistrationConsumer>();
// });
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