using System;
using System.Collections.Generic;
using System.Text;
using CineTup.Application.Requests;
using CineTup.Application.Responses;

namespace CineTup.Application.Abstractions
{
    public interface IMovieService
    {
        List<MovieResponse> GetAll();
        MovieResponse GetById(int id);
        MovieResponse Create(MovieRequest movie);
        void Update(MovieRequest movie, int id);
        void Delete(int id);
        
    }
}
