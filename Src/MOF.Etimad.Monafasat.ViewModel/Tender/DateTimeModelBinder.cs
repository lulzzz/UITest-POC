using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Globalization;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class DateTimeModelBinder : IModelBinder
    {
        private string[] _customFormat = new string[] { "dd/MM/yyyy", "d/M/yyyy" };

        public static bool IsGreg(string greg)
        {
            string[] allFormats ={"yyyy/MM/dd","yyyy/M/d",
            "dd/MM/yyyy","d/M/yyyy",
            "dd/M/yyyy","d/MM/yyyy","yyyy-MM-dd",
            "yyyy-M-d","dd-MM-yyyy","d-M-yyyy",
            "dd-M-yyyy","d-MM-yyyy","yyyy MM dd",
            "yyyy M d","dd MM yyyy","d M yyyy",
            "dd M yyyy","d MM yyyy"};
            var enCul = new CultureInfo("en-GB");
            try
            {
                if (greg == null || greg.Length <= 0)
                    return false;
                DateTime tempDate = DateTime.ParseExact(greg, allFormats, enCul.DateTimeFormat, DateTimeStyles.AllowWhiteSpaces);
                if (tempDate.Year >= 1900 && tempDate.Year <= 2200)
                    return true;
                else
                    return false;
            }
            catch //(Exception ex)
            {
                return false;
            }
        }

        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            DateTime? result;
            try
            {
                var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
                if (IsGreg(value.FirstValue?.ToString()))
                    result = DateTime.ParseExact(value.FirstValue, _customFormat, new CultureInfo("en-GB"), DateTimeStyles.None);
                else
                {
                    var cul = new CultureInfo("ar-SA");
                    cul.DateTimeFormat.Calendar = new UmAlQuraCalendar();
                    result = DateTime.ParseExact(value.FirstValue, _customFormat, cul, DateTimeStyles.None);
                }
                bindingContext.Result = ModelBindingResult.Success(result);
                return Task.CompletedTask;
            }
            catch //(Exception ex)
            {
                bindingContext.Result = ModelBindingResult.Failed();
                return Task.CompletedTask;
            }
        }
    }
}
