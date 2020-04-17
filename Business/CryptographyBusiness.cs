using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using ECDiffieHellman_Bouncy_Castle.Entity;

namespace ECDiffieHellman_Bouncy_Castle.Business
{
    public static class CryptographyBusiness
    {
        //on this class, we encrypt and decrypt the message
        public static EncryptedMessage Cryptograph(byte[] simetricKey, string message)
        {
            EncryptedMessage ret = new EncryptedMessage();
            
            using (Aes aes = new AesCryptoServiceProvider())
            {
                aes.Key = simetricKey;
                ret.iv = CommonBusiness.bytesToString(aes.IV);

                // Encrypt the message
                using (MemoryStream ciphertext = new MemoryStream())
                using (CryptoStream cs = new CryptoStream(ciphertext, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] plaintextMessage = Encoding.UTF8.GetBytes(message);
                    cs.Write(plaintextMessage, 0, plaintextMessage.Length);
                    cs.Close();
                    ret.encryptedText = CommonBusiness.bytesToString(ciphertext.ToArray());
                }
            }

            return ret;
        }

        public static string Decryptograph(byte[] simetricKey, EncryptedMessage message)
        {
            string ret;

            using (Aes aes = new AesCryptoServiceProvider())
            {
                aes.Key = simetricKey;
                aes.IV = CommonBusiness.stringToBytes(message.iv);
                // Decrypt the message
                using (MemoryStream plaintext = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(plaintext, aes.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        byte[] encBytes = CommonBusiness.stringToBytes(message.encryptedText);
                        cs.Write(encBytes, 0, encBytes.Length);
                        cs.Close();
                        ret = Encoding.UTF8.GetString(plaintext.ToArray());
                    }
                }
            }
            
            return ret;
        }

        public static string EncryptedToString(EncryptedMessage entrada)
        {
            string ret;

            ret = entrada.iv + entrada.encryptedText;

            return ret;
        }

        public static EncryptedMessage StringToEncrypted(string entrada)
        {
            EncryptedMessage ret = new EncryptedMessage();
            
            ret.encryptedText = entrada.Substring(ReadOnlyParam.ivSize);
            ret.iv = entrada.Substring(0,ReadOnlyParam.ivSize);

            return ret;
        }
    }
}