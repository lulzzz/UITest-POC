using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("MandatoryListProductChangeRequest", Schema = "MandatoryList")]
    public class MandatoryListProductChangeRequest : AuditableEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(400)]
        public string CSICode { get; set; }

        [StringLength(400)]
        public string NameAr { get; set; }

        [StringLength(400)]
        public string NameEn { get; set; }

        public string DescriptionAr { get; set; }

        public string DescriptionEn { get; set; }

        public double PriceCelling { get; set; }

        public void Update(MandatoryListProductChangeRequest entity)
        {
            CSICode = entity.CSICode;
            NameAr = entity.NameAr;
            NameEn = entity.NameEn;
            DescriptionAr = entity.DescriptionAr;
            DescriptionEn = entity.DescriptionEn;
            PriceCelling = entity.PriceCelling;

            EntityUpdated();
        }

        public void SetInActive()
        {
            IsActive = false;
            EntityUpdated();
        }

        public void Add()
        {
            EntityCreated();
        }
    }
}
