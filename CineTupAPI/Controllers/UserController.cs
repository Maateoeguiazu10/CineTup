using CineTup.Application.Services;
using CineTup.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CineTupAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserServices _service;
        public UserController(UserServices service)
        {
            _service = service;
        }
        
        [HttpGet("{name}")]
        public IActionResult Get([FromRoute] string name)
        {
            return Ok(_service.Get(name));
        }
    }
}
