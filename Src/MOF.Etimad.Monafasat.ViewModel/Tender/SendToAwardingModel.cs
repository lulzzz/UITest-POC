using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SendToAwardingModel
    {

        public List<string> CRs { get; set; }
        public int TenderId { get; set; }
        public string EncryptedTenderId { get; set; }
        public string AgencyCode { get; set; }

    }

}
