using System;
using System.Runtime.Serialization;

namespace MOF.Etimad.Monafasat.Web.Models
{
   [Serializable]
   public class FileUploadModel
   {
      public string Name { get; set; }

      public int FileMaxSize { get; set; }

      public string[] AllowedExtensions { get; set; }

      public FileUploadModel() { }

      protected FileUploadModel(SerializationInfo info, StreamingContext context)
      {
         //var Name = info.GetString("Name");
      }
   }
}
