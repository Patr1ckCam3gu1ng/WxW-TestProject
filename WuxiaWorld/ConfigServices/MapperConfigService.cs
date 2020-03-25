namespace WuxiaWorld.ConfigServices {

    using System;
    using System.Linq;

    using AutoMapper;

    using DAL.Entities;
    using DAL.Models;

    using Microsoft.Extensions.DependencyInjection;

    public class MapperProfile : Profile {

        public MapperProfile() {

            CreateMap<NovelModel, Novels>();

            CreateMap<GenreModel, Genres>();
            CreateMap<Genres, GenreModel>();

            CreateMap<Genres, IdNameModel>();

            CreateMap<ChapterModel, Chapters>()
                .ForMember(c => c.ChapterNumber,
                    f => f.MapFrom(c => c.Number));

            CreateMap<Chapters, ChapterModel>().ForMember(c => c.Number,
                f => f.MapFrom(c => c.ChapterNumber));

            CreateMap<IdNameModel, object>();

            CreateMap<Novels, NovelResult>()
                .ForMember(c => c.Genres,
                    f => f.MapFrom(c => c.NovelGenres.ToList()))
                .ForMember(c => c.Chapters,
                    f => f.MapFrom(c => c.Chapters.ToList()));

            CreateMap<NovelGenres, NovelGenreResult>()
                .ForMember(c => c.Id,
                    f => f.MapFrom(c => c.Genres.Id))
                .ForMember(c => c.Name,
                    f => f.MapFrom(c => c.Genres.Name));

            CreateMap<Chapters, NovelChaptersResult>();
        }
    }

    public static class MapperConfigService {
        public static IServiceCollection RegisterMapper(this IServiceCollection services) {

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            return services;
        }
    }

}