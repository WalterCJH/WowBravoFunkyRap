using Microsoft.AspNetCore.Authorization;
using System.Data;

namespace WowBravoFunkyRap.Extension
{
    /// <summary>
    /// 自訂聲明
    /// </summary>
    public enum Policy
    {
    }

    public class AuthorizePolicy : AuthorizeAttribute
    {
        public AuthorizePolicy(params Policy[] policies)
        {
            Policy = string.Join(",", policies);
        }
    }
}
