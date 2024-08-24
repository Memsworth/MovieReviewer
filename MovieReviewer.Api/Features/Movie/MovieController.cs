using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using Ardalis.Result.AspNetCore;
using MovieReviewer.Shared.View;

namespace MovieReviewer.Api.Features.Movie
{
    [ApiController]
    [TranslateResultToActionResult]
    [Route("[controller]")]
    public class MovieController(MovieRepository movieRepository) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<Result<MovieViewModel>> GetMovieByIdAsync(int id)
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
        public async Task<Result> UpdateMovie(int id, [FromBody] MovieUpdateModel movieUpdateModel)
        {
            var result = await movieRepository.UpdateMovie(id, movieUpdateModel);
            return result;
        }
    }
}
