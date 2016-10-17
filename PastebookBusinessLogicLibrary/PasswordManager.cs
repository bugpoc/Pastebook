using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PastebookBusinessLogicLibrary
{
    //http://www.codeproject.com/Articles/608860/Understanding-and-Implementing-Password-Hashing

    /// <summary>
    /// This class contains method for password hashing.
    /// </summary>
    public class PasswordManager
    {
        private StringBuilder _sb;

        /// <summary>
        /// This method generates password hash by adding the plain text password and salt. This method also out the salt string.
        /// </summary>
        /// <param name="plainTextPassword">Plain text password inputted by the user.</param>
        /// <param name="salt">The reference where the salt will be stored.</param>
        /// <returns>Generated Password Hash and out the salt string.</returns>
        public string GeneratePasswordHash(string plainTextPassword, out string salt)
        {
            salt = GetSaltString();

            _sb = new StringBuilder(plainTextPassword);
            _sb.Append(salt);

            return GetPasswordHashAndSalt(_sb.ToString());
        }

        /// <summary>
        /// This method checks if the username and password of the user match.
        /// </summary>
        /// <param name="plainTextPassword">Plain text password inputted by the user.</param>
        /// <param name="salt">Salt that is retrieve from the database.</param>
        /// <param name="hash">Password hash that is retrieve from the database.</param>
        /// <returns>True if the credentials match and False if the credentials did not match.</returns>
        public bool IsPasswordMatch(string plainTextPassword, string salt, string hash)
        {
            _sb = new StringBuilder(plainTextPassword);
            _sb.Append(salt);

            return hash == GetPasswordHashAndSalt(_sb.ToString());
        }

        private string GetPasswordHashAndSalt(string message)
        {
            SHA256 sha = new SHA256CryptoServiceProvider();
            byte[] dataBytes = GetBytes(message);
            byte[] resultBytes = sha.ComputeHash(dataBytes);

            return GetString(resultBytes);
        }

        private string GetSaltString()
        {
            RNGCryptoServiceProvider cryptoServiceProvider = new RNGCryptoServiceProvider();
            const int saltSize = 24;

            // Create a byte array to store the salt bytes
            byte[] saltBytes = new byte[saltSize];

            // Generate the salt in the byte array
            cryptoServiceProvider.GetNonZeroBytes(saltBytes);

            string saltString = GetString(saltBytes);

            return saltString;
        }

        private static byte[] GetBytes(string message)
        {
            //Convert string to byte
            return Encoding.ASCII.GetBytes(message);
        }

        private static string GetString(byte[] resultBytes)
        {
            //Convert byte to string
            return Encoding.ASCII.GetString(resultBytes);
        }
    }
}
