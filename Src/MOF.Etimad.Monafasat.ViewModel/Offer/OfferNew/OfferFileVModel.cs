using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel.Offer
{


    public class OfferFileVModel
    {
        [Required]
        public bool isSolidarity { get; set; }
        public Enums.TenderType tenderType { get; set; }

        public string offerIdString { get; set; }
        public string tenderIdString { get; set; }
        public OfferStatusModel offerStatusModel { get; set; } = new OfferStatusModel();

        public Enums.OfferPresentationWayId OfferPresentationWay { get; set; }
        public string TechnicalandFinancialFilesReferenceIds { get; set; }
        public List<SupplierAttachmentModel> TechnicalandFinancialFiles { get; set; }

        public string SolidarityrFilesReferenceIds { get; set; }
        public List<SupplierAttachmentModel> SolidarityFiles { get; set; }
 
        public string TechnicalFilesReferenceIds { get; set; }
        public List<SupplierAttachmentModel> TechnicalFiles { get; set; }


        public string FinancialFilesReferenceId { get; set; }
        public List<SupplierAttachmentModel> FinancialFiles { get; set; }
        public bool IsValidToApplyOfferLocalContentChanges { get; set; }
  
        public bool IsOldTender
        {
            get => tenderType == Enums.TenderType.CurrentTender || tenderType == Enums.TenderType.CurrentDirectPurchase || tenderType == Enums.TenderType.NationalTransformationProjects;
        }

    }
}
