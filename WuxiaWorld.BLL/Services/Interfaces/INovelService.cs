namespace WuxiaWorld.BLL.Services.Interfaces {

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Models;

    public interface INovelService {

        Task<NovelResult> Create(NovelModel input);


        Task<List<NovelResult>> GetAll(int? novelId = null);
    }

}