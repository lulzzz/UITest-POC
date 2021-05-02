using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MOF.Etimad.Monafasat.Core.Entities
{

    [Table("AnnouncementConstructionWorkSupplierTemplate", Schema = "AnnouncementTemplate")]
    public class AnnouncementConstructionWorkSupplierTemplate : AuditableEntity
    {
        [Key]
        public int Id { get; private set; }
        [ForeignKey("ConstructionWork")]
        public int ConstructionWorkId { private set; get; }
        public ConstructionWork ConstructionWork { private set; get; }
        [ForeignKey("AnnouncementSupplierTemplate")]
        public int AnnouncementId { get; private set; }
        public AnnouncementSupplierTemplate Announcement { private set; get; }

        public AnnouncementConstructionWorkSupplierTemplate()
        {

        }
        public AnnouncementConstructionWorkSupplierTemplate(int constructionWorkId)
        {
            ConstructionWorkId = constructionWorkId;
            EntityCreated();
        }
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
        internal void Delete()
        {
            EntityDeleted();
        }

        public void Test_Delete()
        {
            this.Delete();
        }
    }
}
