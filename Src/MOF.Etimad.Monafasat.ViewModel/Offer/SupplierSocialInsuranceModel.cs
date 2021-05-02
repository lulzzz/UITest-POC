using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierSocialInsuranceModel:SupplierAttachmentModel
    {
        public bool IsSocialInsuranceAttached { get; set; }
        public bool IsSocialInsuranceValidDate { get; set; }
    }
}
