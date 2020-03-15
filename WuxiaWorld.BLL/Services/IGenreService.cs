﻿namespace WuxiaWorld.BLL.Services {

    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    public interface IGenreService {

        Task<List<Genres>> GetAll();


        Task<Genres> GetByName(string genreName);


        Task<Genres> Create(GenreModel genre);
    }

}