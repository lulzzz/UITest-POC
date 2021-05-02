using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage
{
    public class OpenOfferModel
    {
        #region New Props
        public string OfferIdString { get; set; }
        public string TenderIdString { get; set; }
        public string CombinedIdString { get; set; }
        public string OfferstatusName { get; set; }
        public bool IsSolidriaty { get; set; }
        public Enums.OfferStatus OfferStatus { get; set; }
        public QuantityTableforInsertModel QuantityTableforInsertModel { get; set; }
        public TenderDataTabModel TenderDataTabModel { get; set; }
        public List<OfferAttachmentModel> OfferAttachmentModels { get; set; }
        public List<QuantityTableModel> QuantityTableModels { get; set; }
        public List<CombinedSupplierModel> CombinedSupplierModel { get; set; }
        #endregion
    }
}
