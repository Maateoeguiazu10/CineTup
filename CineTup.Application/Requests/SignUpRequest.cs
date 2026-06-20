using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Application.Requests
{
    public class SignUpRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
    }
}
