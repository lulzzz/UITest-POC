using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public class SupplierCommercialRegisterAttachment : SupplierAttachment
    {
        [Required]
        public bool IsCommercialRegisterAttached { get; private set; }
        [Required]
        public bool IsCommercialRegisterValid { get; private set; }
    }
}
