using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core
{
    [Table("TenderDatesChanges", Schema = "Tender")]
    public class TenderDatesChange : AuditableEntity
    {
        #region Constructors
        public TenderDatesChange()
        {
        }
        // userId not used for now, maybe later
        public TenderDatesChange(DateTime? lastEnqueriesDate, DateTime? lastOfferPresentationDate, string lastOfferPresentationTime,
            DateTime? offersOpeningDate, string offersOpeningTime, DateTime? offersCheckingDate, string offersCheckingTime, int tenderId)//, int userId)
        {
            LastEnqueriesDate = lastEnqueriesDate;
            LastOfferPresentationDate = lastOfferPresentationDate;
            LastOfferPresentationTime = lastOfferPresentationTime;
            OffersOpeningDate = offersOpeningDate;
            OffersOpeningTime = offersOpeningTime;
            OffersCheckingDate = offersCheckingDate;
            OffersOpeningTime = offersCheckingTime;
            StatusId = (int)Enums.TenderStatus.Established;
            EntityCreated();
        }

        #endregion

        #region Fields
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RevisionDateId { get; private set; }

        [ForeignKey(nameof(Status))]
        public int StatusId { get; private set; }
        public TenderStatus Status { get; private set; }
        public string RejectionReason { get; private set; }
        public DateTime? LastEnqueriesDate { get; private set; }
        public DateTime? LastOfferPresentationDate { get; private set; }
        public string LastOfferPresentationTime { get; private set; }
        public DateTime? OffersOpeningDate { get; private set; }
        public string OffersOpeningTime { get; private set; }
        public DateTime? OffersCheckingDate { get; private set; }
        public string OffersCheckingTime { get; private set; }

        [ForeignKey(nameof(ChangeRequest))]
        public int TenderChangeRequestId { get; private set; }
        public TenderChangeRequest ChangeRequest { get; private set; }
        #endregion

        #region Methods
        public void AcceptRevision()
        {
            StatusId = (int)Enums.TenderStatus.Approved;
            EntityUpdated();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }

        public void Delete()
        {
            EntityDeleted();
        }
        public void RejectRevision(String rejectionReason)
        {
            StatusId = (int)Enums.TenderStatus.Rejected;
            RejectionReason = rejectionReason;
            EntityUpdated();
        }
        #endregion
    }
}