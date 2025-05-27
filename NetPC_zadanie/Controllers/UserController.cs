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

        public UserController(UserService userService, AuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        [HttpGet("name/{username}")]
        public ActionResult<User> GetUserByName(string username)
        {
            var user = _userService.GetUserByName(username);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpGet("{id:int}")]
        public ActionResult<User> GetUserById(int Id)
        {
            var user = _userService.GetUserById(Id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] RegisterUserDTO dto)
        {
            if(string.IsNullOrWhiteSpace(dto.Username) || string.IsNullOrWhiteSpace(dto.Password))
            {
                return BadRequest("Username or password was incorect");
            }    

            var register = _authService.RegisterUser(dto.Username, dto.Password);

            if (register)
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
