using NetPC_zadanie.Models;

namespace NetPC_zadanie.Interface
{
    public interface IUserRepository
    {
        User? GetUserByName(string username);
        User? GetUserById(int Id);
        bool CreateUser(User user);
        bool UpdateUser(User user);
        bool DeleteUser(User user);
    }
}
