using CineTup.Application.Abstractions;
using CineTup.Application.Exceptions;
using CineTup.Application.Responses;
using CineTup.Presentation.Authorization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CineTup.Presentation.Controllers
{
    [Authorize(Policy = Policies.SysAdminOnly)]
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
        public ActionResult<List<UserResponse>> GetAll()
        {
            try
            {
                var users = _userService.GetAllUsers();
                if (users == null || users.Count == 0)
                    return NotFound("No se encontraron usuarios.");
                return Ok(users);
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

        [HttpPost("{id}/assign-role")]
        public ActionResult AssignRole(
            [FromRoute] int id, 
            [FromQuery] string currentRole, 
            [FromQuery] string newRole)
        {
            try
            {
                _userService.AssignRole(id, currentRole, newRole);
                return NoContent();
            }
            catch (NotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Message);
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
    }
}
