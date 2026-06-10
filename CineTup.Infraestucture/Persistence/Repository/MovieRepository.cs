using System;
using System.Collections.Generic;
using System.Text;
using CineTup.Application.Abstractions.Infraestructure;
using CineTup.Domain.Entities;
using CineTup.Infrastructure.Persistance;

namespace CineTup.Infraestucture.Persistence.Repository
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(CineTupDbContext context) : base(context)
        {
        }
    }
}
