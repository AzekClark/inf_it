using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Threading;
using System.IO;
namespace E2tima.Crypto
{
    public class SHA1
    {
        public string Key { get; private set; }

        public SHA1(string Key)
        {
            this.Key = Key;
        }

        public string Encode(string Data)
        {
            if (string.IsNullOrEmpty(Data))
                return "";

            byte[] initVecB = Encoding.ASCII.GetBytes("a8doSuDitOz1hZe#");
            byte[] solB = Encoding.ASCII.GetBytes("E2tima");
            byte[] ishTextB = Encoding.UTF8.GetBytes(Data);

            PasswordDeriveBytes derivPass = new PasswordDeriveBytes(Key, solB, "SHA1", 2);
            byte[] keyBytes = derivPass.GetBytes(256 / 8);
            RijndaelManaged symmK = new RijndaelManaged();
            symmK.Mode = CipherMode.CBC;

            byte[] cipherTextBytes = null;

            using (ICryptoTransform encryptor = symmK.CreateEncryptor(keyBytes, initVecB))
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    using (CryptoStream cryptoStream = new CryptoStream(memStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(ishTextB, 0, ishTextB.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherTextBytes = memStream.ToArray();
                        memStream.Close();
                        cryptoStream.Close();
                    }
                }
            }

            symmK.Clear();
            return Convert.ToBase64String(cipherTextBytes);
        }

        //метод дешифрования строки
        public  string Decode(string Data)
        {
            if (string.IsNullOrEmpty(Data))
                return "";

            byte[] initVecB = Encoding.ASCII.GetBytes("a8doSuDitOz1hZe#");
            byte[] solB = Encoding.ASCII.GetBytes("E2tima");
            byte[] cipherTextBytes = Encoding.Default.GetBytes(Data);

            PasswordDeriveBytes derivPass = new PasswordDeriveBytes(Key, solB, "SHA1", 2);
            byte[] keyBytes = derivPass.GetBytes(256 / 8);

            RijndaelManaged symmK = new RijndaelManaged();
            symmK.Mode = CipherMode.CBC;

            byte[] plainTextBytes = new byte[cipherTextBytes.Length];
            int byteCount = 0;

            using (ICryptoTransform decryptor = symmK.CreateDecryptor(keyBytes, initVecB))
            {
                using (MemoryStream mSt = new MemoryStream(cipherTextBytes))
                {
                    using (CryptoStream cryptoStream = new CryptoStream(mSt, decryptor, CryptoStreamMode.Read))
                    {
                        byteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                        mSt.Close();
                        cryptoStream.Close();
                    }
                }
            }

            symmK.Clear();
            return Encoding.UTF8.GetString(plainTextBytes, 0, byteCount);
        }


    }
}
