namespace WuxiaWorld.BLL.Services {

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    using Exceptions;

    using Interfaces;

    using Repositories.Interfaces;

    public class NovelService : INovelService {

        private readonly INovelRepository _novelRepository;

        public NovelService(INovelRepository novelRepository) {

            _novelRepository = novelRepository ?? throw new ArgumentNullException(nameof(novelRepository));
        }

        public async Task<NovelResult> Create(NovelModel input) {

            var newNovel = await _novelRepository.Create(input).ConfigureAwait(false);

            if (newNovel == null) {

                throw new FailedCreatingNewException("Failed creating new novel");
            }

            return newNovel;
        }

        public async Task<List<NovelResult>> GetAll() {

            return await _novelRepository.GetAll().ConfigureAwait(false);
        }

        public async Task<NovelResult> GetById(int novelId) {

            return await _novelRepository.GetAll(novelId).ConfigureAwait(false);
        }
    }

}