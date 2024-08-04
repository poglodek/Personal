namespace Trainer.Domain.ValueObject;

public record Name(string Value)
{
    public static implicit operator Name(string value) => new (value);
}