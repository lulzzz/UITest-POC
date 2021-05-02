using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core
{
    [Table("PlaintRequestNotification", Schema = "CommunicationRequest")]
    public class PlaintRequestNotification : AuditableEntity
    {
        #region Constructors
        public PlaintRequestNotification()
        {

        }
        public PlaintRequestNotification(int PplaintRequestNotificationId, bool isSent)
        {
            IsSent = isSent;
            if (PplaintRequestNotificationId != 0)
            {
                EntityUpdated();
            }
            else
            {
                EntityCreated();
            }
        }
        #endregion
        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlaintRequestNotificationId { get; private set; }
        public bool IsSent { get; private set; }
        public int CommunicationRequestId { get; private set; }
        [ForeignKey("CommunicationRequestId")]
        public AgencyCommunicationRequest CommunicationRequest { get; private set; }
        public DateTime? ApprovalDate { private set; get; }
        #endregion
        #region Methods
        public void UpdateApprovalDate()
        {
            ApprovalDate = DateTime.Now;
            EntityUpdated();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }

        public PlaintRequestNotification Update(bool isSent)
        {
            IsSent = isSent;
            EntityUpdated();
            return this;
        }
        #endregion
    }
}