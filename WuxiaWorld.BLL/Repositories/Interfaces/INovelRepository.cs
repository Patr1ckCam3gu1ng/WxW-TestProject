namespace WuxiaWorld.BLL.Repositories.Interfaces {

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    public interface INovelRepository {

        Task<NovelResult> Create(NovelModel input);


        Task<List<NovelResult>> GetAll();


        Task<NovelResult> GetById(int novelId);
    }

}