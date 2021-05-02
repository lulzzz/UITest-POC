using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierCommercialRegisterModel:SupplierAttachmentModel
    {
        public bool IsCommercialRegisterAttached { get;  set; }
        
        public bool IsCommercialRegisterValid { get;  set; }
    }
}
