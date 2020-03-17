namespace WuxiaWorld.BLL.Repositories {

    using System;
    using System.Collections.Generic;
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

            using (_dbContext) {

                // INFO: Delete first the existing records

                var novel = await _dbContext.Novels
                    .FirstOrDefaultAsync(c => c.NovelId == novelId)
                    .ConfigureAwait(false);

                _dbContext.Remove(novel);

                // INFO: Then lets re insert the new ones

                foreach (var genreId in genreIds) {

                    await _dbContext.NovelGenres
                        .AddAsync(
                            new NovelGenres {
                                GenreId = genreId,
                                NovelId = novelId
                            })
                        .ConfigureAwait(false);
                }

                var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

                var result = await _dbContext
                    .SaveChangesAsync(ct.Token)
                    .ConfigureAwait(false);

                return result == 1;
            }
        }
    }

}