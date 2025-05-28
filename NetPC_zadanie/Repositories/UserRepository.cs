using AutoMapper;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NetPC_zadanie.Data;
using NetPC_zadanie.Interface;
using NetPC_zadanie.Models;

namespace NetPC_zadanie.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UserRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /*
         * Dodaje nowego użytkownika do bazy danych.
         * Zwraca true, jeśli operacja się powiodła.
         */
        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            return Save();
        }

        /*
         * Usuwa użytkownika z bazy danych.
         * Zwraca true, jeśli operacja się powiodła.
         */
        public bool DeleteUser(User user)
        {
            _context.Users.Remove(user);
            return Save();
        }

        /*
         * Pobiera użytkownika na podstawie ID.
         * Zwraca obiekt User lub null, jeśli nie znaleziono.
         */
        public User? GetUserById(int Id)
        {
            return _context.Users.Where(user => user.Id == Id).FirstOrDefault();
        }

        /*
         * Pobiera użytkownika na podstawie nazwy użytkownika.
         * Zwraca obiekt User lub null, jeśli nie znaleziono.
         */
        public User? GetUserByName(string username)
        {
            return _context.Users.Where(user => user.Name == username).FirstOrDefault();
        }

        /*
         * Zwraca listę wszystkich użytkowników z bazy danych.
         */
        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        /*
         * Aktualizuje dane użytkownika w bazie danych.
         * Zwraca true, jeśli operacja się powiodła.
         */
        public bool UpdateUser(User user)
        {
            _context.Users.Update(user);
            return Save();
        }

        /*
         * Zapisuje zmiany w bazie danych.
         * Zwraca true, jeśli operacja się powiodła.
         */
        private bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
