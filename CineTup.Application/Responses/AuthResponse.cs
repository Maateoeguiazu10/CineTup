using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Application.Responses
{
    public class AuthResponse
    {
        public string Token { get; set; } = string.Empty;
        public string Rol { get; set; } = string.Empty;
        public int UserId { get; set; }
        public string Email { get; set; } = string.Empty;

    }
}
