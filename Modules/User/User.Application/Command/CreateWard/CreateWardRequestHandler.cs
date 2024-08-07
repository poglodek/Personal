using MediatR;
using User.Application.Repositories;
using User.Domain.ValueObject;

namespace User.Application.Command.CreateWard;

public class CreateWardRequestHandler : IRequestHandler<CreateWardRequestCommand, Unit>
{
    private readonly IUserRepository _repository;
    private readonly TimeProvider _timeProvider;

    public CreateWardRequestHandler(IUserRepository repository, TimeProvider timeProvider)
    {
        _repository = repository;
        _timeProvider = timeProvider;
    }
    
    public async Task<Unit> Handle(CreateWardRequestCommand request, CancellationToken cancellationToken)
    {
        var ward = await _repository.GetByEmail(request.Email, cancellationToken);

        if (ward is null)
        {
            ward = Domain.Entity.User.CreateWard(  request.FirstName, request.LastName,new Mail(request.Email),request.DateOfBirth, _timeProvider, request.TrainerId);
            await _repository.AddAsync(ward, cancellationToken);
        }
        
        return Unit.Value;
    }
}