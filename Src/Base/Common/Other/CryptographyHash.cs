using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Common.Other
{
    public class CryptographyHash
    {
        private HashAlgorithm _algoritm;

        public CryptographyHash(HashAlgorithm algoritm)
        {
            _algoritm = algoritm;
        }

        public string EncryptPassword(string password)
        {
            var encodedValue = Encoding.UTF8.GetBytes(password);
            var encryptedPassword = _algoritm.ComputeHash(encodedValue);

            var sb = new StringBuilder();
            foreach (var character in encryptedPassword)
                sb.Append(character.ToString("X2"));

            return sb.ToString();
        }

        public bool CheckPassword(string passwordInputed, string passwordInDb)
        {
            if (string.IsNullOrEmpty(passwordInDb))
                throw new NullReferenceException("Password is Empty");

            var encryptedPassword = _algoritm.ComputeHash(Encoding.UTF8.GetBytes(passwordInputed));

            var sb = new StringBuilder();
            foreach (var character in encryptedPassword)
            {
                sb.Append(character.ToString("X2"));
            }

            return sb.ToString() == passwordInDb;
        }
    }
}
