namespace WuxiaWorld.BLL.Services.Interfaces {

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    public interface INovelService {

        Task<Novels> Create(NovelModel input);


        Task<List<Novels>> GetAll();


        Task<Novels> GetById(int novelId);
    }

}