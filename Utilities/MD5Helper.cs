using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utilities
{
    public static class MD5Helper
    {
        public static string MD5Encrypt(string pwd)
        {
            string encrypt= "";
            MD5 md5 = MD5.Create();
            var s=md5.ComputeHash(Encoding.UTF8.GetBytes(pwd));
            foreach (var item in s)
            {
                encrypt += item.ToString("X");
            }
            return encrypt;
        }
    }
}
