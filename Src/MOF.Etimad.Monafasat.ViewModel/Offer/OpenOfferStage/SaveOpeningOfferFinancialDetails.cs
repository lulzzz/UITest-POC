using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SaveOpeningOfferFinancialDetails
    {
        public List<SaveTableModel> tables { get; set; }
        public FinancialOpenOfferDetails model { get; set; }

        public List<QuantityTableForOpeningModel> QuantityTables { get; set; }

    }
}
