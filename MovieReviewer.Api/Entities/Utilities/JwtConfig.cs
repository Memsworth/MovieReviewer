namespace MovieReviewer.Api.Entities.Utilities;

public class JwtConfig
{
    public required string Issuer { get; set; }
    public required string Audience { get; set; }
    public required string SecurityKey { get; set; }
}