namespace MOF.Etimad.Monafasat.ViewModel
{
    public class PrintTenderReceiptReportModel
    {
        public int OfferId { get; set; }
        public int TenderId { get; set; }
        public string TenderName { get; set; }
        public string PurshesingDate { get; set; }
        public decimal BookletPrice { get; set; }

        public string SupplierName { get; set; }
        public string CommercialNumber { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
