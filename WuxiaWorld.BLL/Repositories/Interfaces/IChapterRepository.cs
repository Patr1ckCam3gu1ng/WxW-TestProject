﻿namespace WuxiaWorld.BLL.Repositories.Interfaces {

    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    public interface IChapterRepository {

        Task<Chapters> Create(int novelId, ChapterInput input);


        Task<Chapters> GetByChapterNumber(int novelId, int chapterNumber);


        Task<Chapters> Publish(int novelId, int chapterNumber);


        Task<bool> IsAlreadyPublished(int novelId, int chapterNumber);


        Task<ChapterNovelResult> GetByNovelId(int novelId, int chapterNumber, bool? isIncludeContent);
    }

}