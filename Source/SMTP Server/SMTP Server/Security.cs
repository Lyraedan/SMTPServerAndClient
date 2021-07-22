using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SMTP_Server
{
    class Security
    {

        /// <summary>
        /// Encrypt a string to the ECB cipher
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        public static string Encrypt(string body)
        {
            if (!string.IsNullOrEmpty(body))
            {
                byte[] data = Encoding.ASCII.GetBytes(body);

                SymmetricAlgorithm algorithm = DES.Create();
                algorithm.Key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
                algorithm.Mode = CipherMode.ECB;
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, algorithm.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(data, 0, data.Length);
                    }

                    return Convert.ToBase64String(memoryStream.ToArray()); //Encoding to UTF8 here makes the decrytion return "Bad data", same goes with Unicode
                }
            }
            return string.Empty;
        }

        /// <summary>
        /// Decrypt a string from the ECB cipher
        /// </summary>
        /// <param name="encrypted"></param>
        /// <returns></returns>
        public static string Decrypt(string encrypted)
        {
            if (!string.IsNullOrEmpty(encrypted))
            {
                byte[] encryptedBytes = Convert.FromBase64String(encrypted);

                SymmetricAlgorithm algorithm = DES.Create();
                algorithm.Key = new byte[8] { 1, 2, 3, 4, 5, 6, 7, 8 };
                algorithm.Mode = CipherMode.ECB;

                using (MemoryStream memoryStream = new MemoryStream(encryptedBytes))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memoryStream, algorithm.CreateDecryptor(), CryptoStreamMode.Read))
                    {
                        byte[] decryptedBytes = new byte[encryptedBytes.Length];
                        cryptoStream.Read(decryptedBytes, 0, decryptedBytes.Length);
                        return Encoding.UTF8.GetString(decryptedBytes); // Converting to base 64 doesn't work here
                    }
                }
            }
            return string.Empty;
        }
    }
}
