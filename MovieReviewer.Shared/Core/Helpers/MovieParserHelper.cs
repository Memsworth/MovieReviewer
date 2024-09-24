using MovieReviewer.Shared.Core.Domain;
using MovieReviewer.Shared.Core.DTO.Inputs;
using MovieReviewer.Shared.Core.DTO.Outputs;
using MovieReviewer.Shared.Core.Enums;

namespace MovieReviewer.Shared.Core.Helpers;

public static class MovieParserHelper
{

    public static CreateMovieInputModel ToCreateMovie(this ImdbMovieDTO imdbMovieDTO)
    {
        return new CreateMovieInputModel()
        {
            Title = imdbMovieDTO.Title,
            Plot = imdbMovieDTO.Plot,
            ImdbId = imdbMovieDTO.ImDbId,
            MovieRating = ParseMeAMovieRating(imdbMovieDTO.Rated),
            MovieLanguage = ParseMeAMovieLanguage(imdbMovieDTO.Language),
            ImdbRating = double.Parse(imdbMovieDTO.ImDbRating),
        };
    }
    
    public static MovieViewDTO ToMovieViewDTO(this Movie movie)
    {
        return new MovieViewDTO
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
    
    public static Movie ToMovieModel(this CreateMovieInputModel createMovie)
    {
        var movieData = new Movie
        {
            Title = createMovie.Title,
            ImdbId = createMovie.ImdbId,
            ImdbRating = createMovie.ImdbRating,
            Plot = createMovie.Plot,
            MovieRating = createMovie.MovieRating,
            MovieLanguage = createMovie.MovieLanguage,
            LastUpdatedAt = DateTime.UtcNow,
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