using Auth;
using MediatR;

namespace User.Application.Command.RefreshToken;

public record RefreshTokenRequestCommand(Guid UserId,  string RefreshToken) : IRequest<JwtTokenDto>;
