using Microsoft.AspNetCore.Mvc;
using NetPC_zadanie.DTO;
using NetPC_zadanie.Models;
using NetPC_zadanie.Services;

namespace NetPC_zadanie.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;

        // Konstruktor - wstrzykiwanie serwisów
        public UserController(UserService userService, AuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }


        /*
         * Pobiera użytkownika na podstawie nazwy użytkownika.
         * Zwraca 200 OK z użytkownikiem, lub 404 jeśli nie znaleziono.
         */
        [HttpGet("name/{username}")]
        public ActionResult<User> GetUserByName(string username)
        {
            var user = _userService.GetUserByName(username);
            if (user == null) return NotFound();
            return Ok(user);
        }

        /*
         * Pobiera użytkownika na podstawie ID.
         * Zwraca 200 OK z użytkownikiem, lub 404 jeśli nie znaleziono.
         */
        [HttpGet("{id:int}")]
        public ActionResult<User> GetUserById(int Id)
        {
            var user = _userService.GetUserById(Id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        /*
         * Rejestruje nowego użytkownika.
         * Jeśli dane są niepoprawne (np. puste pola), zwraca 400 BadRequest.
         * Jeśli rejestracja się powiedzie, zwraca 200 OK.
         * Jeśli użytkownik nie może być zarejestrowany, zwraca 400 BadRequest.
         */
        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] RegisterUserDTO dto)
        {
            if(string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
                return BadRequest("Username or password was incorect");    

            var success = _authService.RegisterUser(dto.Username, dto.Password);

            if (success)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Cannot register this user");
            }
        }
    }
}
