using MCI.Security.Cryptography;
using System;
using System.Linq;

namespace MOF.Etimad.Monafasat.SharedKernel.Encryption
{
    public class ConfigurationBasedStringEncrypter : IEncryptString
    {

        private static readonly string key = "4BBD3A44513248490C657885C932A3D8637144DE0321FB97892BCBD436434C19";
        private static readonly string iv = "FB4BF381051FB1B47AAF8AF296C3C150";
        private static readonly string _prefix = "e_";
        private static readonly int _hashIterationCounts = 1;
        private static readonly byte[] _keyArray = StringToByteArray(key);
        private static readonly byte[] _ivArray = StringToByteArray(iv);

        public ConfigurationBasedStringEncrypter()
        {
            //read settings from configuration
            if (string.IsNullOrEmpty(key))
            {
                throw new InvalidOperationException("[EncryptionKey] appSettings key is not defined or has no value.");
            }
            if (string.IsNullOrEmpty(iv))
            {
                throw new InvalidOperationException("[EncryptionIV] appSettings key is not defined or has no value.");
            }
        }

        private static byte[] StringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        #region IEncryptionSettingsProvider Members
        public string Encrypt(string value)
        {
            var encryptedBytes = MCI.Security.Cryptography.Encryption.Encrypt(value, _keyArray, _ivArray);
            var encrypted = Convert.ToBase64String(encryptedBytes);
            return encrypted;
        }
        public string Decrypt(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;
            else
            {
                var bytes = Convert.FromBase64String(value);
                var decrypted = MCI.Security.Cryptography.Encryption.Decrypt(bytes, _keyArray, _ivArray);
                return decrypted;
            }
        }

        public string Hash(string value)
        {
            byte[] sha512Hash = Hashing.GenerateHash(value, null, _hashIterationCounts);
            return System.Text.Encoding.UTF8.GetString(sha512Hash);
        }

        public bool CompaireHash(string hashedText, string plainText)
        {
            plainText = Hash(plainText);
            return hashedText.Trim() == plainText.Trim();
        }

        public int HashIterationCounts
        {
            get { return _hashIterationCounts; }
        }

        public string Prefix
        {
            get { return _prefix; }
        }
        public bool KeyIVExist
        {
            get
            {
                return !string.IsNullOrWhiteSpace(key) && !string.IsNullOrWhiteSpace(iv);
            }
        }
        #endregion
    }
}
