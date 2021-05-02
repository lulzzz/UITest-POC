using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public class SupplierSpecificationAttachment : SupplierAttachment
    {
        [Required]
        public bool? IsForApplier { get; private set; }
        public int? Degree { get; private set; }
        public int? ConstructionWorkId { get; private set; }
        [ForeignKey("ConstructionWorkId")]
        public virtual ConstructionWork ConstructionWork { private set; get; }
        public int? MaintenanceRunningWorkId { get; private set; }
        [ForeignKey("MaintenanceRunningWorkId")]
        public virtual MaintenanceRunningWork MaintenanceRunningWork { private set; get; }
        public SupplierSpecificationAttachment() { }
        public SupplierSpecificationAttachment(int attachmentId, int offerId, bool? isForApplier, int? degree, int? constructionWorkId, int? maintenanceRunningWorkId)
        {
            IsForApplier = isForApplier;
            Degree = degree;
            ConstructionWorkId = constructionWorkId;
            MaintenanceRunningWorkId = maintenanceRunningWorkId;

            if (attachmentId == 0)
            {
                EntityCreated();
            }
            else
            {
                EntityUpdated();
            }
        }
    }
}
