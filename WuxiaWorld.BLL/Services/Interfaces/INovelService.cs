namespace WuxiaWorld.BLL.Services.Interfaces {

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    public interface INovelService {

        Task<NovelResult> Create(NovelModel input);


        Task<List<NovelResult>> GetAll();


        Task<NovelResult> GetById(int novelId);
    }

}