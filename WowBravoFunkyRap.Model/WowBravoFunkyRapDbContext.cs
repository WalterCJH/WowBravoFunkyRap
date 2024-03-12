using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using WowBravoFunkyRap.Model.Enums;
using WowBravoFunkyRap.Model.Tables;
using WowBravoFunkyRap.Model.Tables.Interface;
using WowBravoFunkyRap.Shared.Services.Interface;

namespace WowBravoFunkyRap.Model
{
    public class WowBravoFunkyRapDbContext : DbContext
    {
        public DbSet<PublicityImage> PublicityImages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RoleItem> RoleItems { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        public WowBravoFunkyRapDbContext(DbContextOptions<WowBravoFunkyRapDbContext> options) : base(options) 
        { 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //modelBuilder.Entity<UserRole>().HasNoKey();

            //modelBuilder.Entity<ProductRate>()
            //    .HasOne(c => c.Order)
            //    .WithMany(c => c.ProductRates)
            //    .OnDelete(DeleteBehavior.Restrict);

            SeedData(modelBuilder);
        }


        private static void SeedData(ModelBuilder builder)
        {
            int seq = 1;

            var rolePublicityImageSetting = new Role() { Id = Guid.NewGuid(), Name = "宣傳圖片設定", DisplaySeq = seq++ };
            var rolePublicityImageQuery = new Role() { Id = Guid.NewGuid(), Name = "宣傳圖片查詢", DisplaySeq = seq++ };
            //var roleCitySetting = new Role() { Id = Guid.NewGuid(), Name = "鄉鎮市區設定", DisplaySeq = seq++ };
            //var roleCityQuery = new Role() { Id = Guid.NewGuid(), Name = "鄉鎮市區查詢", DisplaySeq = seq++ };
            //var roleOrderCancelReasonSetting = new Role() { Id = Guid.NewGuid(), Name = "訂單取消原因設定", DisplaySeq = seq++ };
            //var roleOrderCancelReasonQuery = new Role() { Id = Guid.NewGuid(), Name = "訂單取消原因查詢", DisplaySeq = seq++ };
            //var roleShipMethodSetting = new Role() { Id = Guid.NewGuid(), Name = "配送方式設定", DisplaySeq = seq++ };
            //var roleShipMethodQuery = new Role() { Id = Guid.NewGuid(), Name = "配送方式查詢", DisplaySeq = seq++ };
            //var roleProductCategorySetting = new Role() { Id = Guid.NewGuid(), Name = "產品類別設定", DisplaySeq = seq++ };
            //var roleProductCategoryQuery = new Role() { Id = Guid.NewGuid(), Name = "產品類別查詢", DisplaySeq = seq++ };
            //var roleProductSetting = new Role() { Id = Guid.NewGuid(), Name = "產品設定", DisplaySeq = seq++ };
            //var roleProductQuery = new Role() { Id = Guid.NewGuid(), Name = "產品查詢", DisplaySeq = seq++ };
            var roleUserSetting = new Role() { Id = Guid.NewGuid(), Name = "使用者設定", DisplaySeq = seq++ };
            var roleUserQuery = new Role() { Id = Guid.NewGuid(), Name = "使用者查詢", DisplaySeq = seq++ };
            var roleRoleSetting = new Role() { Id = Guid.NewGuid(), Name = "角色設定", DisplaySeq = seq++ };
            var roleRoleQuery = new Role() { Id = Guid.NewGuid(), Name = "角色查詢", DisplaySeq = seq++ };

            var IRoles = new List<Role> {
                rolePublicityImageSetting, rolePublicityImageQuery,
                //roleCitySetting, roleCityQuery,
                //roleOrderCancelReasonSetting, roleOrderCancelReasonQuery,
                //roleShipMethodSetting, roleShipMethodQuery,
                //roleProductCategorySetting, roleProductCategoryQuery,
                //roleProductSetting, roleProductQuery,
                roleUserSetting, roleUserQuery,
                roleRoleSetting, roleRoleQuery };
            builder.Entity<Role>().HasData(IRoles);

            var IRoleItems = new List<RoleItem>(){
                new RoleItem() { Id = eRole.PublicityImageWrite.ToString(), RoleId = rolePublicityImageSetting.Id },
                new RoleItem() { Id = eRole.PublicityImageRead.ToString(), RoleId = rolePublicityImageQuery.Id },
                //new RoleItem() { Id = eRole.CityWrite.ToString(), RoleId = roleCitySetting.Id },
                //new RoleItem() { Id = eRole.CityRead.ToString(), RoleId = roleCityQuery.Id },
                //new RoleItem() { Id = eRole.OrderCancelReasonRead.ToString(), RoleId = roleOrderCancelReasonSetting.Id },
                //new RoleItem() { Id = eRole.OrderCancelReasonWrite.ToString(), RoleId = roleOrderCancelReasonQuery.Id },
                //new RoleItem() { Id = eRole.ShipMethodRead.ToString(), RoleId = roleShipMethodSetting.Id },
                //new RoleItem() { Id = eRole.ShipMethodWrite.ToString(), RoleId = roleShipMethodQuery.Id },
                //new RoleItem() { Id = eRole.ProductCategoryRead.ToString(), RoleId = roleProductCategorySetting.Id },
                //new RoleItem() { Id = eRole.ProductCategoryWrite.ToString(), RoleId = roleProductCategoryQuery.Id },
                //new RoleItem() { Id = eRole.ProductRead.ToString(), RoleId = roleProductSetting.Id },
                //new RoleItem() { Id = eRole.ProductWrite.ToString(), RoleId = roleProductQuery.Id },
                new RoleItem() { Id = eRole.UserWrite.ToString(), RoleId = roleUserSetting.Id },
                new RoleItem() { Id = eRole.UserRead.ToString(), RoleId = roleUserQuery.Id },
                new RoleItem() { Id = eRole.RoleWrite.ToString(), RoleId = roleRoleSetting.Id },
                new RoleItem() { Id = eRole.RoleRead.ToString(), RoleId = roleRoleQuery.Id },
            };
            builder.Entity<RoleItem>().HasData(IRoleItems);

            /// password = 1234
            var IUsers = new List<User>(){
                new User() { Id = Guid.NewGuid(), Account = "walter", LastName = "Chen", FirstName = "Walter", Email = "smickey33@gmail.com", PasswordHash = "AQAAAAIAAYagAAAAELfLsvo0htr56tBtNkkj7EjJkwlKPRlUJKp/8lbTVdhtjzd9dfx7SjtBaAp9oWHbbA==", IsEnabled = true },
                new User() { Id = Guid.NewGuid(), Account = "joanne", LastName = "Wang", FirstName = "Joanne", Email = "wwjoannems@gmail.com", PasswordHash = "AQAAAAIAAYagAAAAELfLsvo0htr56tBtNkkj7EjJkwlKPRlUJKp/8lbTVdhtjzd9dfx7SjtBaAp9oWHbbA==", IsEnabled = true }
            };

            builder.Entity<User>().HasData(IUsers);
            //var IUser = new User() { Id = Guid.NewGuid(), Account = "system", LastName = "Chen", FirstName = "Walter", Email = "smickey33@gmail.com", PasswordHash = "AQAAAAIAAYagAAAAELfLsvo0htr56tBtNkkj7EjJkwlKPRlUJKp/8lbTVdhtjzd9dfx7SjtBaAp9oWHbbA==", IsEnabled = true };
            //builder.Entity<User>().HasData(IUser);

            var IUser = IUsers.FirstOrDefault();

            foreach (var IRole in IRoles)
            {
                builder.Entity<UserRole>().HasData(new UserRole() { UserId = IUser.Id, RoleId = IRole.Id });
            }
        }

        public new async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            var _claimService = this.GetService<IClaimService>();
            foreach (var entry in ChangeTracker.Entries<IUserLog>())
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreateUserId = _claimService.GetUserName();
                        entry.Entity.CreateTime = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        entry.Entity.UpdateUserId = _claimService.GetUserName();
                        entry.Entity.UpdateTime = DateTime.Now;
                        break;
                }

            return await base.SaveChangesAsync(cancellationToken);
        }

    }
}
