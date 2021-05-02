using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
//using Microsoft.AspNetCore.Mvc.ViewFeatures.Internal;
using Microsoft.AspNetCore.Routing;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Encodings.Web;

namespace MvcHtmlHelpers
{
   public static class DisplayExtensions
   {
      /// <summary>
      /// Returns display value with caption as bootstrap form item
      /// </summary>
      /// <param name="value">Item value to display</param>
      /// <param name="labelText">Item caption text</param>
      /// <param name="spanOf12">Number represents bootstrap column span for the item width</param>
      /// <param name="htmlAttributes">custom htmlAttributes applied to the value tag</param>
      /// <param name="postBackValue">If true, value will be posted back in hidden input field</param>
      /// <param name="name">Unique name for the hidden field</param>
      ///  <param name="hint">Optional text to add after the caption label as a hint</param>
      /// <returns>HtmlString</returns>
      public static IHtmlContent DisplayFormItem<TModel>(
       this IHtmlHelper<TModel> helper, string value, string labelText = "", int spanOf12 = 6, object htmlAttributes = null, bool postBackValue = false, string name = "postValue", string hint = "")
      {
         HtmlContentBuilder htmlContentBuilder = new HtmlContentBuilder();

         var col = new ItemsColumnTagBuilder(spanOf12, null);
         var frmGroup = new FormGroupTagBuilder();
         var valueParagraph = new ParagraphTagBuilder(name, value, htmlAttributes);

         htmlContentBuilder.AppendHtml(col.RenderStartTag())
             .AppendHtml(frmGroup.RenderStartTag())
             .AppendHtml(helper.Label(labelText));

         if (!string.IsNullOrEmpty(hint))
         {
            htmlContentBuilder.AppendHtml(new FormItemHintTagBuilder(hint));
         }

         htmlContentBuilder.AppendHtml(valueParagraph);

         if (postBackValue)
         {
            htmlContentBuilder.AppendHtml(helper.Hidden(name, value));
         }

         htmlContentBuilder.AppendHtml(frmGroup.RenderEndTag())
             .AppendHtml(col.RenderEndTag());

         return htmlContentBuilder;
      }

      /// <summary>
      /// Returns display value for model property with the display name as caption in bootstrap form item
      /// </summary>
      /// <param name="expression">Lambda expression for the property</param>
      /// <param name="spanOf12">Number represents bootstrap column span for the item width</param>
      /// <param name="htmlAttributes">custom htmlAttributes applied to the value tag</param>
      /// <param name="postBackValue">If true, value will be posted back in hidden input field</param>
      /// <param name="hint">Optional text to add after the caption label as a hint</param>
      /// <returns>HtmlString</returns>
      public static IHtmlContent DisplayFormItemFor<TModel, TProperty>(
       this IHtmlHelper<TModel> helper,
       Expression<Func<TModel, TProperty>> expression, int spanOf12 = 6, object htmlAttributes = null, bool postBackValue = false, string hint = "")
      {
         var expresionProvider = helper.ViewContext.HttpContext.RequestServices
          .GetService(typeof(ModelExpressionProvider)) as ModelExpressionProvider;
         HtmlContentBuilder htmlContentBuilder = new HtmlContentBuilder();
         string name = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expresionProvider.GetExpressionText(expression));

         var col = new ItemsColumnTagBuilder(spanOf12, null);
         var frmGroup = new FormGroupTagBuilder();
         var valueParagraph = new ParagraphTagBuilder(name, htmlAttributes);

         valueParagraph.InnerHtml.AppendHtml(helper.DisplayFor(expression));

         htmlContentBuilder.AppendHtml(col.RenderStartTag())
             .AppendHtml(frmGroup.RenderStartTag())
             .AppendHtml(helper.LabelFor(expression));

         if (!string.IsNullOrEmpty(hint))
         {
            htmlContentBuilder.AppendHtml(new FormItemHintTagBuilder(hint));
         }

         htmlContentBuilder.AppendHtml(valueParagraph);

