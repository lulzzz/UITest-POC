using MOF.Etimad.Monafasat.Core.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.TestsBuilders
{
    public class SupplierCombinedDetailDefaults
    {
        public SupplierCombinedDetail BuildSupplierCombinedDetail()
        {
            SupplierCombinedDetail supplierCombinedDetail = new SupplierCombinedDetail();
            supplierCombinedDetail.UpdateAttachmentDetails(1, 1, true, true, true, true, true, true, true, true, true, true, true, true);
            return supplierCombinedDetail;
        }
    }
}
