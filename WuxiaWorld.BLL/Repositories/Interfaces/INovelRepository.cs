namespace WuxiaWorld.BLL.Repositories.Interfaces {

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    public interface INovelRepository {

        Task<Novels> Create(NovelModel input);


        Task<List<Novels>> GetAll();


        Task<Novels> GetById(int novelId);
    }

}