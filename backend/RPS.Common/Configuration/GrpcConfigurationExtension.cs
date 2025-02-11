using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using RPS.Common.Options.KestrelOptions;

namespace RPS.Common.Configuration;

public static class GrpcConfigurationExtension
{
    public static IWebHostBuilder ConfigureKestrel(this IWebHostBuilder builder, KestrelOptions kestrelOptions)
    {
        return builder.UseKestrel(options =>
        {
            var restOptions = kestrelOptions.Options.First(x => x.EndpointType == EndpointType.Rest);
            options.ListenAnyIP(restOptions.Port, listenOptions => { listenOptions.Protocols = HttpProtocols.Http1; });

            var grpcOptions = kestrelOptions.Options.First(x => x.EndpointType == EndpointType.Grpc);
            options.ListenAnyIP(grpcOptions.Port, listenOptions => { listenOptions.Protocols = HttpProtocols.Http2; });
        });
    }
}