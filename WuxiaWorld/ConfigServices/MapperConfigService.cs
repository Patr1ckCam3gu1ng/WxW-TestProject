namespace WuxiaWorld.ConfigServices {

    using System;

    using AutoMapper;

    using DAL.Entities;
    using DAL.Models;

    using Microsoft.Extensions.DependencyInjection;

    public class MapperProfile : Profile {
        public MapperProfile() {

            CreateMap<NovelModel, Novels>();
            CreateMap<GenreModel, Genres>()
                .ForMember(c => c.GenreName,
                    f => f.MapFrom(c => c.Name));
        }
    }

    public static class MapperConfigService {
        public static IServiceCollection RegisterMapper(this IServiceCollection services) {

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }

}