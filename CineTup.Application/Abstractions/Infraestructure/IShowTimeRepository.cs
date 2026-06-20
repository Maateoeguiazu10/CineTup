using CineTup.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Application.Abstractions.Infraestructure
{
    public interface IShowTimeRepository : IBaseRepository<ShowTime>
    {
        bool ExistsOverlappingShowTime(DateTime startTime, DateTime endTime);
    }
}
