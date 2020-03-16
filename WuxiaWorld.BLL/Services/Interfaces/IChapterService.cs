namespace WuxiaWorld.BLL.Services.Interfaces {

    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    public interface IChapterService {

        Task<Chapters> Create(int novelId, ChapterModel input);
    }

}