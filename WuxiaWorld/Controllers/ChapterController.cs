namespace WuxiaWorld.Controllers {

    using System;
    using System.Threading.Tasks;

    using BLL.ActionFilters;
    using BLL.Exceptions;
    using BLL.Services.Interfaces;

    using DAL.Models;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/novels")]
    [TypeFilter(typeof(DbContextActionFilter))]
    [TypeFilter(typeof(AdminOnlyActionFilter))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ChapterController : Controller {
        private readonly IChapterService _chapterService;

        public ChapterController(IChapterService chapterService) {

            _chapterService = chapterService ?? throw new ArgumentNullException(nameof(chapterService));
        }

        [HttpGet]
        [Route("{novelId}/chapters")]
        public async Task<IActionResult> Get(int novelId) {
            try {
                var novelChapter = await _chapterService.GetByNovelId(novelId);

                return Ok(novelChapter);
            }
            catch (FailedCreatingNewException exception) {
                return BadRequest(exception.Message);
            }
            catch (NovelChapterNumberExistsException exception) {
                return BadRequest(exception.Message);
            }
            catch (NoRecordFoundException exception) {
                return BadRequest(exception.Message);
            }
            catch (Exception exception) {
                return BadRequest(exception.Message);
            }
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
            catch (NoRecordFoundException exception) {
                return BadRequest(exception.Message);
            }
            catch (Exception exception) {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("{novelId}/chapters/{chapterNumber}/publish")]
        public async Task<IActionResult> Publish(int novelId, int chapterNumber) {

            try {
                return Ok(await _chapterService
                    .Publish(novelId, chapterNumber)
                    .ConfigureAwait(false));
            }
            catch (FailedToPublishChapterException exception) {
                return BadRequest(exception.Message);
            }
            catch (NovelChapterNotFoundException exception) {
                return BadRequest(exception.Message);
            }
            catch (ChapterAlreadyPublished exception) {
                return BadRequest(exception.Message);
            }
            catch (Exception exception) {
                return BadRequest(exception.Message);
            }
        }
    }

}