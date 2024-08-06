using Shared.Exceptions;

namespace User.Application.Exception;

public class UserBlockedException(Guid id) : BaseException($"User with id {id} is blocked")
{
    public override string ErrorMessage => "user_blocked";
}