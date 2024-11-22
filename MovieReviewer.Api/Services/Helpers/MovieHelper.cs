using MovieReviewer.Shared.Domain.Entities;
using MovieReviewer.Shared.Domain.Enums;
using MovieReviewer.Shared.Domain.Interfaces;
using MovieReviewer.Shared.Dto.Input;
using MovieReviewer.Shared.Dto.Output;

namespace MovieReviewer.Api.Services.Helpers;

public static class MovieHelper
{
    public static CreateMovieDto ToCreateMovieDto(this ImdbMovieDTO imdbMovieDto)
    {
        return new CreateMovieDto()
        {
            Title = imdbMovieDto.Title,
            Plot = imdbMovieDto.Plot,
            ImdbId = imdbMovieDto.ImDbId,
            MovieRating = ParseMovieRating(imdbMovieDto.Rated),
            MovieLanguage = ParseMovieLanguage(imdbMovieDto.Language),
            ImdbRating = double.Parse(imdbMovieDto.ImDbRating),
        };
    }

    public static UpdateMovieDto ToUpdateMovieDto(this MovieViewDto movie)
    {
        return new UpdateMovieDto()
        {
            Disabled = false,
            ImdbRating = movie.ImdbRating,
            MovieLanguage = movie.MovieLanguage,
            MovieRating = movie.MovieRating,
            Plot = movie.Plot,
            Title = movie.Title
        };
    }
    
    public static MovieViewDto ToMovieViewDto(this Movie movie)
    {
        return new MovieViewDto
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
    
    public static Movie ToMovieModel(this CreateMovieDto createMovie)
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