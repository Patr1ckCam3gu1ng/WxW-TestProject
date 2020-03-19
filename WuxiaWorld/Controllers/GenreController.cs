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

        public GenreController(IGenreService genreService) {

            _genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
        }

        [HttpGet]
        public async Task<IActionResult> Get() {

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
        [Route("{genreName}")]
        public async Task<IActionResult> Get(string genreName) {

            try {
                return Ok(await _genreService.GetByName(genreName));
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
        public async Task<IActionResult> New([FromBody] GenreModel input) {

            try {
                return Ok(await _genreService.Create(input));
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
        }
    }

}