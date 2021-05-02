using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using System;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class FileNetController : BaseController
    {
        private readonly IFileNetService _fileNetService;
        private readonly ITenderAppService _tenderService;
        private readonly IOfferAppService _offerService;
        public FileNetController(IOfferAppService offerService, IFileNetService fileNetService, ITenderAppService tenderService, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _fileNetService = fileNetService;
            _tenderService = tenderService;

            _offerService = offerService;
        }

        [HttpPost("Upload")]
        [DisableRequestSizeLimit]
        public async Task<string> Upload([FromBody] UploadFileModel model)
        {
            Integration.UploadFileModel uploadFileModel = new Integration.UploadFileModel { FileNameField = model.FileNameField, FileContentField = model.FileContentField, FileLengthField = model.FileLengthField, MIMETypeField = model.MIMETypeField };
            string fileId = await _fileNetService.UploadFile(uploadFileModel);
            uploadFileModel = null;
            return fileId;
        }

        [AllowAnonymous]
        [HttpGet("Download")]
        public async Task<byte[]> Download(string referenceId)
        {
            byte[] file = null;
            Guid guid = new Guid();
            if (Guid.TryParse(referenceId.Replace("idd_", ""), out guid))
            {
                file = await _fileNetService.downlaodFile(referenceId);
            }
            return file;
        }

        [HttpPost("Delete")]
        public async Task<string> Delete([FromBody] DeleteFileModel model)
        {
            return await DeleteFileReferences(model);
        }

        private async Task<string> DeleteFileReferences(DeleteFileModel model)
        {
            if (model.ModuleType == (int)Enums.DeleteModule.Tender)
            {
                string isDeletedFile = "";
                var physicalDelete = await _tenderService.DeleteAttachmentAsync(model.FileName);
                if (physicalDelete)
                {
                    isDeletedFile = await _fileNetService.deleteFile(model.FileName);
                }
                return isDeletedFile;
            }
            var isDeleted = await _fileNetService.deleteFile(model.FileName);
            switch (model.ModuleType)
            {
                case (int)Enums.DeleteModule.Tender:
                    await _tenderService.DeleteAttachmentAsync(model.FileName);
                    break;
                case (int)Enums.DeleteModule.QuantityTable:
                    break;
                case (int)Enums.DeleteModule.Offer:
                    await _offerService.DeleteAttachement(model.FileName);
                    break;
                case (int)Enums.DeleteModule.Block:
                    break;
                case (int)Enums.DeleteModule.TechniciansReport:
                    await _tenderService.DeleteAttachmentAsync(model.FileName);
                    break;
                case (int)Enums.DeleteModule.TenderAttachementChanges:
                    await _tenderService.DeleteTenderAttachmentChangesAsync(model.FileName);
                    break;
                case (int)Enums.DeleteModule.Qualification:
                    await _tenderService.DeleteQualificationAttachments(model.FileName);
                    break;
            }
            return isDeleted;
        }
    }
}
