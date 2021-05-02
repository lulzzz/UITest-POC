using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AnnouncementJoinRequest", Schema = "Announcement")]

    public class AnnouncementJoinRequest : AuditableEntity
    {
        [Key]
        public int Id { get; private set; }
        public int StatusId { get; private set; }
        public int AnnouncementId { get; private set; }
        public string Cr { get; set; }
        [ForeignKey(nameof(StatusId))]
        public AnnouncementJoinRequestStatus joinRequestStatus { get; private set; }
        [ForeignKey(nameof(AnnouncementId))]
        public Announcement Announcement { get; private set; }
        [ForeignKey(nameof(Cr))]
        public Supplier Supplier { get; private set; }

        public AnnouncementJoinRequest()
        {

        }
        public AnnouncementJoinRequest(int AnnouncementId, string Cr, int StatusId)
        {

            this.AnnouncementId = AnnouncementId;
            this.Cr = Cr;
            this.StatusId = StatusId;
            EntityCreated();

        }

        public void UpdateAnnouncementJoinRequest(int AnnouncementId, string Cr, int StatusId)
        {

            this.AnnouncementId = AnnouncementId;
            this.Cr = Cr;
            this.StatusId = StatusId;
            EntityUpdated();

        }
        public void WithDraw()
        {
            this.StatusId = (int)Enums.AnnouncementJoinRequestStatus.WithDraw;
            EntityUpdated();

        }
    }
}
