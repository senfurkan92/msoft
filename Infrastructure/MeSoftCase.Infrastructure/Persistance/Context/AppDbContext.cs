using MeSoftCase.Domain.Entities;
using MeSoftCase.Infrastructure.Persistance.Mapping;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MeSoftCase.Infrastructure.Persistance.Context
{

    /// <summary>
    /// represents the database context for the application, inheriting from <see cref="IdentityDbContext"/> to provide
    /// access to user and role management functionality, along with additional custom entities and mappings
    /// </summary>
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, string>
    {
        public AppDbContext(DbContextOptions<AppDbContext> ctx) : base(ctx)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new BlockedIpMap());

            builder.Entity<AppRole>().HasData(
                new AppRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
                new AppRole { Id = "2", Name = "Manager", NormalizedName = "MANAGER" },
                new AppRole { Id = "3", Name = "Customer", NormalizedName = "CUSTOMER" }
            );

            var adminUserId = Guid.NewGuid().ToString("N");
            var hasher = new PasswordHasher<AppUser>();
            var adminUser = new AppUser
            {
                Id = adminUserId,
                UserName = "admin@mail.com",
                NormalizedUserName = "ADMIN@MAIL.COM",
                Email = "admin@mail.com",
                NormalizedEmail = "ADMIN@MAIL.COM",
                EmailConfirmed = false,
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                LockoutEnabled = true
            };

            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin2025");

            builder.Entity<AppUser>().HasData(adminUser);

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                UserId = adminUserId,
                RoleId = "1"
            });

            base.OnModelCreating(builder);
        }
    }
}
