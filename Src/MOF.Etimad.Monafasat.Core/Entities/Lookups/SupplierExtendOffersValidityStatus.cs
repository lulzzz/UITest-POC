using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("SupplierExtendOffersValidityStatus", Schema = "LookUps")]
    public class SupplierExtendOffersValidityStatus
    {
        public SupplierExtendOffersValidityStatus()
        {

        }
        public SupplierExtendOffersValidityStatus(int id, string name)
        {
            SupplierExtendOffersValidityStatusId = id;
            Name = name;
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int SupplierExtendOffersValidityStatusId { get; private set; }

        [StringLength(100)]
        public string Name { get; private set; }
    }
}
