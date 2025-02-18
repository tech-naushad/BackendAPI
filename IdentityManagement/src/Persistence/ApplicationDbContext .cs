using IdentityManagement.Domain.Entities;
using IdentityManagement.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace IdentityManagement.Persistence
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Apply the configuration for models
            modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());         
        }
    }
}
