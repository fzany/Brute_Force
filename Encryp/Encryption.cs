using System;
using System.IO;
using System.Security.Cryptography;

namespace Encryp
{
    public class Encryption
    {
        private static readonly byte[] BookriteKeyByteArray = Convert.FromBase64String("WLFwhMRRi95OfQ2/rJaD/rzhg3EJnuXvEbhRamPX3W8=");
        private static readonly byte[] BookriteIvByteArray = Convert.FromBase64String("jCs36/TBH5MjB/hkXLa+9g==");

         /// <summary>
        /// 
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string EncryptString(string plainText)
        {
            byte[] Key = BookriteKeyByteArray;
            byte[] IV = BookriteIvByteArray;

            // Check arguments.
            if (plainText == null || plainText.Length <= 0)
            {
                return string.Empty;
            }

            byte[] encrypted;
            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for encryption.
                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                        {

                            //Write all data to the stream.
                            swEncrypt.Write(plainText);
                        }
                        encrypted = msEncrypt.ToArray();
                    }
                }
            }
            // Return the encrypted bytes from the memory stream.
            return Convert.ToBase64String(encrypted);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="encryptedText"></param>
        /// <returns></returns>
        public static string DecryptString(string encryptedText)
        {
            byte[] cipherText = Convert.FromBase64String(encryptedText);
            byte[] Key = BookriteKeyByteArray;
            byte[] IV = BookriteIvByteArray;
            // Check arguments.
            if (cipherText == null || cipherText.Length <= 0)
            {
                return string.Empty;
            }
            // Declare the string used to hold
            // the decrypted text.
            string plaintext = null;

            // Create an Aes object
            // with the specified key and IV.
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = Key;
                aesAlg.IV = IV;

                // Create a decrytor to perform the stream transform.
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                // Create the streams used for decryption.
                using (MemoryStream msDecrypt = new MemoryStream(cipherText))
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                    {
                        using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                        {

                            // Read the decrypted bytes from the decrypting stream and place them in a string.
                            plaintext = srDecrypt.ReadToEnd();
                        }
                    }
                }

            }

            return plaintext;
        }
    }

}
