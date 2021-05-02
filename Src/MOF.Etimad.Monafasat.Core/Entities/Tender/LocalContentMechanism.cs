using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("LocalContentMechanism", Schema = "Tender")]

    public class LocalContentMechanism : AuditableEntity
    {
        [Key]
        public int Id { get; private set; }
        [ForeignKey(nameof(LocalContentMechanismPreference))]
        public int LocalContentMechanismPreferenceId { get; private set; }
        public LocalContentMechanismPreference LocalContentMechanismPreference { private set; get; }

        [ForeignKey(nameof(TenderLocalContent))]
        public int TenderLocalContentId { get; private set; }
        public TenderLocalContent TenderLocalContent { private set; get; }

        public LocalContentMechanism(int localContentMechanismPreferenceId)
        {
            LocalContentMechanismPreferenceId = localContentMechanismPreferenceId;
            EntityCreated();
        }

        public LocalContentMechanism()
        {

        }
        public void Delete()
        {
            EntityDeleted();
        }
    }
}
