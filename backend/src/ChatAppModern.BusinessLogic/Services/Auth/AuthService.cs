namespace ChatAppModern.BusinessLogic.Services.Auth;

public sealed class AuthService : IAuthService
{
    private readonly IJwtService _jwtService;
    private readonly UserManager<User> _userManager;
    
    public AuthService(IJwtService jwtService, UserManager<User> userManager)
    {
        _jwtService = jwtService;
        _userManager = userManager;
    }
    
    public async Task<Result<UserAuthTokensDto>> LoginAsync(LoginDto loginData)
    {
        var user = await _userManager.FindByEmailAsync(loginData.Email);
        if (user is null)
        {
            return new InvalidEmailException().ToFailedResult();
        }
        var isPasswordCorrect = await _userManager.CheckPasswordAsync(user, loginData.Password);
        if (!isPasswordCorrect)
        {
            return new InvalidPasswordException().ToFailedResult();
        }

        var refreshToken = _jwtService.CreateRefreshToken();
        await UpdateUserRefreshTokenAsync(user, refreshToken);

        return CreateTokenPair(user);
    }

    public async Task<Result<UserAuthTokensDto>> RegisterAsync(RegistrationDto registerData)
    {
        if (await _userManager.FindByEmailAsync(registerData.Email) is not null)
        {
            return new EmailAlreadyRegisteredException().ToFailedResult();
        }

        if (await _userManager.FindByNameAsync(registerData.UserName) is not null)
        {
            return new UsernameAlreadyRegisteredException().ToFailedResult();
        }

        var refreshToken = _jwtService.CreateRefreshToken();
        var user = new User
        {
            UserName = registerData.UserName,
            Email = registerData.Email,
            RefreshToken = refreshToken.Token,
            TokenExpiresAt = refreshToken.ExpirationTime
        };
        var identityResult = await _userManager.CreateAsync(user, registerData.Password);
        var registeredUser = await _userManager.FindByEmailAsync(user.Email);
        if (!identityResult.Succeeded || registeredUser is null)
        {
            return new BadRegistrationException().ToFailedResult();
        }

        return CreateTokenPair(registeredUser, refreshToken.Token);
    }

    public async Task<Result> ChangePasswordAsync(ChangePasswordDto changePasswordData, int id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());
        if (user is null)
        {
            return new EntityNotFoundException(nameof(User));
        }
        var changePasswordResult = await _userManager.ChangePasswordAsync(
            user, changePasswordData.CurrentPassword, changePasswordData.NewPassword);

        return !changePasswordResult.Succeeded
            ? new ChangePasswordFailedException()
            : Result.Ok()!;
    }

    public async Task<Result<UserAuthTokensDto>> RefreshTokenPairAsync(AuthTokensDto tokens)
    {
        var userId = _jwtService.GetIdClaim(tokens.AccessToken);
        if (userId.IsFailed)
        {
            return Result.Fail(userId.Errors!);
        }
        var user = await _userManager.FindByIdAsync(userId.Value.ToString());
        if (user is null)
        {
            return new EntityNotFoundException(nameof(User)).ToFailedResult();
        }
        if (user.RefreshToken is null)
        {
            return new EntityNotFoundException(nameof(RefreshToken)).ToFailedResult();
        }
        if (user.TokenExpiresAt <= DateTime.UtcNow)
        {
            return new RefreshTokenExpiredException().ToFailedResult();
        }
        if (tokens.RefreshToken != user.RefreshToken)
        {
            return new InvalidTokenException(nameof(RefreshToken)).ToFailedResult();
        }

        var newRefreshToken = _jwtService.CreateRefreshToken();
        await UpdateUserRefreshTokenAsync(user, newRefreshToken);

        return CreateTokenPair(user, newRefreshToken.Token);
    }

    private UserAuthTokensDto CreateTokenPair(User user, string? refreshToken = default)
        => new()
        {
            UserId = user.Id.ToString(),
            AccessToken = _jwtService.CreateAccessToken(user.Id, user.UserName!, user.Email!),
            RefreshToken = refreshToken ?? user.RefreshToken ?? throw new Exception("Refresh token is not set!")
        };

    private async Task UpdateUserRefreshTokenAsync(User user, RefreshToken refreshToken)
    {
        user.RefreshToken = refreshToken.Token;
        user.TokenExpiresAt = refreshToken.ExpirationTime;
        await _userManager.UpdateAsync(user);
    }
}