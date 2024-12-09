﻿namespace Chat.Application.RequestExceptions;

public class InvalidEmailException : RequestException
{
    public InvalidEmailException() : base(
        "Invalid email address!",
        ErrorType.InvalidEmail,
        HttpStatusCode.BadRequest)
    {
    }
}