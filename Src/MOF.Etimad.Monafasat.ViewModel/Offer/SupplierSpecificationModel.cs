namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SupplierSpecificationModel : SupplierAttachmentModel
    {
        public int SpecificationId { get; set; }
        public bool? IsForApplier { get; set; }
        public string IsForApplierString { get; set; }
        public int? Degree { get; set; }
        public int? ConstructionWorkId { get; set; }
        public int? MaintenanceRunningWorkId { get; set; }
        public string ConstructionWorkString { get; set; }
        public string MaintenanceRunningWorkString { get; set; }
        public object MaintenanceRunningWork { get; set; }
        public object ConstructionWork { get; set; }
    }
}
