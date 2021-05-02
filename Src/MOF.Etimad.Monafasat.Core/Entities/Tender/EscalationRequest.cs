using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core
{
    [Table("EscalationRequest", Schema = "CommunicationRequest")]
    public class EscalationRequest : AuditableEntity
    {
        #region Constructors
        public EscalationRequest()
        {

        }
        public EscalationRequest(int escalationRequestId, int plaintRequestId, string attachmentId, string attachmentName)
        {
            PlaintRequestId = plaintRequestId;
            if (escalationRequestId != 0)
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
        public int EscalationRequestId { get; private set; }
        public int PlaintRequestId { get; private set; }
        [ForeignKey("PlaintRequestId")]
        public PlaintRequest PlaintRequest { get; private set; }
        [StringLength(1000)]
        public String EscalationNotes { get; private set; }
        #endregion
        #region Methods
        public void AcceptRevision()
        {
            EntityUpdated();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }

        public void UpdateNotes(string escalationNotes)
        {
            EscalationNotes = escalationNotes;
            EntityUpdated();
        }
        #endregion
    }
}