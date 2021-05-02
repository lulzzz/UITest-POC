using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [System.ComponentModel.DataAnnotations.Schema.Table("TenderUnitStatusesHistory", Schema = "Tender")]
    public class TenderUnitStatusesHistory : AuditableEntity
    {
        #region Fields

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public int TenderUnitStatusesHistoryId { get; private set; }
        public string Comment { get; private set; }

        [ForeignKey(nameof(Tender))]
        public int TenderId { get; private set; }
        public Tender Tender { private set; get; }

        public int TenderUnitStatusId { get; private set; }
        [ForeignKey(nameof(TenderUnitStatusId))]
        public TenderUnitStatus TenderUnitStatus { get; private set; }

        public int? TenderUnitUpdateTypeId { get; private set; }
        [ForeignKey(nameof(TenderUnitUpdateTypeId))]
        public TenderUnitUpdateType TenderUnitUpdateType { get; private set; }

        public decimal? EstimatedValue { get; private set; }

        #endregion

        #region Constructors

        public TenderUnitStatusesHistory()
        {

        }

        public TenderUnitStatusesHistory(string comment, int tenderUnitStatusId, decimal? estimatedValue, int? updateTypeId = null)
        {
            this.Comment = comment;
            this.TenderUnitStatusId = tenderUnitStatusId;
            TenderUnitUpdateTypeId = updateTypeId;
            EstimatedValue = estimatedValue;
            EntityCreated();
        }

        #endregion

    }
}
