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
        MovieResponse? GetById(int id);
        MovieResponse Create(MovieRequest movie);
        bool Update(MovieRequest movie, int id);
        bool Delete(int id);
        
    }
}
