using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public class SupplierOfferLetterAttachment : SupplierAttachment
    {
        [Required]
        public bool IsOfferLetterAttached { get; private set; }
        [StringLength(500)]
        public string OfferLetterNumber { get; private set; }
        public DateTime? OfferLetterDate { get; private set; }

    }
}
