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

    [Route("api/genres")]
    [TypeFilter(typeof(DbContextActionFilter))]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class GenreController : Controller {

        private readonly IGenreService _genreService;
        private readonly INovelService _novelService;

        public GenreController(IGenreService genreService, INovelService novelService) {

            _genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
            _novelService = novelService ?? throw new ArgumentNullException(nameof(novelService));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() {

            try {
                return Ok(await _genreService.GetAll());
            }
            catch (NoRecordFoundException) {
                return NoContent();
            }
            catch {
                return BadRequest(ExceptionError.Message);
            }
        }

        [HttpGet]
        [Route("{genreId}/novels")]
        public async Task<IActionResult> GetNovelsByGender(int genreId) {

            try {
                var result = await _novelService.GetGenderId(genreId).ConfigureAwait(false);

                return Ok(result);
            }
            catch (NoRecordFoundException) {
                return NoContent();
            }
            catch {
                return BadRequest(ExceptionError.Message);
            }
        }

        [HttpPost]
        [TypeFilter(typeof(AdminOnlyActionFilter))]
        public async Task<IActionResult> New([FromBody] GenreModel[] genres) {

            try {
                return Ok(await _genreService.Create(genres));
            }
            catch (NoRecordFoundException) {
                return NoContent();
            }
            catch (RecordAlreadyExistsException exception) {
                return BadRequest(exception.Message);
            }
            catch (FailedCreatingNewException exception) {
                return BadRequest(exception.Message);
            }
            catch (Exception exception) {
                return BadRequest(exception.Message);
            }

            return BadRequest();
        }
    }

}