namespace WuxiaWorld.ConfigServices {

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class MvcConfigService {

        public static IServiceCollection RegisterMvc(this IServiceCollection services) {

            services.AddControllers()
                .AddNewtonsoftJson(SerializerSettings);

            return services;
        }

        private static void SerializerSettings(MvcNewtonsoftJsonOptions options) {

            options.SerializerSettings.Formatting = Formatting.Indented;
            options.SerializerSettings.ContractResolver = new DefaultContractResolver();

            //This will stop the Circular Reference.
            options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }
    }

}