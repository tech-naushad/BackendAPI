using IdentityManagement.Persistence;
using IdentityManagement.Persistence.Repositories;
using IdentityManagement.Persistence.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IdentityManagement.Application
{
    public static class ServicesExtension
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<ApplicationDbContext>();
            services.AddScoped<ApplicationDbContextIntializer>();
            return services;
        }
    }
}
