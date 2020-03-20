namespace WuxiaWorld.BLL.Repositories {

    using System;
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
            }

            return result == 1 ? novelChapter : null;
        }

        public async Task<Chapters> GetByChapterNumber(int novelId, int chapterName) {

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var cacheResult = _cache.GetCache(_pathValue);

            if (cacheResult != null) {

                return cacheResult as Chapters;
            }

            var result = await (
                from chapter in _dbContext.Chapters
                where chapter.ChapterNumber == chapterName && chapter.NovelId == novelId
                select chapter).FirstOrDefaultAsync(ct.Token).ConfigureAwait(false);

            await _cache.CreateAsync(_pathValue, result, new CancellationChangeToken(ct.Token)).ConfigureAwait(false);

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

            return result == 1 ? chapter : null;
        }

        public async Task<bool> IsAlreadyPublished(int novelId, int chapterNumber) {

            var cacheResult = _cache.GetCache(_pathValue);

            if (cacheResult != null) {

                return cacheResult is bool;
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
    }

}