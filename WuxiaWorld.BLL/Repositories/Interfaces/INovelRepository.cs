namespace WuxiaWorld.BLL.Repositories.Interfaces {

    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    public interface INovelRepository {

        Task<Novels> Create(NovelModel input);
    }

}