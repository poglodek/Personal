using MediatR;

namespace User.Application.Command.CreatePassword;

public record CreatePasswordRequest(string Password, Domain.Entity.User User) : IRequest<Unit>;
