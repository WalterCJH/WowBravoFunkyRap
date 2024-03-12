using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WowBravoFunkyRap.Shared.Services.Interface;
using WowBravoFunkyRap.Extension;
using WowBravoFunkyRap.Areas.Manage.ViewModels.PublicityImages;
using WowBravoFunkyRap.Model;
using WowBravoFunkyRap.Model.Const;
using WowBravoFunkyRap.Model.Enums;
using WowBravoFunkyRap.Model.Tables;
using X.PagedList;
using Nelibur.ObjectMapper;
using WowBravoFunkyRap.Service.Services;
using WowBravoFunkyRap.Service.Models;

namespace WowBravoFunkyRap.Areas.Manage.Controllers
{
    [AuthorizeRoles(eRole.PublicityImageRead, eRole.PublicityImageWrite)]
    public class PublicityImagesController : BaseController
    {
        private ImageService _imageService;

        public PublicityImagesController(WowBravoFunkyRapDbContext dbContext,
            IWebHostEnvironment webHostEnvironment,
            ILogger<PublicityImagesController> iLogger,
            IClaimService claimService,
            ImageService imageService) :
            base(dbContext, webHostEnvironment, iLogger, claimService)
        {
            _imageService = imageService;
        }

        public async Task<IActionResult> Index(PublicityImageFilter filter)
        {
            var query = db.PublicityImages.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                query = query.Where(p => p.ImageTitle.Contains(filter.Keyword));
            }

            if (filter.PublicityImageType != null)
            {
                query = query.Where(p => p.PublicityImageType == filter.PublicityImageType);
            }

            query = query.OrderBy($"{filter.SortBy} {filter.SortDirection}");

            filter.Results = await query.ToPagedListAsync(filter.PageNo, filter.PageSize);

            return View(filter);
        }

        [AuthorizeRoles(eRole.PublicityImageRead)]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || db.PublicityImages == null)
            {
                return NotFound();
            }

            var entity = await db.PublicityImages.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            var vm = TinyMapper.Map<PublicityImageCommon>(entity);
            return View(vm);
        }

        [AuthorizeRoles(eRole.PublicityImageWrite)]
        public IActionResult Create()
        {
            var vm = new PublicityImageCommon();
            int? seq = db.PublicityImages.Max(c => (int?)c.DisplaySeq);
            vm.DisplaySeq = seq == null ? 10 : seq.Value + 10;
            vm.StartTime = DateTime.Now;
            vm.EndTime = DateTime.MaxValue;

            return View(vm);
        }

        [AuthorizeRoles(eRole.PublicityImageWrite)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(PublicityImageCommon vm)
        {
            if (ModelState.IsValid)
            {
                var entity = TinyMapper.Map<PublicityImage>(vm);
                entity.Id = Guid.NewGuid();

                await UploadImageAsync(vm, entity);

                db.Add(entity);
                await db.SaveChangesAsync();
                TempData[SessionStr.SuccessMessage] = "新增成功";
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        [AuthorizeRoles(eRole.PublicityImageWrite)]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || db.PublicityImages == null)
            {
                return NotFound();
            }

            var entity = await db.PublicityImages.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            var vm = TinyMapper.Map<PublicityImageCommon>(entity);
            return View(vm);
        }

        [AuthorizeRoles(eRole.PublicityImageWrite)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(PublicityImageCommon vm)
        {
            if (ModelState.IsValid)
            {
                var entity = await db.PublicityImages.FindAsync(vm.Id);
                entity = TinyMapper.Map(vm, entity);

                await UploadImageAsync(vm, entity);

                await db.SaveChangesAsync();
                TempData[SessionStr.SuccessMessage] = "修改成功";
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        [AuthorizeRoles(eRole.PublicityImageWrite)]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || db.PublicityImages == null)
            {
                return NotFound();
            }

            var entity = await db.PublicityImages.FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            var vm = TinyMapper.Map<PublicityImageCommon>(entity);
            return View(vm);
        }

        [AuthorizeRoles(eRole.PublicityImageWrite)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var entity = await db.PublicityImages.FindAsync(id);
            if (entity != null)
            {
                db.PublicityImages.Remove(entity);
                await db.SaveChangesAsync();
            }

            TempData[SessionStr.SuccessMessage] = "刪除成功";

            return RedirectToAction(nameof(Index));
        }

        public async Task UploadImageAsync(PublicityImageCommon vm, PublicityImage entity)
        {
            if (vm.UploadImage == null) return;

            var saveImageDto = new SaveImageDto();
            saveImageDto.File = vm.UploadImage;
            saveImageDto.DirectoryNameList = new List<string>() { FileStr.PublicityImages };
            saveImageDto.NewFileName = entity.Id.ToString();
            saveImageDto.NewWidth = ImageSize.BannerWidth;
            saveImageDto.NewWidthSm = ImageSize.BannerWidth_sm;
            saveImageDto.NewWidthXs = ImageSize.BannerWidth_xs;
            var result = await _imageService.SaveMultipleImageFileAsync(saveImageDto);
            entity.ImageUrl = result.ImageUrl;
            entity.ImageName = result.ImageName;
            entity.ImageNameSm = result.ImageNameSm;
            entity.ImageNameXs = result.ImageNameXs;
        }
    }
}
