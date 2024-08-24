using MovieReviewer.Api.Features.Services;
using MovieReviewer.Shared.Core.Models;
using MovieReviewer.Shared.Core.Utilities;
namespace MovieReviewer.Api.Utilities
{
    public static class Parsers
    {
        public static Movie ParseMovieData(this MovieInformation RawMovieData)
        {
            var movieData = new Movie
            {
                Title = RawMovieData.Title,
                ImdbId = RawMovieData.ImDbId,
                ImdbRating = double.Parse(RawMovieData.ImDbRating),
                LastUpdatedAt = DateTime.UtcNow,
                MovieRating = ParseMeAMovieRating(RawMovieData.Rated),
                MovieLanguage = ParseMeAMovieLanguage(RawMovieData.Language),
                Plot = RawMovieData.Plot,
            };
            return movieData;
        }

        private static RatingSystem ParseMeAMovieRating(string rated)
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

        private static Language ParseMeAMovieLanguage(string language)
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
