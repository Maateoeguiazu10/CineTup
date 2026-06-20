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
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;

        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        [HttpGet]
        public ActionResult<MovieResponse> GetAll()
        {
            var movies = _movieService.GetAll();
            if (!movies.Any())
                return NotFound();
            return Ok(movies);
        }

        [HttpGet("{id}")]
        public ActionResult<MovieResponse> GetById([FromRoute] int id)
        {
            var movie = _movieService.GetById(id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        [HttpPost]
        public ActionResult<MovieResponse> Create([FromBody] MovieRequest movie)
        {
            var createdMovie = _movieService.Create(movie);

            return CreatedAtAction(nameof(GetById), new { id = createdMovie.Id }, createdMovie);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] int id)
        {
            var createdMovie = _movieService.Delete(id);

            if (!createdMovie)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] MovieRequest movie, [FromRoute] int id)
        {
            var updatedMovie = _movieService.Update(movie, id);

            if (!updatedMovie)
                return NotFound();

            return NoContent();
        }
    }
}