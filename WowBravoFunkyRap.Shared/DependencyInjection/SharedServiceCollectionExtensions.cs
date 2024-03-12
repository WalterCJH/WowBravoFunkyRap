using Microsoft.Extensions.Configuration;
using System;
using WowBravoFunkyRap.Shared.Services.Interface;
using WowBravoFunkyRap.Shared.Services;
using WowBravoFunkyRap.Shared;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class SharedServiceCollectionExtensions
    {
        public static IServiceCollection AddSharedService(this IServiceCollection services, IConfiguration config)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (config == null) throw new ArgumentNullException(nameof(config));

            services.Configure<SharedServiceOptions>(config);
            services.AddHttpContextAccessor();
            services.AddSingleton<IClaimService, ClaimService>();
            return services;
        }
    }
}
