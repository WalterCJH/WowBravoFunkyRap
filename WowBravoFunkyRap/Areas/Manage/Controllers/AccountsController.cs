using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WowBravoFunkyRap.Shared.Services.Interface;
using WowBravoFunkyRap.Areas.Manage.ViewModels.Accounts;
using WowBravoFunkyRap.Helper;
using WowBravoFunkyRap.Model.Const;
using WowBravoFunkyRap.Model.Tables;
using WowBravoFunkyRap.Model;

namespace WowBravoFunkyRap.Areas.Manage.Controllers
{
    public class AccountsController : BaseController
    {
        private IAuthenticationSchemeProvider _schemeProvider;
        private IPasswordHasher<User> _passwordHasher;

        public AccountsController(WowBravoFunkyRapDbContext dbContext,
                                  IWebHostEnvironment webHostEnvironment,
                                  ILogger<AccountsController> iLogger,
                                  IAuthenticationSchemeProvider schemeProvider,
                                  IPasswordHasher<User> passwordHasher,
                                  IClaimService claimService) : base(dbContext, webHostEnvironment, iLogger, claimService)
        {
            _schemeProvider = schemeProvider;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(Register register)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    Account = register.UserName,
                    LastName = register.LastName,
                    FirstName = register.FirstName,
                    Email = register.Email,
                };

                user.PasswordHash = _passwordHasher.HashPassword(user, register.Password);

                db.Users.Add(user);
                await db.SaveChangesAsync();

                return RedirectToAction("Login");
            }

            TempData[SessionStr.ErrorMessage] = "資料有誤，請檢查欄位";
            return View(register);
        }

        [HttpGet]
        public async Task<IActionResult> Login()
        {
            var login = new Login();
            login.UserName = "walter";
            login.Password = "1234";
            var loginResult = await LoginAsync(login);
            if (loginResult)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (ModelState.IsValid)
            {
                var loginResult = await LoginAsync(login);
                if (loginResult)
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            TempData[SessionStr.ErrorMessage] = "帳號密碼錯誤";
            return View(login);
        }

        private async Task<bool> LoginAsync(Login login)
        {
            var user = await db.Users.FirstOrDefaultAsync(c => c.Account == login.UserName);

            var verifyResult = _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, login.Password);
            if (verifyResult == PasswordVerificationResult.Success || verifyResult == PasswordVerificationResult.SuccessRehashNeeded)
            {
                if (verifyResult == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    var newHashedPassword = _passwordHasher.HashPassword(user, login.Password);
                    user.PasswordHash = newHashedPassword; // 更新用戶的密碼哈希值
                    await db.SaveChangesAsync();
                }
                // 獲取所有身份驗證方案
                //var allSchemes = (await _schemeProvider.GetAllSchemesAsync()).Select(s => s.Name).ToList();

                // 假設你想在某些情況下使用另一個方案
                //var authScheme = allSchemes.Contains("AnotherScheme")
                //    ? "AnotherScheme"
                //    : "DefaultScheme";

                var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, user.Name),
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    };

                var roleStrList = await db.UserRoles.Where(c => c.UserId == user.Id).SelectMany(c => c.Role.RoleItems.Select(c => c.Id)).Distinct().ToListAsync();
                foreach (var roleStr in roleStrList)
                {
                    var claim = new Claim(ClaimTypes.Role, roleStr);
                    claims.Add(claim);
                }

                if (AuthorizeHelper.IsAdmin(user.Email))
                {
                    claims.Add(new Claim(ClaimTypes.System, "YES"));
                }

                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme));

                var properties = new AuthenticationProperties
                {
                    IsPersistent = true,
                    ExpiresUtc = DateTimeOffset.Now.AddHours(4)
                    //ExpiresUtc = DateTimeOffset.UtcNow.AddHours(4)
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, properties);

                return true;
            }

            return false;
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            //await HttpContext.SignOutAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Accounts");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult NotFound()
        {
            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
