using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public class SupplierSocialInsuranceAttachment : SupplierAttachment
    {
        [Required]
        public bool IsSocialInsuranceAttached { get; private set; }
        [Required]
        public bool IsSocialInsuranceValidDate { get; private set; }
    }

}
