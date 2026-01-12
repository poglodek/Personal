using MediatR;

namespace User.Application.Command.CreateWard;

public record CreateWardRequestCommand(Guid TrainerId,string Email, string FirstName, string LastName, DateOnly DateOfBirth) : IRequest<Unit>;