using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.IO;
using System.Security.Cryptography;

namespace MOF.Etimad.Monafasat.Integration
{
    public class CertificateEncryption : ProxyBase, ICertificateEncryption
    {
        public CertificateEncryption(IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {

        }
        public byte[] Encrypt(byte[] content, RSACng rsaPublicKey)
        {
            using (var aesManaged = new AesManaged())
            {
                aesManaged.KeySize = 256;
                aesManaged.BlockSize = 128;
                aesManaged.Mode = CipherMode.CBC;
                using (var transform = aesManaged.CreateEncryptor())
                {
                    var keyFormatter = new RSAPKCS1KeyExchangeFormatter(rsaPublicKey);
                    var keyEncrypted = keyFormatter.CreateKeyExchange(aesManaged.Key, aesManaged.GetType());
                    var LenK = new byte[4];
                    var LenIV = new byte[4];
                    var lKey = keyEncrypted.Length;
                    LenK = BitConverter.GetBytes(lKey);
                    var lIV = aesManaged.IV.Length;
                    LenIV = BitConverter.GetBytes(lIV);
                    using (var outFs = new MemoryStream())
                    {
                        outFs.Write(LenK, 0, 4);
                        outFs.Write(LenIV, 0, 4);
                        outFs.Write(keyEncrypted, 0, lKey);
                        outFs.Write(aesManaged.IV, 0, lIV);
                        using (var outStreamEncrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                        {
                            // By encrypting a chunk at
                            // a time, you can save memory
                            // and accommodate large files.
                            var count = 0;
                            var offset = 0;
                            // blockSizeBytes can be any arbitrary size.
                            var blockSizeBytes = aesManaged.BlockSize / 8;
                            var data = new byte[blockSizeBytes];
                            var bytesRead = 0;
                            using (var inFs = new MemoryStream(content))
                            {
                                do
                                {
                                    count = inFs.Read(data, 0, blockSizeBytes);
                                    offset += count;
                                    outStreamEncrypted.Write(data, 0, count);
                                    bytesRead += blockSizeBytes;
                                }
                                while (count > 0);
                                inFs.Close();
                            }
                            outStreamEncrypted.FlushFinalBlock();
                            outStreamEncrypted.Close();
                        }
                        outFs.Close();
                        return outFs.ToArray();
                    }
                }
            }
        }

        public byte[] Decrypt(byte[] content, RSACng rsaPrivateKey)
        {

            using (AesManaged aesManaged = new AesManaged())
            {
                aesManaged.KeySize = 256;
                aesManaged.BlockSize = 128;
                aesManaged.Mode = CipherMode.CBC;

                // Create byte arrays to get the length of
                // the encrypted key and IV.
                // These values were stored as 4 bytes each
                // at the beginning of the encrypted package.
                byte[] LenK = new byte[4];
                byte[] LenIV = new byte[4];
                using var inFs = new MemoryStream(content);
                inFs.Seek(0, SeekOrigin.Begin);
                inFs.Seek(0, SeekOrigin.Begin);
                inFs.Read(LenK, 0, 3);
                inFs.Seek(4, SeekOrigin.Begin);
                inFs.Read(LenIV, 0, 3);

                // Convert the lengths to integer values.
                int lenK = BitConverter.ToInt32(LenK, 0);
                int lenIV = BitConverter.ToInt32(LenIV, 0);
                int startC = lenK + lenIV + 8;
                byte[] KeyEncrypted = new byte[lenK];
                byte[] IV = new byte[lenIV];

                inFs.Seek(8, SeekOrigin.Begin);
                inFs.Read(KeyEncrypted, 0, lenK);
                inFs.Seek(8 + lenK, SeekOrigin.Begin);
                inFs.Read(IV, 0, lenIV);

                // Use RSACryptoServiceProvider
                // to decrypt the AesManaged key.
                byte[] KeyDecrypted = rsaPrivateKey.Decrypt(KeyEncrypted, RSAEncryptionPadding.Pkcs1);

                // Decrypt the key.
                using ICryptoTransform transform = aesManaged.CreateDecryptor(KeyDecrypted, IV);

                // Decrypt the cipher text from
                // from the FileSteam of the encrypted
                // file (inFs) into the FileStream
                // for the decrypted file (outFs).
                using var outFs = new MemoryStream();
                int count = 0;
                int offset = 0;

                int blockSizeBytes = aesManaged.BlockSize / 8;
                byte[] data = new byte[blockSizeBytes];

                // By decrypting a chunk a time,
                // you can save memory and
                // accommodate large files.

                // Start at the beginning
                // of the cipher text.
                inFs.Seek(startC, SeekOrigin.Begin);
                using (CryptoStream outStreamDecrypted = new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                {
                    do
                    {
                        count = inFs.Read(data, 0, blockSizeBytes);
                        offset += count;
                        outStreamDecrypted.Write(data, 0, count);

                    }
                    while (count > 0);

                    outStreamDecrypted.FlushFinalBlock();
                    outStreamDecrypted.Close();
                }
                inFs.Close();
                outFs.Close();
                return outFs.ToArray();
            }
        }
    }
}
