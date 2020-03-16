namespace WuxiaWorld.Controllers {

    using System;
    using System.Threading.Tasks;

    using BLL.Exceptions;
    using BLL.Services;

    using DAL.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    [Route("api/novels")]
    public class NovelController : Controller {

        private readonly INovelService _novelService;

        public NovelController(INovelService novelService) {

            _novelService = novelService ?? throw new ArgumentNullException(nameof(novelService));
        }

        [HttpPost]
        public async Task<IActionResult> New([FromBody] NovelModel input) {

            try {
                return Ok(await _novelService.Create(input));
            }
            catch (FailedCreatingNewException exception) {
                return BadRequest(exception.Message);
            }
            catch (Exception exception) {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [Route("{novelId}/publish")]
        public IActionResult New(int novelId) {

            return Ok();
        }
    }

}