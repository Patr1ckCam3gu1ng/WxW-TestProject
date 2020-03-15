namespace WuxiaWorld.Controllers {

    using System;
    using System.Threading.Tasks;

    using BLL.Exceptions;
    using BLL.Services;

    using DAL.Models;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [AllowAnonymous]
    public class GenreController : BaseController {

        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService) {

            _genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
        }

        [HttpGet]
        [Route("genres")]
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
        [Route("genres/{genreName}")]
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
        [Route("genres")]
        public async Task<IActionResult> New([FromBody] GenreModel input) {

            try {
                return Ok(await _genreService.Create(input));
            }
            catch (NoRecordFoundException) {
                return NoContent();
            }
            catch (GenreAlreadyExistsException exception) {
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