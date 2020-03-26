namespace WuxiaWorld.BLL.Services {

    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using AutoMapper;

    using DAL.Entities;
    using DAL.Models;

    using Exceptions;

    using Interfaces;

    using Repositories.Interfaces;

    public class ChapterService : IChapterService {

        private readonly IChapterRepository _chapterRepository;
        private readonly IMapper _mapper;
        private readonly INovelService _novelService;

        public ChapterService(IChapterRepository chapterRepository, INovelService novelService, IMapper mapper) {

            _chapterRepository = chapterRepository ?? throw new ArgumentNullException(nameof(chapterRepository));
            _novelService = novelService ?? throw new ArgumentNullException(nameof(chapterRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(chapterRepository));
        }

        public async Task<ChapterModel> Create(int novelId, ChapterInput input) {

            var novelByChapterNumber = await _chapterRepository.GetByChapterNumber(novelId, input.Number);

            // INFO: This means that the chapter number does not yet exists within the novel
            if (novelByChapterNumber == null) {

                var novel = await _novelService.GetAll(novelId).ConfigureAwait(false);

                if (novel != null) {

                    var novelChapter = await _chapterRepository.Create(novelId, input);

                    return _mapper.Map<ChapterModel>(novelChapter);
                }

                throw new NoRecordFoundException("Novel not found");
            }

            throw new NovelChapterNumberExistsException();
        }

        public async Task<Chapters> Publish(int novelId, int chapterNumber) {

            var isAlreadyPublished =
                await _chapterRepository.IsAlreadyPublished(novelId, chapterNumber).ConfigureAwait(false);

            if (isAlreadyPublished) {
                throw new ChapterAlreadyPublished("Chapter is already published");
            }

            var result = await _chapterRepository.Publish(novelId, chapterNumber).ConfigureAwait(false);

            if (result == null) {
                throw new FailedToPublishChapterException("Failed publishing this novel's chapter");
            }

            return result;
        }

        public async Task<ChapterNovelResult> GetByNovelId(int novelId, int chapterNumber, bool? isIncludeContent) {

            return await _chapterRepository.GetByNovelId(novelId, chapterNumber, isIncludeContent).ConfigureAwait(false);
        }
    }

}