using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class BillAppService : IBillAppService
    {
        #region Private Members
        private readonly INotificationAppService _notificationAppService;
        private readonly IIDMAppService _idmAppService;
        private readonly IBillCommand _billCommands;
        private readonly IBillArchiveCommand _billArchiveCommands;

        private readonly IBillQueries _billQueries;
        private readonly IBillingProxy _billProxy; private readonly string _billAgencyJebayaIdentifierForSaddad; 
        private readonly string _billRefIdForBillingForSaddad;
        private readonly string _billAgencyCodeForSaddad;
        private readonly string _clientKey;
        private readonly string _benAgencyCodeForInvitations;
        private readonly string _benAgencyCodeForConditionsBooklet;
        private readonly string _gFSCODEForConditionalBooklet;
        private readonly string _gFSCODEForInvitations;
        private readonly string _gFSCODEForAddedValue;
        private readonly ILogger<BillAppService> _logger;
        #endregion

        #region Constructor
        public BillAppService(IOptionsSnapshot<RootConfigurations> rootConfiguration, IBillingProxy billingProxy, INotificationAppService notificationAppService, IBillCommand billCommands, IBillQueries billQueries, IIDMAppService idmAppService, ILogger<BillAppService> logger, IBillArchiveCommand billArchiveCommands)
        {
            _billProxy = billingProxy;
            _billCommands = billCommands;
            _billArchiveCommands = billArchiveCommands;
            _billQueries = billQueries;
            _notificationAppService = notificationAppService;
            _idmAppService = idmAppService;
            RootConfigurations _rootConfiguration = rootConfiguration.Value;
            _billRefIdForBillingForSaddad = _rootConfiguration.EsbSettingsConfiguration.BillRefIdForBillingForSaddad;
            _billAgencyCodeForSaddad = _rootConfiguration.EsbSettingsConfiguration.BillAgencyCodeForSaddad;
            _billAgencyJebayaIdentifierForSaddad = _rootConfiguration.EsbSettingsConfiguration.BillAgencyCodeentifierForSaddad;
            _clientKey = _rootConfiguration.BillingConfiguration.ClientKey;
            _benAgencyCodeForInvitations = _rootConfiguration.EsbSettingsConfiguration.BenAgencyCodeForInvitations;
            _benAgencyCodeForConditionsBooklet = _rootConfiguration.EsbSettingsConfiguration.BenAgencyCodeForConditionsBooklet;
            _gFSCODEForConditionalBooklet = _rootConfiguration.BillingConfiguration.GFSCODEForConditionalBooklet;
            _gFSCODEForInvitations = _rootConfiguration.BillingConfiguration.GFSCODEForInvitations;
            _gFSCODEForAddedValue = _rootConfiguration.BillingConfiguration.GFSCODEForAddedValue;
            _logger = logger;
        }
        #endregion
        #region Private Utilities 
        private void VerifyPaymentNotification(PaymentNotificationServiceModel paymentNotificationModel)
        {
            if (string.IsNullOrEmpty(paymentNotificationModel.BillAccount))
            {
                throw new InvalidDataException("Empty or incorrect BillAccount: " + paymentNotificationModel.BillAccount);
            }
            else if (string.IsNullOrEmpty(paymentNotificationModel.AgencyCode))
            {
                throw new InvalidDataException("Empty or incorrect AgencyCode: " + paymentNotificationModel.AgencyCode);
            }
            else if (string.IsNullOrEmpty(paymentNotificationModel.IntermediatePaymentId)
                || paymentNotificationModel.PaymentMethodDetail == null
                || string.IsNullOrEmpty(paymentNotificationModel.PaymentStatusCode)
                || paymentNotificationModel.PaymentDate == DateTime.MinValue || !paymentNotificationModel.PaymentDate.HasValue
                || string.IsNullOrEmpty(paymentNotificationModel.PaymentMethod)
                || string.IsNullOrEmpty(paymentNotificationModel.BillAccount)
                || paymentNotificationModel.PaymentAmount == 0 || !paymentNotificationModel.PaymentAmount.HasValue)
            {
                throw new NullReferenceException("missing some required data");
            }
        }
        #endregion
        #region Store Bills And Upload To Sadad
        public async Task<bool> StoreBillsAndUploadToSadad(BillModel billInfoModel, BillInfo billInfo, string cr, int agencyType, bool throughA4Sadad = true)
        {
            var supplierInfo = await GetSupplierInfoByCR(cr);
            var billModel = new BillModel
            {
                AmountDue = billInfoModel.AmountDue,
                BillInvoiceNumber = "",
                DueDate = billInfoModel.DueDate,
                ExpDate = billInfoModel.ExpDate,
                AgencyCode = billInfoModel.AgencyCode,
                DisplayLabelAr = supplierInfo != null ? supplierInfo.supplierName : "",
                DisplayLabelEn = supplierInfo != null ? supplierInfo.CRNameEN : "",
                AgencyType = agencyType,
                RequestType = billInfoModel.RequestType,
                TenderFees = billInfoModel.TenderFees
            };
            billModel = await UpdateAgencyDetailsRelatedToSadad(billModel);
            if (!throughA4Sadad)
                billInfo.AgencyCode = _billAgencyCodeForSaddad;

            if (billModel.AmountDue == 0)
            {
                billInfo.UpdateFreeTenderPurchaseDetails(billInfoModel.AmountDue, DateTime.Now);
                await _billCommands.UpdateBillAsync(billInfo);
                return true;
            }
            else
            {
                var result = await UploadThroughTahseel(billModel, billInfo);
                return result;
            }
        }

        public async Task<SupplierFullDataModel> GetSupplierInfoByCR(string CR)
        {
            return await _idmAppService.GetSupplierInfoByCR(CR);
        }

        private async Task<BillModel> UpdateAgencyDetailsRelatedToSadad(BillModel model)
        {

            var govAgencyInfo = await _idmAppService.GetAgencyDetailsRelatedToSadad(model.AgencyCode, model.AgencyType);
            if (govAgencyInfo != null)
            {
                model.ChapterNumber = !string.IsNullOrEmpty(govAgencyInfo.beneficiaryClassNo) ? govAgencyInfo.beneficiaryClassNo : "000";
                model.BankBranchId = !string.IsNullOrEmpty(govAgencyInfo.beneficiaryBranchNo) ? govAgencyInfo.beneficiaryBranchNo : "000";
                model.SectionId = !string.IsNullOrEmpty(govAgencyInfo?.beneficiarySectionNo) ? govAgencyInfo.beneficiarySectionNo : "000";
                model.SequenceNumber = !string.IsNullOrEmpty(govAgencyInfo.beneficiarySerialNo) ? govAgencyInfo.beneficiarySerialNo : "000";
                model.NumOfSubDepartments = !string.IsNullOrEmpty(govAgencyInfo.managementNoForBeneficiary) ? govAgencyInfo.managementNoForBeneficiary : "000";
                model.NumOfSubSections = !string.IsNullOrEmpty(govAgencyInfo.sectionNoForBeneficiary) ? govAgencyInfo.sectionNoForBeneficiary : "000";
            }
            return model;
        }

        #region Upload To Sadad through proxy
        private async Task<bool> UploadThroughTahseel(BillModel billModel, BillInfo billInfo)
        {
            NewBillModel objBill = new NewBillModel();
            decimal totalAmount = 0;
            List<RevenueEntryInfoType> revList = new List<RevenueEntryInfoType>();
            foreach (var item in billModel.TenderFees)
            {
                totalAmount += item.FeesValue;
                if (item.FessType == (int)Enums.TenderFeesType.FinantialCostForInvitation)
                {
                    revList.Add(
                          new RevenueEntryInfoType()
                          {
                              BenAgencyId = _benAgencyCodeForInvitations,
                              Amt = item.FeesValue,
                              GFSCode = _gFSCODEForInvitations
                          });
                }
                else if (item.FessType == (int)Enums.TenderFeesType.FinantialCostForConditionalBooklet)
                {
                    revList.Add(
                          new RevenueEntryInfoType()
                          {
                              BenAgencyId = _benAgencyCodeForConditionsBooklet,
                              Amt = item.FeesValue,
                              GFSCode = _gFSCODEForAddedValue
                          });
                }
                else if (item.FessType == (int)Enums.TenderFeesType.ConditionalBookletPrice)
                {
                    revList.Add(
                         new RevenueEntryInfoType()
                         {
                             BenAgencyId = billModel.ChapterNumber + billModel.BankBranchId + billModel.SectionId + billModel.SequenceNumber + billModel.NumOfSubDepartments + billModel.NumOfSubSections,
                             Amt = item.FeesValue,
                             GFSCode = _gFSCODEForConditionalBooklet
                         });
                }
            }
            objBill.RevList = revList;
            objBill.ClientKey = _clientKey;
            objBill.AmountDue = billModel.AmountDue;
            objBill.DueDate = billModel.DueDate;
            objBill.ExpDate = billModel.ExpDate;
            objBill.billRefIdForBillingForSaddad = _billRefIdForBillingForSaddad; objBill.AgencyJebayaCodeForSaddad = _billAgencyJebayaIdentifierForSaddad;
            objBill.SupplierNameAr = billModel.DisplayLabelAr;
            objBill.SupplierNameEn = billModel.DisplayLabelAr;
            var BillInvoiceNumber = await _billProxy.CreateNewBill(objBill);

            if (!string.IsNullOrEmpty(BillInvoiceNumber))
            {
                foreach (var item in billModel.TenderFees)
                {
                    billInfo.BillPaymentDetails.Add(new BillPaymentDetails(item.FeesValue, BillInvoiceNumber, item.GFSCode, item.FessType));
                }
                billInfo.UpdateBillInfo(BillInvoiceNumber, billModel.ChapterNumber, billModel.BankBranchId, billModel.NumOfSubSections, billModel.SequenceNumber, billModel.NumOfSubDepartments, billModel.NumOfSubSections, _billAgencyCodeForSaddad, billModel.DisplayLabelAr, billModel.DisplayLabelEn, billModel.BatchId);
                await _billCommands.UpdateBillAsync(billInfo);
                var tender = new Tender();
                string cr = "";
                string CrName = "";
                if (billInfo != null)
                {
                    if (billModel.RequestType == (int)Enums.RequestType.ConditionalBookletRequest)
                    {
                        tender = billInfo.ConditionsBooklet.Tender;
                        CrName = string.IsNullOrEmpty(billInfo.DisplayLabelAr) ? billInfo.DisplayLabelEn : billInfo.DisplayLabelAr;
                        cr = billInfo.ConditionsBooklet.CommericalRegisterNo;
                    }
                    else
                    {
                        tender = billInfo.Invitation.Tender;
                        CrName = string.IsNullOrEmpty(billInfo.DisplayLabelAr) ? billInfo.DisplayLabelEn : billInfo.DisplayLabelAr;
                        cr = billInfo.Invitation.CommericalRegisterNo;
                    }
                    NotificationArguments NotificationArgument = new NotificationArguments
                    {
                        BodyEmailArgs = new object[] { CrName, tender.TenderName, BillInvoiceNumber },
                        PanelArgs = new object[] { BillInvoiceNumber },
                        SMSArgs = new object[] { BillInvoiceNumber }
                    };
                    MainNotificationTemplateModel billStatusSuccessUploadedForSupplier = new MainNotificationTemplateModel(
                          NotificationArgument,
                            $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}",
                            NotificationEntityType.Tender,
                            tender.TenderId.ToString(),
                            tender.BranchId
                        );
                    await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.sadadgeneratedbill, new List<string> { cr }, billStatusSuccessUploadedForSupplier);
                }
            }
            else
            {
                if (billModel.RequestType == (int)Enums.RequestType.ConditionalBookletRequest)
                {
                    BillArchive billArchiveForCondtionalBooklet = new BillArchive(billInfo.ConditionsBookletID, billInfo.InvitationId, billInfo.ConditionsBooklet.TenderId, billInfo.ConditionsBooklet.Tender.ReferenceNumber, BillInvoiceNumber, billInfo.AgencyCode, billInfo.ConditionsBooklet.CommericalRegisterNo);
                    await _billArchiveCommands.CreateAsync(billArchiveForCondtionalBooklet);

                    billInfo.DeleteBooklet();
                    await _billCommands.DeleteBillAsync(billInfo);
                }
                else
                {
                    if (billInfo.ConditionsBookletID != null)
                    {
                        billInfo.DeleteBooklet();
                    }
                    BillArchive billArchive = new BillArchive(billInfo.ConditionsBookletID, billInfo.InvitationId, billInfo.Invitation.TenderId, billInfo.Invitation.Tender.ReferenceNumber, BillInvoiceNumber, billInfo.AgencyCode, billInfo.Invitation.CommericalRegisterNo);
                    await _billArchiveCommands.CreateWithoutSave(billArchive);
                    await _billCommands.DeleteWithoutSave(billInfo); billInfo.DeleteInvitation();
                    await _billCommands.Save();
                }
                throw new BusinessRuleException(Resources.TenderResources.Messages.IDmSadadDetailsNotExist);
            }
            return true;
        }

        #endregion
        #endregion
        #region Set Bill Paid tahseel Push to API  
        public async Task<string> SetBillPaid(PaymentNotificationServiceModel postedModel)
        {
            VerifyPaymentNotification(postedModel);

            var bill = await _billQueries.FindBulkBillByInvoiceNumberWithTenderInfoAsync(postedModel.BillAccount);
            var billnumbersUpdated = bill.BillInvoiceNumber;
            var CurrentBillStatus = bill.BillStatusId;
            try
            {
                Check.EntityNotNull(nameof(bill), bill, Resources.SharedResources.DisplayInputs.NotFound);
                if (bill.InvitationId != null)
                {
                    bill.Invitation.UpdateStatus((int)Enums.InvitationStatus.Approved, "");
                }
                bill.UpdatePaymentDetails(postedModel.IntermediatePaymentId, postedModel.PaymentMethodDetail?.SadadPaymentTranactionId, postedModel.PaymentMethodDetail.BankReversalTransactionID,
                              postedModel.PaymentStatusCode, postedModel.BillAccount, postedModel.PaymentAmount,
                              postedModel.PaymentDate,
                              postedModel.PaymentMethodDetail.BankId, postedModel.PaymentMethodDetail.PaymentChannel, postedModel.PaymentMethod, postedModel?.PaymentReferenceInfo,
                              postedModel.PaymentMethodDetail.BankPaymentID, postedModel.PayorPOIDetail.POINumber, postedModel.PayorPOIDetail.POIType);
                await _billCommands.UpdateBillAsync(bill);

                if (CurrentBillStatus != (int)Enums.BillStatus.Paid)
                {
                    if (bill.ConditionsBookletID != null)
                    {
                        var billobj = await _billQueries.FindBillByInvoiceNumberWithTender(postedModel.BillAccount);
                        var tender = billobj.ConditionsBooklet.Tender;

                        NotificationArguments NotificationArgument = new NotificationArguments
                        {
                            BodyEmailArgs = new object[] { bill.ConditionsBooklet.CommericalRegisterNo, tender.TenderName },
                            PanelArgs = new object[] { tender.TenderName },
                            SMSArgs = new object[] { tender.TenderName }
                        };
                        MainNotificationTemplateModel billStatusSuccessUploadedForSupplier = new MainNotificationTemplateModel(
                              NotificationArgument,
                                $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}",
                                NotificationEntityType.Tender,
                                tender.TenderId.ToString(),
                                tender.BranchId);
                        await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.sadadpaymentnotification, new List<string> { bill.ConditionsBooklet.CommericalRegisterNo }, billStatusSuccessUploadedForSupplier);

                    }
                    else
                    {
                        var tender = bill.Invitation.Tender;
                        NotificationArguments NotificationArgument = new NotificationArguments
                        {
                            BodyEmailArgs = new object[] { bill.Invitation.CommericalRegisterNo, tender.TenderName },
                            PanelArgs = new object[] { tender.TenderName },
                            SMSArgs = new object[] { tender.TenderName }
                        };
                        MainNotificationTemplateModel billStatusSuccessUploadedForSupplier = new MainNotificationTemplateModel(
                              NotificationArgument,
                                $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}",
                                NotificationEntityType.Tender,
                                tender.TenderId.ToString(),
                                tender.BranchId
                            );
                        await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.sadadpaymentnotification, new List<string> { bill.Invitation.CommericalRegisterNo }, billStatusSuccessUploadedForSupplier);
                    }
                }


                return billnumbersUpdated;
            }
            catch (Exception ex)
            {
                _logger.LogError("ex", ex.Message.ToString());
                throw;
            }
        }
        public async Task<bool> UpdateBulkBillsStatus(List<BillModel> billModel)
        {
            try
            {
                foreach (var item in billModel)
                {
                    var bill = await _billQueries.FindBillByInvoiceNumberAndAgencyCode(item.BillInvoiceNumber, item.AgencyCode);
                    bill.UpdateActionStatus((BillActionStatus)item.ActionStatus);
                    if (item.ActionStatus == (int)Enums.BillActionStatus.CancelBill)
                    {
                        bill.UpdateActionReason(item.ActionReason);
                    }
                    await _billCommands.UpdateWithoutSave(bill);
                }
                await _billCommands.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> SetBillPaidForRolloutTeam(BillViewModel postedModel)
        {
            var bill = await _billQueries.FindBillByInvoiceNumberWithoutIncludesForRolloutTeam(postedModel.BillInvoiceNumber);
            try
            {
                if (bill != null)
                {
                    bill.UpdateBillStatusForRollOutTeam();
                    await _billCommands.UpdateBillAsync(bill);
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {
                _logger.LogError("ex", ex.Message.ToString());
                return false;
            }
        }
        #endregion
    }
}
