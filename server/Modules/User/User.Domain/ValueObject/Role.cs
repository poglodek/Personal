namespace User.Domain.ValueObject;

public record Role(string Value)
{
    public static Role Admin => new("Admin");
    public static Role User => new("User");
}