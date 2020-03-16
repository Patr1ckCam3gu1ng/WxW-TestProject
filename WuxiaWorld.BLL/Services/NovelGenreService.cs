namespace WuxiaWorld.BLL.Services {

    using System;
    using System.Collections.Generic;

    using Interfaces;

    using Repositories.Interfaces;

    public class NovelGenreService : INovelGenreService {

        private readonly INovelGenreRepository _novelGenreRepository;

        public NovelGenreService(INovelGenreRepository novelGenreRepository) {

            _novelGenreRepository =
                novelGenreRepository ?? throw new ArgumentNullException(nameof(novelGenreRepository));
        }

        public void Assign(int novelId, List<int> inputGenreIds) {

            xx

            throw new NotImplementedException();
        }
    }

}