using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("SupplierExtendOfferDatesRequest", Schema = "CommunicationRequest")]
    public class SupplierExtendOfferDatesRequest : AuditableEntity
    {
        #region Constructors====================================================
        public SupplierExtendOfferDatesRequest()
        {
        }
        public SupplierExtendOfferDatesRequest(string extendOfferDatesReason, DateTime extendOfferDatesRequestedDate, int agencyCommunicationRequestId, string cr, int supplierExtendOfferDatesRequestId = 0)
        {
            AgencyCommunicationRequestId = agencyCommunicationRequestId;
            ExtendOfferDatesRequestReason = extendOfferDatesReason;
            ExtendOfferDatesRequestedDate = extendOfferDatesRequestedDate;
            CR = cr;
            EntityCreated();
        }
        #endregion Constructors====================================================

        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SupplierExtendOfferDatesRequestId { get; private set; }
        public string ExtendOfferDatesRequestReason { get; private set; }
        public DateTime? ExtendOfferDatesRequestedDate { get; private set; }
        public string CR { get; private set; }
        public int AgencyCommunicationRequestId { get; private set; }
        [ForeignKey("AgencyCommunicationRequestId")]
        public AgencyCommunicationRequest AgencyCommunicationRequest { get; private set; }
        #endregion Fields====================================================

        #region Methods====================================================
        public void Delete()
        {
            EntityDeleted();
        }

        public void Update()
        {
            EntityUpdated();
        }
        public void DeActive()
        {
            IsActive = false;
            EntityUpdated();
        }
        public void SetActive()
        {
            IsActive = true;
            EntityUpdated();
        }
        #endregion Methods====================================================
    }
}
