namespace WuxiaWorld.ConfigServices {

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class JsonFilesConfigService {

        public static IServiceCollection RegisterJsonFiles(this IServiceCollection services,
            IConfigurationRoot configurationRoot) {

            // services.Configure<Api>(configurationRoot.GetSection("Api"));

            return services;
        }
    }

}