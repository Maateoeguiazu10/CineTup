using CineTup.Application.Abstractions.Infraestructure;
using CineTup.Domain.Entities;
using CineTup.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
namespace CineTup.Infraestucture.Persistence.Repository
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(CineTupDbContext context) : base(context)
        {
        }

        public async Task<bool> IsSeatSoldAsync(int showTimeId, int seatNumber)
        {
            return await _dbSet.AnyAsync(t =>
                !t.IsDeleted &&
                t.ShowTimeId == showTimeId &&
                t.SeatNumber == seatNumber &&
                !t.IsAvailable);
        }

        public async Task<List<Ticket>> GetAvailableTicketsAsync(int showTimeId)
        {
            return await _dbSet.Where(t =>
                !t.IsDeleted &&
                t.ShowTimeId == showTimeId &&
                t.IsAvailable)
                .ToListAsync();
        }
    }
}