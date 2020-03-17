namespace WuxiaWorld.BLL.Repositories {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    using Interfaces;

    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Options;

    public class NovelGenreRepository : INovelGenreRepository {

        private readonly int _cancelTokenFromSeconds;

        private readonly WuxiaWorldDbContext _dbContext;

        public NovelGenreRepository(WuxiaWorldDbContext dbContext, IOptions<CancelToken> cancelToken) {

            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _cancelTokenFromSeconds = cancelToken.Value.FromSeconds;
        }

        public async Task<bool> Assign(int novelId, List<int> genreIds) {

            // INFO: Delete first the existing records

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            var novels = await _dbContext.NovelGenres
                .Where(c => c.NovelId == novelId)
                .ToListAsync(ct.Token)
                .ConfigureAwait(false);

            if (novels != null) {
                if (novels.Count > 0) {
                    _dbContext.RemoveRange(novels);
                }
            }

            // INFO: Then lets re insert the new ones

            foreach (var genreId in genreIds) {

                await _dbContext.NovelGenres
                    .AddAsync(
                        new NovelGenres {
                            GenreId = genreId,
                            NovelId = novelId
                        }, ct.Token)
                    .ConfigureAwait(false);
            }

            var result = await _dbContext
                .SaveChangesAsync(ct.Token)
                .ConfigureAwait(false);

            return result == 1;
        }
    }

}