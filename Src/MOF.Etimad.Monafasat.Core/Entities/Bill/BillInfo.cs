using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("BillInfo", Schema = "Sadad")]
    public class BillInfo : AuditableEntity
    {
        #region Constructors====================================================
        public BillInfo()
        { }
        public BillInfo(decimal amountDue, DateTime dueDt, DateTime expiryDate, string agencyCode)
        {
            this.AmountDue = amountDue;
            this.DueDate = dueDt;
            this.ExpiryDate = expiryDate;
            this.BillStatusId = (int)Enums.BillStatus.New;
            this.AgencyCode = agencyCode;
            BillPaymentDetails = new List<BillPaymentDetails>();
            EntityCreated();
        }
        #endregion

        #region Billing Fields====================================================
        public int Id { get; private set; }
        [StringLength(50)]
        public string AgencyCode { get; set; }
        [StringLength(50)]
        public string BillInvoiceNumber { get; private set; }
        [Column(TypeName = "numeric(15, 2)")]
        public decimal AmountDue { get; private set; }
        public DateTime DueDate { get; private set; }
        public DateTime ExpiryDate { get; private set; }
        [StringLength(100)]
        public string BillReferenceInfo { get; private set; }
        [StringLength(50)]
        public string BenChapterNumber { get; private set; }
        [StringLength(50)]
        public string BenSectionNumber { get; private set; }
        [StringLength(50)]
        public string BenSequenceNumber { get; private set; }
        [StringLength(50)]
        public string BenSubDepartmentsCount { get; private set; }
        [StringLength(50)]
        public string BenSubSectionsCount { get; private set; }
        public string SadadBatchId { get; private set; }
        [StringLength(100)]
        public string DisplayLabelAr { get; private set; }
        [StringLength(100)]
        public string DisplayLabelEn { get; private set; }
        public int? ActionStatus { get; private set; }
        public string ActionReason { get; private set; }
        [StringLength(50)]
        public string POINumber { get; private set; }
        [StringLength(50)]
        public string POIType { get; private set; }
        [StringLength(50)]
        public string BankPaymentId { get; private set; }
        [StringLength(50)]
        public string BankId { get; private set; }
        [StringLength(50)]
        public string BankBranchId { get; private set; }
        [StringLength(50)]
        public string PaymentChannel { get; private set; }
        [StringLength(50)]
        public string BankReversalTransactionID { get; private set; }
        public string IntermediatePaymentId { get; private set; }
        [StringLength(50)]
        public string SadadPaymentTranactionId { get; private set; }
        [StringLength(50)]
        public string PaymentStatusCode { get; private set; }
        [Column(TypeName = "money")]
        public decimal? CurrnetAmount { get; private set; }
        public DateTime? PurchaseDate { get; private set; }
        [StringLength(50)]
        public string BillingAccount { get; private set; }
        [StringLength(50)]
        public string PaymentMethod { get; private set; }
        [StringLength(100)]
        public string PaymentReferencefInfo { get; private set; }

        public int? ConditionsBookletID { get; private set; }
        [ForeignKey(nameof(ConditionsBookletID))]
        public ConditionsBooklet ConditionsBooklet { get; private set; }
        public int? InvitationId { get; private set; }

        [ForeignKey(nameof(InvitationId))]
        public Invitation Invitation { get; private set; }

        public int BillStatusId { get; private set; }

        [ForeignKey(nameof(BillStatusId))]
        public BillStatus BillStatus { get; private set; }
        public List<BillPaymentDetails> BillPaymentDetails { get; private set; }

        #endregion
        #region Methods ===========================================================
        public void UpdateBillStatus(Enums.BillStatus billStatus)
        {
            this.BillStatusId = (int)billStatus;
            EntityUpdated();
        }
        public void UpdateActionStatus(Enums.BillActionStatus billActionStatus)
        {
            this.ActionStatus = (int)billActionStatus;
            EntityUpdated();
        }
        public void UpdateActionStatusAndExpiryDate(Enums.BillActionStatus billActionStatus, DateTime? expiryDate)
        {
            this.ActionStatus = (int)billActionStatus;
            this.ExpiryDate = expiryDate.Value;
            EntityUpdated();
        }

        public void UpdateExpiryDateWithoutChangeState(DateTime? expiryDate)
        {
            this.ExpiryDate = expiryDate.Value;
        }
        public void UpdateActionReason(string actionReason)
        {
            this.ActionReason = actionReason;
            EntityUpdated();
        }
        public void UpdateBillInfo(string billRefInfo, string benChapterNo, string benBranchNo, string benSectionNo, string benSequenceNo, string benSubDepartmentsCount, string benSubSectionsCount, string agencyCode, string displayLabelAr, string displayLabelEn, string sadadBatchId)
        {
            this.BillInvoiceNumber = billRefInfo;
            this.BillReferenceInfo = billRefInfo;
            this.BenChapterNumber = benChapterNo;
            this.BankBranchId = benBranchNo;
            this.BenSectionNumber = benSectionNo;
            this.BenSequenceNumber = benSequenceNo;
            this.BenSubDepartmentsCount = benSubDepartmentsCount;
            this.BenSubSectionsCount = benSubSectionsCount;
            this.AgencyCode = agencyCode;
            this.DisplayLabelAr = displayLabelAr;
            this.DisplayLabelEn = DisplayLabelEn;
            this.SadadBatchId = sadadBatchId;
            this.BillStatusId = (int)Enums.BillStatus.SuccessUploaded;
            EntityUpdated();
        }
        public void UpdatePaymentDetails(string intermediatePaymentId, string sadadPaymentTranactionId, string bankReversalTransactionID,
               string paymentStatusCode, string billingAccount, decimal? currnetAmount,
              DateTime? purchaseDate,
              string BankId, string accessChannel, string paymentMethod, string paymentReferencefInfo, string bankPaymentID, string poiNumber, string poiType)
        {
            this.IntermediatePaymentId = intermediatePaymentId;
            this.SadadPaymentTranactionId = sadadPaymentTranactionId;
            this.BankReversalTransactionID = bankReversalTransactionID;
            this.PaymentStatusCode = paymentStatusCode;
            this.BankId = BankId;
            this.PurchaseDate = purchaseDate;
            this.PaymentChannel = accessChannel;
            this.PaymentMethod = paymentMethod;
            this.CurrnetAmount = currnetAmount.Value;
            this.PaymentReferencefInfo = paymentReferencefInfo;
            this.BillingAccount = billingAccount;
            this.BankPaymentId = bankPaymentID;
            this.POINumber = poiNumber;
            this.POIType = poiType;
            this.BillStatusId = (int)Enums.BillStatus.Paid;
            EntityUpdated();
        }

        public void UpdateFreeTenderPurchaseDetails(decimal currnetAmount, DateTime purchaseDate)
        {
            this.CurrnetAmount = currnetAmount;
            this.PurchaseDate = purchaseDate;
            this.BillStatusId = (int)Enums.BillStatus.Paid;
            EntityUpdated();
        }
        public void DeleteBooklet()
        {
            this.ConditionsBooklet.Delete();
            this.BillPaymentDetails.ForEach(x => x.Delete());
            EntityDeleted();
        }
        public void DeleteInvitation()
        {
            this.Invitation.UpdateStatus((int)Enums.InvitationStatus.New, "");
            this.IsActive = false;
            this.InvitationId = null;
            this.BillPaymentDetails.ForEach(x => x.Delete()); // If Not Deleted DeAtivate It 
            EntityDeleted();
        }
        public void DeleteBillInWithdrawAndRejectFromInvitaion()
        {
            this.BillPaymentDetails.ForEach(x => x.Delete()); // If Not Deleted DeAtivate It 
            EntityDeleted();
        }

        public void UpdateBillStatusForRollOutTeam()
        {
            this.BillStatusId = (int)Enums.BillStatus.Paid;
            EntityUpdated();
        }
        #endregion

        #region Test Helper Methods

        public void Test_AddBooklet(ConditionsBooklet conditionsBooklet)
        {
            this.ConditionsBooklet = conditionsBooklet;
        }

        public void Test_AddInvitation(Invitation invitation)
        {
            this.Invitation = invitation;
        }


        public BillInfo(BillInfoModel billModel)
        {
            this.AmountDue = billModel.AmountDue;
            this.DueDate = billModel.DueDate;
            this.ExpiryDate = billModel.ExpDate;
            this.AgencyCode = billModel.AgencyCode;
            this.BillInvoiceNumber = billModel.BillInvoiceNumber;
            this.ActionStatus = billModel.ActionStatus;
            this.BenChapterNumber = billModel.ChapterNumber;
            this.BenSectionNumber = billModel.SectionId;
            this.BenSequenceNumber = billModel.SequenceNumber;
            this.BenSubDepartmentsCount = billModel.NumOfSubDepartments;
            this.BenSubSectionsCount = billModel.NumOfSubSections;
            this.BankBranchId = billModel.BankBranchId;
            this.ConditionsBookletID = billModel.ConditionBookletId;
            this.BillStatusId = billModel.BillStatusId;
            this.BillReferenceInfo = billModel.BillReferenceInfo;
            this.CreatedBy = billModel.CreatedBy;
            this.CreatedAt = billModel.CreatedAt;
            this.PurchaseDate = billModel.PurchaseDate;
            this.IsActive = billModel.IsActive;
            BillPaymentDetails = new List<BillPaymentDetails>();
            EntityCreated();
        }

        #endregion
    }
}
