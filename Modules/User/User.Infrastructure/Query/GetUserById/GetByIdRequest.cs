using MediatR;
using User.Infrastructure.Dto;

namespace User.Infrastructure.Query.GetUserById;

public record GetByIdRequest(Guid Id) : IRequest<UserDto>;