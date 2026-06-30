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
        public async Task<List<MovieResponse>> GetAllAsync()
        {
            var movies = await _movieRepository.GetAllAsync();
            return movies
                .OrderBy(x => x.Title)
                .Select(x => x.ToMovieResponse())
                .ToList();

        }

        public async Task<MovieResponse> GetByIdAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie == null)
                throw new NotFoundException("Movie not found");
            return movie.ToMovieResponse();
        }

        public async Task<MovieResponse> CreateAsync(MovieRequest movie)
        {
            var newMovie = movie.ToMovie();
            var createdMovie = await _movieRepository.AddAsync(newMovie);
            return createdMovie.ToMovieResponse();
        }

        public async Task DeleteAsync(int id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);

            if (movie == null)
                throw new NotFoundException("No se encontro la pelicula");

            await _movieRepository.DeleteAsync(id);
        }
        public async Task UpdateAsync(MovieRequest movie, int id)
        {
            var movieToUpdate = await _movieRepository.GetByIdAsync(id);

            if (movieToUpdate == null)
                throw new NotFoundException("No se encontro la pelicula");


            movieToUpdate.Title = movie.Title;
            movieToUpdate.Director = movie.Director;
            movieToUpdate.Category = movie.Category;
            movieToUpdate.Summary = movie.Summary;
            movieToUpdate.ImageUrl = movie.ImageUrl;
            movieToUpdate.BannerUrl = movie.BannerUrl;
            movieToUpdate.Duration = movie.Duration;
            movieToUpdate.Language = movie.Language;
            movieToUpdate.IsAvailable = movie.IsAvailable;


            await _movieRepository.UpdateAsync(movieToUpdate);
        }
    }
}
