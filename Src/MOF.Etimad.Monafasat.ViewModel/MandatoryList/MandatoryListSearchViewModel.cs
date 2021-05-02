using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class MandatoryListSearchViewModel : SearchCriteria
    {
        public MandatoryListSearchViewModel()
        {
            SortDirection = SortDirection = SharedKernal.SortDirection.Descending;
            Sort = "CreatedAt";
            PageSize = 10;
        }

        public string DivisionName { get; set; }

        public string DivisionCode { get; set; }

        public string CSICode { get; set; }

        public double? PriceCelling { get; set; }

        public int? StatusId { get; set; }

        public string ProductNameAr { get; set; }
    }
}
