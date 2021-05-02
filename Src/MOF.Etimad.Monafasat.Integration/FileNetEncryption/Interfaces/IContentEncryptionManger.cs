using System.IO;

namespace MOF.Etimad.Monafasat.Integration
{
    public interface IContentEncryptionManger
    {
        byte[] Encrypt(byte[] content, string certName);

        string Encrypt(Stream content, string certName);

        byte[] Decrypt(byte[] content, string certName);

        string Decrypt(Stream content, string certName);
    }
}
