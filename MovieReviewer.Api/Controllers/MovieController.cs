﻿using System.ComponentModel.DataAnnotations;
using Ardalis.Result;
using FluentValidation;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using MovieReviewer.Api.Services;
using MovieReviewer.Shared.Dto.Input;

namespace MovieReviewer.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MovieController(MovieService movieService,
        IValidator<CreateMovieDto> createValidator, IValidator<UpdateMovieDto> updateValidator) : ControllerBase
    {

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMovieByIdAsync([Required] int id)
        {
            var result = await movieService.GetMovieByIdAsync(id);

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound();
        }

        [HttpGet("movieInfo/{imdbId}")]
        public async Task<IActionResult> GetMovieInfoAsync([Required] string imdbId)
        {
            //here is IMDBId
            var result = await movieService.GetMovieInfoFromApiAsync(imdbId);
            
            //TODO: change this a bit
            return result.IsSuccess ? Ok(result.Value) : NotFound();
        }
        [HttpGet]
        public IActionResult GetMovies()
        {
            var movies = movieService.GetAllMoviesAsync();
            return Ok(movies);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMovie(CreateMovieDto movieDto)
        {
            var validationResult = await createValidator.ValidateAsync(movieDto);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);
            
            var result = await movieService.CreateMovieAsync(movieDto);

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
        public async Task<IActionResult> UpdateMovie([Required] int id, [FromBody] UpdateMovieDto movieUpdate)
        {
            var validationResult = await updateValidator.ValidateAsync(movieUpdate);
            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var result = await movieService.UpdateMovieAsync(id, movieUpdate);

            if (result.IsSuccess)
                return NoContent();

            return NotFound();
        }
    }
}