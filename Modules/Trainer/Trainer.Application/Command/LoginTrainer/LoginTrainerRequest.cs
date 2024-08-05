using Auth;
using MediatR;

namespace Trainer.Application.Command.LoginTrainer;

public record LoginTrainerRequest(string Email, string Password) : IRequest<JwtTokenDto>, IRequest<Unit>;