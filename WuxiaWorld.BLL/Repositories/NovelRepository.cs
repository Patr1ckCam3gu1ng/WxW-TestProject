namespace WuxiaWorld.BLL.Repositories {

    using System;
    using System.Collections.Generic;
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

        public async Task<List<Novels>> GetAll() {

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var novels = await _dbContext.Novels.ToListAsync(ct.Token).ConfigureAwait(false);

            if (novels.Count > 0) {

                return novels;
            }

            throw new NoRecordFoundException(string.Empty);
        }

        public async Task<Novels> GetById(int novelId) {

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var novel = await _dbContext.Novels
                .FirstOrDefaultAsync(c => c.NovelId == novelId, ct.Token)
                .ConfigureAwait(false);

            if (novel == null) {

                throw new NoRecordFoundException(string.Empty);
            }

            return novel;

        }
    }

}