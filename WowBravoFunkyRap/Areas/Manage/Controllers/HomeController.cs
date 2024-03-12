using WowBravoFunkyRap.Models;
using WowBravoFunkyRap.Shared.Services.Interface;
using WowBravoFunkyRap.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace WowBravoFunkyRap.Areas.Manage.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(WowBravoFunkyRapDbContext dbContext, 
            IWebHostEnvironment webHostEnvironment, 
            ILogger<HomeController> iLogger, 
            IClaimService claimService) : 
            base(dbContext, webHostEnvironment, iLogger, claimService)
        {
            _logger = iLogger;
        }

        public IActionResult Index()
        {
            return View(new Dashboard());
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}