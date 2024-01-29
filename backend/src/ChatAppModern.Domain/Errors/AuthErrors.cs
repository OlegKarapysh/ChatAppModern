namespace ChatAppModern.Domain.Errors;

public static class AuthErrors
{
    public const string InvalidEmail = "Auth.InvalidEmail";
    public const string InvalidUsername = "Auth.InvalidUsername";
    public const string InvalidPassword = "Auth.InvalidPassword";
    public const string InvalidEmailOrPassword = "Auth.InvalidEmailOrPassword";
    public const string InvalidToken = "Auth.InvalidToken";
    public const string TokenExpired = "Auth.TokenExpired";
}