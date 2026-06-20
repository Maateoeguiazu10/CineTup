using CineTup.Application.Abstractions;
using CineTup.Application.Requests;
using CineTup.Application.Responses;
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
            var showTimes = _showTimeService.GetAll();

            if (!showTimes.Any())
                return NotFound();

            return Ok(showTimes);
        }

        [HttpGet("{id}")]
        public ActionResult<ShowTimeResponse> GetById([FromRoute] int id)
        {
            var showTime = _showTimeService.GetById(id);

            if (showTime == null)
                return NotFound();

            return Ok(showTime);
        }

        [HttpPost]
        public ActionResult<ShowTimeResponse> Create([FromBody] ShowTimeRequest showTime)
        {
            var createdShowTime = _showTimeService.Create(showTime);

            return CreatedAtAction(
                nameof(GetById),
                new { id = createdShowTime.Id },
                createdShowTime);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var deleted = _showTimeService.Delete(id);

            if (!deleted)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update(
            [FromBody] ShowTimeRequest showTime,
            [FromRoute] int id)
        {
            var updated = _showTimeService.Update(showTime, id);

            if (!updated)
                return NotFound();

            return NoContent();
        }
    }
}
