﻿namespace Chat.IntegrationTests.TestHelpers;

public class IntegrationTest : IDisposable
{
    internal readonly HttpClient HttpClient;
    internal readonly TestApplicationFactory TestAppFactory;
    internal readonly LoginDto LoggedInUser;
    internal readonly RegistrationDto RegisteredUser;
    
    public IntegrationTest()
    {
        TestAppFactory = new TestApplicationFactory();
        HttpClient = TestAppFactory.CreateClient();
        LoggedInUser = new()
        {
            Email = "oleh@a.a",
            Password = "asdfA1!"
        };
        RegisteredUser = new()
        {
            UserName = "DefaultUser",
            Email = "email@gmail.com",
            Password = "somethingA1!",
            RepeatPassword = "somethingA1!"
        };
    }

    public void Dispose()
    {
        HttpClient.Dispose();
        TestAppFactory.Dispose();
    }

    internal async Task<TokenPairDto> RegisterAsync(RegistrationDto? registrationDto = null)
    {
        const string registerRoute = "api/auth/register";
        var registrationResponse = await HttpClient.PostAsJsonAsync(registerRoute, registrationDto ?? RegisteredUser);
        if (!registrationResponse.IsSuccessStatusCode)
        {
            throw new BadRegistrationException();
        }

        var tokens = await registrationResponse.Content.ReadFromJsonAsync<TokenPairDto>();
        if (tokens is null)
        {
            throw new Exception("Couldn't get authentication tokens!");
        }
        
        SetAuthorizationHeader(tokens.AccessToken);
        return tokens;
    }

    internal async Task<TokenPairDto> LoginAsync(LoginDto? loginDto = null)
    {
        const string loginRoute = "api/auth/login";
        var loginResponse = await HttpClient.PostAsJsonAsync(loginRoute, loginDto ?? LoggedInUser);
        if (!loginResponse.IsSuccessStatusCode)
        {
            throw new Exception("Login failed!");
        }

        var tokens = await loginResponse.Content.ReadFromJsonAsync<TokenPairDto>();
        if (tokens is null)
        {
            throw new Exception("Couldn't get authentication tokens!");
        }
        
        SetAuthorizationHeader(tokens.AccessToken);
        return tokens;
    }

    internal void SetAuthorizationHeader(string jwt)
    {
        var authHeader = new AuthenticationHeaderValue(JwtBearerDefaults.AuthenticationScheme, jwt);
        HttpClient.DefaultRequestHeaders.Authorization = authHeader;
    }
}