using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("UnRegisteredSuppliersInvitation", Schema = "Invitation")]
    public class UnRegisteredSuppliersInvitation : AuditableEntity
    {
        #region Fields
        [Key]
        public int Id { get; private set; }
        [ForeignKey("Tender")]
        public int TenderId { get; private set; }
        [StringLength(1024)]
        public string Email { get; private set; }
        public string MobileNo { get; private set; }
        public Tender Tender { get; private set; }
        [ForeignKey("InvitationStatusId")]
        public InvitationStatus InvitationStatus { get; private set; }
        public int? InvitationStatusId { get; private set; }
        [StringLength(50)]
        public string CrNumber { get; private set; }
        public int InvitationTypeId { get; private set; }
        [StringLength(2056)]
        public string Description { get; private set; }
        #endregion
        #region Constractors
        public UnRegisteredSuppliersInvitation()
        {

        }
        public UnRegisteredSuppliersInvitation(string emailOrMobileNo, int sentTypeId)
        {
            Email = emailOrMobileNo;
            MobileNo = sentTypeId.ToString();
            EntityCreated();
        }
        public UnRegisteredSuppliersInvitation(string crNumber, int invitationTypeId, string email, string mobileNo, Enums.InvitationStatus statusId, string description)
        {
            CrNumber = crNumber;
            InvitationTypeId = invitationTypeId;
            Email = email;
            MobileNo = mobileNo;
            InvitationStatusId = (int)statusId;
            Description = description;
            EntityCreated();
        }
        #endregion
        #region Methods

        public void Delete()
        {
            EntityDeleted();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void UpdateStatus(int invitionStatusId)
        {
            InvitationStatusId = invitionStatusId;
            EntityUpdated();
        }
        #endregion
    }
}
