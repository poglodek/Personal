using Shared.Exceptions;

namespace Trainer.Domain.Exceptions;

public class PhoneNumberNotValid(string number) : BaseException($"Phone number '{number}' is not valid!")
{
    public override string Message => "phone_number_not_valid";
}