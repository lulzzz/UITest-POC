
using System;

namespace MOF.Etimad.Monafasat.ViewModel.LCGDto
{
    public class TenderList
    {
        public int ResponseStatusCode { get; set; }
        public int TenderReferenceId { get; set; }
        public int GovernmentEntityId { get; set; }
        public string TenderName { get; set; }
        public string TenderPurpose { get; set; }
        public decimal ExpectedValue { get; set; }
        public decimal? MinThreshold { get; set; }
        public int TenderStatusId { get; set; }
        public DateTime OpenningBidsDate { get; set; }
        public int TenderSectorId { get; set; }





    }
}
