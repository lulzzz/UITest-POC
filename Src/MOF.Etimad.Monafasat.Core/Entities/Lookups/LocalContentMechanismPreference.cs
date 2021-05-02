using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("LocalContentMechanismPreference", Schema = "LookUps")]

    public class LocalContentMechanismPreference
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; private set; }

        [StringLength(1024)]
        public string NameEn { get; private set; }

        [StringLength(1024)]
        public string NameAr { get; private set; }
        public List<LocalContentMechanism> LocalContentMechanism { private set; get; }

    }
}
