using MeSoftCase.Application.Interfaces;
using MeSoftCase.Infrastructure.Interfaces;
using MeSoftCase.Infrastructure.Models;
using MeSoftCase.Infrastructure.Persistance.Repositories.Abstract;
using MeSoftCase.Infrastructure.Persistance.Repositories.Concrete;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeSoftCase.Infrastructure.Services
{
    public static class ServicesRegistration
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IAppUserRepository, AppUserRepository>();
            services.AddScoped<IBlockedIpService, BlockedIpService>();
            services.AddScoped<IBlockedIpRepository, BlockedIpRepository>();
            services.AddScoped<ITokenService, TokenService>();

            services.Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)));
        }
    }
}
