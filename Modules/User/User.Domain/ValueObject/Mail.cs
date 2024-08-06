using System.Net.Mail;
using User.Domain.Exceptions;

namespace User.Domain.ValueObject;

public record Mail
{
    public string Value { get; init; }
    
    public Mail(string mail)
    {
        try
        {
            var email = new MailAddress(mail);
            Value = email.Address;
        }
        catch (Exception e)
        {
            throw new MailNotValid(mail);
        }
    }
}