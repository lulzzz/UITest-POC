using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public partial class SupplierPreQualificationAttachment
    {
        public void Update(string FileName, string FileNetReferenceId, string SupPreQAttachmentId, bool? isActive = true)
        {
            FileName = FileName;
            FileNetReferenceId = FileNetReferenceId;
            SupPreQAttachmentId = SupPreQAttachmentId;

            EntityUpdated();
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

    }
}
