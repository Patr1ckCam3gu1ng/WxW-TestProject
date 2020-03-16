namespace WuxiaWorld.BLL.Services {

    using System;
    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    using Exceptions;

    using Interfaces;

    using Repositories;
    using Repositories.Interfaces;

    public class NovelService : INovelService {

        private readonly INovelRepository _novelRepository;

        public NovelService(INovelRepository novelRepository) {

            _novelRepository = novelRepository ?? throw new ArgumentNullException(nameof(novelRepository));
        }

        public async Task<Novels> Create(NovelModel input) {

            var newNovel = await _novelRepository.Create(input);

            if (newNovel == null) {

                throw new FailedCreatingNewException("Failed creating new novel");
            }

            return newNovel;
        }
    }

}