using Auth;
using MediatR;
using Microsoft.Extensions.Logging;
using User.Application.Exception;
using User.Application.Repositories;

namespace User.Application.Command.RefreshToken;

public class RefreshTokenRequestCommandHandler : IRequestHandler<RefreshTokenRequestCommand, JwtTokenDto>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IJsonWebTokenManager _jsonWebTokenManager;
    private readonly ILogger<RefreshTokenRequestCommandHandler> _logger;
    private readonly TimeProvider _timeProvider;

    public RefreshTokenRequestCommandHandler(
        IRefreshTokenRepository refreshTokenRepository,
        IUserRepository userRepository,
        IJsonWebTokenManager jsonWebTokenManager,
        ILogger<RefreshTokenRequestCommandHandler> logger,
        TimeProvider timeProvider)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
        _jsonWebTokenManager = jsonWebTokenManager;
        _logger = logger;
        _timeProvider = timeProvider;
    }

    public async Task<JwtTokenDto> Handle(RefreshTokenRequestCommand request, CancellationToken cancellationToken)
    {
        var tokenHash = _jsonWebTokenManager.HashRefreshToken(request.RefreshToken);
        var refreshToken = await _refreshTokenRepository.GetByTokenHashAsync(tokenHash, cancellationToken);

        if (refreshToken is null || !refreshToken.IsActive())
        {
            _logger.LogWarning("Invalid or expired refresh token for user {UserId}", request.UserId);
            throw new UnauthorizedAccessException("Invalid or expired refresh token");
        }

        if (refreshToken.UserId != request.UserId)
        {
            _logger.LogWarning("Refresh token user ID mismatch. Expected: {Expected}, Actual: {Actual}",
                refreshToken.UserId, request.UserId);
            throw new UnauthorizedAccessException("Token user mismatch");
        }

        var user = await _userRepository.GetById(request.UserId, cancellationToken);

        if (user is null)
        {
            _logger.LogError("User {UserId} not found during refresh token validation", request.UserId);
            throw new UserNotFoundException(request.UserId.ToString());
        }

        if (user.Activated is null)
        {
            _logger.LogError("User {UserId} account not activated", user.Id);
            throw new UserNotActivated(user.Id);
        }

        if (user.Blocked is not null)
        {
            _logger.LogError("User {UserId} account is blocked: {Reason}", user.Id, user.Blocked.Reason);
            throw new UserBlockedException(user.Id);
        }

        var newRefreshToken = _jsonWebTokenManager.GenerateRefreshToken();
        var newRefreshTokenHash = _jsonWebTokenManager.HashRefreshToken(newRefreshToken);

        refreshToken.Revoke(_timeProvider.GetUtcNow(), newRefreshTokenHash);

        var newRefreshTokenEntity = Domain.Entity.RefreshToken.Create(
            user.Id,
            newRefreshTokenHash,
            _timeProvider.GetUtcNow().AddHours(168),
            _timeProvider);

        await _refreshTokenRepository.AddAsync(newRefreshTokenEntity, cancellationToken);

        var jwt = _jsonWebTokenManager.CreateToken(user.Id, user.MailAddress.Value, user.Role.Value);

        user.SetLastLogin(_timeProvider);

        return jwt with { RefreshToken = newRefreshToken };
    }
}