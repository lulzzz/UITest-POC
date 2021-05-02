using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.TestsBuilders.TenderDefaults;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace MOF.Etimad.Monafasat.TestsBuilders.BillDefaults
{
    public class BillDefaults
    {
        public readonly decimal _amountDue = 10;
        public readonly DateTime _dueDt = DateTime.Now;
        public readonly DateTime _expiryDate = DateTime.Now.AddDays(7);
        public readonly string _agencyCode = "0410010000000002000";
        public readonly int _actionStatus = 1;
        public readonly string _chapterNumber = "067";
        public readonly string _sectionNumber = "000";
        public readonly string _sequanceNumber = "000";
        public readonly string _subdepartmentNumber = "000";
        public readonly string _subsectionsNumber = "000";
        public readonly string _bankbranch = "000";
        public readonly string _billInvoicenumber = "93991132000";
        public readonly int _billStatusId = 3;
        public readonly int _conditionalBookletId = 720;
        public readonly int _invitationid = 720;
        public readonly string _billreferenceInfo = "93991132000";
        public readonly string _createdBy = "hhhh";
        public readonly DateTime _createdAt = DateTime.Now;
        public readonly DateTime _purchaseDate = DateTime.Now;
        public readonly DateTime _updatedAt = DateTime.Now;
        public readonly int _tenderId = 502;
        public readonly bool _isActive = true;
        public readonly bool _isValid = true;
        public readonly string _cr = "1299656801";
        public int _agencyType = 1;


        public BillInfo returnBillInfoDefaults()
        {
            return new BillInfo(_amountDue, _dueDt, _expiryDate, _agencyCode);
        }

        public BillInfo GetBillInfoData()
        {
            BillInfoModel billInfoModel = new BillInfoModel()
            {
                AmountDue = _amountDue,
                DueDate = _dueDt,
                ExpDate = _expiryDate,
                AgencyCode = _agencyCode,
                ActionStatus = _actionStatus,
                ChapterNumber = _chapterNumber,
                NumOfSubSections = _sectionNumber,
                SequenceNumber = _sequanceNumber,
                NumOfSubDepartments = _subdepartmentNumber,
                SectionId = _subsectionsNumber,
                BankBranchId = _bankbranch,
                BillInvoiceNumber = _billInvoicenumber,
                BillStatusId = _billStatusId,
                ConditionBookletId = _conditionalBookletId,
                BillReferenceInfo = _billreferenceInfo,
                CreatedBy = _createdBy,
                CreatedAt = _createdAt,
                PurchaseDate = _purchaseDate,
            };

            var billInfo = new BillInfo(billInfoModel);

            var conditionbooklet = new ConditionsBooklet(_cr, billInfo);
            Tender tender = new Tender();
            Supplier supplier = new Supplier(_cr, _cr, new List<UserNotificationSetting>());

            var invitation = new Invitation(_cr,Enums.InvitationStatus.Approved,Enums.InvitationRequestType.Invitation,true);
            tender.SetOfferPresenationDate_ForTest();
            tender.SetTenderName_ForTest("test");
            conditionbooklet.Test_AddTender(tender);
            conditionbooklet.Test_AddSupplier(supplier);
            invitation.Test_AddTender(tender);
            billInfo.Test_AddInvitation(invitation);
            billInfo.Test_AddBooklet(conditionbooklet);
            return billInfo;
        }

        public List<BillInfo> GetBillsInfoData()
        {
            BillInfoModel billInfoModel = new BillInfoModel()
            {
                AmountDue = _amountDue,
                DueDate = _dueDt,
                ExpDate = _expiryDate,
                AgencyCode = _agencyCode,
                ActionStatus = _actionStatus,
                ChapterNumber = _chapterNumber,
                NumOfSubSections = _sectionNumber,
                SequenceNumber = _sequanceNumber,
                NumOfSubDepartments = _subdepartmentNumber,
                SectionId = _subsectionsNumber,
                BankBranchId = _bankbranch,
                BillInvoiceNumber = _billInvoicenumber,
                BillStatusId = _billStatusId,
                ConditionBookletId = _conditionalBookletId,
                BillReferenceInfo = _billreferenceInfo,
                CreatedBy = _createdBy,
                CreatedAt = _createdAt,
                PurchaseDate = _purchaseDate,
            };
            BillInfo billInfo = new BillInfo(billInfoModel);
            var conditionbooklet = new ConditionsBooklet(_cr, billInfo);
            Tender tender = new Tender();
            tender.SetOfferPresenationDate_ForTest();
            conditionbooklet.Test_AddTender(tender);
            billInfo.Test_AddInvitation(new Invitation());
            billInfo.Test_AddBooklet(conditionbooklet);
            List<BillInfo> billInfos = new List<BillInfo>();
            billInfos.Add(billInfo);
            return billInfos;
        }


        public SupplierFullDataModel GetSupplierFullData() {

            return new SupplierFullDataModel()
            {
                supplierName = _cr,
                CRNameEN = _cr
            };
        }

        public AgencyInfoModel GetAgencyInfoModelData()
        {
            return new AgencyInfoModel() {
                beneficiaryClassNo = _chapterNumber,
                sectionNoForBeneficiary = _sectionNumber,
                beneficiarySerialNo = _sequanceNumber,
                managementNoForBeneficiary = _subdepartmentNumber,
                beneficiarySectionNo = _subsectionsNumber,
                beneficiaryBranchNo = _bankbranch,
            };
        }

        public BillModel GetBillModelData()
        {
           List<TenderFinantialCostModel> TenderFees = new List<TenderFinantialCostModel>();
           TenderFees.Add(GetTenderFinantialCostModelData());
            return new BillModel()
            {
                AmountDue = _amountDue,
                DueDate = _dueDt,
                ExpDate = _expiryDate,
                AgencyCode = _agencyCode,
                ActionStatus = _actionStatus,
                ChapterNumber = _chapterNumber,
                NumOfSubSections = _sectionNumber,
                SequenceNumber = _sequanceNumber,
                NumOfSubDepartments = _subdepartmentNumber,
                SectionId = _subsectionsNumber,
                BankBranchId = _bankbranch,
                BillInvoiceNumber = _billInvoicenumber,
                ConditionBookletId = _conditionalBookletId,
                BillReferenceInfo = _billreferenceInfo,
                AgencyType= _agencyType,
                TenderFees= TenderFees,
                RequestType =2
            };
        }

        public TenderFinantialCostModel GetTenderFinantialCostModelData()
        {
            return new TenderFinantialCostModel() {
                FessType=3,
                FeesValue=10
            };
        }


        public BillArchive GetBillArchiveData()
        {
            BillArchive billArchive = new BillArchive(_conditionalBookletId, 1, 1, "123", _billInvoicenumber, _agencyCode, _cr);
            return billArchive;
        }

        public PaymentNotificationServiceModel GetPaymentNotificationServiceData()
        {
            return new PaymentNotificationServiceModel(_agencyCode,"intermadiate","billcategary",_billInvoicenumber,_amountDue,DateTime.Now, "SADAD", "Succeeded", "Monafasat", "transactionId", "RJHISARI",
                "bankreversal", "X00174002562031904310I0209000092", "INTERNET", "poiNumber","poitype");
        }

        public BillInfo GetBillInfoDataForPaidBill()
        {
            BillInfoModel billInfoModel = new BillInfoModel()
            {
                AmountDue = _amountDue,
                DueDate = _dueDt,
                ExpDate = _expiryDate,
                AgencyCode = _agencyCode,
                ActionStatus = _actionStatus,
                ChapterNumber = _chapterNumber,
                NumOfSubSections = _sectionNumber,
                SequenceNumber = _sequanceNumber,
                NumOfSubDepartments = _subdepartmentNumber,
                SectionId = _subsectionsNumber,
                BankBranchId = _bankbranch,
                BillInvoiceNumber = _billInvoicenumber,
                BillStatusId = 4,
                ConditionBookletId = _conditionalBookletId,
                BillReferenceInfo = _billreferenceInfo,
                CreatedBy = _createdBy,
                CreatedAt = _createdAt,
                PurchaseDate = _purchaseDate,
            };

            var billInfo = new BillInfo(billInfoModel);

            var conditionbooklet = new ConditionsBooklet(_cr, billInfo);
            Tender tender = new Tender();
            Supplier supplier = new Supplier(_cr, _cr, new List<UserNotificationSetting>());

            var invitation = new Invitation(_cr, Enums.InvitationStatus.Approved, Enums.InvitationRequestType.Invitation, true);
            tender.SetOfferPresenationDate_ForTest();
            tender.SetTenderName_ForTest("test");
            conditionbooklet.Test_AddTender(tender);
            conditionbooklet.Test_AddSupplier(supplier);
            invitation.Test_AddTender(tender);
            billInfo.Test_AddInvitation(invitation);
            billInfo.Test_AddBooklet(conditionbooklet);
            return billInfo;
        }


        public BillInfo GetBillInfoDataForInvitation()
        {
            BillInfoModel billInfoModel = new BillInfoModel()
            {
                AmountDue = _amountDue,
                DueDate = _dueDt,
                ExpDate = _expiryDate,
                AgencyCode = _agencyCode,
                ActionStatus = _actionStatus,
                ChapterNumber = _chapterNumber,
                NumOfSubSections = _sectionNumber,
                SequenceNumber = _sequanceNumber,
                NumOfSubDepartments = _subdepartmentNumber,
                SectionId = _subsectionsNumber,
                BankBranchId = _bankbranch,
                BillInvoiceNumber = _billInvoicenumber,
                BillStatusId = _billStatusId,
                InvitationId = _invitationid,
                BillReferenceInfo = _billreferenceInfo,
                CreatedBy = _createdBy,
                CreatedAt = _createdAt,
                PurchaseDate = _purchaseDate,
                
            };
            var invitation = new Invitation(_cr, Enums.InvitationStatus.Approved, Enums.InvitationRequestType.Invitation, true);
            var tender = new TenderDefault().GetGeneralTender();
            invitation.Test_AddTender(tender);
            PropertyInfo supplierProp = invitation.GetType().GetProperty("Supplier");
            Supplier sub = new Supplier(_cr, "Supplier Name", new List<UserNotificationSetting>());
            supplierProp.SetValue(invitation,sub);
            var billInfo = new BillInfo(billInfoModel);

            billInfo.Test_AddInvitation(invitation);
            return billInfo;
        }

    }
}
