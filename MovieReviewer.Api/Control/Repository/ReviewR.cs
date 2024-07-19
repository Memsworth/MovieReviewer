using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Api.Boundary;
using MovieReviewer.Api.Boundary.Models;
using MovieReviewer.Api.Data;

namespace MovieReviewer.Api.Control.Repository
{
    public class ReviewR(ApplicationDbContext context)
    {
        public async Task<Result> CreateReview(ReviewCreateModel review, int movieId)
        {
            //TODO: I am checking if movie is in the db. Should this even be here? Come back and perform test/write tests
            if (await context.Movies.FirstOrDefaultAsync(x => x.Id == movieId) is null)
                return Result.NotFound();

            var item = review.ToReviewEntity(movieId);
            await context.Reviews.AddAsync(item);
            await context.SaveChangesAsync();
            return Result.Success();
        }
        public async Task<Result<ReviewViewModel>> GetReviewById(int reviewId)
        {
            var item = await context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
            return item is null ? Result.NotFound() : Result.Success(item.ToReviewViewModel());
        }
        public async Task<Result<List<ReviewViewModel>>> GetAllReviews()
        {
            var items = await context.Reviews.Select(x => x.ToReviewViewModel()).ToListAsync();
            return Result.Success(items);
        }

        public async Task<Result> DeleteReview(int reviewId)
        {
            var item = await context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
            if (item is null)
                return Result.NotFound();

            item.IsDisabled = true;
            await context.SaveChangesAsync();
            return Result.NoContent();
        }
        public async Task<Result> UpdateReview(int reviewId, ReviewUpdateModel review)
        {
            var item = await context.Reviews.FirstOrDefaultAsync(x => x.Id == reviewId);
            if (item is null)
                return Result.NotFound();

            item.IsDisabled = review.IsDisabled;
            item.ReviewContent = review.ReviewContent;
            item.ReviewScore = review.ReviewScore;
            await context.SaveChangesAsync();
            return Result.NoContent();
        }
    }
}
