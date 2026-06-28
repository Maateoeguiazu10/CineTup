using CineTup.Application.Abstractions;
using CineTup.Application.Abstractions.Infraestructure;
using CineTup.Application.Exceptions;
using CineTup.Application.Mapper;
using CineTup.Application.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public TicketResponse GetById(int id)
        {
            var ticket = _ticketRepository.GetById(id);
            if (ticket == null)
                throw new NotFoundException("Ticket not found");
            return ticket.ToTicketResponse();
        }

        public void Delete(int id)
        {
            var ticket = _ticketRepository.GetById(id);

            if (ticket == null)
                throw new NotFoundException("Ticket not found");


            _ticketRepository.Delete(id);

        }

        public TicketResponse BuyTicket(int ticketId, int clientId)
        {
            var ticket = _ticketRepository.GetById(ticketId);
            if (ticket == null)
                throw new NotFoundException("Ticket no encontrado.");

            if (!ticket.IsAvailable)
                throw new ConflictException("El ticket ya ha sido comprado.");

            ticket.IsAvailable = false;
            ticket.ClientId = clientId;
            ticket.PurchaseDate = DateTime.Now;
            _ticketRepository.Update(ticket);
            return ticket.ToTicketResponse();
        }

        public List<TicketResponse> GetAvailableTickets(int showTimeId)
        {
            return _ticketRepository
                .GetAvailableTickets(showTimeId)
                .Select(x => x.ToTicketResponse())
                .ToList();
        }
    }
}
