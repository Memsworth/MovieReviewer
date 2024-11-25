using System.Text.Json;
using MovieReviewer.Shared.Dto.Output;

namespace MovieReviewer.Web.Services;

public class HomePageService(HttpClient httpClient)
{
    JsonSerializerOptions _options = new(JsonSerializerDefaults.Web);
    public async Task<List<MovieViewDto>> FetchMovies()
    {
        var json = await httpClient.GetStringAsync("/api/Movie");
        return JsonSerializer.Deserialize<List<MovieViewDto>>(json, _options) ?? [];
    }
}