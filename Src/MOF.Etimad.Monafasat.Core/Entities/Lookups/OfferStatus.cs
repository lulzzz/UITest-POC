using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities.Lookups
{
    [Table("OfferStatus", Schema = "LookUps")]
    public class OfferStatus
    {
        public OfferStatus()
        {

        }
        public OfferStatus(int id, string nameEn, string nameAr)
        {
            OfferStatusId = id;
            NameAr = nameAr;
            NameEn = nameEn;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int OfferStatusId { get; private set; }

        [StringLength(100)]
        public string NameAr { get; private set; }

        [StringLength(100)]
        public string NameEn { get; private set; }
    }
}
