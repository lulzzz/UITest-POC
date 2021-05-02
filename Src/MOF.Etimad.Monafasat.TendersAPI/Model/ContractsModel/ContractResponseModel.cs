using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.TendersAPI.Model
{
    public class ContractResponseModel
    {
        public string StatusCode { get; set; }
        public string StatusDesc { get; set; }
        public bool canRegisterContract { get; set; }
        public string Message { get; set; }
        public string MessageCode { get; set; }
        public string TenderReferenceNumber { get; set; }
        public string TenderName { get; set; }
        public string AgencyCode { get; set; }
    }
}
