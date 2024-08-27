using System.Text.Json;
using System.Text.Json.Serialization;
using MovieReviewer.Shared.View;

namespace MovieReviewer.WebUI.Client.Services;

public class MovieReviewerApiService
{
    private readonly HttpClient _httpClient;

    public MovieReviewerApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("http://localhost:5161/");
    }

    public async Task<List<MovieViewModel>?> GetMoviesAsync()
    {
        var response = await _httpClient.GetAsync("/Movie");
        if (!response.IsSuccessStatusCode)
            return null;
        var data = await response.Content.ReadAsStringAsync();
        var movies = JsonSerializer.Deserialize<List<MovieViewModel>>(data);
        return movies;
    }
}