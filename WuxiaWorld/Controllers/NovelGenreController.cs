namespace WuxiaWorld.Controllers {

    using System;
    using System.Threading.Tasks;

    using BLL.ActionFilters;
    using BLL.Exceptions;
    using BLL.Services.Interfaces;

    using DAL.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    [Route("api/novels")]
    [TypeFilter(typeof(DbContextActionFilter))]
    public class NovelGenreController : Controller {

        private readonly INovelGenreService _novelGenreService;

        public NovelGenreController(INovelGenreService novelGenreService) {

            _novelGenreService = novelGenreService ?? throw new ArgumentNullException(nameof(novelGenreService));
        }

        [HttpPost]
        [Route("{novelId}/genre")]
        public async Task<IActionResult> Publish(int novelId, [FromBody] NovelGenreModel input) {

            try {
                await _novelGenreService
                    .Assign(novelId, input.GenreIds)
                    .ConfigureAwait(false);

                return Ok();
            }
            catch (NoRecordFoundException exception) {
                return BadRequest(exception.Message);
            }
            catch (NovelNotFoundException exception) {
                return BadRequest(exception.Message);
            }
        }
    }

}