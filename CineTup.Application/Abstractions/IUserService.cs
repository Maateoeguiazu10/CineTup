using CineTup.Application.Responses;
using System.Collections.Generic;

namespace CineTup.Application.Abstractions
{
    public interface IUserService
    {
        Task<List<UserResponse>> GetAllUsersAsync();
        Task AssignRoleAsync(int userId, string currentRole, string newRole);
        Task DeleteUserAsync(int userId);
    }
}
