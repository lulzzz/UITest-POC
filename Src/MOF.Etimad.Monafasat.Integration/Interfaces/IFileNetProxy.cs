using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Integration
{
    public interface IFileNetProxy
    {
        Task<Byte[]> DownloadFile(string referenceId);
        Task<string> UploadFile(UploadFileModel file, IFileNetScan fileScan);
        Task<string> DeleteFile(string referenceId);
    }
}
