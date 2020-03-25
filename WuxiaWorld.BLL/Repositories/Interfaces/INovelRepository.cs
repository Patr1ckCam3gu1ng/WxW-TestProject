namespace WuxiaWorld.BLL.Repositories.Interfaces {

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Models;

    public interface INovelRepository {

        Task<List<NovelResult>> Create(NovelModel[] novels);


        Task<List<NovelResult>> GetAll(int? novelId = null);


        Task<List<NovelResult>> GetByGenderId(int genreId);
    }

}