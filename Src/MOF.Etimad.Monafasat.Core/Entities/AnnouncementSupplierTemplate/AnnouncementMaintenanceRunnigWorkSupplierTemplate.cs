using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AnnouncementMaintenanceRunnigWorkSupplierTemplate", Schema = "AnnouncementTemplate")]
    public class AnnouncementMaintenanceRunnigWorkSupplierTemplate : AuditableEntity
    {
        [Key]
        public int Id { get; private set; }
        [ForeignKey("MaintenanceRunningWork")]
        public int MaintenanceRunningWorkId { private set; get; }
        public MaintenanceRunningWork MaintenanceRunningWork { private set; get; }
        [ForeignKey("AnnouncementSupplierTemplate")]
        public int AnnouncementId { get; private set; }
        public AnnouncementSupplierTemplate AnnouncementSupplierTemplate { private set; get; }

        public AnnouncementMaintenanceRunnigWorkSupplierTemplate()
        {

        }
        public AnnouncementMaintenanceRunnigWorkSupplierTemplate(int maintenanceRunningWorkId)
        {
            MaintenanceRunningWorkId = maintenanceRunningWorkId;
            EntityCreated();
        }

        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }
        internal void Delete()
        {
            EntityDeleted();
        }

        public void Test_Delete()
        {
            this.Delete();
        }
    }
}
