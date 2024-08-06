using MediatR;
using User.Application.Command.CreatePassword;
using User.Application.Dto;
using User.Application.Repositories;
using User.Domain.ValueObject;

namespace User.Application.Command.CreateUser;

public class CreateUserRequestHandler(IUserRepository userRepository, IMediator mediator, 
    TimeProvider timeProvider) : IRequestHandler<CreateUserRequest,UserDtoId>
{
    public async Task<UserDtoId> Handle(CreateUserRequest request, CancellationToken cancellationToken)
    {
        var address = new Address(request.City, request.Street, request.PostalCode, request.County);
        
        var user = new Domain.Entity.User(request.FirstName, request.LastName, new PhoneNumber(request.PhoneNumber),
            new Mail(request.MailAddress), address, request.DateOfBirth,  timeProvider);

        SetClaims(user, request.Role, timeProvider);    
            
        await mediator.Send(new CreatePasswordRequest(request.Password, user), cancellationToken);

        await userRepository.AddAsync(user, cancellationToken);

        return new UserDtoId(user.Id);
    }
    
    private void SetClaims(Domain.Entity.User user, string role,TimeProvider timeProvider)
    {
        switch (role)
        {
            case "Trainer": user.SetDefaultClaimsForTrainer(timeProvider);
                break;
            default: user.SetDefaultClaimsForUser(timeProvider);
                break;
        }
    }

    
}