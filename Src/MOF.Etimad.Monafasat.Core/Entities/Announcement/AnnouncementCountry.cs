using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AnnouncementCountry", Schema = "Announcement")]

    public class AnnouncementCountry : AuditableEntity
    {
        [Key]
        public int Id { get; private set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; private set; }
        public Country Country { private set; get; }

        [ForeignKey(nameof(Announcement))]
        public int AnnouncementId { get; private set; }
        public Announcement Announcement { private set; get; }

        public AnnouncementCountry(int countryId)
        {
            CountryId = countryId;
            EntityCreated();
        }
        public AnnouncementCountry()
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