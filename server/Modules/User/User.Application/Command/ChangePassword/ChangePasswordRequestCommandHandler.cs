using MediatR;
using User.Application.Command.CreatePassword;
using User.Application.Exception;
using User.Application.Repositories;

namespace User.Application.Command.ChangePassword;

public class ChangePasswordRequestCommandHandler : IRequestHandler<ChangePasswordRequestCommand, Unit>
{
    private readonly IUserRepository _repository;
    private readonly IMediator _mediator;

    public ChangePasswordRequestCommandHandler(IUserRepository repository, IMediator mediator)
    {
        _repository = repository;
        _mediator = mediator;
    }
    
    public async Task<Unit> Handle(ChangePasswordRequestCommand request, CancellationToken cancellationToken)
    {
        var user = await _repository.GetById(request.Id, cancellationToken);
        if (user is null)
        {
            throw new UserNotFoundException(request.Id);
        }
        
        var createPasswordRequestCommand = new CreatePasswordRequestCommand(request.Password, user);
        await _mediator.Send(createPasswordRequestCommand, cancellationToken);
        
        return Unit.Value;
    }
}