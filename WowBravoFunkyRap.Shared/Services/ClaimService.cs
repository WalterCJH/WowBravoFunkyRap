using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using WowBravoFunkyRap.Shared.Services.Interface;

namespace WowBravoFunkyRap.Shared.Services
{
    public class ClaimService : IClaimService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetUserId()
        {
            return GetClaim(ClaimTypes.NameIdentifier);
        }

        public string GetUserName()
        {
            return GetClaim(ClaimTypes.Name);
        }

        public List<string> QueryUserRole()
        {
            return QueryClaim(ClaimTypes.Role);
        }

        public bool GetUserIsAdmin()
        {
            return !string.IsNullOrWhiteSpace(GetClaim(ClaimTypes.System));
        }

        public string? GetClaim(string key)
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(key)?.Value;
        }

        public List<string> QueryClaim(string key)
        {
            return _httpContextAccessor.HttpContext?.User?.FindAll(key).Select(c => c.Value).ToList();
        }
    }
}
