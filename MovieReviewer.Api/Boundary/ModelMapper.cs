using MovieReviewer.Api.Boundary.Models;
using MovieReviewer.Api.Entities;

namespace MovieReviewer.Api.Boundary
{
    public static class ModelMapper
    {
        public static MovieViewModel ToMovieViewModel(this Movie movie)
        {
            return new MovieViewModel
            {
                Id = movie.Id,
                Plot = movie.Plot,
                Title = movie.Title,
                MovieRating = movie.MovieRating,
                MovieLanguage = movie.MovieLanguage,
                ImdbId = movie.ImdbId,
                ImdbRating = movie.ImdbRating,
            };
        }

        public static ReviewViewModel ToReviewViewModel(this Review review)
        {
            return new ReviewViewModel
            {
                Id = review.Id,
                ReviewContent = review.ReviewContent,
                ReviewScore = review.ReviewScore,
                MovieId = review.MovieId,
            };
        }

        public static Review ToReviewEntity(this ReviewCreateModel reviewCreateModel, int movieId)
        {
            return new Review
            {
                ReviewContent = reviewCreateModel.ReviewContent,
                ReviewScore = reviewCreateModel.ReviewScore,
                MovieId = movieId
            };
        }

        public static MovieUpdateModel ToMovieUpdateModel(this MovieViewModel movieViewModel)
        {
            return new MovieUpdateModel()
            {
                Title = movieViewModel.Title,
                Plot = movieViewModel.Plot,
                MovieRating = movieViewModel.MovieRating,
                MovieLanguage = movieViewModel.MovieLanguage,
                ImdbRating = movieViewModel.ImdbRating
            };
        }
    }
}
