using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AssignUserCommittee", Schema = "Committee")]
    public class AssignUserCommittee : AuditableEntity
    {
        #region Constructors
        public AssignUserCommittee() { }
        public AssignUserCommittee(int assignUserCommitteeId, string committeeName, string address, int phone, int fax, string email, string postalCode, string zipCode, int organizationBranchId, bool? isActive = true)
        {
            //AssignUserCommitteeId = assignUserCommitteeId;
            IsActive = isActive;
        }

        #endregion

        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int AssignUserCommitteeId { get; private set; }
        [ForeignKey("UserId")]
        public int UserId { get; private set; }
        //public User User { get; private set; }


        public int CommitteeId { get; private set; }
        [ForeignKey("CommitteeId")]
        public Committee Committee { get; private set; }

        #endregion

        #region Methods
            
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

        #endregion


    }
}
