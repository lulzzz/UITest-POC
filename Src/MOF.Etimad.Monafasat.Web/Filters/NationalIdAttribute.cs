using System;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.Web.Filters
{
   public class NationalIdAttribute : ValidationAttribute//, IClientModelValidator
   {
      public NationalIdAttribute(params string[] propertyNames)
      {
         this.PropertyNames = propertyNames;
      }

      public string[] PropertyNames { get; private set; }

      protected override ValidationResult IsValid(object value, ValidationContext validationContext)
      {
         //string nationalId = value.ToString();
         if (value != null && !IsNationalID(value.ToString()))
            return new ValidationResult(ErrorMessage);

         return ValidationResult.Success;
      }

      public bool IsNationalID(string id)
      {
         try
         {
            if (!string.IsNullOrEmpty(id))
            {
               if (id.Length != 10)
                  return false;
               // is Saudi?
               if (!id.StartsWith("1") && !id.StartsWith("2"))
                  return false;
               // end Saudi condition

               int sum = 0;
               for (int i = 0; i <= 8; i++)
               {
                  int currentDigit = Convert.ToInt32(id.Substring(i, 1));
                  if (i % 2 == 0)
                  {
                     currentDigit *= 2;
                     if (currentDigit > 9)
                        currentDigit = 1 + (currentDigit % 10);
                  }
                  sum += currentDigit;
               }
               int lastDigit = sum % 10;
               if (lastDigit != 0)
                  lastDigit = 10 - lastDigit;
               if (lastDigit != Convert.ToInt32(id.Substring(9, 1)))
                  return false;
            }
            return true;
         }
         catch { return false; }
      }

      /*   public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
         {
            var rule = new ModelClientValidationRule
            {
               ErrorMessage = this.ErrorMessageString,
               ValidationType = "nationalid"
            };

            yield return rule;
         }*/
   }
}
