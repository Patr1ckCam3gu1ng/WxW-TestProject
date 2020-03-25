namespace WuxiaWorld.BLL.Services {

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Models;

    using Exceptions;

    using Interfaces;

    using Repositories.Interfaces;

    public class NovelService : INovelService {

        private readonly INovelRepository _novelRepository;

        public NovelService(INovelRepository novelRepository) {

            _novelRepository = novelRepository ?? throw new ArgumentNullException(nameof(novelRepository));
        }

        public async Task<List<NovelResult>> Create(NovelModel[] novels) {

            var newNovels = await _novelRepository.Create(novels).ConfigureAwait(false);

            if (newNovels == null) {

                throw new FailedCreatingNewException("Failed creating new novel");
            }

            return newNovels;
        }

        public async Task<List<NovelResult>> GetAll(int? novelId) {

            return await _novelRepository.GetAll(novelId).ConfigureAwait(false);
        }
    }

}