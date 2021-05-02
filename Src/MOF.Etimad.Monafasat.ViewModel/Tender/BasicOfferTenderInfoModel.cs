using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class BasicOfferTenderInfoModel
    {

        public int TenderId { get; set; }
        public List<string> TenderAreaNameList { get; set; }
        public string TenderName { get; set; }
        public string TenderReferenceNumber { get; set; }
        public string ExcuationLocation { get; set; }
        public string InitialGuaranteeAddress { get; set; }
        public bool? InsideKSA { get; set; }



    }

}
