using System;
using System.Collections.Generic;
using System.Text;
using CineTup.Application.Abstractions.Infraestructure;
using CineTup.Domain.Entities;
using CineTup.Infrastructure.Persistance;

namespace CineTup.Infraestucture.Persistence.Repository
{
    public class ShowTimeRepository : BaseRepository<ShowTime>, IShowTimeRepository
    {
        public ShowTimeRepository(CineTupDbContext context) : base(context)
        {
        }

        public bool ExistsOverlappingShowTime(DateTime startTime, DateTime endTime)
        {
            return _dbSet.Any(st =>
        !st.IsDeleted &&
        st.StartTime < endTime &&
        st.StartTime.AddMinutes(st.Movie.Duration) > startTime);
        }
    }
}
