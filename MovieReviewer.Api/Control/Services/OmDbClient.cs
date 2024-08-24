using Ardalis.Result;
using Microsoft.Extensions.Options;
using MovieReviewer.Api.Utilities;
using Newtonsoft.Json;

namespace MovieReviewer.Api.Control.Services
{
    public class OmDbClient : IMovieClient
    {
        private readonly HttpClient _httpClient;
        private readonly IOptions<Settings> _settings;
        public OmDbClient(IOptions<Settings> settings)
        {
            _settings = settings;
            _httpClient = new HttpClient()
            {
                BaseAddress = new Uri("https://www.omdbapi.com/")
            };
        }

        public async Task<Result<MovieInformation>> GetMovieInfo(string imdbId)
        {
            var result = await GenerateDataFromExternalApi(imdbId);
            if (!result.IsSuccess)
                return Result.NotFound();

            return Result.Success(new MovieInformation
            {
                Title = result.Value.Title,
                Rated = result.Value.Rated,
                Plot = result.Value.Plot,
                Language = result.Value.Language,
                ImDbRating = result.Value.ImDbRating,
                ImDbId = result.Value.ImDbId
            });
        }

        private async Task<Result<OmDbMovieDataResponse>> GenerateDataFromExternalApi(string imdbId)
        {
            var url = $"?i={imdbId}&apikey={_settings.Value.ApiKey}";
            var apiResponse = await _httpClient.GetAsync(url);
            var result = await ParseRawDataIntoObjects(apiResponse.Content);
            return result.IsSuccess ? result : Result.Error();
        }

        private async Task<Result<OmDbMovieDataResponse>> ParseRawDataIntoObjects(HttpContent content)
        {
            var contentText = await content.ReadAsStringAsync();
            try
            {
                var response = JsonConvert.DeserializeObject<OmDbResponse>(contentText);
                if (response.Response != "True") return Result.NotFound();
                var movieDataRaw = JsonConvert.DeserializeObject<OmDbMovieDataResponse>(contentText);
                return Result.Success(movieDataRaw);
            }
            catch (JsonException exception)
            {
                return Result.NotFound();
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
}