using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.Services.Common.NotificationServices.Models
{
    public class EmailModel
    {
        public List<string> To { get; set; } = new List<string>();
        public string Subject { get; set; }
        public string Body { get; set; }
        public Dictionary<string, string> ListOfEmails { get; set; } = new Dictionary<string, string>();
    }
}
