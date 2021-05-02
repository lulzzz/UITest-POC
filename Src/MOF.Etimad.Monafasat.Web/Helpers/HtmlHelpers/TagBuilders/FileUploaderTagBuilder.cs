using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace MvcHtmlHelpers
{
   public class FileUploaderTagBuilder : TagBuilder
   {
      public FileUploaderTagBuilder(string name, string controllerName, IEnumerable<string> allowedExtensions, int fileMaxSize, int numberOfFilesToUpload, bool multiple, string onSuccessCallback, string onDeleteCallback)
          : base("div")
      {
         var id = TagBuilder.CreateSanitizedId(name, "_");
         string divName = string.Format("{0}_attachement_div", id);
         string allowedExtensionsString =
             string.Format("[{0}]", string.Join(',', allowedExtensions.Select(x => $"'{x}'")));

         InnerHtml.AppendHtml(string.Format("<div id='{0}' class='uploader'><div class='file_uploader'></div><div class='file_uploader_message'></div></div>", divName));
         InnerHtml.AppendHtml(string.Format("<script>$(function () {{initUploader('{0}', '{1}', '{2}', {3}, {4},  '{5}', '{6}', '{7}', {8});}});</script>",

             divName,
             id,
             controllerName,
             allowedExtensionsString,
             fileMaxSize.ToString(),
             onSuccessCallback,
             onDeleteCallback,
             multiple,
             numberOfFilesToUpload
             ));
      }
   }
}
