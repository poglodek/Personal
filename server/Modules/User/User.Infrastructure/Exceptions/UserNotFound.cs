using Shared.Exceptions;

namespace User.Infrastructure.Exceptions;

public class UserNotFound(Guid id) : BaseException($"User with id {id} not found")
{
    public override string ErrorMessage => "user_not_found";
}