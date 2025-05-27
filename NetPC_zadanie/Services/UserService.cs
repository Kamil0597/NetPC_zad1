using NetPC_zadanie.Interface;
using NetPC_zadanie.Models;
using NetPC_zadanie.Repositories;

namespace NetPC_zadanie.Services
{
    public class UserService
    {
        private IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool UpdateUser(User user)
        {
            return _userRepository.UpdateUser(user);
        }

        public bool DeleteUser(User user)
        {
            return _userRepository.DeleteUser(user);
        }

        public User? GetUserByName(string username)
        {
            return _userRepository.GetUserByName(username);
        }

        public User? GetUserById(int Id)
        {
            return _userRepository.GetUserById(Id);
        }

        //public List<User> GetAllUsers()
        //{
        //    return _userRepository.GetUsers();
        //}
    }
}
