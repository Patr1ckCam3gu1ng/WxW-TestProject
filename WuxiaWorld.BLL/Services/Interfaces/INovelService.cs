namespace WuxiaWorld.BLL.Services.Interfaces {

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Models;

    public interface INovelService {

        Task<List<NovelResult>> Create(NovelModel[] novels);


        Task<List<NovelResult>> GetAll(int? novelId = null);


        Task<List<NovelResult>> GetGenderId(int genreId);
    }

}