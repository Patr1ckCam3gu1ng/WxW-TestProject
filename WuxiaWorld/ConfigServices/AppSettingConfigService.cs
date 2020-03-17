namespace WuxiaWorld.ConfigServices {

    using DAL.Models;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class AppSettingConfigService {

        public static IServiceCollection RegisterAppSetting(this IServiceCollection services,
            IConfigurationRoot configurationRoot) {

            services.Configure<CancelToken>(configurationRoot.GetSection("CancelToken"));
            services.Configure<JwtToken>(configurationRoot.GetSection("Token"));

            return services;
        }
    }

}