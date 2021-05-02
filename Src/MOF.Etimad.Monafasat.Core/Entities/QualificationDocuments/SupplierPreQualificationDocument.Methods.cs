using System.Collections.Generic;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public partial class SupplierPreQualificationDocument
    {
        public void Update(string SupplierId, int PreQualificationId, PreQSupplierDocumentStatus StatusId)
        {
            this.SupplierId = SupplierId;
            this.PreQualificationId = PreQualificationId;
            this.StatusId = (int)StatusId;
            EntityCreated();
        }

        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }

        public void UpdatePreQualificationDocumentStatus(int? preQualificationResult, string rejectionReason)
        {
            PreQualificationResult = preQualificationResult;
            RejectionReason = rejectionReason;
            EntityUpdated();

        }

        public SupplierPreQualificationDocument UpdateAttachments(List<SupplierPreQualificationAttachment> attachments)
        {
            foreach (var item in supplierPreQualificationAttachments)
            {
                item.Delete();
            }

            foreach (var attachment in attachments)
            {
                supplierPreQualificationAttachments.Add(attachment);
            }

            EntityUpdated();
            return this;
        }

    }
}
