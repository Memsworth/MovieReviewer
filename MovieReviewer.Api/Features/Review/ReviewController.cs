using Ardalis.Result;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReviewer.Shared.View;
using System.ComponentModel.DataAnnotations;

namespace MovieReviewer.Api.Features.Review
{
    [ApiController]
    [Route("[controller]")]
    public class ReviewController(ReviewService reviewService) : ControllerBase
    {
        private readonly ReviewCreateValidator _createValidator = new();

        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewByIdAsync([Required] int id)
        {
            var result = await reviewService.GetReviewById(id);
            if (!result.IsSuccess)
                return NotFound();

            return Ok(result.Value);
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews()
        {
            var items = await reviewService.GetAllReviews();
            return Ok(items);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview([Required] int movieId, ReviewCreateModel reviewCreate)
        {
            var validationResult = _createValidator.Validate(reviewCreate);
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
            var result = await reviewService.DeleteReivew(id);

            if (!result.IsSuccess)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview([Required] int id, ReviewCreateModel reviewUpdate)
        {
            var validationResult = _createValidator.Validate(reviewUpdate);
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

    public class ReviewCreateValidator : AbstractValidator<ReviewCreateModel>
    {
        public ReviewCreateValidator()
        {
            RuleFor(x => x.ReviewContent).NotEmpty();
            RuleFor(x => x.ReviewScore).InclusiveBetween(1, 10);
        }
    }
}
