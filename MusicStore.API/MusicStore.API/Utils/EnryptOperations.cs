using System.Security.Cryptography;
using System.Text;
using MusicStore.API.Configurations;

namespace MusicStore.API.Utils;

public class EnryptOperations
{
    public static string EncryptAES(string plainText)
        {
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(AppSettings.Key);
                aesAlg.IV = Encoding.UTF8.GetBytes(AppSettings.InitializationVector);

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);


                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encrypted);
        }

        public static string DecryptAES(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            string decryptedText;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Encoding.UTF8.GetBytes(AppSettings.Key);
                aesAlg.IV = Encoding.UTF8.GetBytes(AppSettings.InitializationVector);

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                using (MemoryStream msDecrypt = new MemoryStream(cipherBytes))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {
                            decryptedText = srDecrypt.ReadToEnd();
                        }
                    }
                }
                aesAlg.Clear();
            }

            return decryptedText;
        }
}