using MeSoftCase.Application.Models;
using Microsoft.Extensions.DependencyInjection;

namespace MeSoftCase.Application.Services
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ApiContext>();

            services.AddMediatR(config =>
            {
                config.RegisterServicesFromAssembly(typeof(ServiceRegistration).Assembly);
            });
        }
    }
}
