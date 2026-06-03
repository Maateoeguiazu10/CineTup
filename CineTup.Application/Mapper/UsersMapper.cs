using CineTup.Application.Requests;
using CineTup.Application.Responses;
using CineTup.Domain.Entities;

namespace CineTup.Application.Mapper
{
    public static class UsersMapper
    {
        public static UserResponse ToUserResponse(this User user)
        {
            return new UserResponse
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email
            };
        }

        public static User ToUser(this UserRequest userRequest)
        {
            return new User
            {
                Name = userRequest.Name,
                Email = userRequest.Email,
                Password = userRequest.Password,
                IsDeleted = false
            };
        }
    }
}
               