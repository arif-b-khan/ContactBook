using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.IO;
using System.Text;
using ContactBook.Domain.Contexts;

namespace ContactBook.Domain.Security
{
    public static class ContactBookCrypto
    {
        static readonly byte[] key;
        static readonly byte[] pValue;
        
        static ContactBookCrypto()
        {
            Dictionary<string, string> secretResult = new ContactBookSecretContext().GetKeyValue();
            
            if (!secretResult.ContainsKey("SecretKey") && !secretResult.ContainsKey("SecretValue"))
            {
                throw new Exception("Unable to secret key and value in CB_Secret table");
            }

            key = Convert.FromBase64String(secretResult["SecretKey"]);
            pValue = Convert.FromBase64String(secretResult["SecretValue"]);

        }

        public static async Task<string> EncryptAsync(string plainStr)
        {
            string hexResult = string.Empty;
            var aesManaged = AesManaged.Create();

            using (aesManaged)
            {
                ICryptoTransform transform = aesManaged.CreateEncryptor(key, pValue);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Write))
                    {
                        byte[] plainByte = Encoding.Default.GetBytes(plainStr);
                        await cs.WriteAsync(plainByte, 0, plainByte.Length);
                    }
                    byte[] result = ms.ToArray();
                    hexResult = Convert.ToBase64String(result);
                }
            }
            return hexResult;
        }

        public static async Task<string> DecryptAsync(string cipherText)
        {
            string plainText = string.Empty;

            using (var aesManaged = AesManaged.Create())
            {
                ICryptoTransform transform = aesManaged.CreateDecryptor(key, pValue);
                using (MemoryStream ms = new MemoryStream(Convert.FromBase64String(cipherText)))
                {
                    using (var cryptoStream = new CryptoStream(ms, transform, CryptoStreamMode.Read))
                    {
                        using (var srReader = new StreamReader(cryptoStream))
                        {
                            plainText = await srReader.ReadToEndAsync();
                        }
                    }
                }
            }

            return plainText;
        }

    }
}