using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("AnnouncementCountrySupplierTemplate", Schema = "AnnouncementTemplate")]

    public class AnnouncementCountrySupplierTemplate : AuditableEntity
    {
        [Key]
        public int Id { get; private set; }

        [ForeignKey(nameof(Country))]
        public int CountryId { get; private set; }
        public Country Country { private set; get; }

        [ForeignKey(nameof(AnnouncementSupplierTemplate))]
        public int AnnouncementId { get; private set; }
        public AnnouncementSupplierTemplate Announcement { private set; get; }

        public AnnouncementCountrySupplierTemplate(int countryId)
        {
            CountryId = countryId;
            EntityCreated();
        }
        public AnnouncementCountrySupplierTemplate()
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
