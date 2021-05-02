using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AnnouncementArea", Schema = "Announcement")]

    public class AnnouncementArea : AuditableEntity
    {
        [Key]
        public int Id { get; private set; }

        [ForeignKey(nameof(Area))]
        public int AreaId { get; private set; }
        public Area Area { private set; get; }

        [ForeignKey(nameof(Announcement))]
        public int AnnouncementId { get; private set; }
        public Announcement Announcement { private set; get; }

        public AnnouncementArea(int areaId)
        {
            AreaId = areaId;
            EntityCreated();
        }

        public AnnouncementArea()
        {

        }

        public void Delete()
        {
            EntityDeleted();
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
    }
}