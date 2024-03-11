using Microsoft.EntityFrameworkCore;
using WowBravoFunkyRap.Model.Tables;

namespace WowBravoFunkyRap.Model
{
    public class WowBravoFunkyRapContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public WowBravoFunkyRapContext(DbContextOptions<WowBravoFunkyRapContext> options) : base(options) 
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

            //SeedData(modelBuilder);
        }

    }
}
