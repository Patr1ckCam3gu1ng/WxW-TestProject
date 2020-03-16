namespace WuxiaWorld.Controllers {

    using System;
    using System.Threading.Tasks;

    using BLL.Exceptions;
    using BLL.Services.Interfaces;

    using DAL.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    [Route("api/novels")]
    public class ChapterController : Controller {
        private readonly IChapterService _chapterService;

        public ChapterController(IChapterService chapterService) {

            _chapterService = chapterService ?? throw new ArgumentNullException(nameof(chapterService));
        }

        [HttpPost]
        [Route("{novelId}/chapters")]
        public async Task<IActionResult> New([FromBody] ChapterModel input, int novelId) {

            try {
                return Ok(await _chapterService.Create(novelId, input));
            }
            catch (FailedCreatingNewException exception) {
                return BadRequest(exception.Message);
            }
            catch (NovelChapterNumberExistsException exception) {
                return BadRequest(exception.Message);
            }
            catch (Exception exception) {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("{novelId}/chapters/{chapterId}/publish")]
        public IActionResult Publish(int novelId, int chapterId) {

            return Ok();
        }
    }

}