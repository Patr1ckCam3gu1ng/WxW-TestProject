namespace WuxiaWorld.BLL.Repositories {

    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using DAL.Entities;
    using DAL.Models;

    using Interfaces;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    public class ChapterRepository : IChapterRepository {

        private readonly int _cancelTokenFromSeconds;
        private readonly WuxiaWorldDbContext _dbContext;
        private readonly IMapper _mapper;

        public ChapterRepository(WuxiaWorldDbContext dbContext, IMapper mapper, IOptions<CancelToken> cancelToken) {

            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cancelTokenFromSeconds = cancelToken.Value.FromSeconds;
        }

        public async Task<Chapters> Create(int novelId, ChapterModel input) {

            using (_dbContext) {

                await _dbContext.Database.OpenConnectionAsync().ConfigureAwait(false);

                var novelChapter = _mapper.Map<Chapters>(input);

                novelChapter.NovelId = novelId;

                await _dbContext.AddAsync(novelChapter).ConfigureAwait(false);

                var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

                var result = await _dbContext.SaveChangesAsync(ct.Token).ConfigureAwait(false);

                return result == 1 ? novelChapter : null;
            }
        }

        public async Task<Chapters> GetByChapterNumber(int novelId, int chapterName) {

            using (_dbContext) {

                await _dbContext.Database.OpenConnectionAsync().ConfigureAwait(false);

                var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

                var result = await (
                    from chapter in _dbContext.Chapters
                    where chapter.ChapterNumber == chapterName && chapter.NovelId == novelId
                    select chapter).FirstOrDefaultAsync(ct.Token).ConfigureAwait(false);

                return result;
            }
        }
    }

}