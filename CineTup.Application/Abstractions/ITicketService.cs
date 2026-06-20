using CineTup.Application.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Application.Abstractions
{
    public interface ITicketService
    {
        List<TicketResponse> GetAll();
        TicketResponse? GetById(int id);
        bool Delete(int id);
    }
}
