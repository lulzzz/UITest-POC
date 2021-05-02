using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("PostQualificationSuppliersInvitations", Schema = "Tender")]
    public class PostQualificationSuppliersInvitations : AuditableEntity
    {
        #region Fielsa
        [Key]
        public int PostQualificationSupplierId { get; private set; }
        public int PostQualificationId { get; private set; }
        [ForeignKey(nameof(PostQualificationId))]
        public Tender PostQualification { get; private set; }
        public string CommercialNumber { get; private set; }
        [ForeignKey(nameof(CommercialNumber))]
        public Supplier Supplier { get; private set; }
        public int StatusId { get; private set; }
        [ForeignKey(nameof(StatusId))]
        public InvitationStatus Status { get; set; }
        #endregion
        #region Constractors
        public PostQualificationSuppliersInvitations()
        {

        }
        public PostQualificationSuppliersInvitations(string commercialNumber)
        {
            CommercialNumber = commercialNumber;
            StatusId = (int)Enums.InvitationStatus.New;
            EntityCreated();
        }
        #endregion
        #region Methods
        public void Delete()
        {
            IsActive = false;
            EntityUpdated();
        }
        #endregion
    }
}
