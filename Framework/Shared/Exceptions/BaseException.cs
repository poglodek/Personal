namespace Shared.Exceptions;

public abstract class BaseException(string message) : Exception(message)
{
    public abstract string ErrorMessage { get;}
}