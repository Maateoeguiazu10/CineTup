using CineTup.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Application.Abstractions
{
    public interface ITicketService
    {
        Task<List<TicketResponse>> GetAllAsync();
        Task<TicketResponse> GetByIdAsync(int id);
        Task DeleteAsync(int id);
        Task<TicketResponse> BuyTicketAsync(int ticketId, int clientId);
        Task<List<TicketResponse>> GetAvailableTicketsAsync(int showTimeId);
    }
}
