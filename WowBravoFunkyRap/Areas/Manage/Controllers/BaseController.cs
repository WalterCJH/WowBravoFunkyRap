using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using WowBravoFunkyRap.Shared.Services.Interface;
using WowBravoFunkyRap.ActionFilter;
using WowBravoFunkyRap.Model;

namespace WowBravoFunkyRap.Areas.Manage.Controllers
{
    [ServiceFilter(typeof(MenuActionFilter))]
    [Area("Manage")]
    public class BaseController : Controller
    {
        protected readonly WowBravoFunkyRapDbContext db;
        protected readonly IWebHostEnvironment enviroment;
        protected readonly ILogger logger;
        protected readonly IClaimService _claimService;

        public BaseController(WowBravoFunkyRapDbContext dbContext, IWebHostEnvironment webHostEnvironment, ILogger ilogger, IClaimService claimService)
        {
            db = dbContext;
            enviroment = webHostEnvironment;
            logger = ilogger;
            _claimService = claimService;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            ViewBag.IsAdmin = _claimService.GetUserIsAdmin();
            ViewBag.UserName = _claimService.GetUserName();
            //if (Request.Path.Value.Contains("/Admin/"))
            //{
            //    var userId = context.HttpContext.Users.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            //    if (userId == null) context.Result = new UnauthorizedResult();
            //    else
            //    {
            //        var user = db.Users.FirstOrDefault(c => c.Id == userId.Value || c.Email == userId.Value);
            //        if (user == null) context.Result = new UnauthorizedResult();
            //        else if (!user.IsActive) context.Result = new UnauthorizedResult();
            //    }
            //}
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}
