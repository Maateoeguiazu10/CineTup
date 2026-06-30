using CineTup.Application.Responses;
using System.Collections.Generic;

namespace CineTup.Application.Abstractions
{
    public interface IUserService
    {
        List<UserResponse> GetAllUsers();
        void AssignRole(int userId, string currentRole, string newRole);
        void DeleteUser(int userId);
    }
}
