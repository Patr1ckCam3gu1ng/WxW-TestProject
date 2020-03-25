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
    using Microsoft.Extensions.Options;
    using Microsoft.Extensions.Primitives;

    public class NovelRepository : INovelRepository {

        private readonly ICacheRepository _cache;

        private readonly int _cancelTokenFromSeconds;

        private readonly WuxiaWorldDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly string _pathValue;

        public NovelRepository(WuxiaWorldDbContext dbContext, IMapper mapper, IOptions<CancelToken> cancelToken,
            IHttpContextAccessor httpContextAccessor, ICacheRepository cache) {

            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _pathValue = httpContextAccessor.HttpContext.Request.Path.Value ??
                         throw new ArgumentNullException(nameof(httpContextAccessor));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _cancelTokenFromSeconds = cancelToken.Value.FromSeconds;
        }

        public async Task<NovelResult> Create(NovelModel input) {

            var newNovel = _mapper.Map<Novels>(input);

            newNovel.TimeCreated = DateTime.UtcNow;

            await _dbContext.AddAsync(newNovel).ConfigureAwait(false);

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var result = await _dbContext.SaveChangesAsync(ct.Token).ConfigureAwait(false);

            if (result > 0) {

                await _cache.UpsertAsync(_pathValue, newNovel.Id, newNovel, ct.Token);
            }

            return result == 1 ? _mapper.Map<NovelResult>(newNovel) : null;
        }

        public async Task<List<NovelResult>> GetAll(int? novelId = null) {

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            if (GetNovelCache(novelId, out var cacheList)) {

                return cacheList;
            }

            var query =
                from novel in _dbContext.Novels
                    .Include(c => c.Chapters)
                    .Include(c => c.NovelGenres)
                    .ThenInclude(c => c.Genres)
                    .Where(c=>c.Id == (novelId ?? c.Id))
                select new Novels {
                    Id = novel.Id,
                    Name = novel.Name,
                    TimeCreated = novel.TimeCreated,
                    NovelGenres = novel.NovelGenres,
                    Chapters = (ICollection<Chapters>) novel.Chapters.Where(c => c.ChapterPublishDate != null)
                };

            var novels = await query.ToListAsync(ct.Token).ConfigureAwait(false);

            if (novels != null) {

                if (novels.Any()) {

                    var novelResult = _mapper.Map<List<NovelResult>>(novels);

                    await CacheNovels(novelResult, ct);

                    foreach (var novel in novels) {

                        await CacheNovelsById(novel, ct);

                        await CacheNovelChapters(novel, ct);

                        await CacheNovelChaptersByNumber(novel, ct);

                        await CacheGenreById(novel, ct);

                        await CacheGenreList(novel, ct);
                    }

                    return novelResult;
                }
            }

            throw new NoRecordFoundException("Novel not found");
        }

        #region Cache Management

        private bool GetNovelCache(int? novelId, out List<NovelResult> list) {

            if (novelId == null) {

                var cacheResult = _cache.GetCache(_pathValue);

                if (cacheResult != null) {

                    var cacheResultMapped = cacheResult as List<NovelResult>;

                    {
                        list = cacheResultMapped;
                        return true;
                    }
                }
            }
            else {

                var cacheResult = _cache.GetCache($"/api/novels/{novelId}");

                if (cacheResult != null) {

                    var novelResult = _mapper.Map<NovelResult>(cacheResult as Novels);

                    {
                        list = new List<NovelResult> {
                            novelResult
                        };
                        return true;
                    }
                }
            }

            list = null;

            return false;
        }

        private async Task CacheGenreList(Novels novel, CancellationTokenSource ct) {

            var keyValue = "/api/genres";

            var genreList = novel.NovelGenres.Select(novelGenre => _mapper.Map<IdNameModel>(novelGenre.Genres))
                .ToList();

            if (_cache.GetCache(keyValue) is List<IdNameModel> cacheGenreList) {

                foreach (var cacheGenre in cacheGenreList) {

                    var genreToRemove = genreList.FirstOrDefault(r => r.Id == cacheGenre.Id);

                    if (genreToRemove != null) {

                        genreList.Remove(genreToRemove);
                    }
                }

                cacheGenreList.AddRange(genreList);

                await UpsertGenreList(cacheGenreList);
            }
            else {

                if (genreList.Any()) {

                    await UpsertGenreList(genreList);
                }
            }

            return;

            async Task UpsertGenreList(List<IdNameModel> list) {

                await _cache.RemoveAsync(keyValue);

                await _cache.CreateAsync(keyValue,
                        list,
                        new CancellationChangeToken(ct.Token))
                    .ConfigureAwait(false);
            }
        }

        private async Task CacheGenreById(Novels novel, CancellationTokenSource ct) {

            foreach (var novelGenre in novel.NovelGenres) {

                var keyValue = $"api/genres/{novelGenre.Genres.Id}";

                await _cache.RemoveAsync(keyValue);

                await _cache.CreateAsync(keyValue,
                        novelGenre.Genres,
                        new CancellationChangeToken(ct.Token))
                    .ConfigureAwait(false);
            }
        }

        private async Task CacheNovelChapters(Novels novel, CancellationTokenSource ct) {

            var keyValue = $"{_pathValue}/{novel.Id}/chapters";

            await _cache.RemoveAsync(keyValue);

            await _cache.CreateAsync(keyValue,
                    novel.Chapters,
                    new CancellationChangeToken(ct.Token))
                .ConfigureAwait(false);
        }

        private async Task CacheNovelChaptersByNumber(Novels novel, CancellationTokenSource ct) {

            foreach (var chapter in novel.Chapters) {

                var keyValue = $"{_pathValue}/{novel.Id}/chapters/{chapter.ChapterNumber}";

                await _cache.RemoveAsync(keyValue);

                await _cache.CreateAsync(keyValue,
                        chapter,
                        new CancellationChangeToken(ct.Token))
                    .ConfigureAwait(false);
            }
        }

        private async Task CacheNovels(List<NovelResult> novelResult, CancellationTokenSource ct) {

            await _cache.CreateAsync(_pathValue, novelResult, new CancellationChangeToken(ct.Token))
                .ConfigureAwait(false);
        }

        private async Task CacheNovelsById(Novels novel, CancellationTokenSource ct) {

            var keyValue = $"{_pathValue}/{novel.Id}";

            await _cache.RemoveAsync(keyValue);

            await _cache.CreateAsync(keyValue,
                    novel,
                    new CancellationChangeToken(ct.Token))
                .ConfigureAwait(false);
        }

        #endregion

        // public async Task<NovelResult> GetById(int novelId) {
        //
        //     var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));
        //
        //     var apiEndpoint = $"/api/novels/{novelId}";
        //
        //     var cacheResult = _cache.GetCache(apiEndpoint);
        //
        //     if (cacheResult != null) {
        //
        //         return ReturnNovel(cacheResult as Novels);
        //     }
        //
        //     var result = await _dbContext.Novels
        //         .FirstOrDefaultAsync(c => c.Id == novelId, ct.Token)
        //         .ConfigureAwait(false);
        //     
        //     await _cache.CreateAsync(apiEndpoint, result, new CancellationChangeToken(ct.Token)).ConfigureAwait(false);
        //
        //     return ReturnNovel(result);
        //
        //     NovelResult ReturnNovel(Novels novel) {
        //
        //         return _mapper.Map<NovelResult>(novel);
        //     }
        // }
    }

}