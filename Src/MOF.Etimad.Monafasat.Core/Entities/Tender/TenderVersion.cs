using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderVersion", Schema = "Tender")]
    public class TenderVersion : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int Id { get; private set; }

        [ForeignKey(nameof(Tender))]
        public int TenderId { get; private set; }
        public Tender Tender { private set; get; }

        [ForeignKey(nameof(Version))]
        public int VersionId { get; private set; }
        public VersionHistory Version { private set; get; }

        #endregion

        #region Constructors====================================================

        public TenderVersion()
        {

        }
        
        public TenderVersion(int versionId)
        {
            VersionId = versionId;
            EntityCreated();
        }
        #endregion

        #region Methods====================================================
        
        public void Delete()
        {
            EntityDeleted();
        }

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
        #endregion
    }
}
