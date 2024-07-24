using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReviewer.Api.Control.Repository;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewer.Api.Boundary
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class MovieController(MovieRepository movieRepository) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var result = await movieRepository.GetAllMovies();
            if (!result.IsSuccess)
                return BadRequest();
            return Ok(result.Value);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie([Required] string ImdbId)
        {
            var result = await movieRepository.CreateMovie(ImdbId);
            if (!result.IsSuccess)
                return BadRequest();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateMovie(int id)
        {
            throw new NotImplementedException();
        }
    }
}
