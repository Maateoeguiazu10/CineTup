using CineTup.Application.Abstractions;
using CineTup.Application.Exceptions;
using CineTup.Application.Responses;
using CineTup.Presentation.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace CineTup.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [Authorize(Policy = Policies.AdminOnly)]
        [HttpGet]
        public ActionResult<List<TicketResponse>> GetAll()
        {
            try
            {
                var tickets = _ticketService.GetAll();
                if (!tickets.Any())
                    return NotFound("No se encontraron tickets.");
                return Ok(tickets);
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }

        [Authorize(Policy = Policies.AdminOnly)]
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
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }

        [Authorize(Policy = Policies.ClientOnly)]
        [HttpPost("{id}/buy")]
        public ActionResult<TicketResponse> BuyTicket([FromRoute] int id)
        {
            try
            {
                var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value 
                                  ?? User.FindFirst("sub")?.Value;

                if (userIdClaim == null || !int.TryParse(userIdClaim, out int clientId))
                {
                    return Unauthorized("No se pudo identificar al cliente.");
                }

                var ticket = _ticketService.BuyTicket(id, clientId);
                return Ok(ticket);
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ConflictException ex)
            {
                return Conflict(ex.Message);
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }

        [HttpGet("showtime/{showTimeId}/available")]
        public ActionResult<List<TicketResponse>> GetAvailableTickets([FromRoute] int showTimeId)
        {
            try
            {
                var tickets = _ticketService.GetAvailableTickets(showTimeId);
                return Ok(tickets);
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error inesperado.");
            }
        }
    }
}
