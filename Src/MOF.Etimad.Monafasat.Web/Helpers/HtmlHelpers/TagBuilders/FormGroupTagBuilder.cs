using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace MvcHtmlHelpers
{
   public class FormGroupTagBuilder : TagBuilder
   {
      public FormGroupTagBuilder(object htmlAttributes = null)
          : base("div")
      {
         var customAttributes = htmlAttributes != null ? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) : new RouteValueDictionary();
         if (!customAttributes.Keys.Any(k => k == "class"))
            customAttributes.Add("class", "");
         customAttributes["class"] += " form-group";

         this.MergeAttributes(customAttributes);
      }
   }
}