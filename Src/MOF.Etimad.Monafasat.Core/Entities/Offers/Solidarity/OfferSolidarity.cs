using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("OfferSolidarity", Schema = "Offer")]
    public class OfferSolidarity : AuditableEntity
    {

        #region [FIELDS]
        [Key]
        public int Id { get; private set; }

        public SupplierCombinedDetail SupplierCombinedDetail { get; private set; }
        public int OfferId { get; protected set; }
        [ForeignKey("OfferId")]
        public Offer Offer { get; protected set; }
        [StringLength(1024)]
        public string RejectionReason { get; protected set; }
        public string Email { get; protected set; }
        public string Mobile { get; protected set; }

        public int StatusId { get; protected set; }
        [ForeignKey(nameof(StatusId))]
        public Lookups.OfferSolidarityStatus SolidarityStatus { get; private set; }
        public DateTime? SubmissionDate { get; protected set; }



        public int SolidarityTypeId { get; protected set; }
        public string CRNumber { get; protected set; } = "4030059098";
        [ForeignKey(nameof(CRNumber))]
        public Supplier Supplier { get; protected set; }

        public void UpdateStatus(Enums.SupplierSolidarityStatus status)
        {
            if (status == Enums.SupplierSolidarityStatus.Approved)
                SubmissionDate = DateTime.Now.Date;
            StatusId = (int)status;
            EntityUpdated();
        }
        public void Delete()
        {
            IsActive = false;
            EntityDeleted();
        }
        #endregion

        #region Methods====================================================

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

        public OfferSolidarity(string email, string mobile, Enums.UnRegisteredSuppliersInvitationType unRegisteredSuppliersInvitationType = UnRegisteredSuppliersInvitationType.Existed, Enums.SupplierSolidarityStatus supplierSolidarityStatus = SupplierSolidarityStatus.New, string LicenceOrCR = "")
        {
            Email = email;
            SolidarityTypeId = (int)unRegisteredSuppliersInvitationType;
            CRNumber = LicenceOrCR;
            Mobile = mobile;
            StatusId = (int)supplierSolidarityStatus;
            EntityCreated();

        }
        public OfferSolidarity() { }
        public OfferSolidarity(int offerId)
        {
            OfferId = offerId;
            StatusId = (int)Enums.SupplierSolidarityStatus.New;
            EntityCreated();
        }
        public OfferSolidarity(int _offerId, string commericalRegisterNo)
        {
            CRNumber = commericalRegisterNo;
            OfferId = _offerId;
        }

        public OfferSolidarity(string cr, int typeId = (int)Enums.UnRegisteredSuppliersInvitationType.Existed)
        {
            CRNumber = cr;
            StatusId = (int)Enums.SupplierSolidarityStatus.New;
            SolidarityTypeId = typeId;
            EntityCreated();
        }

        public OfferSolidarity(string cr, Enums.SupplierSolidarityStatus status = Enums.SupplierSolidarityStatus.New, Enums.UnRegisteredSuppliersInvitationType typeId = Enums.UnRegisteredSuppliersInvitationType.Existed)
        {
            CRNumber = cr;
            SolidarityTypeId = (int)typeId;
            StatusId = (int)status;
            EntityCreated();
        }

        public void LinkOfferForTest(Offer offer)
        {
            OfferId = offer.OfferId;
            Offer = offer;
        }
        #endregion


    }


}
