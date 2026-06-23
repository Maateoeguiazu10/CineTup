using CineTup.Application.Abstractions;
using CineTup.Application.Abstractions.Infraestructure;
using CineTup.Application.Exceptions;
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

        public ShowTimeResponse GetById(int id)
        {
            var showTime = _showTimeRepository.GetById(id);
            if (showTime == null)
                throw new NotFoundException("No se encontro la funcion con id '{id}'");
            return showTime.ToShowTimeResponse();
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

        public void Update(ShowTimeRequest request, int id)
        {
            var showTimeToUpdate = _showTimeRepository.GetById(id);

            if (showTimeToUpdate == null)
                throw new NotFoundException("No se encontro la funcion con id '{id}'");

            showTimeToUpdate.MovieId = request.MovieId;
            showTimeToUpdate.StartTime = request.StartTime;
            showTimeToUpdate.TicketPrice = request.TicketPrice;

            _showTimeRepository.Update(showTimeToUpdate);
        }

        public void Delete(int id)
        {
            var showTime = _showTimeRepository.GetById(id);

            if (showTime == null)
                throw new NotFoundException("No se encontro la funcion con id '{id}'");

            _showTimeRepository.Delete(id);
        }
    }
}