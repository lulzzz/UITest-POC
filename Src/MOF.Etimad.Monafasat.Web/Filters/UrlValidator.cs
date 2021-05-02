using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace MOF.Etimad.Monafasat.Web.Filters
{

   [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
   public class UrlValidator : ValidationAttribute, IClientModelValidator
   {
      public void AddValidation(ClientModelValidationContext context)
      {
         MergeAttribute(context.Attributes, "data-val", "true");
         var errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
         MergeAttribute(context.Attributes, "data-val-urlvalidator", errorMessage);
      }
      private bool MergeAttribute(
        IDictionary<string, string> attributes,
        string key,
        string value)
      {
         if (attributes.ContainsKey(key))
         {
            return false;
         }
         attributes.Add(key, value);
         return true;
      }
      protected override ValidationResult IsValid(object value, ValidationContext validationContext)
      {
         if (value != null)
         {
            var x = value.ToString();
            if (Regex.IsMatch(x, @"^(https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9][a-zA-Z0-9-]+[a-zA-Z0-9]\.[^\s]{2,}|https?:\/\/(?:www\.|(?!www))[a-zA-Z0-9]\.[^\s]{2,}|www\.[a-zA-Z0-9]\.[^\s]{2,})$", RegexOptions.IgnoreCase))
            {
               return ValidationResult.Success;
            }
            else
            {
               return new ValidationResult(ErrorMessage);
            }
         }
         else
         {
            return new ValidationResult("Please enter some value");
         }
      }
   }
}
