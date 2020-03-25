namespace WuxiaWorld.BLL.Services.Interfaces {

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Models;

    public interface IGenreService {

        Task<List<IdNameModel>> GetAll();


        // Task<Genres> GetByName(string genreName);


        Task<List<GenreModel>> Create(GenreModel[] genres);
    }

}