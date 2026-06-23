using System;
using System.Collections.Generic;
using System.Text;
using CineTup.Application.Abstractions;
using CineTup.Application.Abstractions.Infraestructure;
using CineTup.Application.Requests;
using CineTup.Application.Responses;
using CineTup.Application.Mapper;
using CineTup.Domain.Entities;
using CineTup.Application.Exceptions;

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

        public MovieResponse GetById(int id)
        {
            var movie = _movieRepository.GetById(id);
            if (movie == null) 
                throw new NotFoundException("Movie not found");
            return movie.ToMovieResponse();
        }

        public MovieResponse Create(MovieRequest movie)
        {
            var newMovie = movie.ToMovie();
            _movieRepository.Add(newMovie);
            return newMovie.ToMovieResponse();
        }

        public void Delete(int id)
        {
            var movie = _movieRepository.GetById(id);

            if (movie == null)
                throw new NotFoundException("No se encontro la pelicula con id '{id}'");

            _movieRepository.Delete(id);
        }
        public void Update(MovieRequest movie, int id)
        {
            var movieToUpdate = _movieRepository.GetById(id);

            if (movieToUpdate == null)
                throw new NotFoundException("No se encontro la pelicula con id '{id}'");


            movieToUpdate.Title = movie.Title;
            movieToUpdate.Director = movie.Director;
            movieToUpdate.Category = movie.Category;
            movieToUpdate.Summary = movie.Summary;
            movieToUpdate.ImageUrl = movie.ImageUrl;
            movieToUpdate.BannerUrl = movie.BannerUrl;
            movieToUpdate.Duration = movie.Duration;
            movieToUpdate.Language = movie.Language;
            movieToUpdate.IsAvailable = movie.IsAvailable;


            _movieRepository.Update(movieToUpdate);
        }
    }
}
