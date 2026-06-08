using CineTup.Application.Abstractions;
using CineTup.Application.Abstractions.Infraestructure;
using CineTup.Application.Mapper;
using CineTup.Application.Requests;
using CineTup.Application.Responses;
using CineTup.Domain.Entities;

namespace CineTup.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public List<UserResponse> GetAll()
        {
                return _userRepository 
                .GetAll()
                .OrderBy(x => x.Name)
                .Select(x => x.ToUserResponse()).ToList();

        }

        public UserResponse? GetById(int id)
        {
            return _userRepository.GetById(id)?.ToUserResponse();

        }

        public UserResponse Create(UserRequest user)
        {
            var newUser = user.ToUser();
            _userRepository.Add(newUser);
            return newUser.ToUserResponse();
        }

        public bool Delete(int id)
        {
            _userRepository.Delete(id);
            return true;
        }
        public bool Update(UserRequest user, int id)
        {
            var userToUpdate = _userRepository.GetById(id);

            if (userToUpdate == null)
                return false;

            userToUpdate.Name = user.Name;
            userToUpdate.Email = user.Email;
            userToUpdate.Password = user.Password;

            _userRepository.Update(userToUpdate);
            return true;
        }
    }
}
