namespace WuxiaWorld.BLL.Repositories {

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using AutoMapper;

    using DAL.Entities;
    using DAL.Models;

    using Interfaces;

    using Microsoft.EntityFrameworkCore;

    public class ChapterRepository : IChapterRepository {

        private readonly WuxiaWorldDbContext _dbContext;
        private readonly IMapper _mapper;

        public ChapterRepository(WuxiaWorldDbContext dbContext, IMapper mapper) {

            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(WuxiaWorldDbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Chapters> Create(int novelId, ChapterModel input) {

            using (_dbContext) {

                await _dbContext.Database.OpenConnectionAsync();

                var novelChapter = _mapper.Map<Chapters>(input);

                novelChapter.NovelId = novelId;

                await _dbContext.AddAsync(novelChapter);

                var result = await _dbContext.SaveChangesAsync();

                return result == 1 ? novelChapter : null;
            }
        }

        public async Task<Chapters> GetByChapterNumber(int novelId, int chapterName) {

            using (_dbContext) {

                await _dbContext.Database.OpenConnectionAsync();

                var result = await (
                    from chapter in _dbContext.Chapters
                    where chapter.ChapterNumber == chapterName && chapter.NovelId == novelId
                    select chapter).FirstOrDefaultAsync();

                return result;
            }
        }
    }

}