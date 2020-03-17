namespace WuxiaWorld.Controllers {

    using System;
    using System.Threading.Tasks;

    using BLL.Services;

    using DAL.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    [Route("api/novels")]
    public class NovelGenreController : Controller {

        private readonly NovelGenreService _novelGenreService;

        public NovelGenreController(NovelGenreService novelGenreService) {

            _novelGenreService = novelGenreService ?? throw new ArgumentNullException(nameof(novelGenreService));
        }

        [HttpPost]
        [Route("{novelId}/genre")]
        public async Task<IActionResult> Publish(int novelId, [FromBody] NovelGenreModel input) {

            await _novelGenreService.Assign(novelId, input.GenreIds).ConfigureAwait(false);

            return Ok();
        }
    }

}