using MediatR;

namespace User.Application.Command.CreatePassword;

public record CreatePasswordRequestCommand(string Password, Domain.Entity.User User) : IRequest<Unit>;
