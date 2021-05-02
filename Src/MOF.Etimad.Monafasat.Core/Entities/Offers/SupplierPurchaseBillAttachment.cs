using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public class SupplierPurchaseBillAttachment : SupplierAttachment
    {
        [Required]
        public bool IsPurchaseBillAttached { get; private set; }
    }

}
