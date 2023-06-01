namespace Application.Exceptions;

public class AddProductFailedException : Exception
{
    public AddProductFailedException() : base("While adding product error occur")
    {
    }

    public AddProductFailedException(string? message) : base(message)
    {
    }

    public AddProductFailedException(string? message, Exception? innerException) : base(message, innerException)
    {
    }
}