using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public partial class SupplierPreQualificationAttachment
    {
        public SupplierPreQualificationAttachment()
        {

        }
        public SupplierPreQualificationAttachment(string FileName, string FileNetReferenceId, string SupPreQAttachmentId, bool? isActive = true)
        {
            FileName = FileName;
            FileNetReferenceId = FileNetReferenceId;
            SupPreQAttachmentId = SupPreQAttachmentId;

            EntityCreated();
        }

    }
}
