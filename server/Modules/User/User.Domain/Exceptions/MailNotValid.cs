using Shared.Exceptions;

namespace User.Domain.Exceptions;

public class MailNotValid(string address) : BaseException($"Mail '{address}' is not valid!")
{
    public override string ErrorMessage => "email_address_not_valid";
}