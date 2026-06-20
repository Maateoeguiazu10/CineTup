using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Application.Requests
{
    public class ShowTimeRequest
    {
        public int MovieId { get; set; }
        public DateTime StartTime { get; set; }
        public decimal TicketPrice { get; set; }
    }
}
