using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;

namespace RPS.Common.Grpc.Clients;

public static class AddGrpcClientExtensions
{
    public static IServiceCollection AddGrpcClientService<TClient>(this IServiceCollection services, string uri)
        where TClient : ClientBase
    {
        services
            .AddGrpcClient<TClient>(o => { o.Address = new Uri(uri); })
            .ConfigureChannel(ch =>
            {
                ch.HttpHandler = new SocketsHttpHandler
                {
                    EnableMultipleHttp2Connections = true,
                    PooledConnectionIdleTimeout = Timeout.InfiniteTimeSpan,
                    KeepAlivePingDelay = TimeSpan.FromSeconds(60),
                    KeepAlivePingTimeout = TimeSpan.FromSeconds(30),
                };
            });

        return services;
    }
}