using Shared.Exceptions;

namespace User.Domain.Exceptions;

public class PhoneNumberNotValid(string number) : BaseException($"Phone number '{number}' is not valid!")
{
    public override string ErrorMessage => "phone_number_not_valid";
}