namespace WuxiaWorld.BLL.Repositories {

    using System;
    using System.Threading;
    using System.Threading.Tasks;

    using AutoMapper;

    using DAL.Entities;
    using DAL.Models;

    using Interfaces;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    public class NovelRepository : INovelRepository {

        private readonly WuxiaWorldDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly int _cancelTokenFromSeconds;

        public NovelRepository(WuxiaWorldDbContext dbContext, IMapper mapper, IOptions<CancelToken> cancelToken) {

            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _cancelTokenFromSeconds = cancelToken.Value.FromSeconds;
        }

        public async Task<Novels> Create(NovelModel input) {

            using (_dbContext) {

                await _dbContext.Database.OpenConnectionAsync().ConfigureAwait(false);

                var newNovel = _mapper.Map<Novels>(input);

                newNovel.TimeCreated = DateTime.UtcNow;

                await _dbContext.AddAsync(newNovel).ConfigureAwait(false);

                var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

                var result = await _dbContext.SaveChangesAsync(ct.Token).ConfigureAwait(false);

                return result == 1 ? newNovel : null;
            }
        }
    }

}