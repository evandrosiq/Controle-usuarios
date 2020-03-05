using System;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;

namespace Projeto.CrossCutting.Criptography
{
    public class MD5Encrypt
    {
        public string GenerateHash(string value)
        {
            var md5 = new MD5CryptoServiceProvider();

            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(value));

            var result = string.Empty;
            foreach (var item in hash)
            {
                result += item.ToString("X2");
            }
            return result;
        }
    }
}
