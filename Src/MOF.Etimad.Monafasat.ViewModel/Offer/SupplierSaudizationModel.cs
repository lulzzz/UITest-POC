using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.ViewModel 
{
    public class SupplierSaudizationModel:SupplierAttachmentModel
    {
        public bool IsSaudizationAttached { get; private set; }
        public bool IsSaudizationValidDate { get; private set; }
    }
}
