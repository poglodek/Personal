using Shared.Exceptions;

namespace User.Application.Exception;

public class UserNotActivated(Guid id) : BaseException($"User with id {id} is not activated")
{
    public override string Message => "user_not_activated";
}