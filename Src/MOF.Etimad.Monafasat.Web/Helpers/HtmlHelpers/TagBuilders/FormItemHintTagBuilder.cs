using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MvcHtmlHelpers
{
   public class FormItemHintTagBuilder : TagBuilder
   {
      public FormItemHintTagBuilder(string hint)
          : base("i")
      {
         AddCssClass("mci-hint");
         this.InnerHtml.SetContent(hint);
      }
   }
}