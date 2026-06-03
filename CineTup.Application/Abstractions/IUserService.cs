using CineTup.Application.Requests;
using CineTup.Application.Responses;
using CineTup.Application.Services;
using CineTup.Domain.Entities;

namespace CineTup.Application.Abstractions
{
    public interface IUserService
    {
        List<UserResponse> GetAll();
        UserResponse? GetById(int id);
        UserResponse Create(UserRequest user);
        bool Update(UserRequest user, int id);
        bool Delete(int id);
    }
}