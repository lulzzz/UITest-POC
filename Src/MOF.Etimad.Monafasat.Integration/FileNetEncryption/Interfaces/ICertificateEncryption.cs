using System.Security.Cryptography;

namespace MOF.Etimad.Monafasat.Integration
{
    public interface ICertificateEncryption
    {
        /// <summary>
        /// Encrypt Byte Array Content
        /// </summary>
        /// <param name="content"></param>
        /// <param name="rsaPublicKey">PublicKey</param>
        /// <returns></returns>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x509certificate2?view=netframework-4.7.2
        byte[] Encrypt(byte[] content, RSACng rsaPublicKey);
        byte[] Decrypt(byte[] content, RSACng rsaPrivateKey);
    }
}
