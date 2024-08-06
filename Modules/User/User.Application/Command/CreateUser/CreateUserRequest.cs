using MediatR;
using User.Application.Dto;

namespace User.Application.Command.CreateUser;

public record CreateUserRequest(string FirstName, string LastName, string PhoneNumber, string MailAddress, string City, 
    string Street, string PostalCode, string County, DateOnly DateOfBirth, string Password) : IRequest<UserDtoId>;