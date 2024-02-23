using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;

namespace Utility
{
    /// <summary>
    /// Encryption class:  To be coded.
    /// </summary>
    public static class Encryption
    {
        /// <summary>
        /// This method will be used encrypt a file
        /// </summary>
        /// <param name="plaintextFileName">Plain text file name</param>
        /// <param name="encryptedFileName">Encrypted file name</param>
        /// <param name="key">Key for the encryption</param>
        public static void Encrypt(String plaintextFileName, String encryptedFileName, String key)
        {
            FileStream plaintextFS = new FileStream(plaintextFileName, FileMode.Open, FileAccess.Read);
            FileStream encryptedFS = new FileStream(encryptedFileName, FileMode.Create, FileAccess.Write);
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = ASCIIEncoding.ASCII.GetBytes(key);
            des.IV = ASCIIEncoding.ASCII.GetBytes(key);
            ICryptoTransform encrtptor = des.CreateEncryptor();
            CryptoStream cryptoStream = new CryptoStream(encryptedFS, encrtptor, CryptoStreamMode.Write);
            Byte[] byteArray = new Byte[plaintextFS.Length];
            plaintextFS.Read(byteArray, 0, byteArray.Length);
            cryptoStream.Write(byteArray, 0, byteArray.Length);
            //encryptedFS.Write(byteArray, 0, byteArray.Length);
            plaintextFS.Close();
            cryptoStream.Close();
            encryptedFS.Close();
        }

        /// <summary>
        /// This method will decrypt a file
        /// </summary>
        /// <param name="plaintextFileName">Plain text file name</param>
        /// <param name="encryptedFileName">Encrypted file name</param>
        /// <param name="key">Key for the decryption</param>
        /// <exception cref="Exception"></exception>
        public static void Decrypt(String plaintextFileName, String encryptedFileName, String key)
        {
            StreamWriter streamWriter = new StreamWriter(plaintextFileName);
            try
            {
                FileStream encryptedFS = new FileStream(encryptedFileName, FileMode.Open, FileAccess.Read);
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                des.Key = ASCIIEncoding.ASCII.GetBytes(key);
                des.IV = ASCIIEncoding.ASCII.GetBytes(key);
                ICryptoTransform decryptor = des.CreateDecryptor();
                CryptoStream cryptoStream = new CryptoStream(encryptedFS, decryptor, CryptoStreamMode.Read);
                StreamReader reader = new StreamReader(cryptoStream);
                streamWriter.Write(reader.ReadToEnd());
                streamWriter.Flush();
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                streamWriter.Close();
                throw new Exception("Error : Failure to decrypt the file " + encryptedFileName);
            }
        }

    }
}
