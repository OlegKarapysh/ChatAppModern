namespace Chat.Domain.Entities;

public sealed class RefreshToken
{
    public readonly TimeSpan Lifetime = TimeSpan.FromDays(2);

    public string Token { get; init; }
    public DateTime ExpirationTime { get; init; }

    public RefreshToken(string token)
    {
        Token = token;
        ExpirationTime = DateTime.UtcNow + Lifetime;
    }

    public bool HasExpired => DateTime.UtcNow >= ExpirationTime;
}