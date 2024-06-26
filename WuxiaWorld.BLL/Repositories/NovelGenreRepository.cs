﻿namespace WuxiaWorld.BLL.Repositories {

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

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            // INFO: Then lets re insert the new ones
            await AssignNewNovelGenre(novelId, genreIds, ct);

            var isSuccess = await _dbContext
                .SaveChangesAsync(ct.Token)
                .ConfigureAwait(false);

            return isSuccess > 0;
        }

        public async Task UnAssign(int novelId, int genreId) {

            var ct = new CancellationTokenSource(TimeSpan.FromSeconds(_cancelTokenFromSeconds));

            await DeleteExistingNovelGenre(novelId, genreId, ct).ConfigureAwait(false);
        }

        #region Private Methods

        private async Task AssignNewNovelGenre(int novelId, IEnumerable<int> genreIds, CancellationTokenSource ct) {

            foreach (var genreId in genreIds) {

                await _dbContext.NovelGenres
                    .AddAsync(
                        new NovelGenres {
                            GenreId = genreId,
                            NovelId = novelId
                        },
                        ct.Token)
                    .ConfigureAwait(false);
            }
        }

        private async Task DeleteExistingNovelGenre(int novelId, int genreId, CancellationTokenSource ct) {

            var novels = await _dbContext.NovelGenres
                .Where(c => c.NovelId == novelId && c.GenreId == genreId)
                .FirstOrDefaultAsync(ct.Token)
                .ConfigureAwait(false);

            if (novels != null) {
                _dbContext.Remove(novels);
                await _dbContext.SaveChangesAsync();
            }
        }

        #endregion

    }

}