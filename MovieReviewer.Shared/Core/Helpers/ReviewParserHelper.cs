using MovieReviewer.Shared.Core.Domain;
using MovieReviewer.Shared.Core.DTO.Inputs;
using MovieReviewer.Shared.Core.DTO.Outputs;

namespace MovieReviewer.Shared.Core.Helpers;

public static class ReviewParserHelper
{
    public static Review ToReview(this CreateReviewInputModel reviewInputModel, int movieId)
    {
        return new Review()
        {
            Content = reviewInputModel.ReviewContent,
            ReviewScore = reviewInputModel.ReviewScore,
            MovieId = movieId,
        };
    }

    public static ReviewViewDTO TReviewViewDTO(this Review review)
    {
        return new ReviewViewDTO()
        {
            Id = review.Id,
            MovieId = review.MovieId,
            ReviewContent = review.Content,
            ReviewScore = review.ReviewScore
        };
    }
}