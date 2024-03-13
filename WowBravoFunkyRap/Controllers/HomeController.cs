using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using WowBravoFunkyRap.Model;
using WowBravoFunkyRap.Model.Enums;
using WowBravoFunkyRap.Model.Tables;
using WowBravoFunkyRap.Models;
using WowBravoFunkyRap.ViewModels.Home;

namespace WowBravoFunkyRap.Controllers
{
    public class HomeController : Controller
    {
        private readonly WowBravoFunkyRapDbContext _db;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, WowBravoFunkyRapDbContext db)
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

        public async Task<IActionResult> Index()
        {
            var vm = new HomePage();

            DateTime dtNow = DateTime.Now;

            var publicityImages = await _db.PublicityImages.Where(c => c.StartTime <= dtNow && c.EndTime >= dtNow).ToListAsync();
            foreach (var publicityImage in publicityImages.Where(c => c.PublicityImageType == PublicityImageType.Banner).OrderBy(c => c.DisplaySeq))
            {
                var publicityImageVm = new PublicityImageVm();
                vm.BannerImages.Add(publicityImageVm);
                publicityImageVm.ImageLink = publicityImage.ImageLink;
                publicityImageVm.ImageUrl = publicityImage.ImageFullUrl;
                publicityImageVm.ImageUrlSm = publicityImage.ImageFullUrlSm;
                publicityImageVm.ImageUrlXs = publicityImage.ImageFullUrlXs;
                publicityImageVm.ImageTitle = publicityImage.ImageTitle;
                publicityImageVm.ImageAlt = publicityImage.ImageAlt;
            }
            foreach (var publicityImage in publicityImages.Where(c => c.PublicityImageType == PublicityImageType.News).OrderBy(c => c.DisplaySeq))
            {
                var publicityImageVm = new PublicityImageVm();
                vm.NewsImages.Add(publicityImageVm);
                publicityImageVm.ImageLink = publicityImage.ImageLink;
                publicityImageVm.ImageUrl = publicityImage.ImageFullUrl;
                publicityImageVm.ImageUrlSm = publicityImage.ImageFullUrlSm;
                publicityImageVm.ImageUrlXs = publicityImage.ImageFullUrlXs;
                publicityImageVm.ImageTitle = publicityImage.ImageTitle;
                publicityImageVm.ImageAlt = publicityImage.ImageAlt;
            }


            return View(vm);
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