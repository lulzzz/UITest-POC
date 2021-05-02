using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierZakatModel:SupplierAttachmentModel
    {
        public bool IsZakatAttached { get;  set; }
      
        public bool IsZakatValidDate { get;  set; }
    }
}
