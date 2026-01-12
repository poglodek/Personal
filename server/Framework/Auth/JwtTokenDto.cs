namespace Auth;
 
public record JwtTokenDto
{
    public string AccessToken { get; init; }
    public string Role { get; init; }
    public Guid UserId { get; init; }
    public DateTime ExpiresAt { get; init; }
    public string RefreshToken { get; init; } = string.Empty;
}



