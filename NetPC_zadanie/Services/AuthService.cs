using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using NetPC_zadanie.Controllers;
using NetPC_zadanie.Interface;
using NetPC_zadanie.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace NetPC_zadanie.Services
{
    public class AuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly string _jwtKey = "super_secret_key_12345678901234567890!";
        private readonly string _jwtIssuer = "NetPC_zadanie";
        public AuthService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        /*
        * Rejestruje nowego użytkownika.
        * Sprawdza, czy dane wejściowe są poprawne, hashuje hasło, tworzy obiekt User i zapisuje go w bazie.
        * Zwraca true, jeśli rejestracja się powiodła.
        */
        public bool RegisterUser(string username, string password)
        {
            if (username == null || password == null)
                return false;

            string hashedPassword = PasswordService.HashPassword(password);
            if (hashedPassword == null)
                return false;

            var user = new User(username, hashedPassword);

            if (user == null)
                return false;
            return _userRepository.CreateUser(user);
        }

        /*
        * Loguje użytkownika, sprawdzając poprawność hasła.
        * Jeśli dane są poprawne, generuje token JWT i go zwraca.
        * Zwraca null, jeśli dane logowania są niepoprawne.
        */
        public string? LoginUser(string username, string password)
        {
            if (username == null || password == null)
            {
                return null;
            }

            var user = _userRepository.GetUserByName(username);
            if (user == null)
            {
                return null;
            }

            if (!PasswordService.VerifyPassword(password, user.PasswordHash))
            {
                return null;
            }

            var token = GenerateJwtToken(user);
            return token;
        }

        /*
         * Generuje token JWT dla zalogowanego użytkownika.
         * Token zawiera roszczenia (claims) z nazwą użytkownika i jego ID, ważny przez 1 godzinę.
         */
        private string GenerateJwtToken(User user)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _jwtIssuer,
                audience: _jwtIssuer,
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
