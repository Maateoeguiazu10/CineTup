using CineTup.Application.Abstractions.Infraestructure;
using CineTup.Domain.Entities;
using CineTup.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace CineTup.Infraestucture.Persistence.Repository
{
    public class ShowTimeRepository : BaseRepository<ShowTime>, IShowTimeRepository
    {
        public ShowTimeRepository(CineTupDbContext context) : base(context)
        {
        }

        public async Task<bool> ExistsOverlappingShowTimeAsync(int movieId, DateTime startTime, DateTime endTime, int? excludeShowTimeId = null)
        {
            return await _dbSet.AnyAsync(st =>
                st.MovieId == movieId &&
                !st.IsDeleted &&
                st.Id != (excludeShowTimeId ?? 0) &&
                st.StartTime < endTime &&
                st.StartTime.AddMinutes(st.Movie.Duration) > startTime);
        }

        public override async Task DeleteAsync(int id)
        {
            var showTime = await _context.Set<ShowTime>()
                .Include(st => st.Tickets)
                .FirstOrDefaultAsync(st => st.Id == id && !st.IsDeleted);

            if (showTime == null) return;

            foreach (var ticket in showTime.Tickets.Where(t => !t.IsDeleted))
            {
                ticket.IsDeleted = true;
                ticket.DeletedDateTime = DateTime.UtcNow;
                ticket.UpdateDateTime = DateTime.UtcNow;
            }

            showTime.IsDeleted = true;
            showTime.DeletedDateTime = DateTime.UtcNow;
            showTime.UpdateDateTime = DateTime.UtcNow;

            await SaveChangesAsync();
        }
    }
}
