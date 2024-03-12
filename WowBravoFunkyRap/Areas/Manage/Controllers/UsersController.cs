using System.Linq.Dynamic.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WowBravoFunkyRap.Shared.Services.Interface;
using WowBravoFunkyRap.Extension;
using WowBravoFunkyRap.Areas.Manage.ViewModels.Users;
using WowBravoFunkyRap.Helper;
using WowBravoFunkyRap.Model;
using WowBravoFunkyRap.Model.Const;
using WowBravoFunkyRap.Model.Enums;
using WowBravoFunkyRap.Model.Tables;
using X.PagedList;

namespace WowBravoFunkyRap.Areas.Manage.Controllers
{
    [AuthorizeRoles(eRole.UserRead, eRole.UserWrite)]
    public class UsersController : BaseController
    {
        private IPasswordHasher<User> _passwordHasher;

        public UsersController(WowBravoFunkyRapDbContext dbContext, 
            IWebHostEnvironment webHostEnvironment, 
            ILogger<UsersController> iLogger, 
            IPasswordHasher<User> passwordHasher, 
            IClaimService claimService) : 
            base(dbContext, webHostEnvironment, iLogger, claimService)
        {
            _passwordHasher = passwordHasher;
        }

        public async Task<IActionResult> Index(UserFilter filter)
        {
            var query = db.Users.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter.Keyword))
            {
                query = query.Where(p => (p.LastName + p.FirstName).Contains(filter.Keyword) || p.Account.Contains(filter.Keyword) || p.Email.Contains(filter.Keyword));
            }

            query = query.OrderBy($"{filter.SortBy} {filter.SortDirection}");

            filter.Results = await query.ToPagedListAsync(filter.PageNo, filter.PageSize);

