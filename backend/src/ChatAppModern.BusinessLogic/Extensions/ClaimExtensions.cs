namespace ChatAppModern.BusinessLogic.Extensions;

public static class ClaimExtensions
{
    public static Result<Guid> GetIdClaim(this ClaimsPrincipal claimsPrincipal)
    {
        var isParsed = Guid.TryParse(claimsPrincipal.FindFirstValue(IJwtService.IdClaimName), out var id);
        return !isParsed ? new MissingClaimException(IJwtService.IdClaimName).ToFailedResult<Guid>() : id;
    }
}