using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using MOF.Etimad.Monafasat.Web;
using MOF.Etimad.Monafasat.Web.Models;
//using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace MvcHtmlHelpers
{
   public static class FileUploaderExtensions
   {
      private static readonly string[] allowedExtensionsDeafault = { "jpg", "jpeg", "png", "bmp" };

      /// <summary>
      /// Return file uploader control initialized with finUploader plugin
      /// </summary>
      /// <param name="name">Unique name for the uploader</param>
      /// <param name="controllerName">Upload controller name that contains upload and delete actions</param>
      /// <param name="allowedExtensions">Allowed extensions to upload (we be default if null)</param>
      /// <param name="fileMaxSize">Maximum file size to upload</param>
      /// <param name="numberOfFilesToUpload">For multiple files uploader</param>
      /// <param name="onSuccessCallback">Javascript function name to call after upload success</param>
      /// <param name="onDeleteCallback">Javascript function name to call after deleting afile from the uploader</param>
      /// <param name="required">Add required validation?</param>
      /// <param name="validationMessage">Validation message for required validation</param>
      /// <returns>HtmlString for uploader control</returns>
      public static IHtmlContent FileUploader<TModel>(
          this IHtmlHelper<TModel> helper,
          string name,
          string controllerName = "Upload",
          IEnumerable<string> allowedExtensions = null,
          int fileMaxSize = 2048,
         int numberOfFilesToUpload = 1,
          bool multiple = false,
          string onSuccessCallback = "", string onDeleteCallback = "",
          bool required = false, string validationMessage = "الملف مطلوب")
      {
         HtmlContentBuilder result = new HtmlContentBuilder();
         if (allowedExtensions == null || !allowedExtensions.Any())
         {
            allowedExtensions = allowedExtensionsDeafault;
         }

         var textBox = helper.TextBox(name, string.Empty,
             new
             {
                Style = "position:absolute; opacity:0.0; visibility:hidden",
                data_val = required.ToString().ToLower(),
                data_val_required = validationMessage
             });

         result.AppendHtml(textBox)
             .AppendHtml(new FileUploaderTagBuilder(name, controllerName, allowedExtensions, fileMaxSize, numberOfFilesToUpload, multiple, onSuccessCallback, onDeleteCallback));

         GenerateUploadModelSession(helper.ViewContext.HttpContext.Session, name, allowedExtensions, fileMaxSize);

         return result;
      }

      /// <summary>
      /// Return file uploader control initialized with finUploader plugin
      /// </summary>
      /// <param name="expression">Lambda expression for the string property to bind file path to</param>
      /// <param name="controllerName">Upload controller name that contains upload and delete actions</param>
      /// <param name="allowedExtensions">Allowed extensions to upload</param>
      /// <param name="fileMaxSize">Maximum file size to upload</param>
      /// <param name="onSuccessCallback">Javascript function name to call after upload success</param>
      /// <param name="onDeleteCallback">Javascript function name to call after deleting afile from the uploader</param>
      /// <returns>HtmlString for uploader control</returns>
      public static IHtmlContent FileUploaderFor<TModel, TProperty>(
          this IHtmlHelper<TModel> helper,
          Expression<Func<TModel, TProperty>> expression,
          string controllerName = "Upload",
          IEnumerable<string> allowedExtensions = null,
          int fileMaxSize = 2048,
            int numberOfFilesToUpload = 1,
              bool multiple = false,
          string onSuccessCallback = "",
          string onDeleteCallback = "")
      {
         var expresionProvider = helper.ViewContext.HttpContext.RequestServices
           .GetService(typeof(ModelExpressionProvider)) as ModelExpressionProvider;
         HtmlContentBuilder result = new HtmlContentBuilder();
         string name = expresionProvider.GetExpressionText(expression);
         if (allowedExtensions == null || !allowedExtensions.Any())
         {
            allowedExtensions = allowedExtensionsDeafault;
         }

         var textBox = helper.TextBoxFor(expression,
             new
             {
                Style = "position:absolute; opacity:0.0; visibility:hidden"
             });

         result.AppendHtml(textBox)
             .AppendHtml(new FileUploaderTagBuilder(name, controllerName, allowedExtensions, fileMaxSize, numberOfFilesToUpload, multiple, onSuccessCallback, onDeleteCallback));

         GenerateUploadModelSession(helper.ViewContext.HttpContext.Session, name, allowedExtensions, fileMaxSize);

         return result;
      }

      /// <summary>
      /// Return file uploader control full item (with caption) initialized with finUploader plugin
      /// </summary>
      /// <param name="expression">Lambda expression for the string property to bind file path to</param>
      /// <param name="controllerName">Upload controller name that contains upload and delete actions</param>
      /// <param name="allowedExtensions">Allowed extensions to upload</param>
      /// <param name="fileMaxSize">Maximum file size to upload</param>
      /// <param name="withValidation">Add Validation message control?</param>
      /// <param name="spanOf12">Number represents bootstrap column span for the item width</param>
      /// <param name="onSuccessCallback">Javascript function name to call after upload success</param>
      /// <param name="onDeleteCallback">Javascript function name to call after deleting afile from the uploader</param>
      /// <param name="hint">Optional text to add after the caption label as a hint</param>
      /// <returns>HtmlString for uploader control with caption label</returns>
      public static IHtmlContent FileUploadFormItemFor<TModel, TProperty>(
          this IHtmlHelper<TModel> helper,
          Expression<Func<TModel, TProperty>> expression,
          string controllerName = "Upload",
          IEnumerable<string> allowedExtensions = null,
          int fileMaxSize = 2048,
            int numberOfFilesToUpload = 1,
              bool multiple = false,
          bool withValidation = true,
          int spanOf12 = 6,
          string onSuccessCallback = "",
          string onDeleteCallback = "",
          string hint = "")
      {
         HtmlContentBuilder htmlContentBuilder = new HtmlContentBuilder();
         var expresionProvider = helper.ViewContext.HttpContext.RequestServices
          .GetService(typeof(ModelExpressionProvider)) as ModelExpressionProvider;
         string name = expresionProvider.GetExpressionText(expression);
         if (allowedExtensions == null || !allowedExtensions.Any())
         {
            allowedExtensions = allowedExtensionsDeafault;
         }

         var col = new ItemsColumnTagBuilder(spanOf12, null);
         var frmGroup = new FormGroupTagBuilder();
         var uploader = helper.FileUploaderFor(expression, controllerName, allowedExtensions, fileMaxSize, numberOfFilesToUpload, multiple, onSuccessCallback, onDeleteCallback);

         htmlContentBuilder.AppendHtml(col.RenderStartTag())
             .AppendHtml(frmGroup.RenderStartTag())
             .AppendHtml(helper.LabelFor(expression));

         if (!string.IsNullOrEmpty(hint))
         {
            htmlContentBuilder.AppendHtml(new FormItemHintTagBuilder(hint));
         }

         htmlContentBuilder.AppendHtml(uploader);

         if (withValidation)
         {
            htmlContentBuilder.AppendHtml(helper.ValidationMessageFor(expression));
         }

         htmlContentBuilder.AppendHtml(frmGroup.RenderEndTag())
             .AppendHtml(col.RenderEndTag());

         return htmlContentBuilder;
      }

      /// <summary>
      /// Return file uploader control full item (with caption) initialized with finUploader plugin
      /// </summary>
      /// <param name="name">Unique name for the uploader</param>
      /// <param name="spanOf12">Number represents bootstrap column span for the item width</param>
      /// <param name="labelText">Caption text</param>
      /// <param name="controllerName">Upload controller name that contains upload and delete actions</param>
      /// <param name="allowedExtensions">Allowed extensions to upload</param>
      /// <param name="fileMaxSize">Maximum file size to upload</param>
      /// <param name="required">Add required validation?</param>
      /// <param name="validationMessage">Validation message for required validation</param>
      /// <param name="onSuccessCallback">Javascript function name to call after upload success</param>
      /// <param name="onDeleteCallback">Javascript function name to call after deleting afile from the uploader</param>
      /// <param name="hint">Optional text to add after the caption label as a hint</param>
      /// <returns>HtmlString for uploader control with caption label</returns>
      public static IHtmlContent FileUploadFormItem<TModel>(
          this IHtmlHelper<TModel> helper,
          string name,
          int spanOf12 = 6,
          string labelText = "",
          string controllerName = "Upload",
          IEnumerable<string> allowedExtensions = null,
          int fileMaxSize = 2048,
             int numberOfFilesToUpload = 1,
              bool multiple = false,
          bool required = true, string validationMessage = "الملف مطلوب",
          string onSuccessCallback = "", string onDeleteCallback = "", string hint = "")
      {
         HtmlContentBuilder htmlContentBuilder = new HtmlContentBuilder();
         if (allowedExtensions == null || !allowedExtensions.Any())
         {
            allowedExtensions = allowedExtensionsDeafault;
         }

         var col = new ItemsColumnTagBuilder(spanOf12, null);
         var frmGroup = new FormGroupTagBuilder();
         var uploader = helper.FileUploader(name, controllerName, allowedExtensions, fileMaxSize, numberOfFilesToUpload, multiple, onSuccessCallback, onDeleteCallback, required, validationMessage);

         htmlContentBuilder.AppendHtml(col.RenderStartTag())
             .AppendHtml(frmGroup.RenderStartTag())
             .AppendHtml(helper.Label(labelText));

         if (!string.IsNullOrEmpty(hint))
         {
            htmlContentBuilder.AppendHtml(new FormItemHintTagBuilder(hint));
         }

         htmlContentBuilder.AppendHtml(uploader);

         if (required)
         {
            htmlContentBuilder.AppendHtml(helper.ValidationMessage(name, validationMessage));
         }

         htmlContentBuilder.AppendHtml(frmGroup.RenderEndTag())
         .AppendHtml(col.RenderEndTag());

         return htmlContentBuilder;
      }

      private static FileUploadModel GenerateUploadModelSession(ISession session, string name, IEnumerable<string> allowedExtensions, int fileMaxSize)
      {
         //fill uploader model in session for server side validation
         var sessionModels = session.GetObject<List<FileUploadModel>>("FileUploadModels");
         List<FileUploadModel> fileUploadModels = sessionModels != null ? sessionModels : new List<FileUploadModel>();

         FileUploadModel model = fileUploadModels.FirstOrDefault(m => m.Name == name);
         if (model == null)
         {
            model = new FileUploadModel();
            fileUploadModels.Add(model);
         }

         model.Name = name;
         model.FileMaxSize = fileMaxSize;
         model.AllowedExtensions = allowedExtensions.ToArray();

         session.SetObject("FileUploadModels", fileUploadModels);

         return model;
      }


   }



}
