using Microsoft.Extensions.Configuration;
using WowBravoFunkyRap.Service.DependencyInjection;
using WowBravoFunkyRap.Service.Services;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddService(this IServiceCollection services, IConfiguration config)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (config == null) throw new ArgumentNullException(nameof(config));

            services.Configure<ServiceOptions>(config);
            services.AddScoped<ImageService>();
            //services.AddScoped(typeof(IPasswordHasher<>), typeof(PasswordHasher<>));
            return services;
        }
    }
}
