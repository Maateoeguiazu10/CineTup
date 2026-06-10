using System;
using System.Collections.Generic;
using System.Text;
using CineTup.Application.Abstractions;
using CineTup.Application.Abstractions.Infraestructure;
using CineTup.Application.Requests;
using CineTup.Application.Responses;
using CineTup.Application.Mapper;

namespace CineTup.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        public MovieService(IMovieRepository movieRepository)
        {
            _movieRepository = movieRepository;
        }
        public List<MovieResponse> GetAll()
        {
            return _movieRepository
            .GetAll()
            .OrderBy(x => x.Title)
            .Select(x => x.ToMovieResponse()).ToList();

        }

        public MovieResponse? GetById(int id)
        {
            return _movieRepository.GetById(id)?.ToMovieResponse();

        }

        public MovieResponse Create(MovieRequest movie)
        {
            var newMovie = movie.ToMovie();
            _movieRepository.Add(newMovie);
            return newMovie.ToMovieResponse();
        }

        public bool Delete(int id)
        {
            var movie = _movieRepository.GetById(id);

            if (movie == null)
                return false;

            _movieRepository.Delete(id);

            return true;
        }
        public bool Update(MovieRequest movie, int id)
        {
            var movieToUpdate = _movieRepository.GetById(id);

            if (movieToUpdate == null)
                return false;

            movieToUpdate.Title = movie.Title;
            movieToUpdate.Director = movie.Director;
            movieToUpdate.Category = movie.Category;
            movieToUpdate.Summary = movie.Summary;
            movieToUpdate.ImageUrl = movie.ImageUrl;
            movieToUpdate.BannerUrl = movie.BannerUrl;
            movieToUpdate.Duration = movie.Duration;
            movieToUpdate.Language = movie.Language;
            movieToUpdate.IsAvailable = movie.IsAvailable;
            movieToUpdate.ShowTime = movie.ShowTime;


            _movieRepository.Update(movieToUpdate);
            return true;
        }
    }
}
