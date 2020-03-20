namespace WuxiaWorld.ConfigServices {

    using System;

    using AutoMapper;

    using DAL.Entities;
    using DAL.Models;

    using Microsoft.Extensions.DependencyInjection;

    public class MapperProfile : Profile {
        public MapperProfile() {

            CreateMap<NovelModel, Novels>();

            CreateMap<GenreModel, Genres>();

            CreateMap<ChapterModel, Chapters>();
            CreateMap<Chapters, ChapterModel>();

            CreateMap<Novels, NovelResult>()
                .ForMember(c => c.Genres,
                    f => f.MapFrom(c => c.NovelGenres));
            CreateMap<NovelGenres, NovelGenreResult>();
        }
    }

    public static class MapperConfigService {
        public static IServiceCollection RegisterMapper(this IServiceCollection services) {

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }

}