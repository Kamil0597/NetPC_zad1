using System.Security.Cryptography;

namespace NetPC_zadanie.Services
{
    public class PasswordService
    {

        private const int saltSize = 16;
        private const int keySize = 32;
        private const int iterations = 100000;

        /*
         * Haszuje podane hasło przy użyciu algorytmu PBKDF2 z SHA256.
         * Generuje sól i klucz hasła, zwracając je w formacie: iterations.salt.key.
         */
        public static string HashPassword(string password)
        {
            using var rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[saltSize];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            byte[] key = pbkdf2.GetBytes(keySize);

            return $"{iterations}.{Convert.ToBase64String(salt)}.{Convert.ToBase64String(key)}";
        }

        /*
         * Sprawdza poprawność podanego hasła względem zahashowanego hasła.
         * Porównuje wartości w bezpieczny sposób za pomocą FixedTimeEquals.
         * Zwraca true, jeśli hasła się zgadzają.
         */
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            var parts = hashedPassword.Split('.');
            if (parts.Length != 3)
                return false;

            int iterations = int.Parse(parts[0]);
            byte[] salt = Convert.FromBase64String(parts[1]);
            byte[] key = Convert.FromBase64String(parts[2]);

            using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithmName.SHA256);
            byte[] keyToCheck = pbkdf2.GetBytes(keySize);

            return CryptographicOperations.FixedTimeEquals(keyToCheck, key);
        }
    }
}
