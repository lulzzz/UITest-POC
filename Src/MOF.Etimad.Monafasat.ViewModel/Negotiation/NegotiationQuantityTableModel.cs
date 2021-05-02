using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{

    public class NegotiationQuantityTableModel
    {
        public string Id { get; set; }
        public string SupplierQTId { get; set; }
        public string TenderQTId { get; set; }

        public string Name { get; set; }
        public string status { get; set; }
        public DateTime intializeDate { get; set; }
        public string intializeDateString { get; set; }
        public decimal FinalAmount { get; set; }
        public bool isOpenneingTable { get; set; }


        #region [Navigation]
        public List<NegotiationQuantityTableItemModel> NegotiationQuantityTableItems { get; set; }
        #endregion


    }

}
