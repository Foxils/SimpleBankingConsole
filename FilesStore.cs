using System.Security.Cryptography;
using System.Text;

namespace bankaccount
{
    internal class FilesStore
    {
        private static string filePath = "pinstore.txt";

        public static string HashPin(string pin) // Hashing is unsafe and pointless if the PIN file is deleted but fine for this.
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(pin));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static void SavePinToFile(string hashedPin)
        {
            File.WriteAllText(filePath, hashedPin);
        }


        public static string LoadPinFromFile()
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }
            else
            {
                throw new FileNotFoundException("PIN file not found.");
            }
        }
    }
}


