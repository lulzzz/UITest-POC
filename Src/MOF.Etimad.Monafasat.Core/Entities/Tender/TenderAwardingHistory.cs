using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderAwardingHistory", Schema = "Tender")]
    public class TenderAwardingHistory : AuditableEntity
    {
        #region Fields====================================================

        [Key]
        public int TenderAwardingHistoryId { get; private set; }
        public string CommercialRegisterationNumber { get; private set; }
        [ForeignKey(nameof(CommercialRegisterationNumber))]
        public Supplier Supplier { get; private set; }

        public int TenderId { get; private set; }
        [ForeignKey(nameof(TenderId))]
        public Tender Tender { get; private set; }

        public bool? TenderAwardingType { get; private set; }
        public decimal? AwardingValue { get; private set; }
        public int AwardingIndex { get; private set; }

        #endregion

        #region Constructors====================================================

        public TenderAwardingHistory()
        {

        }

        public TenderAwardingHistory(string commercialRegisterationNumber, int tenderId, bool? tenderAwardingType, decimal? awardingValue, int awardingIndex)
        {
            CommercialRegisterationNumber = commercialRegisterationNumber;
            TenderId = tenderId;
            TenderAwardingType = tenderAwardingType;
            AwardingValue = awardingValue;
            AwardingIndex = awardingIndex;
            EntityCreated();
        }

        #endregion

        #region Methods====================================================

        public void Update(string commercialRegisterationNumber, int tenderId, bool? tenderAwardingType, decimal? awardingValue, int awardingIndex)
        {
            CommercialRegisterationNumber = commercialRegisterationNumber;
            TenderId = tenderId;
            TenderAwardingType = tenderAwardingType;
            AwardingValue = awardingValue;
            AwardingIndex = awardingIndex;
            EntityUpdated();
        }

        #endregion
    }
}