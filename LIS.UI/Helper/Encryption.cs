using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace LIS.UI.Helper
{
    
    public class Encryption
    {
        const String KEY = "Fr@mw0rk";
        const String IV = "!1az=-@qt";
        public string Encrypt(String source, string key = KEY)
        {
            Encoding encoding = Encoding.GetEncoding("windows-1252");
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
            provider.Key = Senetizeinput(key, provider.LegalKeySizes[0], encoding);
            provider.IV = Senetizeinput(IV, provider.LegalBlockSizes[0], encoding);
            ICryptoTransform transform = provider.CreateEncryptor();
            byte[] input = encoding.GetBytes(source);
            return CommanFunction.ByteArrayToHexString(transform.TransformFinalBlock(input, 0, input.Length));

        }
        public string Decrypt(String Source, string key = KEY)
        {
            Encoding encoding = Encoding.GetEncoding("windows-1252");
            TripleDESCryptoServiceProvider provider = new TripleDESCryptoServiceProvider();
            provider.Key = Senetizeinput(key, provider.LegalKeySizes[0], encoding);
            provider.IV = Senetizeinput(IV, provider.LegalBlockSizes[0], encoding);
            ICryptoTransform transform = provider.CreateDecryptor();
            byte[] input = CommanFunction.HexStringToByteArray(Source).ToArray();
            return encoding.GetString(transform.TransformFinalBlock(input, 0, input.Length));
        }
        private byte[] Senetizeinput(String input, KeySizes size, Encoding encoding)
        {
            byte[] bInput = encoding.GetBytes(input);
            byte[] output = null;
            if ((size.MinSize / 8) > bInput.Length)
            {
                output = new byte[size.MinSize / 8];

                Array.Copy(bInput, output, bInput.Length);

            }
            if ((size.MaxSize / 8) < bInput.Length)
            {
                output = new byte[size.MaxSize / 8];
                Array.Copy(bInput, output, output.Length);

            }
            bInput = null;
            return output;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {

            }
        }
    }
}