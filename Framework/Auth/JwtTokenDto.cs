namespace Auth;

public record JwtTokenDto
{
    public string Jwt { get; init; }
    public string Role { get; init; }
    public List<string> Claims { get; init; } = [];
    public Guid UserId { get; init; }
    public int ExpiresInHours { get; init; }
}