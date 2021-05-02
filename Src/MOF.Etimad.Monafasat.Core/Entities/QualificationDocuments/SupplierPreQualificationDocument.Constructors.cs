namespace MOF.Etimad.Monafasat.Core.Entities
{
    public partial class SupplierPreQualificationDocument
    {

        public SupplierPreQualificationDocument()
        {

        }
        public SupplierPreQualificationDocument(string SupplierId, int? statusId, int PreQualificationId, int PreQualificationResult, bool? isActive = true)
        {
            this.SupplierId = SupplierId;
            this.StatusId = statusId;
            this.PreQualificationId = PreQualificationId;
            this.PreQualificationResult = PreQualificationResult;
            EntityCreated();
        }
    }
}
