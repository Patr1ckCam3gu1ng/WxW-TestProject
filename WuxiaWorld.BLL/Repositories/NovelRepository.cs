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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _pathValue;

        public NovelRepository(WuxiaWorldDbContext dbContext, IMapper mapper, IOptions<CancelToken> cancelToken,
            IHttpContextAccessor httpContextAccessor, ICacheRepository cache) {

            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<List<NovelResult>> GetAll() {

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var cacheResult = _cache.GetCache(_pathValue);

            if (cacheResult != null) {

                var result = _mapper.Map<List<NovelResult>>(cacheResult);

                return result;
            }

            var query =
                from novel in _dbContext.Novels
                    .Include(c => c.Chapters)
                    .Include(c => c.NovelGenres).ThenInclude(c => c.Genres)
                select new Novels {
                    Id = novel.Id,
                    Name = novel.Name,
                    TimeCreated = novel.TimeCreated,
                    NovelGenres = novel.NovelGenres,
                    Chapters = (ICollection<Chapters>) novel.Chapters.Where(c => c.ChapterPublishDate != null)
                };
            
            var novels = await query.ToListAsync(ct.Token).ConfigureAwait(false);
            
            if (novels != null) {
            
                var novelResult = _mapper.Map<List<NovelResult>>(novels);
            
                await _cache.CreateAsync(_pathValue, novelResult, new CancellationChangeToken(ct.Token))
                    .ConfigureAwait(false);
            
                return novelResult;
            }

            throw new NoRecordFoundException("Novel not found");
        }

        public async Task<NovelResult> GetById(int novelId) {

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var apiEndpoint = $"/api/novels/{novelId}";

            var cacheResult = _cache.GetCache(apiEndpoint);

            if (cacheResult != null) {

                return ReturnNovel(cacheResult as Novels);
            }

            var result = await _dbContext.Novels
                .FirstOrDefaultAsync(c => c.Id == novelId, ct.Token)
                .ConfigureAwait(false);
            
            await _cache.CreateAsync(apiEndpoint, result, new CancellationChangeToken(ct.Token)).ConfigureAwait(false);

            return ReturnNovel(result);

            NovelResult ReturnNovel(Novels novel) {

                return _mapper.Map<NovelResult>(novel);
            }
        }
    }

}