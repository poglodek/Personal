using MediatR;
using User.Application.Command.CreatePassword;
using User.Application.Dto;
using User.Application.Repositories;
using User.Domain.ValueObject;

namespace User.Application.Command.CreateUser;

public class CreateUserRequestHandler : IRequestHandler<CreateUserRequestCommand,UserDtoId>
{
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;
    private readonly TimeProvider _timeProvider;

    public CreateUserRequestHandler(IUserRepository userRepository, IMediator mediator, 
        TimeProvider timeProvider)
    {
        _userRepository = userRepository;
        _mediator = mediator;
        _timeProvider = timeProvider;
    }

    public async Task<UserDtoId> Handle(CreateUserRequestCommand requestCommand, CancellationToken cancellationToken)
    {
        var address = new Address(requestCommand.City, requestCommand.Street, requestCommand.PostalCode, requestCommand.County);
        
        var user = Domain.Entity.User.CreateInstance(requestCommand.FirstName, requestCommand.LastName, new PhoneNumber(requestCommand.PhoneNumber),
            new Mail(requestCommand.MailAddress), address, requestCommand.DateOfBirth,  _timeProvider);

        SetClaims(user, requestCommand.Role, _timeProvider);    
            
        await _mediator.Send(new CreatePasswordRequestCommand(requestCommand.Password, user), cancellationToken);

        await _userRepository.AddAsync(user, cancellationToken);

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