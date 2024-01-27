namespace ChatAppModern.Domain.DTOs.Authentication;

public sealed class TokenPairDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}