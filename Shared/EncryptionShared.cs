using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace APICore.Shared
{
    public static class EncryptionShared
    {
        public static string ConvertToSha256(this string value)
        {
            SHA256 sha256 = new SHA256CryptoServiceProvider();

            string resultSha256 = Convert.ToBase64String(sha256.ComputeHash(Encoding.Default.GetBytes(value)));

            return resultSha256;
        }
    }
}
