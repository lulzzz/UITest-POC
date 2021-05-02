using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MOF.Etimad.Monafasat.Core.Entities
{

    [Table("LinkedAgenciesAnnouncementTemplate", Schema = "AnnouncementTemplate")]
    public class LinkedAgenciesAnnouncementTemplate : AuditableEntity
    {
        [Key]
        public int Id { get; private set; }

        [StringLength(20)]
        [ForeignKey(nameof(Agency))]
        public string AgencyCode { get; private set; }
        public GovAgency Agency { private set; get; }

        [ForeignKey(nameof(AnnouncementSupplierTemplate))]
        public int AnnouncementId { get; private set; }
        public AnnouncementSupplierTemplate AnnouncementSupplierTemplate { private set; get; }

        public LinkedAgenciesAnnouncementTemplate()
        {

        }
        public LinkedAgenciesAnnouncementTemplate(string agencycode)
        {
            AgencyCode = agencycode;
            EntityCreated();
        }

        public void DeActivate()
        {
            IsActive = false;
            EntityUpdated();
        }


    }
}
