using System.IdentityModel.Tokens.Jwt;
using ApiShared;
using Auth.Utils;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using User.Application;
using User.Application.Command.ActivateUser;
using User.Application.Command.ChangePassword;
using User.Application.Command.CreateUser;
using User.Application.Command.LoginUser;
using User.Application.Command.SetPassword;
using User.Infrastructure;
using User.Infrastructure.Query.GetUserById;
using User.Shared.Request;

namespace User.Shared;

public class UserModule : IModule
{
    public string ModuleName => "Users";
    public IServiceCollection InstallModule(IServiceCollection services, IConfiguration configuration)
    {
        services.AddUserApplication();
        services.AddUserInfra(configuration);

        return services;
    }

    public IEndpointRouteBuilder AddEndPoints(IEndpointRouteBuilder endpointRoute)
    {
        #region AllowAnonymous

        endpointRoute.MapPost("/active/user/{id}", async (IMediator mediator, Guid id, CancellationToken ct) =>
        {
            await mediator.Send(new ActivateUserRequestCommand(id), cancellationToken: ct);
            
            return Results.Accepted();
        }).AllowAnonymous();

        endpointRoute.MapPost("register", async (IMediator mediator, CreateUserRequestCommand request, CancellationToken ct) =>
        {
            await mediator.Send(request, cancellationToken: ct);

            return Results.Ok();

        }).AllowAnonymous();
        
        endpointRoute.MapPost("login", async (IMediator mediator, LoginUserRequestCommand request, CancellationToken ct) =>
        {
            await mediator.Send(request, cancellationToken: ct);

            return Results.Ok();

        }).AllowAnonymous();
        
        endpointRoute.MapPost("set-password", async (IMediator mediator, SetPasswordRequest request, CancellationToken ct) =>
        {
            var commandRequest = new SetPasswordRequestCommand(request.Id, request.Password);
            
            await mediator.Send(commandRequest, cancellationToken: ct);

            return Results.Ok();

        }).AllowAnonymous();
        
        #endregion

        #region RequireClaim
        
        endpointRoute.MapGet("/", async ( IMediator mediator, HttpContext httpContext, CancellationToken ct) =>
        {
            var userId = httpContext.GetUserId();
            
            var result = await mediator.Send(new GetByIdRequest(userId), cancellationToken: ct);

            return Results.Ok(result);
            
        }).RequireClaim("User")
            .CacheOutput();
        
        endpointRoute.MapGet("/{id}", async ( IMediator mediator, Guid id, CancellationToken ct) =>
        {
            var result = await mediator.Send(new GetByIdRequest(id), cancellationToken: ct);

            return Results.Ok(result);
            
        }).RequireClaim("GetUserById")
        .CacheOutput();
        
        endpointRoute.MapPost("change-password", async (IMediator mediator, ChangePasswordRequest request, CancellationToken ct, HttpContext httpContext) =>
        {
            var userId = httpContext.GetUserId();
            var commandRequest = new ChangePasswordRequestCommand(userId, request.Password);
            await mediator.Send(commandRequest, cancellationToken: ct);

            return Results.Ok();

        }).RequireClaim("User");
        
        #endregion
        
        return endpointRoute;
    }

    public IServiceProvider InstallModule(IServiceProvider serviceProvider)
    {
        serviceProvider.UseInfra();
        
        return serviceProvider;
    }
}