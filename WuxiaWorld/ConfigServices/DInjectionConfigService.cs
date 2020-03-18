namespace WuxiaWorld.ConfigServices {

    using BLL.Repositories;
    using BLL.Repositories.Interfaces;
    using BLL.Services;
    using BLL.Services.Interfaces;

    using Microsoft.Extensions.DependencyInjection;

    public static class DInjectionConfigService {

        public static IServiceCollection RegisterDInjection(this IServiceCollection services) {

            #region Genre

            services.AddScoped<IGenreService, GenreService>();
            services.AddScoped<IGenreRepository, GenreRepository>();

            #endregion

            #region Novel

            services.AddScoped<INovelService, NovelService>();
            services.AddScoped<INovelRepository, NovelRepository>();

            #endregion

            #region Chapter

            services.AddScoped<IChapterService, ChapterService>();
            services.AddScoped<IChapterRepository, ChapterRepository>();

            #endregion

            #region Novel Genre

            services.AddScoped<INovelGenreService, NovelGenreService>();
            services.AddScoped<INovelGenreRepository, NovelGenreRepository>();

            #endregion

            services.AddScoped<IAuthenticate, Authenticate>();

            services.AddScoped<ICacheRepository, CacheRepository>();

            services.AddScoped<ICommandService, CommandService>();

            return services;
        }
    }

}