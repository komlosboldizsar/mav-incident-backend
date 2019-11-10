using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace mav_incident_processor
{
    static class HashHelper
    {
        public static string HashMD5(this string str)
        {
            // @source https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.md5?view=netframework-4.8
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(str));
                StringBuilder sBuilder = new StringBuilder();
                for (int i = 0; i < data.Length; i++)
                    sBuilder.Append(data[i].ToString("x2"));
                return sBuilder.ToString();
            }
        }
    }
}
