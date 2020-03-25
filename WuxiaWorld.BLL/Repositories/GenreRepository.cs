namespace WuxiaWorld.BLL.Repositories {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using DAL.Entities;
    using DAL.Models;

    using Exceptions;

    using Interfaces;

    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;
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

        public async Task<List<IdNameModel>> GetAllAsync() {

            var cacheResult = _cache.GetCache(_pathValue);

            if (cacheResult != null) {

                if (cacheResult is List<object> cacheObjects) {

                    var cacheGenreList = cacheObjects.Select(cache => cache as Genres).ToList();

                    return _mapper.Map<List<IdNameModel>>(cacheGenreList);
                }
            }

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var genres = await (
                    from genre in _dbContext.Genres
                    select genre).ToListAsync(ct.Token)
                .ConfigureAwait(false);

            var genreObjects = genres.Cast<object>().ToList();

            await _cache.CreateAsync(_pathValue, genreObjects, new CancellationChangeToken(ct.Token))
                .ConfigureAwait(false);

            return _mapper.Map<List<IdNameModel>>(genres);
        }

        // public async Task<Genres> GetByName(string genreName) {
        //
        //     var cacheResult = _cache.GetCache(_pathValue);
        //
        //     if (cacheResult != null) {
        //
        //         return cacheResult as Genres;
        //     }
        //
        //     var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));
        //
        //     var genre = await (
        //             from dbGenre in _dbContext.Genres
        //             where dbGenre.Name.ToLower().Equals(genreName.ToLower())
        //             select dbGenre).FirstOrDefaultAsync(ct.Token)
        //         .ConfigureAwait(false);
        //
        //     await _cache.CreateAsync(_pathValue, genre, new CancellationChangeToken(ct.Token)).ConfigureAwait(false);
        //
        //     return genre;
        // }

        public async Task<List<Genres>> GetByIds(List<int> inputGenreIds) {

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var cacheGenreList = GetCacheGenre();

            // INFO: Is all inputGenreIds are fetched from the cache
            // INFO:    If not, then proceed and fetch 'only' those that are not yet cached.
            if (cacheGenreList.Any() && cacheGenreList.Count == inputGenreIds.Count) {

                return cacheGenreList;
            }

            var genreFromDb = await GetGenreFromDb();

            if (!cacheGenreList.Any()) {
                return genreFromDb;
            }

            cacheGenreList.AddRange(genreFromDb);

            return cacheGenreList;

            #region Private Methods

            List<Genres> GetCacheGenre() {

                return (from genreId in inputGenreIds
                        select _cache.GetCache(GetApiEndpoint(genreId))
                        into cacheResult
                        where cacheResult != null
                        select cacheResult as Genres).ToList();
            }

            List<int> GetNonCacheIds() {

                return inputGenreIds.Where(genreId => cacheGenreList.All(c => c.Id != genreId)).ToList();
            }

            string GetApiEndpoint(int genreId) {

                return $"/api/genres/{genreId}";
            }

            async Task<List<Genres>> GetGenreFromDb() {

                // INFO: Get only the genre 'Ids' of those that are not present in the cache.
                var nonCacheGenreList = GetNonCacheIds();

                if (nonCacheGenreList.Any()) {

                    var genres = await _dbContext.Genres
                        .Where(c => nonCacheGenreList.Any(f => f == c.Id))
                        .ToListAsync(ct.Token)
                        .ConfigureAwait(false);

                    foreach (var genre in genres) {

                        await _cache.CreateAsync(GetApiEndpoint(genre.Id),
                                genre,
                                new CancellationChangeToken(ct.Token))
                            .ConfigureAwait(false);
                    }

                    return genres;
                }

                throw new OneOrMoreGenreNotFoundException();
            }

            #endregion
        }

        public async Task<List<GenreModel>> Create(GenreModel[] genres) {

            var newGenres = new List<GenreModel>();

            foreach (var genre in genres) {

                var newGenre = _mapper.Map<Genres>(genre);

                await _dbContext.AddAsync(newGenre).ConfigureAwait(false);

                var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

                var result = await _dbContext
                    .SaveChangesAsync(ct.Token)
                    .ConfigureAwait(false);

                if (result == 1) {

                    await _cache.UpsertAsync(_pathValue, newGenre.Id, newGenre, ct.Token);

                    newGenres.Add(_mapper.Map<GenreModel>(newGenre));
                }
            }

            return newGenres;
        }
    }

}