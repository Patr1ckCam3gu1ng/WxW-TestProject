namespace WuxiaWorld.ConfigServices {

    using System;

    using AutoMapper;

    using Microsoft.Extensions.DependencyInjection;

    public class MapperProfile : Profile {

    }

    public static class MapperConfigService {
        public static IServiceCollection RegisterMapper(this IServiceCollection services) {

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }

}