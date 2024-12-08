using MovieReviewer.Shared.Domain.Interfaces;

namespace MovieReviewer.Web.Blazor.Services;

public class MovieClient(IHttpClientFactory httpClientFactory) : IMovieClient
{
    public Task<ImdbMovieDTO?> GetMovieInfoFromExternalApiAsync(string imdbId)
    {
        throw new NotImplementedException();
    }

    public async Task<string> GetRawDataFromApi(string imdbId)
    {
        var client = httpClientFactory.CreateClient("TmDb");
        
        var url = await client.GetAsync()

        return "a";
    }
}