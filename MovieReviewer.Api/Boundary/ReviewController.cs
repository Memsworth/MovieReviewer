﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MovieReviewer.Api.Control.Repository;

namespace MovieReviewer.Api.Boundary
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ReviewController(ReviewRepository reviewRepository) : ControllerBase
    {
        [HttpGet("{id}")]
        public async Task<IActionResult> GetReviewByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        [HttpGet]
        public async Task<IActionResult> GetReviews()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview()
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReview(int id)
        {
            throw new NotImplementedException();
        }
    }
}
