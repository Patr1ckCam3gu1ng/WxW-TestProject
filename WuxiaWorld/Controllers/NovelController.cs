namespace WuxiaWorld.Controllers {

    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using BLL.ActionFilters;
    using BLL.Exceptions;
    using BLL.Services.Interfaces;

    using DAL.Models;

    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/novels")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [TypeFilter(typeof(DbContextActionFilter))]
    public class NovelController : Controller {

        private readonly INovelService _novelService;

        public NovelController(INovelService novelService) {

            _novelService = novelService ?? throw new ArgumentNullException(nameof(novelService));
        }

        [HttpGet]
        [HttpGet("{novelId}")]
        public async Task<IActionResult> Get(int? novelId) {

            try {
                if (novelId == null) {
                    return Ok(await _novelService.GetAll());
                }

                var novel = await _novelService.GetAll(novelId);

                return Ok(novel.FirstOrDefault());
            }
            catch (NoRecordFoundException exception) {
                return BadRequest(exception.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(AdminOnlyActionFilter))]
        public async Task<IActionResult> New([FromBody] NovelModel[] novels) {

            try {
                return Ok(await _novelService.Create(novels));
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