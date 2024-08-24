using Microsoft.Extensions.Options;
using MovieReviewer.Api.Utilities;
using Newtonsoft.Json;

namespace MovieReviewer.Api.Features.Services
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

        public async Task<MovieInformation?> GetMovieInfo(string imdbId)
        {
            var result = await GenerateDataFromExternalApi(imdbId);
            if (result is null)
                return null;

            return new MovieInformation
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
            var url = $"?i={imdbId}&apikey={_settings.Value.ApiKey}";
            var apiResponse = await _httpClient.GetAsync(url);
            var result = await ParseRawDataIntoObjects(apiResponse.Content);
            return result is not null ? result : null;
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
}