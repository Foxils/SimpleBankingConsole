using System.Security.Cryptography;
using System.Text;

namespace bankaccount
{
    internal static class FilesStore
    {
        private static readonly string FilePath = "pinstore.txt";

        public static string HashPin(string pin)
        {
            if (string.IsNullOrWhiteSpace(pin))
            {
                throw new ArgumentException("PIN cannot be null or whitespace.", nameof(pin));
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pin));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static void SavePinToFile(string hashedPin)
        {
            if (hashedPin is null)
            {
                throw new ArgumentNullException(nameof(hashedPin));
            }
            File.WriteAllText(FilePath, hashedPin);
        }

        public static string LoadPinFromFile()
        {
            if (!File.Exists(FilePath))
            {
                throw new FileNotFoundException("PIN file not found.", FilePath);
            }
            return File.ReadAllText(FilePath);
        }




    }

}
