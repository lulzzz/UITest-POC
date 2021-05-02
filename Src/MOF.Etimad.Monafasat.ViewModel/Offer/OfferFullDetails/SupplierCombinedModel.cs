using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierCombinedModel
    {
        public string SupplierName { get; set; }
        public string CommericalName { get; set; }
        public string CommericalRegisterNo { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string SolidarityTypeName { get; set; }
        public CombinedType combinedType { get; set; }

    }



}
