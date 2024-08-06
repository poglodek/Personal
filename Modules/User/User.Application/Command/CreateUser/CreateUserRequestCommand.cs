using MediatR;
using User.Application.Dto;

namespace User.Application.Command.CreateUser;

public record CreateUserRequestCommand(string FirstName, string LastName, string PhoneNumber, string MailAddress, string City, 
    string Street, string PostalCode, string County, DateOnly DateOfBirth, string Password, string Role) : IRequest<UserDtoId>;