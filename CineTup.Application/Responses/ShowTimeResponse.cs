using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Application.Responses
{
    public class ShowTimeResponse
    {
        public int Id { get; set; }
        public int MovieId { get; set; }
        public DateTime StartTime { get; set; }
        public decimal TicketPrice { get; set; }
    }
}
