using System;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderNewsModel
    {
        public DateTime TenderCreationDate { get; set; }
        public DateTime OfferOpeningDate { get; set; }
        public TenderRevisionDateModel TenderRevisionDate { get; set; }
        public string OfferAwardingDate { get; set; }
    }
}
