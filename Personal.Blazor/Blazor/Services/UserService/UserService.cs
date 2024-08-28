using Blazor.Clients;

namespace Blazor.Services.UserService;

public class UserService(IPersonalClient client) : IUserService
{

    public async Task Test()
    {
        await client.GetTrainerAsync(Guid.Empty, "");
    }
}