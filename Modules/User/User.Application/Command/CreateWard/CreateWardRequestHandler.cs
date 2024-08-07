using MediatR;

namespace User.Application.Command.CreateWard;

public class CreateWardRequestHandler : IRequestHandler<CreateWardRequestCommand, Unit>
{
    public CreateWardRequestHandler()
    {
        
    }
    
    public Task<Unit> Handle(CreateWardRequestCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}