using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class FavouriteSupplierListModel : SearchCriteria
    {
        public int FavouriteSupplierListId { get; set; }
        public string Name { get; set; }
        public string AgencyCode { get; set; }
        public int BranchId { get; set; }
    }
}
