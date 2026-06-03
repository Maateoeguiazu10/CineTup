using CineTup.Application.Requests;
using CineTup.Application.Responses;
using CineTup.Application.Services;
using CineTup.Domain.Entities;

namespace CineTup.Application.Abstractions
{
    public interface IUserService
    {
        List<UserResponse> GetAll();
        UserResponse? GetById(Guid id);
        UserResponse Create(UserRequest user);
        bool Update(UserRequest user, Guid id);
        bool Delete(Guid id);
    }
}