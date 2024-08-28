using Refit;

namespace Blazor.Clients;

public interface IPersonalClient
{
    [Post("api/Users/set-password")]
    public Task<string> SetNewPasswordAsync([Body]ChangePasswordRequest request);
    
    [Get("api/Ward/GetTrainer/{id}")]
    public Task<TrainerDto> GetTrainerAsync([AliasAs("id")] Guid wardId, [Authorize("Bearer")] string token);
}