namespace ChatAppModern.BusinessLogic.Services.Auth;

public interface IAuthService
{
    Task<Result<UserAuthTokensDto>> LoginAsync(LoginDto loginData);
    Task<Result<UserAuthTokensDto>> RegisterAsync(RegistrationDto registerData);
    Task<Result> ChangePasswordAsync(ChangePasswordDto changePasswordData, int id);
    Task<Result<UserAuthTokensDto>> RefreshTokenPairAsync(AuthTokensDto tokens);
}