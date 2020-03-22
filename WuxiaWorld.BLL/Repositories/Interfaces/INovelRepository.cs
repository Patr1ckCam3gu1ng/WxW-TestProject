namespace WuxiaWorld.BLL.Repositories.Interfaces {

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Models;

    public interface INovelRepository {

        Task<NovelResult> Create(NovelModel input);


        Task<List<NovelResult>> GetAll(int? novelId = null);
    }

}