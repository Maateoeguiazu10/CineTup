using CineTup.Application.Abstractions;
using CineTup.Application.Exceptions;
using CineTup.Application.Requests;
using CineTup.Application.Responses;
using CineTup.Application.Services;
using CineTup.Domain.Entities;
using CineTup.Presentation.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineTup.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ShowTimeController : ControllerBase
    {
        private readonly IShowTimeService _showTimeService;

        public ShowTimeController(IShowTimeService showTimeService)
        {
            _showTimeService = showTimeService;
        }

        [HttpGet]
        public ActionResult<ShowTimeResponse> GetAll()
        {
            try
            {
                var showTimes = _showTimeService.GetAll();
                if (!showTimes.Any())
                    return NotFound("No se encontraron funciones.");
                return Ok(showTimes);
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<ShowTimeResponse> GetById([FromRoute] int id)
        {
            try
            {
                return Ok(_showTimeService.GetById(id));
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

        [Authorize(Policy = Policies.AdminOnly)]
        [HttpPost]
        public ActionResult<ShowTimeResponse> Create([FromBody] ShowTimeRequest showTime)
        {
            try
            {
                var createdShowTime = _showTimeService.Create(showTime);
                return CreatedAtAction(nameof(GetById), new { id = createdShowTime.Id }, createdShowTime);
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [Authorize(Policy = Policies.AdminOnly)]
        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            try
            {
                _showTimeService.Delete(id);
                return NoContent();
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

        [Authorize(Policy = Policies.AdminOnly)]
        [HttpPut("{id}")]
        public ActionResult Update(
            [FromBody] ShowTimeRequest showTime,
            [FromRoute] int id)
        {
            try
            {
                _showTimeService.Update(showTime, id);
                return NoContent();
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
