using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AnnouncementActivity", Schema = "Announcement")]

    public class AnnouncementActivity : AuditableEntity
    {

        [Key]
        public int AnnouncementActivityId { get; private set; }
        [ForeignKey(nameof(ActivityId))]
        public int ActivityId { private set; get; }
        public virtual Activity Activity { private set; get; }
        public int AnnouncementId { private set; get; }
        [ForeignKey(nameof(AnnouncementId))]
        public Announcement Announcement { private set; get; }

        public AnnouncementActivity()
        {

        }

        public AnnouncementActivity(int activityId)
        {
            ActivityId = activityId;
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