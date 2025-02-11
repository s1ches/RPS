namespace RPS.Common.Options;

public class CorsOptions
{
    public string[] AllowedOrigins { get; set; } = [];
    public const string CorsPolicyName = "AllowAll";
}