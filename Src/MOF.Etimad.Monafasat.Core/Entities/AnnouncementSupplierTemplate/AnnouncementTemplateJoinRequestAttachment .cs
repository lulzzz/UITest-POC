
using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AnnouncementTemplateJoinRequestAttachment", Schema = "AnnouncementTemplate")]
    public class AnnouncementTemplateJoinRequestAttachment : AuditableEntity
    {
        [Key]
        public int Id { get; private set; }

        [StringLength(200)]
        [Required]
        public string Name { get; private set; }

        [StringLength(1024)]
        public string FileNetReferenceId { get; private set; }

        [ForeignKey(nameof(AnnouncementJoinRequestSupplierTemplate))]
        public int JoinRequestSupplierTemplateId { get; private set; }
        public AnnouncementJoinRequestSupplierTemplate JoinRequestSupplierTemplate { get; private set; }

        public AnnouncementTemplateJoinRequestAttachment()
        {

        }

        public AnnouncementTemplateJoinRequestAttachment(string name, string fileNetReferenceId)
        {
            Name = name;
            FileNetReferenceId = fileNetReferenceId;
            EntityCreated();
        }


        #region Methods====================================================


        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }

        internal void Delete()
        {
            EntityDeleted();
        }

        public void SetAddedState()
        {
            Id = 0;
            EntityCreated();
        }
        public void DeleteAttachment()
        {
            EntityDeleted();
        }
        #endregion
    }
}
