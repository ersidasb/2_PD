using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Security.Cryptography;

namespace AES_Algoritmas
{
    class AES
    {
        AesCryptoServiceProvider provider;
        private string IVString;
        private int mode;
        public AES(string keyString, string IVString, int mode)
        {
            provider = new AesCryptoServiceProvider();
            this.mode = mode;

            if (keyString.Length < 16)
                while (keyString.Length < 16)
                    keyString += '0';

            if (mode == 1)
                provider.Mode = CipherMode.ECB;

            if (mode == 2)
            {
                if (IVString.Length < 16)
                    while (IVString.Length < 16)
                        IVString += "0";
                this.IVString = IVString;
                //provider.IV = Encoding.Default.GetBytes(IVString);
                provider.Mode = CipherMode.CBC;
            }

            provider.BlockSize = 128;
            provider.KeySize = 128;
            provider.Key = Encoding.Default.GetBytes(keyString);
            provider.Padding = PaddingMode.PKCS7;
        }
        public String encrypt(String text)
        {
            if (mode == 2)
                provider.IV = Encoding.Default.GetBytes(IVString);

            ICryptoTransform transform = provider.CreateEncryptor();
            byte[] encrypted = transform.TransformFinalBlock(ASCIIEncoding.ASCII.GetBytes(text), 0, text.Length);
            string result = Convert.ToBase64String(encrypted);
            return result;
        }

        public String decrypt(String text)
        {
            if(mode == 2)
                provider.IV = Encoding.Default.GetBytes(IVString);

            ICryptoTransform transform = provider.CreateDecryptor();
            byte[] bytesToEncrypt = Convert.FromBase64String(text);
            byte[] decrypted = transform.TransformFinalBlock(bytesToEncrypt, 0, bytesToEncrypt.Length);
            string result = ASCIIEncoding.ASCII.GetString(decrypted);
            return result;
        }
    }
}
