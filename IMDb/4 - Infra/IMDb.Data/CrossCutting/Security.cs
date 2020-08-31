using IMDb.Domain.Core.Security;
using System;
using System.Security.Cryptography;
using System.Text;

namespace IMDb.Data.CrossCutting
{
    public class Security : ISecurity
    {
        /// <summary>
        /// Method that encrypts a string passed as parameter
        /// </summary>
        /// <param name="value">Value to be encrypted</param>
        /// <param name="salt">salt to mix encrypt result</param>
        /// <returns>encrypted value</returns>
        public string Encrypt(string value, string salt)
        {
            byte[] byteRepresentation = UnicodeEncoding.UTF8.GetBytes(value + salt);

            byte[] hashedTextInBytes = null;
            SHA1CryptoServiceProvider mySHA1 = new SHA1CryptoServiceProvider();
            hashedTextInBytes = mySHA1.ComputeHash(byteRepresentation);
            return Convert.ToBase64String(hashedTextInBytes);
        }
    }
}
