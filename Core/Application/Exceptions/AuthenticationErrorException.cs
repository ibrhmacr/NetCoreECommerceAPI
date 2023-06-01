namespace Application.Exceptions;

public class AuthenticationErrorException : Exception
{
    public AuthenticationErrorException() : base("Authentication Failed")
    {
    }

    public AuthenticationErrorException(string? message) : base(message)
    {
    }

    public AuthenticationErrorException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}