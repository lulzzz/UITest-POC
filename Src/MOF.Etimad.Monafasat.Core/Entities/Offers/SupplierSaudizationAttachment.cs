using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public class SupplierSaudizationAttachment : SupplierAttachment
    {
        [Required]
        public bool IsSaudizationAttached { get; private set; }
        [Required]
        public bool IsSaudizationValidDate { get; private set; }
    }
}
