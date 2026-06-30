using System;

namespace CineTup.Application.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? AvatarUrl { get; set; } = string.Empty;

        public string Rol { get; set; } = string.Empty;
    }
}
