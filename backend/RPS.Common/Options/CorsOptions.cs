namespace RPS.Common.Options;

public class CorsOptions
{
    public const string CorsPolicyName = "AllowAll";
    
    public string[] AllowedOrigins { get; set; } = [];
}