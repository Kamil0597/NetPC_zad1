using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using NetPC_zadanie.DTO;
using NetPC_zadanie.Services;

namespace NetPC_zadanie.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(UserService userService, AuthService authService, ILogger<AuthController> logger)
        {
            _userService = userService;
            _authService = authService;
            _logger = logger;
        }


        [HttpPost("login")]
        public IActionResult Login([FromBody] RegisterUserDTO userDTO)
        {
            _logger.LogInformation("Próba logowania użytkownika: {Username}", userDTO.Username);

            var token = _authService.LoginUser(userDTO.Username, userDTO.Password);
            if (token == null)
            {
                _logger.LogWarning("Logowanie nieudane dla użytkownika: {Username}", userDTO.Username);
                return Unauthorized(new { message = "Invalid username or password" });
            }

            _logger.LogInformation("Logowanie udane dla użytkownika: {Username}", userDTO.Username);
            return Ok(new { token });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDTO userDTO)
        {
            _logger.LogInformation("Rejestracja użytkownika: {Username}", userDTO.Username);

            var register = _authService.RegisterUser(userDTO.Username, userDTO.Password);
            if (!register)
            {
                _logger.LogWarning("Rejestracja nieudana dla użytkownika: {Username}", userDTO.Username);
                return BadRequest(new { message = "Registration failed. Username might be taken or data invalid." });
            }

            _logger.LogInformation("Rejestracja udana dla użytkownika: {Username}", userDTO.Username);
            return Ok(new { message = "Registration successful" });
        }
    }
}
