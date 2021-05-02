using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public class SupplierVisitationAttachment : SupplierAttachment
    {
        [Required]
        public bool IsVisitationAttached { get; private set; }
    }
}
