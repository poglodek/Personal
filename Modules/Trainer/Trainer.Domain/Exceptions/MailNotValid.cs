using Shared.Exceptions;

namespace Trainer.Domain.Exceptions;

public class MailNotValid(string address) : BaseException($"Mail '{address}' is not valid!")
{
    public override string Message => "email_address_not_valid";
}