using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WowBravoFunkyRap.Shared.Services.Interface;
using WowBravoFunkyRap.Extension;
using WowBravoFunkyRap.Areas.Manage.ViewModels.Roles;
using WowBravoFunkyRap.Model;
using WowBravoFunkyRap.Model.Const;
using WowBravoFunkyRap.Model.Enums;
using WowBravoFunkyRap.Model.Tables;
using X.PagedList;

namespace WowBravoFunkyRap.Areas.Manage.Controllers
{
    [AuthorizeRoles(eRole.RoleRead, eRole.RoleWrite)]
    public class RolesController : BaseController
    {
        public RolesController(WowBravoFunkyRapDbContext dbContext, 
            IWebHostEnvironment webHostEnvironment, 
            ILogger<RolesController> iLogger, 
            IClaimService claimService) : 
            base(dbContext, webHostEnvironment, iLogger, claimService)
        {
        }

        public async Task<IActionResult> Index(RoleFilter filter)
        {
            var query = db.Roles.Include(c => c.RoleItems).AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                query = query.Where(p => p.Name.Contains(filter.Keyword));
            }

            query = query.OrderBy($"{filter.SortBy} {filter.SortDirection}");

            filter.Results = await query.ToPagedListAsync(filter.PageNo, filter.PageSize);

            return View(filter);
        }

        [AuthorizeRoles(eRole.RoleRead)]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || db.Roles == null)
            {
                return NotFound();
            }

            var entity = await db.Roles.Include(c => c.RoleItems).FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            GetRoleItems(entity);

            return View(entity);
        }

        [AuthorizeRoles(eRole.RoleWrite)]
        public IActionResult Create()
        {
            var vm = new Role();
            int? seq = db.Roles.Max(c => (int?)c.DisplaySeq);
            vm.DisplaySeq = seq == null ? 10 : seq.Value + 10;

            GetRoleItems(vm);

            return View(vm);
        }

        [AuthorizeRoles(eRole.RoleWrite)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Role vm)
        {
            if (ModelState.IsValid)
            {
                vm.Id = Guid.NewGuid();

                await AddRoleItems(vm);

                vm.RoleItems = null;
                db.Add(vm);
                await db.SaveChangesAsync();
                TempData[SessionStr.SuccessMessage] = "新增成功";
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        [AuthorizeRoles(eRole.RoleWrite)]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || db.Roles == null)
            {
                return NotFound();
            }

            var entity = await db.Roles.Include(c => c.RoleItems).FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            GetRoleItems(entity);

            return View(entity);
        }

        [AuthorizeRoles(eRole.RoleWrite)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Role vm)
        {
            if (ModelState.IsValid)
            {
                var entity = await db.Roles.FindAsync(vm.Id);
                entity.Name = vm.Name;
                entity.DisplaySeq = vm.DisplaySeq;
                db.Entry(entity).State = EntityState.Modified;

                var dbRoleItems = await db.RoleItems.Where(c => c.RoleId == vm.Id).ToListAsync();
                db.RemoveRange(dbRoleItems);

                await AddRoleItems(vm);

                await db.SaveChangesAsync();
                TempData[SessionStr.SuccessMessage] = "修改成功";
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        [AuthorizeRoles(eRole.RoleWrite)]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || db.Roles == null)
            {
                return NotFound();
            }

            var entity = await db.Roles.Include(c => c.RoleItems).FirstOrDefaultAsync(c => c.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            GetRoleItems(entity);

            return View(entity);
        }

        [AuthorizeRoles(eRole.RoleWrite)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var entity = await db.Roles.FindAsync(id);
            if (entity != null)
            {
                db.Roles.Remove(entity);
                await db.SaveChangesAsync();
            }

            TempData[SessionStr.SuccessMessage] = "刪除成功";

            return RedirectToAction(nameof(Index));
        }

        public void GetRoleItems(Role role)
        {
            foreach (eRole eRole in Enum.GetValues(typeof(eRole)))
            {
                var roleItem = role.RoleItems.FirstOrDefault(c => c.Id.ToUpper() == eRole.ToString().ToUpper());
                if (roleItem == null)
                {
                    roleItem = new RoleItem();
                    roleItem.Id = eRole.ToString();
                    role.RoleItems.Add(roleItem);
                }
                else
                {
                    roleItem.Selected = true;
                }
            }
            role.RoleItems = role.RoleItems.OrderBy(c => c.Id).ToList();
        }

        public async Task AddRoleItems(Role role)
        {
            var roleItems = role.RoleItems.Where(c => c.Selected).ToList();
            foreach (var roleItem in roleItems)
            {
                roleItem.RoleId = role.Id;
            }
            await db.AddRangeAsync(roleItems);
        }
    }
}
