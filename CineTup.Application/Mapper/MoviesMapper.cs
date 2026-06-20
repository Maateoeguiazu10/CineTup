using CineTup.Application.Requests;
using CineTup.Application.Responses;
using CineTup.Domain.Entities;

namespace CineTup.Application.Mapper
{
    public static class MoviesMapper
    {
        public static MovieResponse ToMovieResponse(this Movie movie)
        {
            return new MovieResponse
            {
                Id = movie.Id,
                Title = movie.Title,
                Director = movie.Director,
                Category = movie.Category,
                Summary = movie.Summary,
                ImageUrl = movie.ImageUrl,
                BannerUrl = movie.BannerUrl,
                Duration = movie.Duration,
                Language = movie.Language,
                IsAvailable = movie.IsAvailable,
            };
        }

        public static Movie ToMovie(this MovieRequest movieRequest)
        {
            return new Movie
            {
                Title = movieRequest.Title,
                Director = movieRequest.Director,
                Category = movieRequest.Category,
                Summary = movieRequest.Summary,
                ImageUrl = movieRequest.ImageUrl,
                BannerUrl = movieRequest.BannerUrl,
                Duration = movieRequest.Duration,
                Language = movieRequest.Language,
                IsAvailable = movieRequest.IsAvailable,
                IsDeleted = false
            };
        }
    }
}
        