using MediatR;
using User.Application.Repositories;
using User.Infrastructure.Dto;
using User.Infrastructure.Exceptions;

namespace User.Infrastructure.Query.GetUserById;

public class GetByIdRequestHandler(IUserRepository userRepository) : IRequestHandler<GetByIdRequest,UserDto>
{
    public async Task<UserDto> Handle(GetByIdRequest request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetById(request.Id, cancellationToken);

        if (user is null)
        {
            throw new UserNotFound(request.Id);
        }

        return user.AsDto();
    }
}