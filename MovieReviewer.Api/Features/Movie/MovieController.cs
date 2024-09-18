using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using MovieReviewer.Shared.View;
using FluentValidation;

namespace MovieReviewer.Api.Features.Movie
{
    [ApiController]
    [Route("[controller]")]
    public class MovieController(MovieService movieService) : ControllerBase
    {
        private readonly MovieUpdateValidator _updateValidator = new();

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieByIdAsync([Required] int id)
        {
            var result = await movieService.GetMovieById(id);

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await movieService.GetAllMovies();
            return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([Required] string ImdbId)
        {
            var result = await movieService.CreateMovie(ImdbId);

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
            var result = await movieService.DeleteMovie(id);

            if (result.IsSuccess)
                return NoContent();

            if (result.IsUnavailable())
                return UnprocessableEntity();

            return NotFound();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie([Required] int id, [FromBody] MovieUpdateModel movieUpdateModel)
        {
            var validationResult = _updateValidator.Validate(movieUpdateModel);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = await movieService.UpdateMovie(id, movieUpdateModel);

            if (result.IsSuccess)
                return NoContent();

            return NotFound();
        }
    }

    public class MovieUpdateValidator : AbstractValidator<MovieUpdateModel>
    {
        public MovieUpdateValidator()
        {
            RuleFor(x=> x.Title).NotEmpty();
            RuleFor(x=> x.Plot).MinimumLength(5);
            RuleFor(x => x.MovieRating).NotEmpty();
            RuleFor(x=> x.MovieLanguage).NotEmpty();
            RuleFor(x => x.ImdbRating).InclusiveBetween(1.0, 10.0);
        }
    }
}
