namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PerformanceReportModel
    {
        public string AgencyName { get; set; }
        public int TotalCountTheTendersReviewed { get; set; }
        public int TotalCountTheTendersApproved { get; set; }
        public int TotalCountTheTendersRejected { get; set; }
        public int TotalCountTheTendersReturned { get; set; }
        public int TotalEstimatedValue { get; set; }
        public int AverageDurationReviewOfoneTender { get; set; }
        public int TotalCountTheTendersTransferredToUnitSpecialistLevel2 { get; set; }

    }
}
