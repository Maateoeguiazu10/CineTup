using CineTup.Application.Abstractions;
using CineTup.Application.Exceptions;
using CineTup.Application.Requests;
using CineTup.Application.Responses;
using Microsoft.AspNetCore.Authorization;
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
        public async Task<ActionResult<AuthResponse>> SingUp([FromBody] SignUpRequest request)
        {
            var response = await _authService.SingUp(request);
            return StatusCode(StatusCodes.Status201Created, response);
        }
        [HttpPost("signin")]
        [AllowAnonymous]
        public async Task<ActionResult<AuthResponse>> SingIn([FromBody] SignInRequest request)
        {
                return Ok(await _authService.SingIn(request));
        }
    }
}
