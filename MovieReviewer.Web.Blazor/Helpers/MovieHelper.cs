using MovieReviewer.Shared.Domain.Entities;
using MovieReviewer.Shared.Domain.Enums;
using MovieReviewer.Shared.Domain.Interfaces;
using MovieReviewer.Shared.Dto.Input;
using MovieReviewer.Shared.Dto.Output;

namespace MovieReviewer.Web.Blazor.Helpers
{
    public static class MovieHelper
    {
        public static MovieViewDto ToMovieView(this Movie entity)
        {
            return new MovieViewDto
            {
                Id = entity.Id,
                Title = entity.Title,
                Plot = entity.Plot,
                MovieRating = entity.MovieRating,
                MovieLanguage = entity.MovieLanguage,
                ImdbId = entity.ImdbId,
                ImdbRating = entity.ImdbRating
            };
        }

        public static Movie ToMovie(this CreateMovieDto entity)
        {
            return new Movie
            {
                Title = entity.Title,
                Plot = entity.Plot,
                MovieRating = entity.MovieRating,
                MovieLanguage = entity.MovieLanguage,
                ImdbId = entity.ImdbId,
                ImdbRating = entity.ImdbRating,
                LastUpdatedAt = DateTime.UtcNow,
                IsDisabled = false,
            };
        }

        public static CreateMovieDto ToCreateMovie(this ImdbMovieDTO entity)
        {
            return new CreateMovieDto()
            {
                Title = entity.Title,
                Plot = entity.Plot,
                ImdbId = entity.ImDbId,
                MovieRating = ParseMovieRating(entity.Rated),
                MovieLanguage = ParseMovieLanguage(entity.Language),
                ImdbRating = double.Parse(entity.ImDbRating),
            };
        }

        private static RatingSystem ParseMovieRating(string rated)
        {
            return rated.ToLower() switch
            {
                "g" => RatingSystem.White,
                "pg" => RatingSystem.Yellow,
                "pg-13" => RatingSystem.Purple,
                "r" => RatingSystem.Red,
                "nc-17" => RatingSystem.Black,
                _ => RatingSystem.NotRated
            };
        }

        private static Language ParseMovieLanguage(string language)
        {
            return language.ToLower() switch
            {
                "english" => Language.En,
                "german" or "deutsche" => Language.De,
                "french" => Language.Fr,
                "korean" => Language.Ko,
                "hindi" => Language.Hi,
                "arabic" => Language.Ar,
                "japanese" => Language.Ja,
                "chinese" => Language.Zh,
                _ => Language.Other
            };
        }

    }
}
