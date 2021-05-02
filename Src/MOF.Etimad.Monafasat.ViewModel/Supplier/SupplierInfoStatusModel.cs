using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierInfoStatusModel
    {
        public SupplierInfoType enSupplierInfoType { get; set; }
        public string StatusName { get; set; }
        public string CompanyName { get; set; }
        public string Notes { get; set; }
        public string Date { get; set; }
        public string SaudiPercentage { get; set; }
    }
}
