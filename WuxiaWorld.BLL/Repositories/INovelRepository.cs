namespace WuxiaWorld.BLL.Repositories {

    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    public interface INovelRepository {

        Task<Novels> Create(NovelModel input);
    }

}