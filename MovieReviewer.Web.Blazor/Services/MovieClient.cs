using Microsoft.Extensions.Options;
using MovieReviewer.Shared.Domain.Interfaces;
using Newtonsoft.Json;

namespace MovieReviewer.Web.Blazor.Services;

public class MovieClient(IHttpClientFactory httpClientFactory, IOptions<TmDb> options) : IMovieClient
{
    public async Task<ImdbMovieDTO?> GetMovieInfoFromExternalApiAsync(string imdbId)
    {
        var result = await GenerateDataFromExternalApi(imdbId);
        if (result is null)
            return null;

        return new ImdbMovieDTO
        {
            Title = result.Title,
            Rated = result.Rated,
            Plot = result.Plot,
            Language = result.Language,
            ImDbRating = result.ImDbRating,
            ImDbId = result.ImDbId
        };
    }

    private async Task<OmDbMovieDataResponse?> GenerateDataFromExternalApi(string imdbId)
    {
        var client = httpClientFactory.CreateClient("TmDb");
        var url = $"?i={imdbId}&apikey={options.Value.TmDbApiKey}";
        var apiResponse = await client.GetAsync(url);
        var result = await ParseRawDataIntoObjects(apiResponse.Content);
        return result;
    }

    private async Task<OmDbMovieDataResponse?> ParseRawDataIntoObjects(HttpContent content)
    {
        var contentText = await content.ReadAsStringAsync();
        try
        {
            var response = JsonConvert.DeserializeObject<OmDbResponse>(contentText);
            if (response.Response != "True") return null;
            var movieDataRaw = JsonConvert.DeserializeObject<OmDbMovieDataResponse>(contentText);
            return movieDataRaw;
        }
        catch (JsonException exception)
        {
            return null;
        }
    }
}
public class OmDbResponse
{
    public required string Response { get; set; }
}
public class OmDbMovieDataResponse : OmDbResponse
{
    public required string Title { get; set; }
    public required string Year { get; set; }
    public required string Rated { get; set; }
    public required string Plot { get; set; }
    public required string Language { get; set; }
    public required string ImDbRating { get; set; }
    public required string ImDbId { get; set; }
}