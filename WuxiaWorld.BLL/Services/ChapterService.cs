namespace WuxiaWorld.BLL.Services {

    using System;
    using System.Threading.Tasks;

    using DAL.Entities;
    using DAL.Models;

    using Exceptions;

    using Interfaces;

    using Repositories.Interfaces;

    public class ChapterService : IChapterService {

        private readonly IChapterRepository _chapterRepository;

        public ChapterService(IChapterRepository chapterRepository) {

            _chapterRepository = chapterRepository ?? throw new ArgumentNullException(nameof(chapterRepository));
        }

        public async Task<Chapters> Create(int novelId, ChapterModel input) {

            var novelByChapterNumber = await _chapterRepository.GetByChapterNumber(novelId, input.ChapterNumber);

            // INFO: This means that the chapter number does not yet exists within the novel
            if (novelByChapterNumber == null) {

                var novelChapter = await _chapterRepository.Create(novelId, input);

                if (novelChapter == null) {
                    throw new FailedCreatingNewException("Failed creating new novel chapter");
                }

                return novelChapter;
            }

            throw new NovelChapterNumberExistsException("Chapter number already exists in this novel");
        }

        public async Task<Chapters> Publish(int novelId, int chapterId) {

            var result = await _chapterRepository.Publish(novelId, chapterId).ConfigureAwait(false);

            if (result == null) {
                throw new FailedToPublishChapterException("Failed publishing this novel's chapter");
            }

            return result;
        }
    }

}