using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CineTup.Application.Responses
{
    public class TmdbApiResponse
    {
        public class TmdbPopularResponse
        {
            [JsonPropertyName("results")]
            public List<TmdbMovieResult> Results { get; set; } = [];
        }

        public class TmdbMovieResult
        {
            [JsonPropertyName("poster_path")]
            public string? PosterPath { get; set; }
        }
    }
}
