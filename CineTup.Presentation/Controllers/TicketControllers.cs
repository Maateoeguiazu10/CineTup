using CineTup.Application.Abstractions;
using CineTup.Application.Abstractions.Infraestructure;
using CineTup.Application.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineTup.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketControllers : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketControllers(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public ActionResult<TicketResponse> GetAll()
        {
            var ticket = _ticketService.GetAll();
            if (!ticket.Any())
                return NotFound();
            return Ok(ticket);
        }
        [HttpGet("{id}")]
        public ActionResult<TicketResponse> GetById([FromRoute] int id)
        {
            var ticket = _ticketService.GetById(id);
            if (ticket == null)
                return NotFound();
            return Ok(ticket);
        }
    }
}
