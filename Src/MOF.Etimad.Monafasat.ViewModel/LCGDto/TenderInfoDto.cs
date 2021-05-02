
using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel.LCGDto
{
    public class TenderInfo
    {
        public int ResponseStatusCode { get; set; }
        public string TenderReferenceId { get; set; }
        public string GovernmentEntityId { get; set; }
        public string TenderName { get; set; }
        public string TenderPurpose { get; set; }
        public decimal ExpectedValue { get; set; }
        public decimal? MinThreshold { get; set; }
        public int TenderStatusId { get; set; }
        public DateTime? OpenningBidsDate { get; set; }
        public List<int> TenderSectorId { get; set; }
    }
}
