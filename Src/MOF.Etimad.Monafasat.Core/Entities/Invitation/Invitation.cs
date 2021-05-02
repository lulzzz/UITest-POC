using MOF.Etimad.Monafasat.Core.Entities.Lookups;
using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("Invitation", Schema = "Invitation")]
    public class Invitation : AuditableEntity
    {

        #region Fileds

        [Key]
        public int InvitationId { get; private set; }

        public string RejectionReason { get; set; }

        public DateTime? SubmissionDate { get; private set; }

        public DateTime? SendingDate { get; private set; }

        public DateTime? WithdrawalDate { get; private set; }

        public bool? InvitedByQualification { get; private set; }

        public int? SupplierType { get; private set; }


        [StringLength(20)]
        [ForeignKey(nameof(Supplier))]
        public string CommericalRegisterNo { get; private set; }
        [ForeignKey(nameof(Tender))]
        public int TenderId { get; private set; }

        [ForeignKey(nameof(InvitationStatus))]
        public int StatusId { get; private set; }

        [ForeignKey(nameof(InvitationType))]
        public int InvitationTypeId { get; private set; }
        public Supplier Supplier { get; set; }
        public Tender Tender { get; private set; }
        public InvitationStatus InvitationStatus { get; private set; }
        public InvitationType InvitationType { get; private set; }
        public BillInfo BillInfo { get; private set; }
        #endregion

        #region Constractors
        public Invitation()
        {

        }

        public Invitation(string commericalRegisterNo, Enums.InvitationStatus statusId, Enums.InvitationRequestType invitationTypeId, bool invitedByQualification)
        {
            CommericalRegisterNo = commericalRegisterNo;
            StatusId = (int)statusId;
            InvitationTypeId = (int)invitationTypeId;
            InvitedByQualification = invitedByQualification;
            EntityCreated();
        }

        public Invitation(string commericalRegisterNo, Enums.InvitationStatus statusId, Enums.InvitationRequestType invitationTypeId, bool invitedByQualification, int supplierType)
        {
            CommericalRegisterNo = commericalRegisterNo;
            StatusId = (int)statusId;
            InvitationTypeId = (int)invitationTypeId;
            InvitedByQualification = invitedByQualification;
            if (statusId == Enums.InvitationStatus.New)
                SendingDate = DateTime.Now.Date;
            SupplierType = supplierType;
            EntityCreated();
        }

        #endregion

        #region Methods
        public void SendInvitation()
        {
            StatusId = (int)Enums.InvitationStatus.New;
            SendingDate = DateTime.Now.Date;
            EntityUpdated();
        }
        public void AddBillInfo(BillInfo billInfo)
        {
            this.BillInfo = billInfo;
            EntityUpdated();
        }
        public void UpdateStatus(int invitationStatusId, string rejectionReason)
        {
            StatusId = invitationStatusId;
            if (!string.IsNullOrEmpty(rejectionReason))
                RejectionReason = rejectionReason;
            if (invitationStatusId == (int)Enums.InvitationStatus.New)
            {
                SendingDate = DateTime.Now.Date;
            }
            else if (invitationStatusId == (int)Enums.InvitationStatus.Withdrawal)
            {
                WithdrawalDate = DateTime.Now.Date;
                if (this.BillInfo != null)
                {
                    this.BillInfo.DeleteBillInWithdrawAndRejectFromInvitaion();
                }

            }
            else if (invitationStatusId == (int)Enums.InvitationStatus.Approved || invitationStatusId == (int)Enums.InvitationStatus.Rejected)
            {
                SubmissionDate = DateTime.Now.Date;  //تاريخ قبول او رفض الدعوة 
            }
            EntityUpdated();
        }
        public void Delete()
        {
            EntityDeleted();
        }

        public void UpdateStatusForResend()
        {
            StatusId = (int)Enums.InvitationStatus.New;
            WithdrawalDate = null;
            RejectionReason = null;
            SubmissionDate = null;
            EntityUpdated();
        }

        public void Test_AddTender(Tender tender)
        {
            this.Tender = tender;
        }
        #endregion
    }
}
