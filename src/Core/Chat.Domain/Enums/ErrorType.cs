namespace Chat.Domain.Enums;

public enum ErrorType
{
    InvalidEmail = 1,
    InvalidUsername,
    InvalidPassword,
    InvalidEmailOrPassword,
    InvalidToken,
    InvalidAction,
    InvalidNaming,
    NotFound,
    Internal,
    TokenExpired,
    Unknown
}