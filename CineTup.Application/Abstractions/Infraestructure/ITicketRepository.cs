using CineTup.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Application.Abstractions.Infraestructure
{
    public interface ITicketRepository : IBaseRepository<Ticket>
    {
        Task<bool> IsSeatSoldAsync(int showTimeId, int seatNumber);
        Task<List<Ticket>> GetAvailableTicketsAsync(int showTimeId);
    }
}
