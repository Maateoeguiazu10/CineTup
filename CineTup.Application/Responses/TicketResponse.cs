using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Application.Responses
{
    public class TicketResponse
    {
        public int Id { get; set; }
        public int ShowTimeId { get; set; }
        public int SeatNumber { get; set; }
        public bool IsAvailable { get; set; }

        public DateTime? PurchaseDate { get; set; }
        public int? ClientId { get; set; }
    }
}
