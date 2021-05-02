using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public interface IFileNetScan
    {
        /// <summary>
        /// Encrypt Byte Array Content
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        /// https://docs.microsoft.com/en-us/dotnet/api/system.security.cryptography.x509certificates.x509certificate2?view=netframework-4.7.2
        Task ScanFile(byte[] content);
    }
}
