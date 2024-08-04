using System.Security.Cryptography;

namespace Trainer.Application.Service.SaltService;

internal class SaltService : ISaltService
{
    public string GenerateSalt()
    {
        var salt = new byte[16];

        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(salt);

        return Convert.ToBase64String(salt);
    }
}