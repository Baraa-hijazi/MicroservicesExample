using System;
using System.IO;
using IdentityService.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace IdentityService.Persistence.Contexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<ApplicationUser> ApplicationUser { get; set; }
        
        public ApplicationDbContext() { } 

        //protected override void OnModelCreating(ModelBuilder modelBuilder) { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> Options) : base(Options)
        { 
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build(); 
        }
        
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            SeedData(builder);
        }
        
        private void SeedData(ModelBuilder builder)
        {
            var hash = new PasswordHasher<ApplicationUser>();
            var ADMIN_ID = Guid.NewGuid().ToString();
            var ROLE_ID = Guid.NewGuid().ToString();

            builder.Entity<IdentityRole>().HasData(new IdentityRole
                { Id = ROLE_ID, Name = "Admin", NormalizedName = "Admin".ToUpper() });

            builder.Entity<ApplicationUser>().HasData(new ApplicationUser
            {
                Id = ADMIN_ID,
                UserName = "admin",
                NormalizedUserName = "admin".ToUpper(),
                Email = "developer@gmail.com",
                NormalizedEmail = "developer@gmail.com".ToUpper(),
                EmailConfirmed = true,
                PhoneNumber = "+962780000000",
                PhoneNumberConfirmed = true,
                PasswordHash = hash.HashPassword(null, "P@ssw0rd"),
                SecurityStamp = String.Empty,
            });

            builder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
            {
                RoleId = ROLE_ID,
                UserId = ADMIN_ID
            });
        }
    }
}