using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using System.Linq;

namespace MvcHtmlHelpers
{
   public class ItemsColumnTagBuilder : TagBuilder
   {
      public ItemsColumnTagBuilder(int spanOf12 = 6, object htmlAttributes = null)
          : base("div")
      {
         var customAttributes = htmlAttributes != null ? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) : new RouteValueDictionary();

         if (!customAttributes.Keys.Any(c => c == "class"))
            customAttributes.Add("class", "");
         customAttributes["class"] += string.Format(" col-sm-{0}", spanOf12.ToString());

         this.MergeAttributes(customAttributes, true);
      }
   }
}