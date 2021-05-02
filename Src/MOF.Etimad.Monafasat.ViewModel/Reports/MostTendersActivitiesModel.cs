namespace MOF.Etimad.Monafasat.ViewModel
{
    public class MostTendersActivitiesModel
    {
        public string Name { get; set; }
        public int NumberOfTenders { get; set; }
        public double NumberOfTendersPercentage { get; set; }
        public decimal? PublicTendersValue { get; set; }
        public decimal? DirectPurchaseValue { get; set; }
        //public List<Tender> tenders{ get; set; }

    }
}
