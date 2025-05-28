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

        /*
         * Aktualizuje dane użytkownika w bazie danych.
         * Zwraca true, jeśli operacja się powiodła.
         */
        public bool UpdateUser(User user)
        {
            return _userRepository.UpdateUser(user);
        }

        /*
         * Usuwa użytkownika z bazy danych.
         * Zwraca true, jeśli operacja się powiodła.
         */
        public bool DeleteUser(User user)
        {
            return _userRepository.DeleteUser(user);
        }

        /*
         * Pobiera użytkownika na podstawie nazwy użytkownika.
         * Zwraca obiekt User lub null, jeśli nie znaleziono.
         */
        public User? GetUserByName(string username)
        {
            return _userRepository.GetUserByName(username);
        }

        /*
         * Pobiera użytkownika na podstawie ID.
         * Zwraca obiekt User lub null, jeśli nie znaleziono.
         */
        public User? GetUserById(int Id)
        {
            return _userRepository.GetUserById(Id);
        }
    }
}
