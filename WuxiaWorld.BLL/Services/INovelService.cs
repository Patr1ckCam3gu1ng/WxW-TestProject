namespace WuxiaWorld.BLL.Services {

    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    public interface INovelService {

        Task<Novels> Create(NovelModel input);
    }

}