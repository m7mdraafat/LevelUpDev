namespace LevelUpDev.Infrastructure.Settings;

public class JwtSettings
{
    public required string Issuer { get; init; }
    public required string Audience { get; init; }
    public required string SecretKey { get; init; }
    public required int ExpirationMinutes { get; init; } = 10080; // 7 days
}
