namespace WuxiaWorld.BLL.Services {

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

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

            try {
                var novel = await _novelRepository.GetById(novelId).ConfigureAwait(false);

                if (novel != null) {

                    var genres = await _genreRepository.GetById(genreIds).ConfigureAwait(false);

                    if (genres.Count == genreIds.Count) {

                        return await _novelGenreRepository.Assign(novelId, genreIds).ConfigureAwait(false);
                    }

                    throw new OneOrMoreGenreNotFoundException("One or more genre not found");
                }
            }
            catch (Exception exception) {
                var innerExceptionMessage = exception.InnerException?.Message;

                if (!string.IsNullOrEmpty(innerExceptionMessage)) {

                    if (innerExceptionMessage.Contains("duplicate key value")) {
                        throw new RecordAlreadyExistsException("Genre already exists");
                    }
                }
            }

            return false;
        }
    }

}