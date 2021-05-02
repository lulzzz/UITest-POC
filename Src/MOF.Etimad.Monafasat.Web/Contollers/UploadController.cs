using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Base;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.Controllers
{
   public class UploadController : BaseController
   {
      // DICTIONARY OF ALL IMAGE FILE HEADER
      private readonly ILogger<UploadController> _logger;
      private readonly IApiClient _ApiClient;

      public UploadController(ILogger<UploadController> logger, IApiClient ApiClient, IOptionsSnapshot<RootConfiguration> rootConfiguration) : base(rootConfiguration)
      {
         _logger = logger;
         _ApiClient = ApiClient;
         _ApiClient.SetTimeout(_rootConfiguration.HttpClientSettingConfiguration.UploadTimeout);
         _rootConfiguration = rootConfiguration.Value;
      }

      //finUploader upload action
      [HttpPost]
      [DisableRequestSizeLimit]
      public async Task<ActionResult> Upload(IFormFile qqfile, string name)
      {
         return await UploadFile(name, qqfile);
      }

      [HttpPost]
      [ActionName("deleteFile")]
      public async Task<ActionResult> DeleteFileAsync(DeleteFileModel model)
      {
         try
         {
            string[] fileNetRefIds;
            if (model.FileName.Contains("/GetFile/"))
               fileNetRefIds = model.FileName.Split("/GetFile/")[1].Split(":");
            else
               fileNetRefIds = model.FileName.Split(":");
            model.FileName = fileNetRefIds[0];
            await _ApiClient.PostAsync<string>("FileNet/Delete", null, model);
            return Json(new { status = 1, success = true });
         }
         catch
         {
            return Json(new { status = 0, success = false, message = "خطأ أثناء حذف الملف!" });
         }
      }

      [HttpGet]
      public async Task<FileResult> GetFile(string Id, int forView = 0)
      {
         string[] names = Id.Split(":");
         var file = await _ApiClient.GetAsync<byte[]>("FileNet/Download", new Dictionary<string, object> { { "referenceId", names[0] } });
         var fileExt = Path.GetExtension(Id);
         var ct = "image/png";
         try
         {
            if (!(file == null || file.Length == 0))
            {
               if (fileExt.ToUpper() == (".PDF"))
                  ct = "application/pdf";
               else if (fileExt.ToUpper() == (".TIF"))
                  ct = "image/tif";
               if (forView == 1)
               {
                  //View File
                  MemoryStream memStream = new MemoryStream();
                  BinaryFormatter binForm = new BinaryFormatter();
                  memStream.Write(file, 0, file.Length);
                  memStream.Seek(0, SeekOrigin.Begin);
                  binForm.Deserialize(memStream);
                  FileStreamResult result = new FileStreamResult(memStream, ct);
                  return result;
               }
               else
               {
                  //DownloadFile
                  FileContentResult result = File(file, ct, names[1]);
                  return result;
               }
            }
            return null;
         }
         catch
         {
            return null;
         }
      }

      private async Task<ActionResult> UploadFile(string name, IFormFile postedFile)
      {
         if (string.IsNullOrEmpty(name))
            throw new InvalidOperationException("لا يمكن إجراء هذه العملية!");
         if (postedFile.FileName.Length > 100) return Json(new { status = 0, success = false, fileName = postedFile.FileName, error = "اسم الملف طويل الحد المسموح به 100 حرف.!" });

         string postedFileName, postedFileExt, newFileName = string.Empty;
         Stream inputStream;
         byte[] bytes = null;
         try
         {
            postedFileName = Path.GetFileName(postedFile.FileName);
            inputStream = postedFile.OpenReadStream();
            postedFileExt = Path.GetExtension(postedFileName);
            using (MemoryStream ms = new MemoryStream())
            {
               inputStream.CopyTo(ms);
               bytes = ms.ToArray();
            }
            if (!string.IsNullOrEmpty(postedFileName))
            {
               postedFileName = RemoveSpecialChars(postedFileName);
            }
            UploadFileModel model = new UploadFileModel { FileNameField = postedFileName, FileContentField = bytes, FileLengthField = bytes.Length.ToString(), MIMETypeField = postedFileExt };
            newFileName = await _ApiClient.PostAsync("FileNet/Upload", null, model);
            return Json(new { status = 1, success = true, fileName = newFileName + ":" + postedFileName, message = "تم التحميل!" });
         }
         catch (Exception ex)
         {
            _logger.LogError(ex.Message + "\n" + ex.StackTrace);
            return Json(new { status = 0, success = false, fileName = newFileName, error = "فشل التحميل!" });
         }
      }
   }
}
