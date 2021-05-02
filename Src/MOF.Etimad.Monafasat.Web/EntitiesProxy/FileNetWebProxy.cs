using Microsoft.AspNetCore.Mvc;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.Web.Infrastructure;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Web.EntitiesProxy
{
    public class FileNetWebProxy
    {
      private static ApiRequest<UploadFileModel> uploadRequest;
      private static ApiRequest<string> downloadRequest;
      private static ApiRequest<DeleteFileModel> deleteRequest;
      public static void Initialize(ControllerContext currentContext)
      {
         uploadRequest = new ApiRequest<UploadFileModel>(currentContext);
         downloadRequest = new ApiRequest<string>(currentContext);
         deleteRequest = new ApiRequest<DeleteFileModel>(currentContext);
   }

      public static async Task<string> UploadAsync(UploadFileModel model)
      {
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<string>(await uploadRequest.PostAsync("FileNet/Upload",model));
         return tenderList;
      }

      public static async Task<byte[]> DownloadAsync(string model)
      {
         var tenderList = JsonConvert.DeserializeObject<byte[]>(await downloadRequest.GetAsync("FileNet/Download?referenceId=" + model));
         return tenderList;
      }

      public static async Task<string> DeleteAsync(DeleteFileModel model)
      {
         string[] fileNetRefIds;
         if (model.FileName.Contains("/GetFile/"))
            fileNetRefIds = model.FileName.Split("/GetFile/")[1].Split(":");
         else
            fileNetRefIds = model.FileName.Split(":");
         model.FileName = fileNetRefIds[0];
         //Deserializing the response recieved from web api and storing into the Tender List  
         var tenderList = JsonConvert.DeserializeObject<string>(await deleteRequest.PostAsync("FileNet/Delete" , model));
         return tenderList;
      }

   }
}
