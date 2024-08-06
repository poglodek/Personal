using MediatR;
using User.Application.Command.CreatePassword;
using User.Application.Dto;
using User.Application.Repositories;
using User.Domain.ValueObject;

namespace User.Application.Command.CreateUser;

public class CreateUserRequestHandler(IUserRepository userRepository, IMediator mediator, 
    TimeProvider timeProvider) : IRequestHandler<CreateUserRequestCommand,UserDtoId>
{
    public async Task<UserDtoId> Handle(CreateUserRequestCommand requestCommand, CancellationToken cancellationToken)
    {
        var address = new Address(requestCommand.City, requestCommand.Street, requestCommand.PostalCode, requestCommand.County);
        
        var user = new Domain.Entity.User(requestCommand.FirstName, requestCommand.LastName, new PhoneNumber(requestCommand.PhoneNumber),
            new Mail(requestCommand.MailAddress), address, requestCommand.DateOfBirth,  timeProvider);

        SetClaims(user, requestCommand.Role, timeProvider);    
            
        await mediator.Send(new CreatePasswordRequestCommand(requestCommand.Password, user), cancellationToken);

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