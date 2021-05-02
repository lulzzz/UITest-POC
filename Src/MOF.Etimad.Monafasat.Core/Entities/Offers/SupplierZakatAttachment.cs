using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public class SupplierZakatAttachment : SupplierAttachment
    {
        [Required]
        public bool IsZakatAttached { get; private set; }
        [Required]
        public bool IsZakatValidDate { get; private set; }
    }
}
