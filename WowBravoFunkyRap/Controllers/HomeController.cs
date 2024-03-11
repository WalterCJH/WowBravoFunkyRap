using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WowBravoFunkyRap.Model;
using WowBravoFunkyRap.Model.Tables;
using WowBravoFunkyRap.Models;

namespace WowBravoFunkyRap.Controllers
{
    public class HomeController : Controller
    {
        private readonly WowBravoFunkyRapContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, WowBravoFunkyRapContext db)
        {
            _logger = logger;
            _db = db;
        }

        //public async Task<IActionResult> A()
        //{
        //    var user = new User();
        //    user.Id = Guid.NewGuid();
        //    user.Account = "test";
        //    _db.Add(user);
        //    await _db.SaveChangesAsync();

        //    return RedirectToAction("Index");
        //}

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("/Exhibitions")]
        public IActionResult Exhibitions()
        {
            return View();
        }

        //public IActionResult Index2()
        //{
        //    return View();
        //}

        public IActionResult Privacy()
        {
            return View();
        }

        [HttpGet("/WOW_BRAVO_&_FUNKY_RAP.html")]
        public IActionResult OldWebsite()
        {
            return RedirectToActionPermanent("Index", "Home");
        }

        [HttpGet("/WOW_BRAVO_%26_FUNKY_RAP.html")]
        public IActionResult OldWebsite2()
        {
            return RedirectToActionPermanent("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}