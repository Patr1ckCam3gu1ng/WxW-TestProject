namespace WuxiaWorld.ConfigServices {

    using System;

    using DAL.Entities;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public static class DbContextConfigService {

        public static IServiceCollection RegisterRdsDatabaseContext(
            this IServiceCollection services,
            IConfigurationRoot json) {

            services.AddDbContext<WuxiaWorldDbContext>(option => option.UseSqlServer(
                json["ConnectionStrings:Local"],
                options => options.CommandTimeout((int) TimeSpan.FromMinutes(3).TotalSeconds)
            ));

            return services;
        }
    }

}