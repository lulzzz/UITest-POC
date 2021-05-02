using System;
using System.Security.Cryptography;
using System.Text;

namespace MOF.Etimad.Monafasat.Web.Helpers
{

   public class EncryptionHelper
   {
      private static byte[] key = { 170, 56, 39, 124, 31, 136, 211, 100, 180, 51, 111, 88, 217, 92, 214, 36, 164, 188, 71, 51, 36, 187, 195, 205, 87, 167, 81, 248, 173, 7, 194, 10 };
      private static byte[] iv = { 33, 162, 253, 195, 255, 140, 120, 198, 25, 99, 222, 141, 182, 152, 94, 28 };

      public static string Encrypt(string plainText)
      {
         var cipher = new RijndaelManaged();
         var encryptor = cipher.CreateEncryptor(key, iv);

         byte[] data = Encoding.Unicode.GetBytes(plainText);

         return Convert.ToBase64String(encryptor.TransformFinalBlock(data, 0, data.Length));
      }

      public static string Decrypt(string encryptedText)
      {
         var cipher = new RijndaelManaged();
         var decryptor = cipher.CreateDecryptor(key, iv);

         byte[] data = Convert.FromBase64String(encryptedText);

         return Encoding.Unicode.GetString(decryptor.TransformFinalBlock(data, 0, data.Length));
      }
   }
}
