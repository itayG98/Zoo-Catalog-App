using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Model.DAL
{
    public class ZooIdentityContext : IdentityDbContext<IdentityUser>
    {
        public ZooIdentityContext(DbContextOptions<ZooIdentityContext> options) : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            var hasher = new PasswordHasher<IdentityUser>();
            base.OnModelCreating(builder);

            IdentityRole adminRole = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Admin",
                NormalizedName = "ADMIN"
            };
            IdentityRole userRole = new IdentityRole()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "User",
                NormalizedName = "USER"
            };
            IdentityUser admin = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "Admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@gmail.com",
                NormalizedEmail = "admin@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "1234567890",
                PhoneNumberConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "1234ABcd@")
            };
            IdentityUser user = new IdentityUser()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = "User",
                NormalizedUserName= "USER",
                Email = "user@gmail.com",
                NormalizedEmail = "user@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "1234567891",
                PhoneNumberConfirmed = true,
                PasswordHash = hasher.HashPassword(null, "1234ABcd@")
            };
            IdentityUserRole<string> adminRoleDict = new IdentityUserRole<string>
            {
                UserId = admin.Id,
                RoleId = adminRole.Id
            };
            IdentityUserRole<string> userRoleDict = new IdentityUserRole<string>
            {
                UserId = user.Id,
                RoleId = userRole.Id
            };

            builder.Entity<IdentityRole>().HasData(new List<IdentityRole>
            {
                adminRole,
                userRole
                });
            builder.Entity<IdentityUser>().HasData(new List<IdentityUser>
            {
                admin,
                user
            });
            builder.Entity<IdentityUserRole<string>>().HasData(adminRoleDict, userRoleDict);

        }
    }
}
