using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace MOF.Etimad.Monafasat.Integration
{
    public class ContentEncryptionManger : ProxyBase, IContentEncryptionManger
    {

        private readonly ICertificateEncryption _contentEncryption;
        public ContentEncryptionManger(ICertificateEncryption contentEncryption, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _contentEncryption = contentEncryption;
        }

        public ContentEncryptionManger(IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
        }
        /// <summary>
        /// Encrypt Byte Array Content
        /// </summary>
        /// <param name="content">Content</param>
        /// <param name="certName">Certificate Name Like (CN=MoF_Cert)</param>
        /// <returns>byte[]</returns>
        public byte[] Encrypt(byte[] content, string certName)
        {
            var certificate = GetCertificateFromStore(certName);
            var rsaPublicKey = (System.Security.Cryptography.RSACng)certificate.PublicKey.Key;
            var result = _contentEncryption.Encrypt(content, rsaPublicKey);
            return result;
        }

        public string Encrypt(Stream content, string certName)
        {
            using (var stream = new MemoryStream())
            {
                content.CopyTo(stream);
                var result = Encrypt(stream.ToArray(), certName);
                return Convert.ToBase64String(result, 0, result.Length);
            }
        }

        public byte[] Decrypt(byte[] content, string certName)
        {
            var certificate = GetCertificateFromStore(certName);
            if (!certificate.HasPrivateKey)
                throw new BusinessRuleException("No Private Key found");
            RSACng rsaPrivateKey = (RSACng)certificate.PrivateKey;
            var result = _contentEncryption.Decrypt(content, rsaPrivateKey);
            return result;
        }

        public string Decrypt(Stream content, string certName)
        {
            using (var stream = new MemoryStream())
            {
                content.CopyTo(stream);
                var result = Decrypt(stream.ToArray(), certName);
                return Convert.ToBase64String(result, 0, result.Length);
            }
        }

        private X509Certificate2 GetCertificateFromStore(string certName)
        {
            certName = certName.Trim();
            // Get the certificate store for the current user.
            X509Store store = new X509Store(StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
            // Place all certificates in an X509Certificate2Collection object.
            X509Certificate2Collection certCollection = store.Certificates;
            // If using a certificate with a trusted root you do not need to FindByTimeValid, instead:
            X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
            X509Certificate2Collection signingCert = currentCerts.Find(X509FindType.FindBySubjectName, certName, false);
            if (signingCert.Count == 0)
            {
                store.Close();
                throw new BusinessRuleException("No Certificate Found");
            }
            else
                // Return the first certificate in the collection, has the right name and is current.
                return signingCert[0];
        }
    }
}
