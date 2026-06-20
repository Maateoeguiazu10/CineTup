using CineTup.Application.Responses;
using CineTup.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Application.Mapper
{
    public static class TicketMapper
    {
        public static TicketResponse ToTicketResponse(this Ticket ticket)
        {
            return new TicketResponse
            {
                Id = ticket.Id,
                ShowTimeId = ticket.ShowTimeId,
                SeatNumber = ticket.SeatNumber,
                IsAvailable = ticket.IsAvailable,
                PurchaseDate = ticket.PurchaseDate,
                ClientId = ticket.ClientId
            };
        }
    }
}
