using CineTup.Application.Abstractions;
using CineTup.Application.Requests;
using CineTup.Application.Responses;
using CineTup.Application.Services;
using CineTup.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CineTup.Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<UserResponse> GetAll()
        {
            return Ok(_userService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<UserResponse> GetById([FromRoute] Guid id)
        {
            var user = _userService.GetById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public ActionResult<UserResponse> Create([FromBody] UserRequest user)
        {
            var createdUser = _userService.Create(user);

            return CreatedAtAction(nameof(GetById), new { id = createdUser.Id }, createdUser);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete([FromRoute] Guid id)
        {
            var createdUser = _userService.Delete(id);

            if (!createdUser)
                return NotFound();

            return NoContent();
        }

        [HttpPut("{id}")]
        public ActionResult Update([FromBody] UserRequest user, [FromRoute] Guid id)
        {
            var updatedUser = _userService.Update(user, id);

            if (!updatedUser)
                return NotFound();

            return NoContent();
        }
    }
}