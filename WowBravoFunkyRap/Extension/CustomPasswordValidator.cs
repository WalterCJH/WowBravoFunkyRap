using Microsoft.AspNetCore.Identity;

namespace WowBravoFunkyRap.Extension
{
    public class CustomPasswordValidator : IPasswordValidator<IdentityUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<IdentityUser> manager, IdentityUser user, string password)
        {
            var errors = new List<IdentityError>();

            //if (password.ToLower().Contains(user.UserName.ToLower()))
            //{
            //    errors.Add(new IdentityError
            //    {
            //        Code = "PasswordContainsUsername",
            //        Description = "Password cannot contain username."
            //    });
            //}

            return Task.FromResult(errors.Count == 0 ? IdentityResult.Success : IdentityResult.Failed(errors.ToArray()));
        }
    }

}
