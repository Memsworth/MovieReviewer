using Ardalis.Result;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using MovieReviewer.Shared.Core.DTO.Inputs;
using MovieReviewer.Shared.Core.Validation;

namespace MovieReviewer.Api.Features.Review
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController(ReviewService reviewService) : ControllerBase
    {
        private readonly CreateReviewInputValidation _createValidator = new();
        private readonly UpdateReviewInputValidation _updateValidator = new();

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewByIdAsync([Required] int id)
        {
            var result = await reviewService.GetReviewById(id);
            if (!result.IsSuccess)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpGet("GetAll/{id}")]
        public async Task<IActionResult> GetReviewsByMovieIdAsync([Required] int id)
        {
            var items = await reviewService.GetReviewsByMovieId(id);
            return items is null ? NotFound() : Ok(items);
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews()
        {
            var items = await reviewService.GetAllReviews();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([Required] int movieId, CreateReviewInputModel reviewCreate)
        {
            var validationResult = await _createValidator.ValidateAsync(reviewCreate);
            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            var result = await reviewService.CreateReview(reviewCreate, movieId);

            if (result.IsNotFound())
                return NotFound();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview([Required] int id)
        {
            var result = await reviewService.DeleteReview(id);

            if (!result.IsSuccess)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview([Required] int id, UpdateReviewInputModel reviewUpdate)
        {
            var validationResult = await _updateValidator.ValidateAsync(reviewUpdate);
            if (!validationResult.IsValid)
                return BadRequest(validationResult);

            var result = await reviewService.UpdateReview(id, reviewUpdate);

            if (result.IsNotFound())
                return NotFound();

            if (result.IsUnavailable())
                return UnprocessableEntity();

            return NoContent();
        }
    }
}
