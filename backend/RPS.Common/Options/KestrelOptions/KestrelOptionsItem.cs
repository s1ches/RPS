namespace RPS.Common.Options.KestrelOptions;

public class KestrelOptionsItem
{
    public EndpointType EndpointType { get; set; } = EndpointType.Rest;

    public int Port { get; set; } = 80;
}