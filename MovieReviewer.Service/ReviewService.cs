using Ardalis.Result;
using Microsoft.EntityFrameworkCore;
using MovieReviewer.Core.DTO.Inputs;
using MovieReviewer.Core.DTO.Outputs;
using MovieReviewer.Core.Infrastructure.Repositories;
using MovieReviewer.Service.Helpers;

namespace MovieReviewer.Service
{
    public class ReviewService(ReviewRepository reviewRepository, MovieService movieService)
    {
        public async Task<Result> CreateReview(CreateReviewInputModel createReview, int movieId)
        {
            //TODO: I am checking if movie is in the db. Should this even be here? Come back and perform test/write tests
            var movieResult = await movieService.MovieExistsById(movieId);
            //this is a code smell. How would I even be submitting an id of a movie if it is not shown?
            //Ask devs later about this
            if (movieResult is false)
                return Result.NotFound();

            var review = createReview.ToReview(movieId);
            await reviewRepository.Create(review);
            return Result.Success();
        }

        public async Task<Result<ReviewViewDTO>> GetReviewById(int reviewId)
        {
            var review = await reviewRepository.GetById(reviewId);

            if (review is null || review.IsDeleted)
                return Result.NotFound();

            return Result.Success(review.ToReviewViewDTO());
        }

        public async Task<List<ReviewViewDTO>> GetAllReviews()
        {
            var items = await reviewRepository.GetAll().Where(x => x.IsDeleted != true)
                .Select(x => x.ToReviewViewDTO()).ToListAsync();
            
            return items;
        }

        public async Task<List<ReviewViewDTO>?> GetReviewsByMovieId(int movieId)
        {
            var movieResult = await movieService.MovieExistsById(movieId);
            //this is a code smell. How would I even be submitting an id of a movie if it is not shown?
            //Ask devs later about this
            if (movieResult is false)
                return null;
            
            var items = await reviewRepository.GetAll().Where(x => x.MovieId == movieId).Select(x => x.ToReviewViewDTO())
                .ToListAsync();
            return items;
        }

        public async Task<Result> UpdateReview(int reviewId ,UpdateReviewInputModel reviewUpdate)
        {
            var review = await reviewRepository.GetById(reviewId);

            if (review is null)
                return Result.NotFound();

            review.Content = reviewUpdate.ReviewContent;
            review.ReviewScore = reviewUpdate.ReviewScore;
            review.IsDeleted = reviewUpdate.IsDisabled;

            await reviewRepository.Update();
            return Result.Success();
        }

        public async Task<Result> DeleteReview(int reviewId)
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