         if (postBackValue)
         {
            htmlContentBuilder.AppendHtml(helper.HiddenFor(expression));
         }

         htmlContentBuilder.AppendHtml(frmGroup.RenderEndTag())
             .AppendHtml(col.RenderEndTag());

         return htmlContentBuilder;
      }

      /// <summary>
      /// Returns display value with caption as bootstrap horizontal form item
      /// </summary>
      /// <param name="value">Item value to display</param>
      /// <param name="labelText">Item caption text</param>
      /// <param name="spanOf12">Number represents bootstrap column span for the item width</param>
      /// <param name="htmlAttributes">custom htmlAttributes applied to the value tag</param>
      /// <param name="postBackValue">If true, value will be posted back in hidden input field</param>
      /// <param name="name">Unique name for the hidden field</param>
      /// <returns>HtmlString</returns>
      public static IHtmlContent DisplayFormItem_H<TModel>(
       this IHtmlHelper<TModel> helper, string value, string labelText = "", int spanOf12 = 6, object htmlAttributes = null, bool postBackValue = false, string name = "postValue")
      {
         HtmlContentBuilder htmlContentBuilder = new HtmlContentBuilder();

         var col = new ItemsColumnTagBuilder(spanOf12, null);
         var frmGroup = new FormDisplayTagBuilder();
         var valueSpan = new SpanTagBuilder(name, value, htmlAttributes);

         htmlContentBuilder.AppendHtml(col.RenderStartTag())
             .AppendHtml(frmGroup.RenderStartTag())
             .AppendHtml(helper.Label(labelText))
             .AppendHtml(valueSpan);

         if (postBackValue)
         {
            htmlContentBuilder.AppendHtml(helper.Hidden(name, value));
         }

         htmlContentBuilder.AppendHtml(frmGroup.RenderEndTag())
             .AppendHtml(col.RenderEndTag());

         return htmlContentBuilder;
      }

      /// <summary>
      /// Returns display value for model property with the display name as caption in bootstrap horizontal form item
      /// </summary>
      /// <param name="expression">Lambda expression for the property</param>
      /// <param name="spanOf12">Number represents bootstrap column span for the item width</param>
      /// <param name="htmlAttributes">custom htmlAttributes applied to the value tag</param>
      /// <param name="postBackValue">If true, value will be posted back in hidden input field</param>
      /// <returns>HtmlString</returns>
      public static IHtmlContent DisplayFormItemFor_H<TModel, TProperty>(
          this IHtmlHelper<TModel> helper,
          Expression<Func<TModel, TProperty>> expression, int spanOf12 = 6, object htmlAttributes = null, bool postBackValue = false)
      {
         var expresionProvider = helper.ViewContext.HttpContext.RequestServices
        .GetService(typeof(ModelExpressionProvider)) as ModelExpressionProvider;
         string name = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expresionProvider.GetExpressionText(expression));

         HtmlContentBuilder htmlContentBuilder = new HtmlContentBuilder();

         var col = new ItemsColumnTagBuilder(spanOf12, null);
         var frmGroup = new FormDisplayTagBuilder();
         var valueSpan = new SpanTagBuilder(name, htmlAttributes);

         valueSpan.InnerHtml.AppendHtml(helper.DisplayFor(expression));

         htmlContentBuilder.AppendHtml(col.RenderStartTag())
             .AppendHtml(frmGroup.RenderStartTag())
             .AppendHtml(helper.LabelFor(expression))
             .AppendHtml(valueSpan);

         if (postBackValue)
         {
            htmlContentBuilder.AppendHtml(helper.HiddenFor(expression));
         }

         htmlContentBuilder.AppendHtml(frmGroup.RenderEndTag())
             .AppendHtml(col.RenderEndTag());

