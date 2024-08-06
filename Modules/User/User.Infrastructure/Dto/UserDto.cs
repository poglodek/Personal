namespace User.Infrastructure.Dto;

public record UserDto(string FirstName, string LastName, string Email, string PhoneNumber, string Role,
    string Street, string City, string PostCode, string Country, DateOnly DateOfBirth);

public static class UserDtoMapper
{
    public static UserDto AsDto(this Domain.Entity.User user)
        => new UserDto(user.FirstName.Value, user.LastName.Value, user.MailAddress.Value, user.PhoneNumber.Value,
            user.Role.Value, user.Address.Street, user.Address.City, user.Address.PostalCode,
            user.Address.County, user.DateOfBirth);
}