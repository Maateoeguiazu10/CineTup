using CineTup.Application.Abstractions;
using CineTup.Application.Exceptions;
using CineTup.Application.Requests;
using CineTup.Application.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using ValidationException = CineTup.Application.Exceptions.ValidationException;

namespace CineTup.Presentation.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("signup")]
        [AllowAnonymous]
        public ActionResult<AuthResponse> SingUp([FromBody] SignUpRequest request)
        {
            try
            {
                var response = _authService.SingUp(request);
                return StatusCode(StatusCodes.Status201Created, response);
            }
            catch (ArgumentException ex)
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
                return StatusCode(500, "Ocurrio un error inesperado");
            }
        }
        [HttpPost("signin")]
        [AllowAnonymous]
        public ActionResult<AuthResponse> SingIn([FromBody] SignInRequest request)
        {
            try
            {
                return Ok(_authService.SingIn(request));
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
            catch (UnauthorizedException ex)
            {
                return Unauthorized(ex.Message);
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
