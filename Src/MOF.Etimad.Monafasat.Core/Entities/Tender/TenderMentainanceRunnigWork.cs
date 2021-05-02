using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderMentainanceRunnigWork", Schema = "Tender")]
    public class TenderMaintenanceRunnigWork : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int Id { get; private set; }

        public int MaintenanceRunningWorkId { private set; get; }
        [ForeignKey("MaintenanceRunningWorkId")]
        public MaintenanceRunningWork MaintenanceRunningWork { private set; get; }


        public int TenderId { get; private set; }
        [ForeignKey("TenderId")]
        public Tender Tender { private set; get; }

        #endregion

        #region NotMapped


        #endregion

        #region Constructors====================================================

        public TenderMaintenanceRunnigWork()
        {

        }

        public TenderMaintenanceRunnigWork(int maintenanceRunningWorkId)
        {
            MaintenanceRunningWorkId = maintenanceRunningWorkId;
            EntityCreated();
        }
        #endregion

        #region Methods====================================================


        public void Update()
        {

            EntityUpdated();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void SetAddedState()
        {
            Id = 0;
            EntityCreated();
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
        #endregion

    }
}
