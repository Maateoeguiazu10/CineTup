using CineTup.Application.Abstractions;
using CineTup.Application.Abstractions.Infraestructure;
using CineTup.Application.Mapper;
using CineTup.Application.Requests;
using CineTup.Application.Responses;
using CineTup.Domain.Entities;

namespace CineTup.Application.Services
{
    public class ShowTimeService : IShowTimeService
    {
        private readonly IShowTimeRepository _showTimeRepository;
        private readonly ITicketRepository _ticketRepository;

        public ShowTimeService(
            IShowTimeRepository showTimeRepository,
            ITicketRepository ticketRepository)
        {
            _showTimeRepository = showTimeRepository;
            _ticketRepository = ticketRepository;
        }

        public List<ShowTimeResponse> GetAll()
        {
            return _showTimeRepository
                .GetAll()
                .OrderBy(x => x.StartTime)
                .Select(x => x.ToShowTimeResponse())
                .ToList();
        }

        public ShowTimeResponse? GetById(int id)
        {
            return _showTimeRepository
                .GetById(id)?
                .ToShowTimeResponse();
        }

        public ShowTimeResponse Create(ShowTimeRequest request)
        {
            var newShowTime = request.ToShowTime();

            _showTimeRepository.Add(newShowTime);

            for (int seat = 1; seat <= 30; seat++)
            {
                _ticketRepository.Add(new Ticket
                {
                    ShowTimeId = newShowTime.Id,
                    SeatNumber = seat,
                    IsAvailable = true
                });
            }

            return newShowTime.ToShowTimeResponse();
        }

        public bool Update(ShowTimeRequest request, int id)
        {
            var showTimeToUpdate = _showTimeRepository.GetById(id);

            if (showTimeToUpdate == null)
                return false;

            showTimeToUpdate.MovieId = request.MovieId;
            showTimeToUpdate.StartTime = request.StartTime;
            showTimeToUpdate.TicketPrice = request.TicketPrice;

            _showTimeRepository.Update(showTimeToUpdate);

            return true;
        }

        public bool Delete(int id)
        {
            var showTime = _showTimeRepository.GetById(id);

            if (showTime == null)
                return false;

            _showTimeRepository.Delete(id);

            return true;
        }
    }
}