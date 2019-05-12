using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace Sarif_Industries
{
    public class Encrypt
    {
        public static string GETMDHash(string input)
        {
            using (MD5CryptoServiceProvider mD5 = new MD5CryptoServiceProvider())
            {
                byte[] b = System.Text.Encoding.UTF8.GetBytes(input);
                b = mD5.ComputeHash(b);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();

                foreach (byte x in b)
                    sb.Append(x.ToString("x2"));

                return sb.ToString();
            }
        }
    }
}