using Trainer.Domain.Exceptions;

namespace Trainer.Domain.ValueObject;

public record PhoneNumber
{
    public string Value { get; init; }
    
    public PhoneNumber(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new PhoneNumberNotValid(value);
        }
        
        if (!value.Contains("+"))
        {
            throw new PhoneNumberNotValid(value);
        }

        Value = value.Trim();
    }
}