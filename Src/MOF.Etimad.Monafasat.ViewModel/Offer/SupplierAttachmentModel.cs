using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel.Offer;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierAttachmentModel
    {
        public int AttachmentId { get; set; }
        public string FileName { get; set; }
        public int OfferId { get; set; }
        public string FileNetReferenceId { get; set; }
        public Enums.AttachmentType type { get; set; }
        public string AttachmentTypeName { get; set; }
    }

    public class SupplierAttachmentPartialVModel
    {
        public List<SupplierAttachmentModel> supplierAttachmentModels { get; set; } = new List<SupplierAttachmentModel>();
        public OfferStatusModel statusModel { get; set; } = new OfferStatusModel();
    }
}