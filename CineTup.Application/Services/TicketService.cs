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
        public async Task<List<TicketResponse>> GetAllAsync()
        {
            var tickets = await _ticketRepository.GetAllAsync();
            return tickets
                .Select(x => x.ToTicketResponse())
                .ToList();
        }

        public async Task<TicketResponse> GetByIdAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);
            if (ticket == null)
                throw new NotFoundException("Ticket not found");
            return ticket.ToTicketResponse();
        }

        public async Task DeleteAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);

            if (ticket == null)
                throw new NotFoundException("Ticket not found");


            await _ticketRepository.DeleteAsync(id);

        }

        public async Task<TicketResponse> BuyTicketAsync(int ticketId, int clientId)
        {
            var ticket = await _ticketRepository.GetByIdAsync(ticketId);
            if (ticket == null)
                throw new NotFoundException("Ticket no encontrado.");

            if (!ticket.IsAvailable)
                throw new ConflictException("El ticket ya ha sido comprado.");

            ticket.IsAvailable = false;
            ticket.ClientId = clientId;
            ticket.PurchaseDate = DateTime.Now;
            await _ticketRepository.UpdateAsync(ticket);
            return ticket.ToTicketResponse();
        }

        public async Task<List<TicketResponse>> GetAvailableTicketsAsync(int showTimeId)
        {
            var tickets = await _ticketRepository.GetAvailableTicketsAsync(showTimeId);
            return tickets
                .Select(x => x.ToTicketResponse())
                .ToList();
        }
    }
}
