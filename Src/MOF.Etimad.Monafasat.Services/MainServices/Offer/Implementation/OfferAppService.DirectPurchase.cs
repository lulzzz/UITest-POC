using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.QuantityTableTemplates;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.Services
{
    public partial class OfferAppService
    {

        #region Checking Stage Direct Purchase

        public async Task<QuantitiesTemplateModel> GetQuantityTablesForDirectPurchase(int tenderId, int offerId)
        {
            QuantitiesTemplateModel lst = new QuantitiesTemplateModel();
            var Qitems = await _offerQueries.GetSupplierQTableItemsForDirectPurchase(offerId);
            lst.QuantitiesItems = Qitems;
            return lst;
        }

        public async Task SaveDirectPurchaseOneFileCheckingDataAndAttachemntsDetial(OfferCheckingContainer container)
        {
            var localContentSetting = await _offerQueries.GetLocalContentSettingsDate();
            OfferDetailModel model = container.model;
            CheckOfferResultModel checkingResult = container.checkingResult;
            SupplierCombinedDetail details = await _offerQueries.GetCombinedOfferDetail(Util.Decrypt(model.CombinedIdString));
            if (details == null)
            {
                details = new SupplierCombinedDetail
                {
                    Combined = await _offerQueries.GetCombinedbyId(Util.Decrypt(model.CombinedIdString))
                };
            }
            await SaveDirectPurchaseAttachment(model, details/*, offer*/);
            await CalculateFinalPriceDirectPurchase(details.Combined.Offer);
            if (details.Combined.Offer.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersChecking)
            {
                details.Combined.Offer.Tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking);
            }
            details.Combined.Offer.UpdateOfferFinantialCheckingStatus(checkingResult.FinantialOfferStatusId, checkingResult.FinantialRejectionReason, checkingResult.FinantialNotes);
            details.Combined.Offer.UpdateOfferTecknicalCheckingStatus(checkingResult.TechnicalOfferStatusId, checkingResult.RejectionReason, checkingResult.Notes);
            if (details.Combined.Offer.Tender.IsValidToApplyOfferLocalContentChanges(localContentSetting.Date.Value) && details.Combined.Offer.OfferlocalContentDetails != null)
            {
                details.Combined.Offer.OfferlocalContentDetails.SetIsBindedToMandatoryList(checkingResult.IsBindedToMandatoryList);
            }
            if (checkingResult.TechniciansReportAttachments != null)
            {
                List<TechnicianReportAttachment> technicianReportAttachments = _mapper.Map<List<TechnicianReportAttachment>>(checkingResult.TechniciansReportAttachments);
                details.Combined.Offer.UpdateTechnicianReportAttachments(technicianReportAttachments);
            }
            await _offerCommands.UpdateCombinedDetailAsync(details);
        }

        public async Task<SupplierCombinedDetail> SaveDirectPurchaseAttachmentsChecking(OfferDetailModel m)
        {
            SupplierCombinedDetail details = await _offerQueries.GetCombinedOfferDetail(Util.Decrypt(m.CombinedIdString));
            if (details == null)
            {
                details = new SupplierCombinedDetail
                {
                    Combined = await _offerQueries.GetCombinedbyId(Util.Decrypt(m.CombinedIdString))
                };
            }
            /*SupplierCombinedDetail */
            CheckIfUserCanAccessLowBudgetTender(details.Combined.Offer.Tender.IsLowBudgetDirectPurchase, details.Combined.Offer.Tender.DirectPurchaseMemberId, _httpContextAccessor.HttpContext.User.UserId());
            details = await SaveDirectPurchaseAttachment(m, details/*, offer*/);
            details = await _offerCommands.UpdateCombinedDetailAsync(details);
            return details;
        }

        public async Task SaveOneFileDirectPurchaseChecking(CheckOfferResultModel checkingResult)
        {
            var localContentSetting = await _offerQueries.GetLocalContentSettingsDate();
            Offer offer = await _offerQueries.GetOfferForDirectPurchaseChekingById(Util.Decrypt(checkingResult.OfferIdString));
            var tender = offer.Tender;
            CheckIfUserCanAccessLowBudgetTender(tender.IsLowBudgetDirectPurchase, tender.DirectPurchaseMemberId, _httpContextAccessor.HttpContext.User.UserId());
            if (offer.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersChecking)
            {
                offer.Tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking);
            }
            await CalculateFinalPriceDirectPurchase(offer);
            offer.UpdateOfferFinantialCheckingStatus(checkingResult.FinantialOfferStatusId, checkingResult.FinantialRejectionReason, checkingResult.FinantialNotes);
            offer.UpdateOfferTecknicalCheckingStatus(checkingResult.TechnicalOfferStatusId, checkingResult.RejectionReason, checkingResult.Notes);
            if (offer.Tender.IsValidToApplyOfferLocalContentChanges(localContentSetting.Date.Value) && offer.OfferlocalContentDetails != null)
            {
                offer.OfferlocalContentDetails.SetIsBindedToMandatoryList(checkingResult.IsBindedToMandatoryList);
            }
            if (checkingResult.TechniciansReportAttachments != null)
            {
                List<TechnicianReportAttachment> technicianReportAttachments = _mapper.Map<List<TechnicianReportAttachment>>(checkingResult.TechniciansReportAttachments);
                offer.UpdateTechnicianReportAttachments(technicianReportAttachments);
            }
            await _offerCommands.UpdateAsync(offer);
        }

        public async Task SaveTwoFileTechnicalCheckingDataAndAttachemntsDetial(OfferCheckingContainer container)
        {
            OfferDetailModel model = container.model;
            CheckOfferResultModel checkingResult = container.checkingResult;
            SupplierCombinedDetail details = await _offerQueries.GetCombinedOfferDetail(Util.Decrypt(model.CombinedIdString));
            if (details == null)
            {
                details = new SupplierCombinedDetail
                {
                    Combined = await _offerQueries.GetCombinedbyId(Util.Decrypt(model.CombinedIdString))
                };
            }
            await SaveDirectPurchaseAttachment(model, details/*, offer*/);
            if (details.Combined.Offer.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersChecking)
            {
                details.Combined.Offer.Tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking);
            }
            details.Combined.Offer.UpdateOfferTecknicalCheckingStatus(checkingResult.TechnicalOfferStatusId, checkingResult.RejectionReason, checkingResult.Notes);
            if (checkingResult.TechniciansReportAttachments != null)
            {
                List<TechnicianReportAttachment> technicianReportAttachments = _mapper.Map<List<TechnicianReportAttachment>>(checkingResult.TechniciansReportAttachments);
                details.Combined.Offer.UpdateTechnicianReportAttachments(technicianReportAttachments);
            }
            await _offerCommands.UpdateCombinedDetailAsync(details);
        }

        public async Task SaveTwoFileFinancialCheckingDataAndAttachemntsDetial(OfferCheckingContainer container)
        {
            var localContentSetting = await _offerQueries.GetLocalContentSettingsDate();
            OfferDetailModel model = container.model;
            CheckOfferResultModel checkingResult = container.checkingResult;
            SupplierCombinedDetail details = await _offerQueries.GetCombinedOfferDetail(Util.Decrypt(model.CombinedIdString));
            if (details == null)
            {
                details = new SupplierCombinedDetail
                {
                    Combined = await _offerQueries.GetCombinedbyId(Util.Decrypt(model.CombinedIdString))
                };
            }
            await SaveDirectPurchaseAttachment(model, details/*, offer*/);
            await CalculateFinalPriceDirectPurchase(details.Combined.Offer);
            if (details.Combined.Offer.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersFinancialChecking)
            {
                details.Combined.Offer.Tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialChecking);
            }
            details.Combined.Offer.UpdateOfferFinantialCheckingStatus(checkingResult.FinantialOfferStatusId, checkingResult.FinantialRejectionReason, checkingResult.FinantialNotes);
            if (details.Combined.Offer.Tender.IsValidToApplyOfferLocalContentChanges(localContentSetting.Date.Value) && details.Combined.Offer.OfferlocalContentDetails != null)
            {
                details.Combined.Offer.OfferlocalContentDetails.SetIsBindedToMandatoryList(checkingResult.IsBindedToMandatoryList);
            }
            await _offerCommands.UpdateCombinedDetailAsync(details);
        }

        public async Task SaveTwoFilesTechnicalDirectPurchaseChecking(CheckOfferResultModel checkingResult)
        {
            Offer offer = await _offerQueries.GetOfferForDirectPurchaseChekingById(Util.Decrypt(checkingResult.OfferIdString));

            if (offer.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersChecking)
            {
                offer.Tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking);
            }
            offer.UpdateOfferTecknicalCheckingStatus(checkingResult.TechnicalOfferStatusId, checkingResult.RejectionReason, checkingResult.Notes);
            if (checkingResult.TechniciansReportAttachments != null)
            {
                List<TechnicianReportAttachment> technicianReportAttachments = _mapper.Map<List<TechnicianReportAttachment>>(checkingResult.TechniciansReportAttachments);
                offer.UpdateTechnicianReportAttachments(technicianReportAttachments);
            }

            await _offerCommands.UpdateAsync(offer);
        }

        public async Task SaveTwoFilesFinancialDirectPurchaseChecking(CheckOfferResultModel checkingResult)
        {
            var localContentSetting = await _offerQueries.GetLocalContentSettingsDate();
            Offer offer = await _offerQueries.GetOfferForDirectPurchaseChekingById(Util.Decrypt(checkingResult.OfferIdString));

            if (offer.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersFinancialChecking)
            {
                offer.Tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialChecking);
            }

            await CalculateFinalPriceDirectPurchase(offer);
            offer.UpdateOfferFinantialCheckingStatus(checkingResult.FinantialOfferStatusId, checkingResult.FinantialRejectionReason, checkingResult.FinantialNotes);
            if (offer.Tender.IsValidToApplyOfferLocalContentChanges(localContentSetting.Date.Value) && offer.OfferlocalContentDetails != null)
            {
                offer.OfferlocalContentDetails.SetIsBindedToMandatoryList(checkingResult.IsBindedToMandatoryList);
            }
            await _offerCommands.UpdateAsync(offer);
        }

        private async Task CalculateFinalPriceDirectPurchase(Offer offer)
        {
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                SubmitAction = "",
                TenderId = offer.TenderId,
                HasAlternative = offer.Tender.HasAlternativeOffer.HasValue && offer.Tender.HasAlternativeOffer.Value,
                CanEditAlternative = offer.Tender.HasAlternativeOffer.HasValue && offer.Tender.HasAlternativeOffer.Value,
                AllowEdit = false,
                ApplySelected = true,
                ActivityId = 1,
                EncryptedOfferId = Util.Encrypt(offer.OfferId),
                EncryptedTenderId = Util.Encrypt(offer.TenderId),
                IsTenderContainsTawreed = offer.Tender.IsTenderContainsTawreedTables.HasValue && offer.Tender.IsTenderContainsTawreedTables.Value,
                QuantityItemDtos = offer.SupplierTenderQuantityTables.SelectMany(q => q.QuantitiyItemsJson.SupplierTenderQuantityTableItems).Select(qq => new TenderQuantityItemDTO
                {
                    ColumnId = qq.ColumnId,
                    ItemNumber = qq.ItemNumber,
                    ColumnName = "",
                    TableName = offer.SupplierTenderQuantityTables.FirstOrDefault(a => a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any(i => i.Id == qq.Id)).Name,
                    TableId = offer.SupplierTenderQuantityTables.FirstOrDefault(a => a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any(i => i.Id == qq.Id)).Id,
                    TemplateId = qq.ActivityTemplateId,
                    TenderId = offer.TenderId,
                    TenderFormHeaderId = qq.TenderFormHeaderId,
                    Value = qq.Value,
                    AlternativeValue = qq.AlternativeValue,
                    IsDefault = qq.IsDefault,
                    Id = qq.Id,
                }).ToList(),
                NPpercentage = offer.Tender.NationalProductPercentage / 100 ?? 0,
                YearsCount = (offer.Tender.TemplateYears.HasValue ? offer.Tender.TemplateYears.Value : 0)
            };
            ApiResponse<TotalCostDTO> obj = await _qantityTemplatesProxy.GetTotalCostForChecking(DTOModel);
            if (obj.StatusCode == 200)
            {
                offer.UpdateFinalPriceAfterDiscount(obj.Data.TotalCostWithdiscount);
            }
            else
            {
                throw new BusinessRuleException("حدث خطأ اثناء حساب قيمة العرض");
            }
            if (DTOModel.IsTenderContainsTawreed ||
                offer.Tender.TenderActivities.Any(a => a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)Enums.ConditionsTemplateCategory.GeneralSuppliesSupply || t.TemplateId == (int)Enums.ConditionsTemplateCategory.GeneralSupply)))
            {
                ApiResponse<TotalCostDTO> npObj = await _qantityTemplatesProxy.GetSupplierTotalCostNP(DTOModel);
                offer.UpdateOfferWeightAfterCalcNPA(npObj.Data?.TotalCostNP);
            }
            else
            {
                if (obj.StatusCode == 200)
                {
                    offer.UpdateOfferWeightAfterCalcNPA(obj.Data?.TotalCostWithdiscount);
                }
            }
        }

        private async Task<SupplierCombinedDetail> SaveDirectPurchaseAttachment(OfferDetailModel model, SupplierCombinedDetail details/*, Offer offer*/)
        {
            var statuses = GetOfferDirectPurchaseStatuses(details.Combined.Offer);
            bool isOneFile = statuses.Item1, isTechnical = statuses.Item2, isFinancial = statuses.Item3;
            List<SupplierSpecificationDetail> SpecificationsLst = new List<SupplierSpecificationDetail>();
            if ((isOneFile || isTechnical) && model.CombinedOwner)
            {
                details.Combined.Offer.UpdateOfferAttachments(model.IsOfferCopyAttached,
                model.IsOfferLetterAttached,
                model.OfferLetterNumber,
                model.OfferLetterDate,
                model.IsVisitationAttached,
                model.IsPurchaseBillAttached,
                model.IsBankGuaranteeAttached);
            }
            if ((isOneFile || isFinancial) && model.CombinedOwner)
            {
                List<SupplierBankGuaranteeDetail> GuaranteesLst = new List<SupplierBankGuaranteeDetail>();
                foreach (var item in model.BankGuaranteeFiles)
                {
                    var BankGuarantee = new SupplierBankGuaranteeDetail(item.BankGuaranteeId, details.Combined.Offer.OfferId, item.IsBankGuaranteeValid, item.GuaranteeNumber, item.BankId.Value, item.Amount, item.GuaranteeStartDate, item.GuaranteeEndDate, item.GuaranteeDays);
                    GuaranteesLst.Add(BankGuarantee);
                }
                details.Combined.Offer.UpdateBankGurnteesDetails(GuaranteesLst);
                details.Combined.Offer.UpdateOfferFinantialCheckingDetails(model.IsFinaintialOfferLetterAttached,
                model.FinantialOfferLetterNumber,
                model.FinantialOfferLetterDate,
                model.IsFinantialOfferLetterCopyAttached,
                model.IsBankGuaranteeAttached);
            }
            if (isOneFile || isTechnical)
            {
                foreach (var item in model.SpecificationsFiles)
                {
                    var Specification = new SupplierSpecificationDetail(item.SpecificationId, Util.Decrypt(model.CombinedIdString), item.IsForApplier, item.Degree, item.ConstructionWorkId, item.MaintenanceRunningWorkId);
                    SpecificationsLst.Add(Specification);
                }
                details.updateAttachmentsList(SpecificationsLst);
                details.UpdateAttachmentDetails(details.CombinedDetailId,
                    Util.Decrypt(model.CombinedIdString),
                    model.IsChamberJoiningAttached,
                    model.IsChamberJoiningValid,
                    model.IsCommercialRegisterAttached,
                    model.IsCommercialRegisterValid,
                    model.IsSaudizationAttached,
                    model.IsSaudizationValidDate,
                    model.IsSocialInsuranceAttached,
                    model.IsSocialInsuranceValidDate,
                    model.IsZakatAttached,
                    model.IsZakatValidDate,
                    model.IsSpecificationAttached,
                    model.IsSpecificationValidDate);

                if (details.Combined.Offer.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersChecking)
                {
                    details.Combined.Offer.Tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking);
                }
            }
            if (isFinancial && details.Combined.Offer.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersFinancialChecking)
            {
                details.Combined.Offer.Tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialChecking);
            }
            return details;
        }

        private (bool, bool, bool) GetOfferDirectPurchaseStatuses(Offer Model)
        {
            var isOneFile = Model.Tender.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.OneFile;

            var isTechnical = Model.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles &&
                    ((Model.Tender.TenderStatusId == (int)Enums.TenderStatus.DirectPurchaseOffersChecking ||
                    Model.Tender.TenderStatusId == (int)Enums.TenderStatus.DirectPurchaseOffersCheckingApprovePending ||
                    Model.Tender.TenderStatusId == (int)Enums.TenderStatus.DirectPurchaseOffersCheckingPending ||
                    Model.Tender.TenderStatusId == (int)Enums.TenderStatus.DirectPurchaseOffersCheckingRejected)
                    ||
                    (Model.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersChecking ||
                    Model.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedPending ||
                    Model.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersCheckedRejected ||
                    Model.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingPending ||
                    Model.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingRejected)
                    );

            var isFinancial = Model.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles &&
                        (Model.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking ||
                        Model.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed ||
                        Model.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingApproved ||
                        Model.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingPending ||
                        Model.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialOfferCheckingRejected ||
                        Model.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStage);

            return (isOneFile, isTechnical, isFinancial);
        }

        public async Task<OfferDetailsForCheckingModel> GetOfferDetailsForDirectPurchaseTenderChecking(int combinedId, int userId)
        {
            OfferSolidarity combined = await _offerQueries.FindOfferByCombinedSupplierId(combinedId);
            Tender tender = await _tenderQueries.FindTenderOfferDetailsByTenderIdForDirectPurchaseCheckStage(combined.Offer.TenderId, userId);
            var result = await _offerQueries.GetOfferDetailsForDirectPurchaseChecking(combinedId);
            if (result == null)
            {
                result = new OfferDetailsForCheckingModel
                {
                    TenderID = tender.TenderId,
                    TenderStatusId = tender.TenderStatusId,
                    CombinedId = combinedId,
                    OfferPresentationWayId = tender.OfferPresentationWayId.Value,
                    TenderIdString = Util.Encrypt(tender.TenderId),
                    OfferIdString = Util.Encrypt(combined.OfferId)
                };
                result.TenderStatusId = tender.TenderStatusId;
            }
            result.SupplierName = (combined.Supplier.SelectedCrName);
            result.IsSupplierCombinedLeader = combined.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader;
            result.CheckingTwoFilesModel = new OfferDetailsForCheckingTwoFilesModel
            {
                OfferIdString = result.OfferIdString,
                TechnicalOfferStatusId = result.TechnicalOfferStatusId,
                RejectionReason = result.RejectionReason,
                Notes = result.Notes,
                TenderStatusId = result.TenderStatusId,
                TechnicalOfferStatusString = result.TechnicalOfferStatusString
            };
            return result;
        }

        public async Task<OfferDetailsForCheckingModel> GetOfferDetailsForDirectPurchaseTenderFinancialChecking(int offerId, int userId)
        {
            var offer = await _offerQueries.GetOfferDetailsForDirectPurchaseCheckingByOfferId(offerId);
            return offer;
        }

        #endregion Checking Stage Direct Purchase
    }
}
