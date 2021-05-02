using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class CombinedSupplierModel : SearchCriteria
    {
        #region New Props
        public string OfferIdString { get; set; }
        public int OfferId { get; set; }
        public string TenderIdString { get; set; }
        public string CombinedIdString { get; set; }
        public int CombinedId { get; set; }
        public string SupplierName { get; set; }
        public string SupplierCr { get; set; }
        public bool IsOwner { get; set; }
        public string roleName { get; set; }
        public int OfferPresentationWayId { get; set; }
        public int ComitteTypeId { get; set; }
        #endregion
    }
}