         return htmlContentBuilder;
      }

      #region truncate display

      /// <summary>
      /// Returns display tag of a value truncated by the given length and use bootstrap pop-over to display the complete value
      /// </summary>
      /// <param name="name">Unique name for the display tag</param>
      /// <param name="value">Value to display</param>
      /// <param name="textMaxLength">Maximum length of the displayed text</param>
      /// <param name="popOverTitle">The pop-over title text</param>
      /// <param name="seeMoreHtml">html text for see more if the text is truncated</param>
      /// <param name="showAllContentInPopover">True if you want to show the complete value in the pop-over. False to display the rest of the value only</param>
      /// <returns>HtmlString</returns>
      public static HtmlContentBuilder DisplayWithTruncate<TModel>(
          this IHtmlHelper<TModel> helper, string name, string value, int textMaxLength = 50, string popOverTitle = "", string seeMoreHtml = " المزيد >>",
          bool showAllContentInPopover = false, TruncateMethod method = TruncateMethod.Popover)
      {
         HtmlContentBuilder result = new HtmlContentBuilder();

         var needTruncate = value.Length > textMaxLength;
         if (needTruncate)//compute real max length with space
         {
            string seperators = " .,-_،()[]&!";
            if (!seperators.Contains(value[textMaxLength]) && !seperators.Contains(value[textMaxLength - 1]))
               textMaxLength = value.Substring(0, textMaxLength).LastIndexOfAny(seperators.ToCharArray());
         }

         string toDisplayText = needTruncate ? value.Substring(0, textMaxLength) + " ... " : value;
         string popoverText = value;
         if (needTruncate && !showAllContentInPopover)
            popoverText = "..." + value.Substring(textMaxLength, value.Length - textMaxLength);

         result.Append(toDisplayText);

         if (needTruncate)
         {
            string truncateHtml = "";
            if (method == TruncateMethod.Popover)
            {
               truncateHtml = string.Format("<a id='{0}_popoverDiv' class='mci-popover' tabindex='0' data-toggle='popover' data-placement='bottom auto' data-trigger='focus' data-container='body' data-title='{1}' data-content='{2}'>{3}</a>", name, popOverTitle, popoverText, seeMoreHtml);
               truncateHtml += string.Format(@"
                            <script>
                                $(function () {{
                                    $('#{0}_popoverDiv').popover();
                                }});
                            </script>", name);
            }
            else
            {
               truncateHtml =
                   string.Format(
                       "<a id='{0}_popoverDiv' class='mci-popover' tabindex='0' data-toggle='modal' data-title='{1}' data-content='{2}'>{3}</a>",
                       name, popOverTitle, popoverText, seeMoreHtml);
            }
            result.AppendHtml(truncateHtml);
         }
         return result;
      }

      /// <summary>
      /// Returns display tag of a value truncated by the given length and use bootstrap pop-over to display the complete value
      /// </summary>
      /// <param name="expression">Lambda expression for the property</param>
      /// <param name="textMaxLength">Maximum length of the displayed text</param>
      /// <param name="popOverTitle">The pop-over title text</param>
      /// <param name="seeMoreHtml">html text for see more if the text is truncated</param>
      /// <param name="showAllContentInPopover">True if you want to show the complete value in the pop-over. False to display the rest of the value only</param>
      /// <returns>HtmlString</returns>
      public static HtmlContentBuilder DisplayWithTruncateFor<TModel, TProperty>(
          this IHtmlHelper<TModel> helper, Expression<Func<TModel, TProperty>> expression,
          int textMaxLength = 50, string popOverTitle = "", string seeMoreHtml = " المزيد >>", bool showAllContentInPopover = false, TruncateMethod method = TruncateMethod.Popover)
      {
         var expresionProvider = helper.ViewContext.HttpContext.RequestServices
      .GetService(typeof(ModelExpressionProvider)) as ModelExpressionProvider;

         string name = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expresionProvider.GetExpressionText(expression));
         IHtmlContent value = helper.DisplayFor(expression);

         HtmlContentBuilder result = helper.DisplayWithTruncate(name, value.GetString(), textMaxLength, popOverTitle, seeMoreHtml, showAllContentInPopover, method);
         return result;
      }

      /// <summary>
      /// Returns display item of a value with caption truncated by the given length and use bootstrap pop-over to display the complete value
      /// </summary>
      /// <param name="name">Unique name for the display tag</param>
      /// <param name="value">Value to display</param>
      /// <param name="labelText">Item caption text</param>
      /// <param name="spanOf12">Number represents bootstrap column span for the item width</param>
      /// <param name="textMaxLength">Maximum length of the displayed text</param>
      /// <param name="popOverTitle">The pop-over title text</param>
      /// <param name="seeMoreHtml">html text for see more if the text is truncated</param>
      /// <param name="showAllContentInPopover">True if you want to show the complete value in the pop-over. False to display the rest of the value only</param>
      /// <param name="htmlAttributes">custom htmlAttributes applied to the value tag</param>
      /// <param name="postBackValue">If true, value will be posted back in hidden input field</param>
      ///  <param name="hint">Optional text to add after the caption label as a hint</param>
      /// <returns>HtmlString</returns>
      public static IHtmlContent DisplayFormItemWithTruncate<TModel>(
          this IHtmlHelper<TModel> helper, string name, string value, string labelText = "", int spanOf12 = 6,
          int textMaxLength = 50, string popOverTitle = "", string seeMoreHtml = " المزيد >>", bool showAllContentInPopover = false,
          object htmlAttributes = null, bool postBackValue = false, string hint = "", TruncateMethod method = TruncateMethod.Popover)
      {
         HtmlContentBuilder htmlContentBuilder = new HtmlContentBuilder();

         var col = new ItemsColumnTagBuilder(spanOf12, null);
         var frmGroup = new FormGroupTagBuilder();
         var valueDisplay = new ParagraphTagBuilder(name, htmlAttributes);

         valueDisplay.InnerHtml.AppendHtml(helper.DisplayWithTruncate(name, value, textMaxLength, popOverTitle, seeMoreHtml, showAllContentInPopover, method));

         htmlContentBuilder.AppendHtml(col.RenderStartTag())
             .AppendHtml(frmGroup.RenderStartTag())
             .AppendHtml(helper.Label(labelText));

         if (!string.IsNullOrEmpty(hint))
         {
            htmlContentBuilder.AppendHtml(new FormItemHintTagBuilder(hint));
         }

         htmlContentBuilder.AppendHtml(valueDisplay);

         if (postBackValue)
         {
            htmlContentBuilder.AppendHtml(helper.Hidden(name, value));
         }

         htmlContentBuilder.AppendHtml(frmGroup.RenderEndTag())
             .AppendHtml(col.RenderEndTag());

         return htmlContentBuilder;
      }

      /// <summary>
      /// Returns display item of a model value with caption truncated by the given length and use bootstrap pop-over to display the complete value
      /// </summary>
      /// <param name="expression">Lambda expression for the property</param>
      /// <param name="spanOf12">Number represents bootstrap column span for the item width</param>
      /// <param name="textMaxLength">Maximum length of the displayed text</param>
      /// <param name="popOverTitle">The pop-over title text</param>
      /// <param name="seeMoreHtml">html text for see more if the text is truncated</param>
      /// <param name="showAllContentInPopover">True if you want to show the complete value in the pop-over. False to display the rest of the value only</param>
      /// <param name="htmlAttributes">custom htmlAttributes applied to the value tag</param>
      /// <param name="postBackValue">If true, value will be posted back in hidden input field</param>
      ///  <param name="hint">Optional text to add after the caption label as a hint</param>
      /// <returns>HtmlString</returns>
      public static IHtmlContent DisplayFormItemWithTruncateFor<TModel, TProperty>(
          this IHtmlHelper<TModel> helper,
          Expression<Func<TModel, TProperty>> expression, int spanOf12 = 6,
          int textMaxLength = 50, string popOverTitle = "", string seeMoreHtml = " المزيد >>",
          bool showAllContentInPopover = false, object htmlAttributes = null, bool postBackValue = false, string hint = "", TruncateMethod method = TruncateMethod.Popover)
      {
         var expresionProvider = helper.ViewContext.HttpContext.RequestServices
   .GetService(typeof(ModelExpressionProvider)) as ModelExpressionProvider;
         string name = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expresionProvider.GetExpressionText(expression));

         HtmlContentBuilder htmlContentBuilder = new HtmlContentBuilder();

         var col = new ItemsColumnTagBuilder(spanOf12, null);
         var frmGroup = new FormDisplayTagBuilder();
         var valueParagraph = new ParagraphTagBuilder(name, htmlAttributes);

         valueParagraph.InnerHtml.AppendHtml(helper.DisplayWithTruncateFor(expression, textMaxLength, popOverTitle, seeMoreHtml, showAllContentInPopover, method));

         htmlContentBuilder.AppendHtml(col.RenderStartTag())
             .AppendHtml(frmGroup.RenderStartTag())
             .AppendHtml(helper.LabelFor(expression));

         if (!string.IsNullOrEmpty(hint))
         {
            htmlContentBuilder.AppendHtml(new FormItemHintTagBuilder(hint));
         }

         htmlContentBuilder.AppendHtml(valueParagraph);

         if (postBackValue)
         {
            htmlContentBuilder.AppendHtml(helper.HiddenFor(expression));
         }

         htmlContentBuilder.AppendHtml(frmGroup.RenderEndTag())
             .AppendHtml(col.RenderEndTag());

         return htmlContentBuilder;
      }

      /// <summary>
      /// Returns horizontal display item of a model value with caption truncated by the given length and use bootstrap pop-over to display the complete value
      /// </summary>
      /// <param name="expression">Lambda expression for the property</param>
      /// <param name="spanOf12">Number represents bootstrap column span for the item width</param>
      /// <param name="textMaxLength">Maximum length of the displayed text</param>
      /// <param name="popOverTitle">The pop-over title text</param>
      /// <param name="seeMoreHtml">html text for see more if the text is truncated</param>
      /// <param name="showAllContentInPopover">True if you want to show the complete value in the pop-over. False to display the rest of the value only</param>
      /// <param name="htmlAttributes">custom htmlAttributes applied to the value tag</param>
      /// <param name="postBackValue">If true, value will be posted back in hidden input field</param>
      /// <returns>HtmlString</returns>
      public static IHtmlContent DisplayFormItemWithTruncateFor_H<TModel, TProperty>(
          this IHtmlHelper<TModel> helper,
          Expression<Func<TModel, TProperty>> expression, int spanOf12 = 6,
          int textMaxLength = 50, string popOverTitle = "", string seeMoreHtml = " المزيد >>", bool showAllContentInPopover = false,
          object htmlAttributes = null, bool postBackValue = false, TruncateMethod method = TruncateMethod.Popover)
      {
         var expresionProvider = helper.ViewContext.HttpContext.RequestServices
   .GetService(typeof(ModelExpressionProvider)) as ModelExpressionProvider;
         string name = helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(expresionProvider.GetExpressionText(expression));

         HtmlContentBuilder htmlContentBuilder = new HtmlContentBuilder();

         var col = new ItemsColumnTagBuilder(spanOf12, null);
         var frmGroup = new FormDisplayTagBuilder();
         var valueSpan = new SpanTagBuilder(name, htmlAttributes);

         valueSpan.InnerHtml.AppendHtml(helper.DisplayWithTruncateFor(expression, textMaxLength, popOverTitle, seeMoreHtml, showAllContentInPopover, method));

         htmlContentBuilder.AppendHtml(col.RenderStartTag())
             .AppendHtml(frmGroup.RenderStartTag())
             .AppendHtml(helper.LabelFor(expression));

         htmlContentBuilder.AppendHtml(valueSpan);

         if (postBackValue)
         {
            htmlContentBuilder.AppendHtml(helper.HiddenFor(expression));
         }

         htmlContentBuilder.AppendHtml(frmGroup.RenderEndTag())
             .AppendHtml(col.RenderEndTag());

         return htmlContentBuilder;
      }

      /// <summary>
      /// Returns horizontal display item of a value with caption truncated by the given length and use bootstrap pop-over to display the complete value
      /// </summary>
      /// <param name="name">Unique name for the display tag</param>
      /// <param name="value">Value to display</param>
      /// <param name="labelText">Item caption text</param>
      /// <param name="spanOf12">Number represents bootstrap column span for the item width</param>
      /// <param name="textMaxLength">Maximum length of the displayed text</param>
      /// <param name="popOverTitle">The pop-over title text</param>
      /// <param name="seeMoreHtml">html text for see more if the text is truncated</param>
      /// <param name="showAllContentInPopover">True if you want to show the complete value in the pop-over. False to display the rest of the value only</param>
      /// <param name="htmlAttributes">custom htmlAttributes applied to the value tag</param>
      /// <param name="postBackValue">If true, value will be posted back in hidden input field</param>
      /// <returns>HtmlString</returns>
      public static IHtmlContent DisplayFormItemWithTruncate_H<TModel>(
          this IHtmlHelper<TModel> helper, string value, string labelText = "", int spanOf12 = 6,
          int textMaxLength = 50, string popOverTitle = "", string seeMoreHtml = " المزيد >>", bool showAllContentInPopover = false,
          object htmlAttributes = null, bool postBackValue = false, string name = "postValue", TruncateMethod method = TruncateMethod.Popover)
      {
         HtmlContentBuilder htmlContentBuilder = new HtmlContentBuilder();

         var col = new ItemsColumnTagBuilder(spanOf12, null);
         var frmGroup = new FormDisplayTagBuilder();
         var valueSpan = new SpanTagBuilder(name, value, htmlAttributes);

         valueSpan.InnerHtml.AppendHtml(helper.DisplayWithTruncate(name, value, textMaxLength, popOverTitle, seeMoreHtml, showAllContentInPopover, method));

         htmlContentBuilder.AppendHtml(col.RenderStartTag())
             .AppendHtml(frmGroup.RenderStartTag())
             .AppendHtml(helper.Label(labelText));

         htmlContentBuilder.AppendHtml(valueSpan);

         if (postBackValue)
         {
            htmlContentBuilder.AppendHtml(helper.Hidden(name, value));
         }

         htmlContentBuilder.AppendHtml(frmGroup.RenderEndTag())
             .AppendHtml(col.RenderEndTag());

         return htmlContentBuilder;
      }

      #endregion truncate display




   }

   public enum TruncateMethod
   {
      Popover,
      Modal
   }


   public class ParagraphTagBuilder : TagBuilder
   {
      public ParagraphTagBuilder(string name, object htmlAttributes = null)
          : base("p")
      {
         var customAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
         MergeAttributes(customAttributes);
         MergeAttribute("id", name, false);
      }

      public ParagraphTagBuilder(string name, string value, object htmlAttributes = null)
          : this(name, htmlAttributes)
      {
         this.InnerHtml.SetContent(value);
      }
   }

   public class FormDisplayTagBuilder : TagBuilder
   {
      public FormDisplayTagBuilder(object htmlAttributes = null)
          : base("div")
      {
         var customAttributes = htmlAttributes != null ? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes) : new RouteValueDictionary();
         if (!customAttributes.Keys.Any(k => k == "class"))
            customAttributes.Add("class", "");
         customAttributes["class"] += " form-display";

         this.MergeAttributes(customAttributes);
      }
   }

   public class SpanTagBuilder : TagBuilder
   {
      public SpanTagBuilder(string name, object htmlAttributes = null)
          : base("span")
      {
         var customAttributes = HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
         MergeAttributes(customAttributes);
         MergeAttribute("id", name, false);
      }

      public SpanTagBuilder(string name, string value, object htmlAttributes = null)
          : this(name, htmlAttributes)
      {
         this.InnerHtml.SetContent(value);
      }
   }

   public static class HtmlContentExtensions
   {
      public static string GetString(this IHtmlContent content)
      {
         using (var writer = new System.IO.StringWriter())
         {
            content.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
         }
      }
   }
}
