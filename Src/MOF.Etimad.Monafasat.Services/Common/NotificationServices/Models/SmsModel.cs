using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.Services.Common.NotificationServices.Models
{
    public class SmsModel
    {
        public List<string> To { get; set; } = new List<string>(); public string Body { get; set; }
        public IDictionary<string, string> ListOfNumbers { get; set; } = new Dictionary<string, string>();
    }
}
