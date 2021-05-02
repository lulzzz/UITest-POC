using MOF.Etimad.Monafasat.SharedKernal;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage
{
    public class CombinedSupplierDetails
    {
        public int OfferId { get; set; }
        public string OfferIdString
        {
            get { if (OfferId == 0) { return ""; } else { return Util.Encrypt(OfferId); } }
        }
        public List<CombinedSupplierModel> CombinedSupplierModel { get; set; }
    }
}
