using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core
{
    [Table("TenderRevisionCancel", Schema = "Tender")]
    //Removed
    public class TenderRevisionCancel_ : AuditableEntity
    {
        #region Constructors
        public TenderRevisionCancel_()
        {
        }
        public TenderRevisionCancel_(Tender tender, String roleName)
        {
            StatusId = (int)Enums.TenderStatus.Established;
            CreatedByRoleName = roleName;
            EntityCreated();
        }

        #endregion

        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RevisionCancelId { get; private set; }
        [StringLength(256)]
        public string CreatedByRoleName { get; set; }
        [ForeignKey("Status")]
        public int StatusId { get; private set; }
        public TenderStatus Status { get; private set; }
        public string RejectionReason { get; private set; }
        public TenderChangeRequest ChangeRequest { get; private set; }
        [ForeignKey("ChangeRequest")]
        public int TenderChangeRequestId { get; private set; }
        #endregion

    }
}