namespace ChatAppModern.BusinessLogic.Services.Auth;

public interface IAuthService
{
    Task<Result<TokenPairDto>> LoginAsync(LoginDto loginData);
    Task<Result<TokenPairDto>> RegisterAsync(RegistrationDto registerData);
    Task<Result> ChangePasswordAsync(ChangePasswordDto changePasswordData, int id);
    Task<Result<TokenPairDto>> RefreshTokenPairAsync(TokenPairDto tokens);
}