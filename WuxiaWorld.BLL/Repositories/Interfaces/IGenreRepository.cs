namespace WuxiaWorld.BLL.Repositories.Interfaces {

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    public interface IGenreRepository {

        Task<List<IdNameModel>> GetAllAsync();


        // Task<Genres> GetByName(string genreName);


        Task<List<GenreModel>> Create(GenreModel[] genres);


        Task<List<Genres>> GetByIds(List<int> inputGenreIds);
    }

}