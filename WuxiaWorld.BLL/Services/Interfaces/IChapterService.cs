namespace WuxiaWorld.BLL.Services.Interfaces {

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    public interface IChapterService {

        Task<ChapterModel> Create(int novelId, ChapterModel input);


        Task<Chapters> Publish(int novelId, int chapterNumber);


        Task<List<ChapterModel>> GetByNovelId(int novelId);
    }

}