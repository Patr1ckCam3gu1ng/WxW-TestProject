namespace WuxiaWorld.BLL.Repositories {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    using Microsoft.EntityFrameworkCore;

    public class GenreRepository : IGenreRepository {

        private readonly WuxiaWorldDbContext _dbContext;

        public GenreRepository(WuxiaWorldDbContext dbContext) {

            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(WuxiaWorldDbContext));
        }

        public async Task<List<Genres>> GetAll() {

            using (_dbContext) {

                await _dbContext.Database.OpenConnectionAsync();

                var genres = await (
                    from genre in _dbContext.Genres
                    select genre).ToListAsync();

                return genres;
            }
        }

        public async Task<Genres> GetByName(string genreName) {

            using (_dbContext) {

                await _dbContext.Database.OpenConnectionAsync();

                var result = await (
                    from genre in _dbContext.Genres
                    where genre.GenreName.ToLower().Equals(genreName.ToLower())
                    select genre).FirstOrDefaultAsync();

                return result;
            }
        }

        public async Task<Genres> Create(GenreModel genre) {

            using (_dbContext) {

                await _dbContext.Database.OpenConnectionAsync();

                var newGenre = new Genres {
                    GenreName = genre.Name
                };

                await _dbContext.AddAsync(newGenre);

                var result = await _dbContext.SaveChangesAsync();

                return result == 1 ? newGenre : null;

            }
        }
    }

}