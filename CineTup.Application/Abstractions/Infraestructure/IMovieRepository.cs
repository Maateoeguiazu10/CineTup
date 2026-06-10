using System;
using System.Collections.Generic;
using System.Text;
using CineTup.Domain.Entities;

namespace CineTup.Application.Abstractions.Infraestructure
{
    public interface IMovieRepository : IBaseRepository<Movie>
    {
    }
}
