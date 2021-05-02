using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierSearchCretria : SearchCriteria
    {
        public string AgencyCode { get; set; }

        public string CommericalRegisterNo { get; set; }

        public bool OnlyActive { get; set; }
    }
}
