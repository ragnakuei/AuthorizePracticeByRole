using System;
using System.Security.Cryptography;
using System.Text;

namespace SharedLibrary.Helpers
{
    public static class PasswordHelpers
    {
        private static readonly string PasswordSalt = "abcd";

        //用來做 SHA256 加密 (含 salt)
        public static string EncryptSha256(this string str)
        {
            //建立一個SHA256
            SHA256 sha256 = new SHA256CryptoServiceProvider();

            //將字串轉為Byte[]，這邊有加 salt，salt 的加法可自己定義
            byte[] source = Encoding.Default.GetBytes(str + PasswordSalt);

            //進行SHA256加密
            byte[] crypto = sha256.ComputeHash(source);

            //把加密後的字串從Byte[]轉為字串
            string result = Convert.ToBase64String(crypto);

            //輸出結果
            //Console.Write("SHA256加密:  " + result);
            return result;
        }
    }
}