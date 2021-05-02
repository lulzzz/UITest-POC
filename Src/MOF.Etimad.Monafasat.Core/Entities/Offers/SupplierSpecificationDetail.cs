using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("SupplierSpecificationDetail", Schema = "Offer")]
    public class SupplierSpecificationDetail : AuditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SpecificationId { get; private set; }
        [Required]
        public bool? IsForApplier { get; private set; }
        public int? Degree { get; private set; }
        public int? ConstructionWorkId { get; private set; }
        [ForeignKey("ConstructionWorkId")]
        public virtual ConstructionWork ConstructionWork { private set; get; }
        public int? MaintenanceRunningWorkId { get; private set; }
        [ForeignKey("MaintenanceRunningWorkId")]
        public virtual MaintenanceRunningWork MaintenanceRunningWork { private set; get; }
        public int CompinedDetailId { get; set; }

        [ForeignKey("CompinedDetailId")]
        public SupplierCombinedDetail CompinedDetail { get; set; }
        public SupplierSpecificationDetail() { }
        public SupplierSpecificationDetail(int id, int compinedDetailId, bool? isForApplier, int? degree, int? constructionWorkId, int? maintenanceRunningWorkId)
        {
            IsForApplier = isForApplier;
            Degree = degree;
            ConstructionWorkId = constructionWorkId;
            MaintenanceRunningWorkId = maintenanceRunningWorkId;
            CompinedDetailId = compinedDetailId;
            if (id == 0)
            {
                EntityCreated();
            }
            else
            {
                EntityUpdated();
            }
        }

        public void Delete()
        {
            EntityDeleted();
        }
    }
}
