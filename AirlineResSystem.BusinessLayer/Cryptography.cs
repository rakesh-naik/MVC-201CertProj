using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;

namespace AirlineResSystem.BusinessLayer
{
    public class Cryptography
    {
        TripleDESCryptoServiceProvider desCryptoProvider;
        MD5CryptoServiceProvider hashMD5Provider;
        const string cryptKey = "DES_ENCRYPT";
        public Cryptography()
        {
            desCryptoProvider = new TripleDESCryptoServiceProvider();
            hashMD5Provider = new MD5CryptoServiceProvider();

        }

        public string Encrypt(string plainText)
        {
            byte[] byteHash;
            byte[] byteBuff;

            byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(cryptKey));
            desCryptoProvider.Key = byteHash;
            desCryptoProvider.Mode = CipherMode.ECB; //CBC, CFB
            byteBuff = Encoding.UTF8.GetBytes(plainText);

            string encoded =
                Convert.ToBase64String(desCryptoProvider.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            return encoded;
        }

        public string Decrypt(string encodedText)
        {
            byte[] byteHash;
            byte[] byteBuff;

            byteHash = hashMD5Provider.ComputeHash(Encoding.UTF8.GetBytes(cryptKey));
            desCryptoProvider.Key = byteHash;
            desCryptoProvider.Mode = CipherMode.ECB; //CBC, CFB
            byteBuff = Convert.FromBase64String(encodedText);

            string plaintext = Encoding.UTF8.GetString(desCryptoProvider.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length));
            return plaintext;
        }
    }
}
