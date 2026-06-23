using CineTup.Application.Abstractions;
using CineTup.Application.Abstractions.Infraestructure;
using CineTup.Application.Exceptions;
using CineTup.Application.Responses;
using CineTup.Application.Services;
using CineTup.Presentation.Authorization;
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

        [Authorize(Policy = Policies.ClientOnly)]
        [HttpGet]
        public ActionResult<TicketResponse> GetAll()
        {
            try
            {
                var ticket = _ticketService.GetAll();
                if (!ticket.Any())
                    return NotFound();
                return Ok(ticket);
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        [Authorize(Policy = Policies.ClientOnly)]
        [HttpGet("{id}")]
        public ActionResult<TicketResponse> GetById([FromRoute] int id)
        {
            try
            {
                return Ok(_ticketService.GetById(id));
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
