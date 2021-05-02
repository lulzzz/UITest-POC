using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierOfferLetterModel:SupplierAttachmentModel
    {
        public bool IsOfferLetterAttached { get; set; }
        public string OfferLetterNumber { get; set; }
        public DateTime? OfferLetterDate { get; set; }
    }
}
