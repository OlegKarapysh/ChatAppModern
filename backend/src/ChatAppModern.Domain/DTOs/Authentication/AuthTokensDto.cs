namespace ChatAppModern.Domain.DTOs.Authentication;

public class AuthTokensDto
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
}