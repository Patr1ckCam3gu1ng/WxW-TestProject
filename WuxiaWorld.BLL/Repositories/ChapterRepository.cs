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

    public class ChapterRepository : IChapterRepository {
        private readonly ICacheRepository _cache;

        private readonly int _cancelTokenFromSeconds;
        private readonly WuxiaWorldDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly string _pathValue;

        public ChapterRepository(WuxiaWorldDbContext dbContext, IMapper mapper, IOptions<CancelToken> cancelToken,
            IHttpContextAccessor httpContextAccessor, ICacheRepository cache) {

            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _pathValue = httpContextAccessor.HttpContext.Request.Path.Value ??
                         throw new ArgumentNullException(nameof(httpContextAccessor));
            _cache = cache ?? throw new ArgumentNullException(nameof(cache));
            _cancelTokenFromSeconds = cancelToken.Value.FromSeconds;
        }

        public async Task<Chapters> Create(int novelId, ChapterModel input) {

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var novelChapter = _mapper.Map<Chapters>(input);

            novelChapter.NovelId = novelId;

            await _dbContext.AddAsync(novelChapter, ct.Token).ConfigureAwait(false);

            var result = await _dbContext.SaveChangesAsync(ct.Token).ConfigureAwait(false);

            if (result == 1) {

                await _cache.CreateAsync($"{_pathValue}/{novelChapter.Id}",
                        novelChapter,
                        new CancellationChangeToken(ct.Token))
                    .ConfigureAwait(false);

                await _cache.CreateAsync($"{_pathValue}",
                        await ConstructChapterList(),
                        new CancellationChangeToken(ct.Token))
                    .ConfigureAwait(false);

                return novelChapter;
            }

            throw new FailedCreatingNewException();

            async Task<List<Chapters>> ConstructChapterList() {

                var cacheNovelChapters = _cache.GetCache(_pathValue);

                if (cacheNovelChapters != null) {

                    if (cacheNovelChapters is List<Chapters> cacheNovelChapter) {

                        await _cache.RemoveAsync(_pathValue);

                        cacheNovelChapter.Add(novelChapter);

                        return cacheNovelChapter;
                    }
                }

                return new List<Chapters> {
                    novelChapter
                };
            }
        }

        public async Task<Chapters> GetByChapterNumber(int novelId, int chapterNumber) {

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var apiEndpoint = $"{_pathValue}/{chapterNumber}";

            var cacheResult = _cache.GetCache(apiEndpoint);

            if (cacheResult != null) {

                return cacheResult as Chapters;
            }

            var result = await (
                from chapter in _dbContext.Chapters
                where chapter.ChapterNumber == chapterNumber && chapter.NovelId == novelId
                select chapter).FirstOrDefaultAsync(ct.Token).ConfigureAwait(false);

            if (result != null) {

                await _cache.CreateAsync(apiEndpoint, result, new CancellationChangeToken(ct.Token))
                    .ConfigureAwait(false);
            }

            return result;
        }

        public async Task<Chapters> Publish(int novelId, int chapterNumber) {

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var chapter = await _dbContext.Chapters
                .Where(c => c.NovelId == novelId && c.ChapterNumber == chapterNumber)
                .FirstOrDefaultAsync(ct.Token)
                .ConfigureAwait(false);

            if (chapter == null) {
                throw new NovelChapterNotFoundException("Record not found");
            }

            chapter.ChapterPublishDate = DateTime.UtcNow;

            var result = await _dbContext.SaveChangesAsync(ct.Token).ConfigureAwait(false);

            await _cache.CreateAsync(_pathValue, chapter, new CancellationChangeToken(ct.Token)).ConfigureAwait(false);

            return result > 0 ? chapter : null;
        }

        public async Task<bool> IsAlreadyPublished(int novelId, int chapterNumber) {

            var cacheResult = _cache.GetCache(_pathValue);

            if (cacheResult != null) {

                return true;
            }

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var isAlreadyPublished = await _dbContext.Chapters
                .Where(c => c.NovelId == novelId
                            && c.ChapterNumber == chapterNumber
                            && c.ChapterPublishDate != null)
                .AnyAsync(ct.Token)
                .ConfigureAwait(false);

            return isAlreadyPublished;
        }


        public async Task<List<ChapterModel>> GetByNovelId(int novelId) {

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var apiEndpoint = $"{_pathValue}";

            var cacheResult = _cache.GetCache(apiEndpoint);

            if (cacheResult != null) {

                if (cacheResult is List<Chapters> cacheChapters) {

                    return cacheChapters.Select(cacheChapter => _mapper.Map<ChapterModel>(cacheChapter)).ToList();
                }
            }

            var result = await (
                from chapter in _dbContext.Chapters
                where  chapter.NovelId == novelId && chapter.ChapterPublishDate != null
                select chapter).ToListAsync(ct.Token).ConfigureAwait(false);

            return _mapper.Map<List<ChapterModel>>(result);
        }
    }

}