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

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    public class NovelRepository : INovelRepository {

        private readonly int _cancelTokenFromSeconds;

        private readonly WuxiaWorldDbContext _dbContext;
        private readonly IMapper _mapper;

        public NovelRepository(WuxiaWorldDbContext dbContext, IMapper mapper, IOptions<CancelToken> cancelToken) {

            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cancelTokenFromSeconds = cancelToken.Value.FromSeconds;
        }

        public async Task<Novels> Create(NovelModel input) {

            var newNovel = _mapper.Map<Novels>(input);

            newNovel.TimeCreated = DateTime.UtcNow;

            await _dbContext.AddAsync(newNovel).ConfigureAwait(false);

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var result = await _dbContext.SaveChangesAsync(ct.Token).ConfigureAwait(false);

            return result == 1 ? newNovel : null;
        }

        public async Task<List<NovelResult>> GetAll() {

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var query =
                from novel in _dbContext.Novels
                    .Include(c => c.Chapters)
                    .Include(c => c.NovelGenres).ThenInclude(c => c.Genres)
                select new Novels {
                    NovelId = novel.NovelId,
                    Name = novel.Name,
                    Synopsis = novel.Synopsis,
                    TimeCreated = novel.TimeCreated,
                    NovelGenres = novel.NovelGenres,
                    Chapters = (ICollection<Chapters>) novel.Chapters.Where(c => c.ChapterPublishDate != null)
                };

            var novels = await query.ToListAsync(ct.Token).ConfigureAwait(false);

            if (novels != null) {

                return _mapper.Map<List<NovelResult>>(novels);
            }

            throw new NoRecordFoundException(string.Empty);
        }

        public async Task<Novels> GetById(int novelId) {

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var novel = await _dbContext.Novels
                .FirstOrDefaultAsync(c => c.NovelId == novelId, ct.Token)
                .ConfigureAwait(false);

            return novel;
        }
    }

}