using System.Net.Http.Json;
using System.Text.Json.Serialization;
using CineTup.Application.Abstractions;
using Microsoft.Extensions.Configuration;
using static CineTup.Application.Responses.TmdbApiResponse;

namespace CineTup.Infraestucture.ExternalServices
{
    public class TmdbService : ITmdbService
    {
        private readonly HttpClient _httpClient;
        private readonly string _imageBaseUrl;

        public TmdbService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _imageBaseUrl = configuration["Tmdb:ImageBaseUrl"]!;
        }

        public async Task<string?> GetRandomMoviePosterAsync()
        {
            var response = await _httpClient.GetFromJsonAsync<TmdbPopularResponse>("movie/popular");

            if (response?.Results == null || response.Results.Count == 0)
                return null;

            var movie = response.Results[Random.Shared.Next(response.Results.Count)];

            return string.IsNullOrEmpty(movie.PosterPath)
                ? null
                : $"{_imageBaseUrl}{movie.PosterPath}";
        }

    }
}
