namespace WuxiaWorld.ConfigServices {

    using BLL.Repositories;
    using BLL.Services;

    using Microsoft.Extensions.DependencyInjection;

    public static class DInjectionConfigService {

        public static IServiceCollection RegisterDInjection(this IServiceCollection services) {

            #region Genre

            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IGenreRepository, GenreRepository>();

            #endregion

            return services;
        }
    }

}