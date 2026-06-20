using CineTup.Application.Abstractions.Infraestructure;
using CineTup.Domain.Entities;
using CineTup.Infrastructure.Persistance;

namespace CineTup.Infraestucture.Persistence.Repository
{
    public class TicketRepository : BaseRepository<Ticket>, ITicketRepository
    {
        public TicketRepository(CineTupDbContext context) : base(context)
        {
        }

        public bool IsSeatSold(int showTimeId, int seatNumber)
        {
            return _dbSet.Any(t =>
                !t.IsDeleted &&
                t.ShowTimeId == showTimeId &&
                t.SeatNumber == seatNumber &&
                !t.IsAvailable);
        }

        public List<Ticket> GetAvailableTickets(int showTimeId)
        {
            return _dbSet.Where(t =>
                !t.IsDeleted &&
                t.ShowTimeId == showTimeId &&
                t.IsAvailable)
                .ToList();
        }
    }
}