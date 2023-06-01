using System.Runtime.Serialization;

namespace Application.Exceptions;

public class UserCreateFailedException : Exception
{
    public UserCreateFailedException()
    {
    }

    public UserCreateFailedException(string? message) : base(message)
    {
    }

    public UserCreateFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}