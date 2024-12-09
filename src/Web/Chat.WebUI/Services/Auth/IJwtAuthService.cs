﻿namespace Chat.WebUI.Services.Auth;

public interface IJwtAuthService
{
    Task<ErrorDetailsDto?> RegisterAsync(RegistrationDto registerData);
    Task<ErrorDetailsDto?> LoginAsync(LoginDto loginData);
    Task<ErrorDetailsDto?> ChangePasswordAsync(ChangePasswordDto changePasswordData);
    Task LogoutAsync();
}