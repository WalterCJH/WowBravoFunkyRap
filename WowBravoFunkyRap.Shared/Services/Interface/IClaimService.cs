using System.Collections.Generic;

namespace WowBravoFunkyRap.Shared.Services.Interface
{
    public interface IClaimService
    {
        string GetUserId();
        string GetUserName();
        List<string> QueryUserRole();
        bool GetUserIsAdmin();

        string? GetClaim(string key);
        List<string> QueryClaim(string key);
    }
}
