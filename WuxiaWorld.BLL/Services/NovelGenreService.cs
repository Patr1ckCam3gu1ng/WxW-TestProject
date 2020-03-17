namespace WuxiaWorld.BLL.Services {

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using Interfaces;

    using Repositories.Interfaces;

    public class NovelGenreService : INovelGenreService {

        private readonly INovelGenreRepository _novelGenreRepository;

        public NovelGenreService(INovelGenreRepository novelGenreRepository) {

            _novelGenreRepository =
                novelGenreRepository ?? throw new ArgumentNullException(nameof(novelGenreRepository));
        }

        public async Task<bool> Assign(int novelId, List<int> inputGenreIds) {

            return await _novelGenreRepository.Assign(novelId, inputGenreIds).ConfigureAwait(false);
        }
    }

}