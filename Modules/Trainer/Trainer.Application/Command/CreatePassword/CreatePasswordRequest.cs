using MediatR;

namespace Trainer.Application.Command.CreatePassword;

public record CreatePasswordRequest(string Password, Domain.Entity.Trainer Trainer) : IRequest<Unit>;
