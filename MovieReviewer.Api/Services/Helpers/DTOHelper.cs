﻿/*using MovieReviewer.Core.DTO.Inputs;
using MovieReviewer.Core.DTO.Outputs;
using MovieReviewer.Domain.Domain;

namespace MovieReviewer.Service.Helpers;

public static class DTOHelper
{
    public static ReviewViewDTO ToReviewViewDTO(this Review review)
    {
        return new ReviewViewDTO
        {
            Id = review.Id,
            ReviewContent = review.Content,
            ReviewScore = review.ReviewScore,
            MovieId = review.MovieId,
        };
    }
    
    public static Review ToReviewModel(this CreateReviewInputModel createReview, int movieId)
    {
        return new Review
        {
            Content = createReview.ReviewContent,
            ReviewScore = createReview.ReviewScore,
            MovieId = movieId
        };
    }
}*/