using MediatR;
using User.Application.Command.ChangePassword;
using User.Application.Exception;
using User.Application.Repositories;

namespace User.Application.Command.SetPassword;

public class SetPasswordRequestCommandHandler : IRequestHandler<SetPasswordRequestCommand,Unit>
{
    private readonly IUserRepository _userRepository;
    private readonly IMediator _mediator;

    public SetPasswordRequestCommandHandler(IUserRepository userRepository, IMediator mediator)
    {
        _userRepository = userRepository;
        _mediator = mediator;
    }
    
    public async Task<Unit> Handle(SetPasswordRequestCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(request.Id, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }
        
        if(user.Password is not null)
        {
            throw new UserAlreadyHasPasswordException(request.Id);
        }

        var createPasswordRequestCommand = new ChangePasswordRequestCommand(request.Id, request.Password);
        await _mediator.Send(createPasswordRequestCommand, cancellationToken);
       
        return Unit.Value;
    }
}