using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using NoteTakingService.Core.Entities;

namespace NoteTakingService.Persistence.Contexts
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Notes> Notes { get; set; }
        
        public ApplicationDbContext() { } 
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> Options) : base(Options)
        { 
            var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build(); 
        }
        
        public class Fix : IDesignTimeDbContextFactory<ApplicationDbContext>
        {
            public ApplicationDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
                optionsBuilder.UseSqlServer("Server=localhost;Database=MicroServiceDatabase;Trusted_Connection=True;");
                return new ApplicationDbContext(optionsBuilder.Options);
            }
        }
    }
}