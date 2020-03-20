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

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Primitives;

    public class GenreRepository : IGenreRepository {

        private readonly ICacheRepository _cache;
        private readonly int _cancelTokenFromSeconds;

        private readonly WuxiaWorldDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly string _pathValue;

        public GenreRepository(WuxiaWorldDbContext dbContext, IMapper mapper, IOptions<CancelToken> cancelToken,
            IHttpContextAccessor httpContextAccessor, ICacheRepository cache) {

            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _pathValue = httpContextAccessor.HttpContext.Request.Path.Value ??
                         throw new ArgumentNullException(nameof(httpContextAccessor));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _cancelTokenFromSeconds = cancelToken.Value.FromSeconds;
        }

        public async Task<List<Genres>> GetAllAsync() {

            var cacheResult = _cache.GetCache(_pathValue);

            if (cacheResult != null) {

                return cacheResult as List<Genres>;
            }

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var genres = await (
                    from genre in _dbContext.Genres
                    select genre).ToListAsync(ct.Token)
                .ConfigureAwait(false);

            await _cache.CreateAsync(_pathValue, genres, new CancellationChangeToken(ct.Token)).ConfigureAwait(false);

            return genres;
        }

        public async Task<Genres> GetByName(string genreName) {

            var cacheResult = _cache.GetCache(_pathValue);

            if (cacheResult != null) {

                return cacheResult as Genres;
            }

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var genre = await (
                    from dbGenre in _dbContext.Genres
                    where dbGenre.GenreName.ToLower().Equals(genreName.ToLower())
                    select dbGenre).FirstOrDefaultAsync(ct.Token)
                .ConfigureAwait(false);

            await _cache.CreateAsync(_pathValue, genre, new CancellationChangeToken(ct.Token)).ConfigureAwait(false);

            return genre;
        }

        public async Task<List<Genres>> GetById(List<int> genreIds) {

            var cacheResult = _cache.GetCache(_pathValue);

            if (cacheResult != null) {

                return cacheResult as List<Genres>;
            }

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var genres = await _dbContext.Genres
                .Where(c => genreIds.Any(f => f == c.GenreId))
                .ToListAsync(ct.Token)
                .ConfigureAwait(false);

            await _cache.CreateAsync(_pathValue, genres, new CancellationChangeToken(ct.Token)).ConfigureAwait(false);

            return genres;
        }

        public async Task<Genres> Create(GenreModel genre) {

            var newGenre = _mapper.Map<Genres>(genre);

            await _dbContext.AddAsync(newGenre).ConfigureAwait(false);

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var result = await _dbContext
                .SaveChangesAsync(ct.Token)
                .ConfigureAwait(false);

            await _cache.CreateAsync(_pathValue, newGenre, new CancellationChangeToken(ct.Token))
                .ConfigureAwait(false);

            return result == 1 ? newGenre : null;
        }
    }

}