namespace ChatAppModern.BusinessLogic.Services.JWT;

public interface IJwtService
{
    public const string IdClaimName = "id";
    public const string UsernameClaimName = "username";
    string CreateAccessToken(Guid id, string userName, string email);
    RefreshToken CreateRefreshToken();
    Result<Guid> GetIdClaim(ClaimsPrincipal claimsPrincipal);
    Result<Guid> GetIdClaim(string accessToken);
}