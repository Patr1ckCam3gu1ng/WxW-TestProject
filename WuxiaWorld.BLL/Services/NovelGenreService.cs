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

            // INFO: This will throw an exception error if no novel were found by id
            await _novelRepository.GetById(novelId).ConfigureAwait(false);

            var genres = await _genreRepository.GetById(genreIds).ConfigureAwait(false);

            // INFO: If not on equal count, meaning one or more genre id is not on database or no longer exists
            if (genres.Count == genreIds.Count) {

                return await _novelGenreRepository.Assign(novelId, genreIds).ConfigureAwait(false);
            }

            throw new OneOrMoreGenreNotFoundException("One or more genre not found");
        }
    }

}