using MediatR;
using User.Application.Command.CreatePassword;
using User.Application.Dto;
using User.Application.Repositories;
using User.Domain.ValueObject;

namespace User.Application.Command.CreateUser;

public class CreateUserRequestHandler(IUserRepository UserRepository, IMediator mediator, 
    TimeProvider timeProvider) : IRequestHandler<CreateUserRequest,UserDtoId>
{
    public async Task<UserDtoId> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var address = new Address(request.City, request.Street, request.PostalCode, request.County);
        
        var User = new Domain.Entity.User(request.FirstName, request.LastName, new PhoneNumber(request.PhoneNumber),
            new Mail(request.MailAddress), address, request.DateOfBirth,  timeProvider);

        await mediator.Send(new CreatePasswordRequest(request.Password, User), cancellationToken);

        await UserRepository.AddAsync(User, cancellationToken);

        return new UserDtoId(User.Id);
    }

    
}