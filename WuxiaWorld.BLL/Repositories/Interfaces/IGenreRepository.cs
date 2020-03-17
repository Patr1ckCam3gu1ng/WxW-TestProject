namespace WuxiaWorld.BLL.Repositories.Interfaces {

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    public interface IGenreRepository {

        Task<List<Genres>> GetAll();


        Task<Genres> GetByName(string genreName);


        Task<Genres> Create(GenreModel genre);


        Task<List<Genres>> GetById(List<int> genreIds);
    }

}