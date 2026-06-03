using CineTup.Application.Abstractions;
using CineTup.Application.Mapper;
using CineTup.Application.Requests;
using CineTup.Application.Responses;
using CineTup.Domain.Entities;

namespace CineTup.Application.Services
{
    public class UserService : IUserService
    {
        private static readonly List<User> _users = new()
        {
            new User
            {
                Id = Guid.NewGuid(),
                Name = "John Doe",
                Email = "john@gmail.com",
                Password = "123"
            }
        };
        public List<UserResponse> GetAll()
        {
            return _users
                .Where(x => x.IsDeleted == false)
                .OrderBy(x => x.Name)
                .Select(x => x.ToUserResponse()).ToList();

        }

        public UserResponse? GetById(Guid id)
        {
            return _users
                .Where(x => x.Id == id)
                .Select(x => x.ToUserResponse())
                .FirstOrDefault();
        }

        public UserResponse Create(UserRequest user)
        {
            var newUser = user.ToUser();
            _users.Add(newUser);
            return newUser.ToUserResponse();
        }

        public bool Delete(Guid id)
        {

            var UserToDelete = _users.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (UserToDelete == null)
                return false;

            UserToDelete.IsDeleted = true;

            return true;
        }
        public bool Update(UserRequest user, Guid id)
        {
            var userToUpdate = _users.FirstOrDefault(x => x.Id == id && !x.IsDeleted);

            if (userToUpdate == null)
                return false;

            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;

            return true;
        }
    }
}
