
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CheckOfferModel
    {
        public CheckOfferModel()
        {
            this.ShowCombinders = true;
            this.ShowQuantitiesTables = true;
        }

        public string OfferIdString { get; set; }
        public string CombinedIdString { get; set; }
        public string OfferstatusName { get; set; }
        public Enums.OfferStatus OfferStatus { get; set; }
        public CheckOfferTenderBasicInfo TenderBasicInfo { get; set; }
        public CheckOfferQuantityTableforInsertModel QuantityTableforInsertModel { get; set; }
        public CheckOfferTenderDataTabModel TenderDataTabModel { get; set; }
        public List<CheckOfferAttachmentModel> OfferAttachmentModels { get; set; }
        public List<ViewModel.Offer.OpenOfferStage.QuantityTableModel> QuantityTableModels { get; set; }
        public List<CheckOfferCombinedSupplierModel> CombinedSupplierModels { get; set; }
        public List<string> TenderAreaNameList { get; set; }
        public bool ShowQuantitiesTables { get; set; }
        public bool ShowCombinders { get; set; }
        public bool IsReadOnly { get; set; }
        public bool IsSolidarity { get; set; }
    }
}