            return View(filter);
        }

        [AuthorizeRoles(eRole.UserRead)]
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || db.Users == null)
            {
                return NotFound();
            }

            var entity = await db.Users.Include(c => c.UserRoles).FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            if (AuthorizeHelper.CheckUserChange(entity.Email, _claimService.GetUserIsAdmin()) == false)
            {
                TempData[SessionStr.ErrorMessage] = "操作異常";
                return RedirectToAction(nameof(Index));
            }

            var vm = await GetUserCommon(entity);
            await QueryRoles(vm);

            return View(vm);
        }

        [AuthorizeRoles(eRole.UserWrite)]
        public async Task<IActionResult> Create()
        {
            var vm = new UserCommon();
            int? seq = db.Users.Max(c => (int?)c.DisplaySeq);
            vm.DisplaySeq = seq == null ? 10 : seq.Value + 10;
            vm.IsEnabled = true;

            await QueryRoles(vm);

            return View(vm);
        }

        [AuthorizeRoles(eRole.UserWrite)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(UserCommon vm)
        {
            if (AuthorizeHelper.CheckUserChange(vm.Email, _claimService.GetUserIsAdmin()) == false)
            {
                return RedirectToAction(nameof(Index));
            }
            if (ModelState.IsValid)
            {
                vm.Id = Guid.NewGuid();
                vm.PasswordHash = _passwordHasher.HashPassword(vm, vm.Password);
                db.Add(vm);

                await AddUserRoles(vm);

                await db.SaveChangesAsync();
                TempData[SessionStr.SuccessMessage] = "新增成功";
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        [AuthorizeRoles(eRole.UserWrite)]
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || db.Users == null)
            {
                return NotFound();
            }

            var entity = await db.Users.Include(c => c.UserRoles).FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            if (AuthorizeHelper.CheckUserChange(entity.Email, _claimService.GetUserIsAdmin()) == false)
            {
                TempData[SessionStr.ErrorMessage] = "操作異常";
                return RedirectToAction(nameof(Index));
            }

            var vm = await GetUserCommon(entity);
            await QueryRoles(vm);

            return View(vm);
        }

        [AuthorizeRoles(eRole.UserWrite)]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UserCommon vm)
        {
            if (AuthorizeHelper.CheckUserChange(vm.Email, _claimService.GetUserIsAdmin()) == false)
            {
                TempData[SessionStr.ErrorMessage] = "操作異常";
                return RedirectToAction(nameof(Index));
            }
            if (ModelState.IsValid)
            {
                var entity = await db.Users.FindAsync(vm.Id);
                entity.Account = vm.Account;
                entity.Email = vm.Email;
                entity.LastName = vm.LastName;
                entity.FirstName = vm.FirstName;
                if (!string.IsNullOrWhiteSpace(vm.PasswordHash)) entity.PasswordHash = _passwordHasher.HashPassword(vm, vm.Password);
                entity.IsEnabled = vm.IsEnabled;
                entity.DisplaySeq = vm.DisplaySeq;
                db.Entry(entity).State = EntityState.Modified;

                var dbUserRoles = await db.UserRoles.Where(c => c.UserId == vm.Id).ToListAsync();
                db.RemoveRange(dbUserRoles);
                await AddUserRoles(vm);

                await db.SaveChangesAsync();
                TempData[SessionStr.SuccessMessage] = "修改成功";
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        [AuthorizeRoles(eRole.UserWrite)]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || db.Users == null)
            {
                return NotFound();
            }

            var entity = await db.Users.Include(c => c.UserRoles).FirstOrDefaultAsync(m => m.Id == id);
            if (entity == null)
            {
                return NotFound();
            }

            if (AuthorizeHelper.CheckUserChange(entity.Email, _claimService.GetUserIsAdmin()) == false)
            {
                TempData[SessionStr.ErrorMessage] = "操作異常";
                return RedirectToAction(nameof(Index));
            }

            var vm = await GetUserCommon(entity);
            await QueryRoles(vm);

            return View(vm);
        }

        [AuthorizeRoles(eRole.UserWrite)]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var entity = await db.Users.FindAsync(id);

            if (AuthorizeHelper.CheckUserChange(entity.Email, _claimService.GetUserIsAdmin()) == false)
            {
                TempData[SessionStr.ErrorMessage] = "操作異常";
                return RedirectToAction(nameof(Index));
            }

            if (entity != null)
            {
                db.Users.Remove(entity);
                await db.SaveChangesAsync();
            }

            TempData[SessionStr.SuccessMessage] = "刪除成功";

            return RedirectToAction(nameof(Index));
        }

        public async Task<UserCommon> GetUserCommon(User entity)
        {
            var vm = new UserCommon();
            vm.Id = entity.Id;
            vm.IsEnabled = entity.IsEnabled;
            vm.Account = entity.Account;
            vm.LastName = entity.LastName;
            vm.FirstName = entity.FirstName;
            vm.Email = entity.Email;
            vm.DisplaySeq = entity.DisplaySeq;
            vm.CreateTime = entity.CreateTime;
            vm.CreateUserId = entity.CreateUserId;
            vm.UpdateTime = entity.UpdateTime;
            vm.UpdateUserId = entity.UpdateUserId;
            vm.UserRoles = entity.UserRoles;
            return vm;
        }

        public async Task QueryRoles(UserCommon userCommon)
        {
            var roles = await db.Roles.AsNoTracking().ToListAsync();
            foreach (var role in roles)
            {
                var userRoleItem = new UserRoleItem();
                userRoleItem.RoleId = role.Id;
                userRoleItem.RoleName = role.Name;
                userRoleItem.DisplaySeq = role.DisplaySeq;
                userCommon.UserRoleItems.Add(userRoleItem);

                var userRole = userCommon.UserRoles.FirstOrDefault(c => c.RoleId == role.Id);
                if (userRole != null)
                {
                    userRoleItem.Selected = true;
                }
            }
            userCommon.UserRoleItems = userCommon.UserRoleItems.OrderBy(c => c.DisplaySeq).ToList();
        }

        public async Task AddUserRoles(UserCommon userCommon)
        {
            var roleItems = userCommon.UserRoleItems.Where(c => c.Selected).ToList();
            foreach (var roleItem in roleItems)
            {
                var userRole = new UserRole();
                userRole.RoleId = roleItem.RoleId;
                userRole.UserId = userCommon.Id;
                await db.AddAsync(userRole);
            }
        }

    }
}
