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

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var novelChapter = _mapper.Map<Chapters>(input);

            novelChapter.NovelId = novelId;

            await _dbContext.Chapters.AddAsync(novelChapter, ct.Token).ConfigureAwait(false);

            var result = await _dbContext.SaveChangesAsync(ct.Token).ConfigureAwait(false);

            return result == 1 ? novelChapter : null;
        }

        public async Task<Chapters> GetByChapterNumber(int novelId, int chapterName) {

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var result = await (
                from chapter in _dbContext.Chapters
                where chapter.ChapterNumber == chapterName && chapter.NovelId == novelId
                select chapter).FirstOrDefaultAsync(ct.Token).ConfigureAwait(false);

            return result;
        }

        public async Task<Chapters> Publish(int novelId, int chapterId) {

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var chapter = await _dbContext.Chapters
                .Where(c => c.NovelId == novelId && c.ChapterId == chapterId)
                .FirstOrDefaultAsync(ct.Token)
                .ConfigureAwait(false);

            if (chapter == null) {
                throw new NovelChapterNotFoundException("Record not found");
            }

            chapter.ChapterPublishDate = DateTime.UtcNow;

            var result = await _dbContext.SaveChangesAsync(ct.Token).ConfigureAwait(false);

            return result == 1 ? chapter : null;
        }
    }

}