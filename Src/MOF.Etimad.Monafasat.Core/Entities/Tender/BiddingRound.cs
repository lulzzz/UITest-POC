using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("BiddingRounds", Schema = "Tender")]
    public class BiddingRound : AuditableEntity
    {
        #region Fields====================================================
        [Key]
        public int BiddingRoundId { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public int BiddingRoundStatusId { get; private set; }
        [ForeignKey(nameof(BiddingRoundStatusId))]
        public BiddingRoundStatus BiddingRoundStatus { get; private set; }
        public int TenderId { get; private set; }
        [ForeignKey(nameof(TenderId))]
        public Tender Tender { get; private set; }

        public List<BiddingRoundOffer> BiddingRoundOffers { get; set; } = new List<BiddingRoundOffer>();

        #endregion

        #region Constructors====================================================

        public BiddingRound()
        {

        }

        public BiddingRound(DateTime startDate, DateTime endDate, int statusId, int tenderId)
        {
            StartDate = startDate;
            EndDate = endDate;
            BiddingRoundStatusId = statusId;
            TenderId = tenderId;
            EntityCreated();
        }

        #endregion

        #region Methods====================================================

        public void Update(DateTime startDate, DateTime endDate, int statusId, int tenderId)
        {
            StartDate = startDate;
            EndDate = endDate;
            BiddingRoundStatusId = statusId;
            TenderId = tenderId;
            EntityUpdated();
        }

        public void UpdateDates(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
            EntityUpdated();
        }

        public void UpdateStatus(int statusId)
        {
            BiddingRoundStatusId = statusId;
            EntityUpdated();
        }

        public void Deactive()
        {
            BiddingRoundOffers.ForEach(a => a.Deactive());
            IsActive = false;
            EntityUpdated();
        }

        public BiddingRound SetTender(Tender tender)
        {
            Tender = tender;
            return this;
        }
        #endregion
    }
}