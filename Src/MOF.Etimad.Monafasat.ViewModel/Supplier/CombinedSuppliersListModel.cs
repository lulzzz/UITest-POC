namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CombinedSuppliersListModel
    {
        public string CommericalRegisterNo { get; set; }
        public string CommericalName { get; set; }
        public string CombinedType { get; set; }
        public CombinedSupplierLeader combinedLeader = new CombinedSupplierLeader();

    }
    public class CombinedSupplierLeader
    {

        public string CommericalRegisterNo { get; set; }
        public string CommericalName { get; set; }
        public string CombinedType { get; set; }
    }
}
