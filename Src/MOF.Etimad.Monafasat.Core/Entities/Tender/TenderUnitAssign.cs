using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderUnitAssign", Schema = "Tender")]
    public class TenderUnitAssign : AuditableEntity
    {
        #region Fields====================================================

        [Key]
        public int TenderUnitAssignId { get; private set; }
        public int UserProfileId { get; private set; }
        [ForeignKey(nameof(UserProfileId))]
        public UserProfile UserProfile { get; private set; }

        public int TenderId { get; private set; }
        [ForeignKey(nameof(TenderId))]
        public Tender Tender { get; private set; }

        public bool IsCurrentlyAssigned { get; private set; }
        public int UnitSpecialistLevel { get; private set; }

        #endregion

        #region Constructors====================================================

        public TenderUnitAssign()
        {

        }

        public TenderUnitAssign(int userProfileId, int tenderId, bool isCurrentlyAssigned, int unitSpecialistLevel)
        {
            UserProfileId = userProfileId;
            TenderId = tenderId;
            IsCurrentlyAssigned = isCurrentlyAssigned;
            UnitSpecialistLevel = unitSpecialistLevel;
            EntityCreated();
        }

        #endregion

        #region Methods====================================================
        internal void DeactiveAssignments()
        {
            IsCurrentlyAssigned = false;
        }

        #endregion
    }
}