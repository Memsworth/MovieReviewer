using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Api.Features.Movie;

namespace MovieReviewer.Api.Features.Review
{
    public class ReviewService(ReviewRepository reviewRepository, MovieService movieService)
    {
        public async Task<Result> CreateReview(ReviewCreateModel review, int movieId)
        {
            //TODO: I am checking if movie is in the db. Should this even be here? Come back and perform test/write tests
            var movieResult = await movieService.GetMovieById(movieId);
            if (!movieResult.IsSuccess)
                return Result.NotFound();

            var item = review.ToReviewEntity(movieId);
            await reviewRepository.Create(item);
            return Result.Success();
        }

        public async Task<Result<ReviewViewModel>> GetReviewById(int reviewId)
        {
            var item = await reviewRepository.GetById(reviewId);

            if (item is null || item.IsDeleted)
                return Result.NotFound();

            return Result.Success(item.ToReviewViewModel());
        }

        public async Task<List<ReviewViewModel>> GetAllReviews()
        {
            var items = await reviewRepository.GetAll().Where(x => x.IsDeleted != true)
                .Select(x => x.ToReviewViewModel()).ToListAsync();
            
            return items;
        }

        public async Task<Result> UpdateReview(int reviewId ,ReviewCreateModel reviewUpdate)
        {
            var review = await reviewRepository.GetById(reviewId);

            if (review is null)
                return Result.NotFound();

            review.Content = reviewUpdate.ReviewContent;
            review.ReviewScore = reviewUpdate.ReviewScore;

            await reviewRepository.Update();
            return Result.Success();
        }

        public async Task<Result> DeleteReivew(int reviewId)
        {
            var review = await reviewRepository.GetById(reviewId);

            if (review is null)
                return Result.NotFound();

            if (review.IsDeleted)
                return Result.Unavailable();

            review.IsDeleted = true;
            await reviewRepository.Delete();
            return Result.Success();
        }
    }
}
