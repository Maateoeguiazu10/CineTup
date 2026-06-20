using CineTup.Application.Abstractions;
using CineTup.Application.Requests;
using CineTup.Application.Responses;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
            var response = _authService.SingUp(request);
            if (response == null)
            {
                return Conflict("El Email ya esta registrado");
            }
            return StatusCode(StatusCodes.Status201Created, response);
        }
        [HttpPost("signin")]
        [AllowAnonymous]
        public ActionResult<AuthResponse> SingIn([FromBody] SignInRequest request)
        {
            var response = _authService.SingIn(request);
            if (response == null)
            {
                return Unauthorized("Credenciales incorrectas.");
            }
            return Ok(response);
        }
    }
}
