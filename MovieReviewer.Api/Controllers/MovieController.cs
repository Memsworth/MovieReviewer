using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using MovieReviewer.Core.DTO.Inputs;
using MovieReviewer.Service;
using MovieReviewer.Service.Validation;

namespace MovieReviewer.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController(MovieService movieService) : ControllerBase
    {
        private readonly UpdateMovieInputValidation _updateValidator = new();
        private readonly CreateMovieInputValidation _createValidator = new();

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieByIdAsync([Required] int id)
        {
            var result = await movieService.GetMovieByIdAsync(id);

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound();
        }

        [HttpGet("MovieInfo/{imdbId}")]
        public async Task<IActionResult> GetMovieInfoAsync([Required] string imdbId)
        {
            //here is IMDBId
            var result = await movieService.CreateMovieInfoAsync(imdbId);
            
            //TODO: change this a bit
            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await movieService.GetAllMoviesAsync();
            return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(CreateMovieInputModel createMovie)
        {
            var validationResult = await _createValidator.ValidateAsync(createMovie);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            
            var result = await movieService.CreateMovieAsync(createMovie);

            if (result.IsSuccess)
                return Ok(result.Value);

            if (result.IsConflict())
                return Conflict();

            if (result.IsNotFound())
                return NotFound();

            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var result = await movieService.DeleteMovieAsync(id);

            if (result.IsSuccess)
                return NoContent();

            if (result.IsUnavailable())
                return UnprocessableEntity();

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie([Required] int id, [FromBody] UpdateMovieInputModel movieUpdate)
        {
            var validationResult = await _updateValidator.ValidateAsync(movieUpdate);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = await movieService.UpdateMovieAsync(id, movieUpdate);

            if (result.IsSuccess)
                return NoContent();

            return NotFound();
        }
    }
}
