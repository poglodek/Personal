using Shared.Exceptions;

namespace User.Application.Exception;

public class UserAlreadyHasPasswordException(Guid id) : BaseException($"User with id {id} already has a password")
{
    public override string ErrorMessage => "user_already_has_password";
}