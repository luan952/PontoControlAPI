using System.Security.Cryptography;
using System.Text;

namespace PontoControl.Application.Services.Cryptography
{
    public class PasswordEncryptor
    {
        public string Encrypt(string password)
        {
            var bytes = Encoding.UTF8.GetBytes(password);
            var sha52 = SHA512.Create();
            byte[] hashBytes = sha52.ComputeHash(bytes);
            return StringBytes(hashBytes);
        }

        private static string StringBytes(byte[] bytes)
        {
            var sb = new StringBuilder();

            foreach (byte b in bytes)
            {
                var hex = b.ToString("x2");
                sb.Append(hex);
            }
            return sb.ToString();
        }
    }
}
