using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WowBravoFunkyRap.Model.Enums;

namespace WowBravoFunkyRap.Extension
{
    public class AuthorizeRoles : AuthorizeAttribute//Attribute, IAuthorizationFilter
    {
        public AuthorizeRoles(params eRole[] roles)
        {
            Roles = string.Join(",", roles);
        }

        //public void OnAuthorization(AuthorizationFilterContext context)
        //{
        //IsUserValid(context);
        //if (!IsUserValid(context))
        //{
        //    context.Result = new StatusCodeResult(StatusCodes.Status403Forbidden);
        //}
        //}

        //private bool IsUserValid(AuthorizationFilterContext context)
        //{
        // 在這裡實作您的自訂授權邏輯
        //var user = context.HttpContext.User;
        //if (user.Identity?.IsAuthenticated == true && user.HasClaim(c => c.Type == "CustomClaim"))
        //{
        //return true;
        //}
        //return false;
        //}
    }
}
