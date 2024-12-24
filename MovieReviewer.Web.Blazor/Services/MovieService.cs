using Ardalis.Result;
using MovieReviewer.Shared.Domain.Entities;
using MovieReviewer.Shared.Domain.Interfaces;
using MovieReviewer.Shared.Dto.Input;
using MovieReviewer.Shared.Dto.Output;
using MovieReviewer.Shared.Service;
using MovieReviewer.Web.Blazor.Helpers;

namespace MovieReviewer.Web.Blazor.Services
{
    public class MovieService(IMovieRepository movieRepository, IMovieClient movieClient) : IMovieService
    {
        public async Task<Result> CreateMovieAsync(CreateMovieDto entity)
        {
            var isMovie = await movieRepository.ExistsByImdbId(entity.ImdbId);

            if (isMovie)
                return Result.Conflict();

            await movieRepository.Add(entity.ToMovie());
            return Result.Success();
        }

        public async Task<Result> DeleteMovieAsync(int id)
        {
            var item = await movieRepository.GetById(id);
            if (item is null)
                return Result.NotFound();
            await movieRepository.Delete(item);
            return Result.Success();
        }

        public IReadOnlyList<MovieViewDto> GetAllMoviesAsync()
        {
            var items = movieRepository.GetAll().Select(x => x.ToMovieView()).ToList();
            return items;
        }

        public async Task<Result<MovieViewDto>> GetMovieAsync(int id)
        {
            var item = await movieRepository.GetById(id);
            if (item is null)
                return Result.Error();
            return item.ToMovieView();
        }

        public async Task<Result<CreateMovieDto>> GetMovieDataAsync(string imdbId)
        {
            var movieInfo = await movieClient.GetMovieInfoFromExternalApiAsync(imdbId);
            return movieInfo is null ? Result.Error() : Result.Success(movieInfo.ToCreateMovie());
        }

        public async Task<Result> UpdateMovieAsync(int id, UpdateMovieDto entity)
        {
            var item = await movieRepository.GetById(id);
            if (item is null)
                return Result.Error();
            UpdateMovie(entity, item);
            await movieRepository.Update(item);
            return Result.Success();
        }

        private void UpdateMovie(UpdateMovieDto updateEntity, Movie entity)
        {
            entity.Title = updateEntity.Title;
            entity.Plot = updateEntity.Plot;
            entity.MovieRating = updateEntity.MovieRating;
            entity.MovieLanguage = updateEntity.MovieLanguage;
            entity.ImdbRating = updateEntity.ImdbRating;
            entity.IsDisabled = updateEntity.Disabled;
        }
    }
}
