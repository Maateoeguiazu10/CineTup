using CineTup.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Application.Abstractions.Infraestructure
{
    public interface IShowTimeRepository : IBaseRepository<ShowTime>
    {
        Task<bool> ExistsOverlappingShowTimeAsync(int movieId, DateTime startTime, DateTime endTime, int? excludeShowTimeId = null);
    }
}
