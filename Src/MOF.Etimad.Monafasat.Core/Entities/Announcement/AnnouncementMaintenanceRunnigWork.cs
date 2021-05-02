using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AnnouncementMaintenanceRunnigWork", Schema = "Announcement")]
    public class AnnouncementMaintenanceRunnigWork : AuditableEntity
    {
        [Key]
        public int Id { get; private set; }
        [ForeignKey("MaintenanceRunningWork")]
        public int MaintenanceRunningWorkId { private set; get; }
        public MaintenanceRunningWork MaintenanceRunningWork { private set; get; }
        [ForeignKey("Announcement")]
        public int AnnouncementId { get; private set; }
        public Announcement Announcement { private set; get; }

        public AnnouncementMaintenanceRunnigWork()
        {

        }
        public AnnouncementMaintenanceRunnigWork(int maintenanceRunningWorkId)
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