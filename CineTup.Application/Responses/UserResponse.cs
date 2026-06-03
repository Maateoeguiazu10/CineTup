using System;
using System.Collections.Generic;
using System.Text;

namespace CineTup.Application.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; }
    }
}