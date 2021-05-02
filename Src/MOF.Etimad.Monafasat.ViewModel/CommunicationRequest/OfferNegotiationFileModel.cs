using MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class OfferNegotiationFileModel
    {
        public int tenderTypeId { get; set; }
        public string offerIdString { get; set; }
        public List<SupplierCombinedModel> CombinedSuppliers { get; set; } = new List<SupplierCombinedModel>();
        public List<OfferAttachmentModel> offerAttachments { get; set; } = new List<OfferAttachmentModel>();
    }
}
