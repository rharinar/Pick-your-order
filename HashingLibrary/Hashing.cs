//using System;
//using System.Security.Cryptography;
//using System.Text;

//namespace HashingLibrary
//{
//    public class Hashing
//    {
//        // Size of the salt used for hashing the password
//        private const int SaltSize = 16;

//        /// <summary>
//        /// The function first converts the salt string from Base64 encoding to a byte array using the Convert.FromBase64String() method.
//        /// It then creates a new instance of the SHA256Managed class to compute the hash of the salted password.
//        /// The password string is converted to a byte array using the Encoding.UTF8.GetBytes() method.
//        /// A new byte array is created with a length equal to the sum of the lengths of the password and salt byte arrays.This array will hold the salted password.
//        /// The Buffer.BlockCopy() method is used to copy the password and salt byte arrays into the salted password array.
//        /// The ComputeHash() method of the SHA256Managed instance is called with the salted password byte array as its input, and it returns a hashed byte array.
//        /// Finally, the hashed byte array is converted to a Base64-encoded string using the Convert.ToBase64String() method and returned by the function.
//        /// </summary>
//        /// <param name="password"></param>
//        /// <param name="salt"></param>
//        /// <returns></returns>
//        public static string HashPassword(string password, string salt)
//        {
//            byte[] saltBytes = Convert.FromBase64String(salt);
//            using (SHA256 sha = SHA256.Create())
//            {
//                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
//                byte[] saltedPassword = new byte[passwordBytes.Length + saltBytes.Length];
//                Buffer.BlockCopy(passwordBytes, 0, saltedPassword, 0, passwordBytes.Length);
//                Buffer.BlockCopy(saltBytes, 0, saltedPassword, passwordBytes.Length, saltBytes.Length);
//                byte[] hash = sha.ComputeHash(saltedPassword);
//                return Convert.ToBase64String(hash);
//            }
//        }
//        /// <summary>
//        /// This is a function that takes three input parameters: a password string, a salt string, and a hashed password string.
//        /// It returns a boolean value indicating whether the provided password matches the hashed password.
//        /// </summary>
//        /// <param name="password"></param>
//        /// <param name="salt"></param>
//        /// <param name="hashedPassword"></param>
//        /// <returns></returns>
//        public static bool VerifyPassword(string password, string salt, string hashedPassword)
//        {
//            string hashedPasswordToVerify = HashPassword(password, salt);
//            return hashedPasswordToVerify == hashedPassword;
//        }

//        // This method generates a random salt of the specified size and returns it as a Base64-encoded string.
//        public static string GenerateSalt()
//        {
//            using (var rng = new RNGCryptoServiceProvider())
//            {
//                byte[] salt = new byte[SaltSize];
//                rng.GetBytes(salt);
//                return Convert.ToBase64String(salt);
//            }
//        }

//    }
//}











using System;
using System.Security.Cryptography;
using System.Text;

namespace Asgn8Web
{
    public static class Hashing
    {
        public static string Encrypt(string input, string key)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = new byte[16];

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                byte[] encryptedBytes = encryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

                return Convert.ToBase64String(encryptedBytes);
            }
        }

        public static string Decrypt(string input, string key)
        {
            byte[] inputBytes = Convert.FromBase64String(input);
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);

            using (Aes aes = Aes.Create())
            {
                aes.Key = keyBytes;
                aes.IV = new byte[16];

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                byte[] decryptedBytes = decryptor.TransformFinalBlock(inputBytes, 0, inputBytes.Length);

                return Encoding.UTF8.GetString(decryptedBytes);
            }
        }
    }
}
