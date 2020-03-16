namespace WuxiaWorld.BLL.Repositories.Interfaces {

    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    public interface IChapterRepository {

        Task<Chapters> Create(int novelId, ChapterModel input);


        Task<Chapters> GetByChapterNumber(int novelId, int chapterName);
    }

}