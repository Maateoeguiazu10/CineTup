using CineTup.Application.Abstractions;
using CineTup.Application.Exceptions;
using CineTup.Application.Requests;
using CineTup.Application.Responses;
using CineTup.Presentation.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CineTup.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public ActionResult<List<MovieResponse>> GetAll()
        {
            try
            {
                var movies = _movieService.GetAll();
                if (!movies.Any())
                    return NotFound("No se encontraron películas.");
                return Ok(movies);
            }
            catch (DatabaseException ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public ActionResult<MovieResponse> GetById([FromRoute] int id)
        {
            try
            {
                return Ok(_movieService.GetById(id));
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
        public ActionResult<MovieResponse> Create([FromBody] MovieRequest movie)
        {
            try
            {
                var createdMovie = _movieService.Create(movie);
                return CreatedAtAction(nameof(GetById), new { id = createdMovie.Id }, createdMovie);
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
                _movieService.Delete(id);
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
        public ActionResult Update([FromBody] MovieRequest movie, [FromRoute] int id)
        {
            try
            {
                _movieService.Update(movie, id);
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
