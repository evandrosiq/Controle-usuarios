using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Projeto.CrossCutting.Criptography
{
    public class SHA1Encrypt
    {
        public string GenerateHash(string value)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(value));

            var result = string.Empty;
            foreach (var item in hash)
            {
                result += item.ToString("X2");
            }
            return result;
        }
    }
}
