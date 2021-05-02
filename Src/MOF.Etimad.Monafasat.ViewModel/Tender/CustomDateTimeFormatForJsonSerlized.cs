using Newtonsoft.Json.Converters;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CustomDateTimeFormatForJsonSerlized : IsoDateTimeConverter
    {
        public CustomDateTimeFormatForJsonSerlized()
        {
            base.DateTimeFormat = "dd/MM/yyyy";
        }
    }
}
