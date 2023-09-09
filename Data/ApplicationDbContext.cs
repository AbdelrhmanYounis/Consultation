using Consultation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Consultation.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPrice> ProductPrice { get; set; }

        public DbSet<ProductConfirmation> ProductsConfirmation { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            this.SeedProductPrices(builder);

            builder.Entity<ApplicationUser>().ToTable("Users", "security");
            builder.Entity<IdentityRole>().ToTable("Roles", "security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "security");
        }
        public void SeedProductPrices(ModelBuilder builder)
        {
            var ProductPrices = new List<ProductPrice>
             {
                 new ProductPrice {
                Id=1,
                Title="Weekly",
                Price=5
                },
                new ProductPrice {
                Id=2,
                Title="Monthly",
                Price=15
                },
                new ProductPrice {
                Id=3,
                Title="Yearly",
                Price=150
                }
               
                };
            builder.Entity<ProductPrice>().HasData(ProductPrices);
        }
    }
}