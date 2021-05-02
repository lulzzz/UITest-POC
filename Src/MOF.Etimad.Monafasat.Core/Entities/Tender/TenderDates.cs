using MOF.Etimad.Monafasat.SharedKernal;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderDates", Schema = "Tender")]
    public class TenderDates : BaseEntity
    {
        #region Fields====================================================
        [Key]
        public int Id { get; private set; } 

        [Required]
        [ForeignKey(nameof(Tender))]
        public int TenderId { get; private set; }
        public Tender Tender { get; private set; }

        public DateTime? AwardingExpectedDate { get; private set; }
        public DateTime? StartingBusinessOrServicesDate { get; private set; }
        public DateTime? ParticipationConfirmationLetterDate { get; private set; }
        public DateTime? OffersDeliveryDate { get; private set; }
        
        #endregion



        #region Constructors====================================================

        public TenderDates()
        {

        }

        public TenderDates(DateTime? awardingExpectedDate, DateTime? startingBusinessOrServicesDate, DateTime? participationConfirmationLetterDate, DateTime? offersDeliveryDate)
        {
            AwardingExpectedDate = awardingExpectedDate;
            StartingBusinessOrServicesDate = startingBusinessOrServicesDate;
            ParticipationConfirmationLetterDate = participationConfirmationLetterDate;
            OffersDeliveryDate = offersDeliveryDate;
            EntityCreated();
        }



        #endregion

        #region Methods====================================================

        public TenderDates UpdateTenderDates(DateTime? awardingExpectedDate, DateTime? startingBusinessOrServicesDate, DateTime? participationConfirmationLetterDate, DateTime? offersDeliveryDate)
        {
            AwardingExpectedDate = awardingExpectedDate;
            StartingBusinessOrServicesDate = startingBusinessOrServicesDate;
            ParticipationConfirmationLetterDate = participationConfirmationLetterDate;
            OffersDeliveryDate = offersDeliveryDate;
            EntityUpdated();
            return this;
        }

        public TenderDates UpdateTenderConfirmationLetterDate(DateTime? participationConfirmationLetterDate)
        {
            ParticipationConfirmationLetterDate = participationConfirmationLetterDate;
            EntityUpdated();
            return this;
        }



        #endregion
    }
}