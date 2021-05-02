using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public class FileNetService : IFileNetService
    {
        private readonly IFileNetProxy _fileNetProxy; private readonly IFileNetScan _fileScan;

        public FileNetService(IFileNetScan fileScan, IFileNetProxy fileNetProxy)
        {
            _fileScan = fileScan;
            _fileNetProxy = fileNetProxy;
        }
        public async Task<string> UploadFile(UploadFileModel model)
        {
            return await _fileNetProxy.UploadFile(model, _fileScan);
        }
        public async Task<byte[]> downlaodFile(string referenceId)
        {
            return await _fileNetProxy.DownloadFile(referenceId);
        }

        public async Task<string> deleteFile(string referenceId)
        {
            try
            {
                return await _fileNetProxy.DeleteFile(referenceId);
            }
            catch
            {
                throw new BusinessRuleException(MOF.Etimad.Monafasat.Resources.SharedResources.ErrorMessages.UnexpectedError);
            }
        }
    }
}
