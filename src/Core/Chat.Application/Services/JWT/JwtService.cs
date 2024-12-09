﻿namespace Chat.Application.Services.JWT;

public sealed class JwtService : IJwtService
{
    private readonly JwtOptions _jwtOptions;
    private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();

    public JwtService(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    public string CreateAccessToken(int id, string userName, string email)
    {
        var identity = GenerateClaimsIdentity(id, userName);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.Email, email),
            new Claim(JwtRegisteredClaimNames.Jti, _jwtOptions.JtiGenerator.Invoke()),
            new Claim(JwtRegisteredClaimNames.Iat,
                ToUnixEpochDate(_jwtOptions.IssuedAt).ToString(), ClaimValueTypes.Integer64),
            identity.FindFirst(IJwtService.IdClaimName),
            identity.FindFirst(IJwtService.UserNameClaimName)
        };

        var jwt = new JwtSecurityToken(
            _jwtOptions.Issuer,
            _jwtOptions.Audience,
            claims,
            _jwtOptions.NotBefore,
            _jwtOptions.Expiration,
            _jwtOptions.SigningCredentials);

        return _jwtSecurityTokenHandler.WriteToken(jwt)!;
    }

    public RefreshToken CreateRefreshToken()
    {
        const int saltLength = 32;
        var salt = new byte[saltLength];
        using var randomNumberGenerator = RandomNumberGenerator.Create();
        randomNumberGenerator.GetBytes(salt);

        return new RefreshToken(Convert.ToBase64String(salt));
    }

    public int GetIdClaim(ClaimsPrincipal claimsPrincipal)
    {
        var isParsed = int.TryParse(claimsPrincipal.FindFirstValue(IJwtService.IdClaimName), out var id);
        if (!isParsed)
        {
            throw new MissingClaimException(IJwtService.IdClaimName);
        }

        return id;
    }

    public int GetIdClaim(string accessToken)
    {
        var claimsPrincipal = _jwtSecurityTokenHandler.ValidateToken(accessToken, new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecretKey)),
            ValidateLifetime = false
        }, out _);
        if (claimsPrincipal is null)
        {
            throw new InvalidTokenException("Access");
        }

        return GetIdClaim(claimsPrincipal);
    }

    private static ClaimsIdentity GenerateClaimsIdentity(int id, string userName)
    {
        return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
        {
            new Claim(IJwtService.IdClaimName, id.ToString()),
            new Claim(IJwtService.UserNameClaimName, userName)
        });
    }

    private long ToUnixEpochDate(DateTime date)
    {
        var unixEpoch = new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero);
        return (long)Math.Round((date.ToUniversalTime() - unixEpoch).TotalSeconds);
    }
}