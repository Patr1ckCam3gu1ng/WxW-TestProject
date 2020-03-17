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
    public class NovelController : Controller {

        private readonly INovelService _novelService;

        public NovelController(INovelService novelService) {

            _novelService = novelService ?? throw new ArgumentNullException(nameof(novelService));
        }

        [HttpGet]
        public async Task<IActionResult> Get() {

            try {
                return Ok(await _novelService.GetAll());
            }
            catch (NoRecordFoundException exception) {
                return NoContent();
            }
        }

        [HttpGet("{novelId}")]
        public async Task<IActionResult> GetById(int novelId) {

            try {
                return Ok(await _novelService.GetById(novelId));
            }
            catch (NoRecordFoundException) {
                return NoContent();
            }
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
    }

}