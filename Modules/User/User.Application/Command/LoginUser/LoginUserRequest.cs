using Auth;
using MediatR;

namespace User.Application.Command.LoginUser;

public record LoginUserRequest(string Email, string Password) : IRequest<JwtTokenDto>;