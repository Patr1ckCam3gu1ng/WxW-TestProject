namespace WuxiaWorld.BLL.Repositories.Interfaces {

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    public interface IChapterRepository {

        Task<Chapters> Create(int novelId, ChapterModel input);


        Task<Chapters> GetByChapterNumber(int novelId, int chapterNumber);


        Task<Chapters> Publish(int novelId, int chapterNumber);


        Task<bool> IsAlreadyPublished(int novelId, int chapterNumber);


        Task<List<ChapterModel>> GetByNovelId(int novelId);
    }

}