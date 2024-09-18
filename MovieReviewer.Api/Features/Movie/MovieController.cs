using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using MovieReviewer.Shared.View;
using FluentValidation;

namespace MovieReviewer.Api.Features.Movie
{
    [ApiController]
    [TranslateResultToActionResult]
    [Route("[controller]")]
    public class MovieController(MovieRepository movieRepository) : ControllerBase
    {
        private readonly MovieUpdateValidator _updateValidator = new();

        [HttpGet("{id}")]
        public async Task<Result<MovieViewModel>> GetMovieByIdAsync([Required] int id)
        {
            var result = await movieRepository.GetMovieById(id);
            return result;
        }

        [HttpGet]
        public async Task<Result<List<MovieViewModel>>> GetMovies()
        {
            var result = await movieRepository.GetAllMovies();
            return result;
        }

        [HttpPost]
        public async Task<Result> CreateMovie([Required] string ImdbId)
        {
            var result = await movieRepository.CreateMovie(ImdbId);
            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Result> DeleteMovie(int id)
        {
            var result = await movieRepository.DeleteMovie(id);
            return result;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie([Required] int id, [FromBody] MovieUpdateModel movieUpdateModel)
        {
            var validationResult = _updateValidator.Validate(movieUpdateModel);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var responseResult = await movieRepository.UpdateMovie(id, movieUpdateModel);
            return result;
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
