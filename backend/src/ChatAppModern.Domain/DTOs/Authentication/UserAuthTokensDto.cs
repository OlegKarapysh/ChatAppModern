namespace ChatAppModern.Domain.DTOs.Authentication;

public sealed class UserAuthTokensDto : AuthTokensDto
{
    public string UserId { get; set; } = string.Empty;
}