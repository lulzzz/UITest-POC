using MOF.Etimad.Monafasat.Integration;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public interface IFileNetService
    {
        Task<string> UploadFile(UploadFileModel model);
        Task<byte[]> downlaodFile(string referenceId);
        Task<string> deleteFile(string referenceId);
    }
}
