using Auth;
using MediatR;

namespace User.Application.Command.LoginUser;

public record LoginUserRequestCommand(string Email, string Password) : IRequest<JwtTokenDto>;