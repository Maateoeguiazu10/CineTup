using CineTup.Application.Abstractions;
using CineTup.Application.Abstractions.Infraestructure;
using CineTup.Application.Mapper;
using CineTup.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Application.Services
{
    public class TicketService : ITicketService
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketService(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }
        public List<TicketResponse> GetAll()
        {
            return _ticketRepository
                .GetAll()
                .Select(x => x.ToTicketResponse())
                .ToList();
        }

        public TicketResponse? GetById(int id)
        {
            return _ticketRepository
                .GetById(id)?
                .ToTicketResponse();
        }

        public bool Delete(int id)
        {
            var ticket = _ticketRepository.GetById(id);

            if (ticket == null)
                return false;

            _ticketRepository.Delete(id);

            return true;
        }
    }
}
