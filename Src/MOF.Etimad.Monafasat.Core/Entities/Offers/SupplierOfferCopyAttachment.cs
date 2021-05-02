using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public class SupplierOfferCopyAttachment : SupplierAttachment
    {
        [Required]
        public bool IsOfferCopyAttached { get; private set; }
    }
}
