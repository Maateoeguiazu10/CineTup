using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Domain.Entities
{
    public class Client : User
    {
        public ICollection<Ticket> Tickets { get; set; } = new List<Ticket>();
    }
}
