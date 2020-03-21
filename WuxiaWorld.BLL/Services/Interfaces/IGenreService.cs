namespace WuxiaWorld.BLL.Services.Interfaces {

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    public interface IGenreService {

        Task<List<IdNameModel>> GetAll();


        // Task<Genres> GetByName(string genreName);


        Task<Genres> Create(GenreModel genre);
    }

}