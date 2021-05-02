using MOF.Etimad.Monafasat.SharedKernel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderAgreementAgency", Schema = "Tender")]
    public class TenderAgreementAgency : AuditableEntity
    {
        #region Fields====================================================

        [Key]
        public int TenderAgreementAgencyId { get; private set; }
        public string AgencyCode { get; private set; }
        [ForeignKey(nameof(AgencyCode))]
        public GovAgency GovAgency { get; private set; }

        public int TenderId { get; private set; }
        [ForeignKey(nameof(TenderId))]
        public Tender Tender { get; private set; }

        #endregion

        #region Constructors====================================================

        public TenderAgreementAgency()
        {
        }

        public TenderAgreementAgency(string agencyCode, int tenderId)
        {
            AgencyCode = agencyCode;
            TenderId = tenderId;
            EntityCreated();
        }

        public TenderAgreementAgency(string agencyCode)
        {
            AgencyCode = agencyCode;
            EntityCreated();
        }

        #endregion

        #region Methods====================================================

        public void Update(string agencyCode, int tenderId)
        {
            AgencyCode = agencyCode;
            TenderId = tenderId;
            EntityUpdated();
        }
        public void Delete()
        {
            EntityDeleted();
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
        public void SetAddedState()
        {
            TenderAgreementAgencyId = 0;
            EntityCreated();
        }

        #endregion
    }
}