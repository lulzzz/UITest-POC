using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderType", Schema = "LookUps")]
    public class TenderType : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int TenderTypeId { get; set; }

        [StringLength(500)]
        public string NameAr { get; set; }

        [StringLength(500)]
        public string NameEn { get; set; }

        public decimal BuyingCost { get; set; }

        public decimal InvitationCost { get; set; }

        #endregion

        #region Constructors====================================================

        public TenderType()
        {

        }

        public TenderType(string nameEn, string nameAr, decimal buyingCost, decimal invitationCost, bool isActive)
        {
            NameAr = nameAr;
            NameEn = nameEn;
            BuyingCost = buyingCost;
            InvitationCost = invitationCost;
        }
        #endregion

        #region Methods====================================================
        public void UpdateCosts(decimal buyingCost, decimal invitationCost)
        {
            BuyingCost = buyingCost;
            InvitationCost = invitationCost;
            EntityUpdated();
        }
        #endregion
    }
}
