using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using User.Application.Exception;
using User.Application.Repositories;

namespace User.Application.Command.RefreshToken;

public class RefreshTokenRequestCommandHandler : IRequestHandler<RefreshTokenRequestCommand, JwtTokenDto>
{
    public Task<JwtTokenDto> Handle(RefreshTokenRequestCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}