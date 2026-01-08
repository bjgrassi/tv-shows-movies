using ContentService.Services.Dto;

public class MovieServiceClient
{
    // http://movie-service/
    private readonly HttpClient _httpClient;

    public MovieServiceClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<MovieDto>> SearchMoviesByIds(List<int> movieIds)
    {
        var response = await _httpClient.PostAsJsonAsync("Movie/SearchMoviesByIds", movieIds);
        
        if (response.IsSuccessStatusCode)
        {
            return await response.Content.ReadFromJsonAsync<List<MovieDto>>() ?? new List<MovieDto>();
        }

        return new List<MovieDto>();
    }
}