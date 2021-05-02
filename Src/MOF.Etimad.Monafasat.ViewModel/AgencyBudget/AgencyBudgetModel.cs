namespace MOF.Etimad.Monafasat.ViewModel.AgencyBudget
{
    public class AgencyBudgetModel
    {
        public string ProjectName { get; set; }
        public decimal? Cash { get; set; }
        public decimal? Cost { get; set; }
        public string StatusCode { get; set; }
        public string ErrorCode { get; set; }
    }

    public class AgencyBudgetNumberModel
    {
        public int AgencyBudgetNumberId { get; set; }
        public int TenderId { get; set; }
        public string ProjectNumber { get; set; }
        public string ProjectName { get; set; }
        public decimal Cache { get; set; }
        public decimal Cost { get; set; }
        public bool? IsProject { get; set; }
        public bool IsGSF { get; set; }

    }
}
