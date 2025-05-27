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

        public bool CreateUser(User user)
        {
            _context.Users.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Users.Remove(user);
            return Save();
        }

        public User? GetUserById(int Id)
        {
            return _context.Users.Where(user => user.Id == Id).FirstOrDefault();
        }

        public User? GetUserByName(string username)
        {
            return _context.Users.Where(user => user.Name == username).FirstOrDefault();
        }

        public List<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool UpdateUser(User user)
        {
            _context.Users.Update(user);
            return Save();
        }
        private bool Save()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
