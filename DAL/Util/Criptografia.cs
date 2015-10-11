using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace DAL.Util
{
    public class Criptografia
    {
        public static string EncriptarMD5(string Senha)
        {
            try
            {

                MD5 md5 = new MD5CryptoServiceProvider();
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(Senha));
                return BitConverter.ToString(hash).Replace("-", string.Empty);

            }
            catch
            {
                throw;
            }

        }

    }
}