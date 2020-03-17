namespace WuxiaWorld.BLL.Repositories {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using DAL.Entities;
    using DAL.Models;

    using Interfaces;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    public class GenreRepository : IGenreRepository {
        private readonly int _cancelTokenFromSeconds;

        private readonly WuxiaWorldDbContext _dbContext;
        private readonly IMapper _mapper;

        public GenreRepository(WuxiaWorldDbContext dbContext, IMapper mapper, IOptions<CancelToken> cancelToken) {

            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cancelTokenFromSeconds = cancelToken.Value.FromSeconds;
        }

        public async Task<List<Genres>> GetAll() {

            using (_dbContext) {

                await _dbContext.Database.OpenConnectionAsync().ConfigureAwait(false);

                var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

                var genres = await (
                        from genre in _dbContext.Genres
                        select genre).ToListAsync(ct.Token)
                    .ConfigureAwait(false);

                return genres;
            }
        }

        public async Task<Genres> GetByName(string genreName) {

            using (_dbContext) {

                await _dbContext.Database.OpenConnectionAsync().ConfigureAwait(false);

                var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

                var result = await (
                        from genre in _dbContext.Genres
                        where genre.GenreName.ToLower().Equals(genreName.ToLower())
                        select genre).FirstOrDefaultAsync(ct.Token)
                    .ConfigureAwait(false);

                return result;
            }
        }

        public async Task<Genres> Create(GenreModel genre) {

            using (_dbContext) {

                await _dbContext.Database.OpenConnectionAsync().ConfigureAwait(false);

                var newGenre = _mapper.Map<Genres>(genre);

                await _dbContext.AddAsync(newGenre).ConfigureAwait(false);

                var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

                var result = await _dbContext
                    .SaveChangesAsync(ct.Token)
                    .ConfigureAwait(false);

                return result == 1 ? newGenre : null;

            }
        }
    }

}