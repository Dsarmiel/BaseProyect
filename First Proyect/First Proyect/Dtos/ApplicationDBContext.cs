using First_Proyect.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace First_Proyect.Data
{
    public class ApplicationDBContext : IdentityDbContext<AppUser>
    {
        public ApplicationDBContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        {
        }
        public DbSet<Stock> Stocks { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Portafolio> Portafolios { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Stock>().HasMany(x => x.Comments).WithOne(x => x.Stock).HasForeignKey(x => x.StockId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Portafolio>(b => b.HasKey(p => new { p.AppUserId, p.StockId }));
            builder.Entity<Portafolio>().HasOne(u => u.AppUser).WithMany(u => u.Portafolios).HasForeignKey(p => p.AppUserId);
            builder.Entity<Portafolio>().HasOne(u => u.Stock).WithMany(u => u.Portafolios).HasForeignKey(p => p.StockId);

            List<IdentityRole> roles = new List<IdentityRole> 
            {
                new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole
                {
                   Name = "User",
                   NormalizedName = "USER"
                }
            };
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
