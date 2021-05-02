using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel.Offer
{
    public class OfferFullDetailsModel
    {

        public bool isSolidarity { get; set; }
        public bool isAltenative { get; set; }
        public bool isOwner { get; set; }
        public int tenderTypeId { get; set; }
        public string OfferIdString { get; set; }
        public string TenderIdString { get; set; }
        public int yearsCount { get; set; }
        public decimal FinalAlternativePrice { get; set; }
        public string FinalAlternativePriceText { get; set; }
        public decimal FinalPrice { get; set; }
        public string FinalPriceText { get; set; }
        public string offerStatus { get; set; }
        public string SupplierName { get; set; }
        public List<AttachmentModel> attachments { get; set; }
        public int FilesCount { get; set; }
        public List<QuantityTableDTO> QuantitiesTables { get; set; }
        public List<SupplierCombinedModel> CombinedSuppliers { get; set; }
        public int statusId { get; set; }
        public QuantitiesTemplateModel QuantitiesTemplateModel { get; set; }
    }
}
