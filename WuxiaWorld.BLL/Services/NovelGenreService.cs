namespace WuxiaWorld.BLL.Services {

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using DAL.Models;

    using Exceptions;

    using Interfaces;

    using Repositories.Interfaces;

    public class NovelGenreService : INovelGenreService {

        private readonly IGenreRepository _genreRepository;
        private readonly INovelGenreRepository _novelGenreRepository;
        private readonly INovelRepository _novelRepository;

        public NovelGenreService(INovelGenreRepository novelGenreRepository, INovelRepository novelRepository,
            IGenreRepository genreRepository) {

            _novelGenreRepository =
                novelGenreRepository ?? throw new ArgumentNullException(nameof(novelGenreRepository));
            _novelRepository = novelRepository ?? throw new ArgumentNullException(nameof(novelGenreRepository));
            _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
        }

        public async Task<bool> Assign(int novelId, List<int> genreIds) {

            var novels = await _novelRepository.GetAll(novelId).ConfigureAwait(false);
            
            if (novels == null) {

                throw new NoRecordFoundException("Novel not found");
            }

            if (IsNovelGenreAlreadyExists(genreIds, novels)) {

                throw new NovelGenreAlreadyExists();
            }

            var genres = await _genreRepository.GetByIds(genreIds).ConfigureAwait(false);

            // INFO: If not on equal count, meaning one or more genre id is not on database or no longer exists
            if (genres.Count == genreIds.Count) {

                return await _novelGenreRepository.Assign(novelId, genreIds).ConfigureAwait(false);
            }

            throw new OneOrMoreGenreNotFoundException();

            bool IsNovelGenreAlreadyExists(IReadOnlyCollection<int> genreIdList, IEnumerable<NovelResult> novelResults) {

                foreach (var novel in novelResults) {

                    foreach (var genre in novel.Genres) {

                        if (genreIdList.Any(c => c == genre.Id)) {

                            return true;
                        }
                    }
                }

                return false;
            }
        }

        public async Task UnAssign(int novelId, int genreId) {

            var novel = await _novelRepository.GetAll(novelId).ConfigureAwait(false);

            if (novel == null) {
                throw new NoRecordFoundException("Novel not found");
            }

            var genres = await _genreRepository.GetByIds(new List<int>() {
                genreId
            }).ConfigureAwait(false);

            if (genres.Count > 0) {

                await _novelGenreRepository.UnAssign(novelId, genreId).ConfigureAwait(false);
            }
        }
    }

}