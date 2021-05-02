using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.SearchCriterias;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Invitation;
using MOF.Etimad.Monafasat.ViewModel.Offer;
using MOF.Etimad.Monafasat.ViewModel.Offer.OpenOfferStage;
using MOF.Etimad.Monafasat.ViewModel.QuantityTableTemplates;
using MOF.Etimad.Monafasat.ViewModel.Reports;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public partial class OfferAppService : IOfferAppService
    {
        private readonly ISupplierQueries _supplierQueries;
        private readonly ISupplierCommands _supplierCommands;
        private readonly IQuantityTemplatesProxy _qantityTemplatesProxy;
        private readonly ILocalContentProxy _localContentProxy;
        private readonly ISMEASizeInquiryProxy _sMEASizeInqueryProxy;
        private readonly IIDMAppService _idmAppService;
        private readonly RootConfigurations _configuration;
        private readonly IBlockAppService _blockAppService;
        private readonly IOfferQueries _offerQueries;
        private readonly IOfferCommands _offerCommands;
        private readonly ITenderQueries _tenderQueries;
        private readonly ITenderAppService _tenderAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ITenderCommands _tenderCommands;
        private readonly IMapper _mapper;
        private readonly INotificationAppService _notificationAppService;
        private readonly IOfferDomainService _offerDomainService;
        private readonly ILocalContentConfigurationSettings _localContentConfigurationSettings;
        private readonly ILocalContentPreferenceService _localContentPreferenceService;
        public OfferAppService(IOfferQueries offerQueries, IOfferCommands offerCommands, IOfferDomainService offerDomainService, IMapper mapper, INotificationAppService notificationAppService, ITenderCommands tenderCommands,
                                ITenderQueries tenderQueries, ITenderAppService tenderAppService, IHttpContextAccessor httpContextAccessor, ISupplierQueries supplierQueries, ISupplierCommands supplierCommands,
                                IIDMAppService iDMAppService, IQuantityTemplatesProxy quantityTemplatesProxy, IBlockAppService blockAppService, IOptionsSnapshot<RootConfigurations> optionsSnapshot
            , ILocalContentProxy localContentProxy, ISMEASizeInquiryProxy sMEASizeInqueryProxy, ILocalContentConfigurationSettings localContentConfigurationSettings, ILocalContentPreferenceService localContentPreferenceService)
        {
            _notificationAppService = notificationAppService;
            _offerQueries = offerQueries;
            _offerCommands = offerCommands;
            _offerDomainService = offerDomainService;
            _tenderCommands = tenderCommands;
            _tenderQueries = tenderQueries;
            _tenderAppService = tenderAppService;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
            _supplierQueries = supplierQueries;
            _supplierCommands = supplierCommands;
            _idmAppService = iDMAppService;
            _blockAppService = blockAppService;
            _qantityTemplatesProxy = quantityTemplatesProxy;
            _configuration = optionsSnapshot.Value;
            _localContentProxy = localContentProxy;
            _sMEASizeInqueryProxy = sMEASizeInqueryProxy;
            _localContentConfigurationSettings = localContentConfigurationSettings;
            _localContentPreferenceService = localContentPreferenceService;
        }

        #region Service Query

        public async Task<QueryResult<Offer>> Find(OfferSearchCriteria criteria)
        {
            return await _offerQueries.FindOffers(criteria);
        }

        public async Task<OpenOfferModel> OpenOffersDetailsForAwarding(int offerid, int userId)
        {
            OpenOfferModel OpenOfferModel = await _offerQueries.OpenOffersDetailsForAwarding(offerid);
            if (OpenOfferModel == null)
            {
                throw new UnHandledAccessException();
            }
            return OpenOfferModel;
        }

        public async Task<Offer> FindById(int offerId)
        {
            return await _offerQueries.FindOfferById(offerId);
        }

        public async Task<OfferCheckingAttachmentsDetailsModel> FindOfferDetailsForCheckById(int offerId)
        {
            OfferCheckingAttachmentsDetailsModel res = await _offerQueries.FindOfferDetailsForCheckById(offerId);
            return res;
        }

        public async Task<OfferCheckingDetailsModel> FindOfferDetailsById(int offerId)
        {
            return await _offerQueries.FindOfferDetailsById(offerId);
        }

        public async Task<QueryResult<AppliedSuppliersReportModel>> FindSuppliersReportByTenderId(int tenderId, int branchId, string AgencyCode, int committeeId, Enums.CommitteeType committeeType)
        {
            QueryResult<AppliedSuppliersReportModel> basicTenderModelList = await _offerQueries.FindSuppliersReportByTenderId(tenderId, branchId, AgencyCode, committeeId, committeeType);
            if (!basicTenderModelList.Items.Any() || basicTenderModelList.Items.Any(c => c.BranchId != branchId))
            {
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            }
            return basicTenderModelList;
        }

        public async Task<QueryResult<AppliedSuppliersReportDetailsModel>> FindSuppliersReportByTenderId__(int tenderId, int pageNumber)
        {
            var basicTenderModelList = await _offerQueries.FindSuppliersReportByTenderId__(tenderId, pageNumber);
            return basicTenderModelList;
        }
        public async Task<List<AppliedSuppliersReportDetailsModel>> ExportAppliedSuppliersReport(int tenderId)
        {
            var basicTenderModelList = await _offerQueries.ExportAppliedSuppliersReport(tenderId);
            return basicTenderModelList;
        }

        public async Task<CombinedSupplierModel> GetTenderOfferIDsByOfferID(int offerId)
        {
            return await _offerQueries.GetTenderOfferIDsByOfferID(offerId);
        }

        #endregion

        #region  Service Command

        public async Task<Offer> AddOffer(Offer offer)
        {
            Check.ArgumentNotNull(nameof(offer), offer);
            Tender tender = await _offerQueries.CheckOfferAdding(offer.TenderId, offer.CommericalRegisterNo);
            if (tender == null)
            {
                throw new UnHandledAccessException();
            }
            await _offerDomainService.IsValidToCreate(offer);
            offer.UpdateStatus(OfferStatus.UnderEstablishing);
            await _offerCommands.CreateAsync(offer);
            return offer;
        }

        public async Task<Offer> SendOffer(int offerId, string cr)
        {
            var offer = await _offerQueries.GetOfferWithTenderDetailsById(offerId);
            var tender = await _tenderQueries.FindTenderWithOffer(offer.TenderId);
            _offerDomainService.IsValidToSend(offer, tender);
            offer.UpdateStatus(Enums.OfferStatus.Received);
            offer.AddActionToOfferHistory(tender.TenderStatusId, offer.OfferStatusId, TenderActions.SubmitOffer, _httpContextAccessor.HttpContext.User.UserId(), cr);
            var localContentConfigSettings = await _localContentConfigurationSettings.getLocalContentSettingsDate();
            if (tender.IsValidToApplyOfferLocalContentChanges(localContentConfigSettings.Date.Value))
            {
                await UpdateCorporationData(offerId, cr, offer, tender);
            }
            await _offerCommands.UpdateAsync(offer);
            NotificationArguments acceptvendoroffer = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.TenderName, tender.TenderName, tender.TenderNumber, tender.Purpose, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersOpeningDate, tender.OffersOpeningDate?.Hour, "Monafasat" },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.TenderNumber },
                SMSArgs = new object[] { tender.TenderNumber }
            };
            MainNotificationTemplateModel templetModel = new MainNotificationTemplateModel(acceptvendoroffer,
                $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}",
                 NotificationEntityType.Tender,
                  tender.TenderId.ToString(),
               tender.BranchId);
            List<string> Crs = new List<string>();
            Crs.AddRange(offer.Combined.Select(c => c.CRNumber).ToList());
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.acceptvendoroffer, Crs, templetModel);
            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.EtimadOfficer.OperationsOnTheTender.vendorsubmitoffer, tender.BranchId, templetModel);
            else
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.vendorsubmitoffer, tender.BranchId, templetModel);
            return offer;
        }

        private async Task UpdateCorporationData(int offerId, string cr, Offer offer, Tender tender)
        {
            offer.OfferlocalContentDetails = await _offerQueries.GetOfferLocalContentDetailsByOfferId(offerId);
            if (offer.OfferlocalContentDetails == null)
            {
                offer.OfferlocalContentDetails = new OfferlocalContentDetails()
                {
                    OfferId = offerId
                };
                offer.OfferlocalContentDetails.CreateEntity();
            }
            var isSMEA = await _sMEASizeInqueryProxy.SMEASizeInquiry(cr);
            if (isSMEA.isSMEA)
            {
                offer.OfferlocalContentDetails.SetCorporationSizeSmallOrMedium();
            }
            else
            {
                offer.OfferlocalContentDetails.SetCorporationSizeLarg();
            }
            var targetPlan = await _localContentProxy.GetTargetPlanScoreInquiry(cr, tender.TenderId.ToString());
            if (targetPlan.isSuccess)
            {
                if (targetPlan.content > offer.Tender.TenderLocalContent.MinimumPercentageLocalContentTarget)
                {
                    offer.OfferlocalContentDetails.SetIsSupplierBindedToTheLowestLocalContent(true);
                }
                else
                {
                    offer.OfferlocalContentDetails.SetIsSupplierBindedToTheLowestLocalContent(false);
                }
                offer.OfferlocalContentDetails.SetLocalContentDesiredPercentage(targetPlan.content);
            }
            var baseline = await _localContentProxy.GetBaselineScoreInquiry(cr);
            if (baseline.isSuccess)
            {
                if (baseline.content > offer.Tender.TenderLocalContent.MinimumBaseline)
                {
                    offer.OfferlocalContentDetails.SetIsSupplierBindedToBaseLine(true);
                }
                else
                {
                    offer.OfferlocalContentDetails.SetIsSupplierBindedToBaseLine(false);
                }
                offer.OfferlocalContentDetails.SetLocalContentBaseLinePercentage(baseline.content);

            }

            var supplierData = await _offerQueries.GetSupplierFromMonayMarketWithCr(offer.CommericalRegisterNo);
            if (supplierData == null)
                offer.OfferlocalContentDetails.SetIsIncludedInMoneyMarket(false);
            else
                offer.OfferlocalContentDetails.SetIsIncludedInMoneyMarket(true);

            offer.OfferlocalContentDetails.AddInitialDataAtOfferApply();
        }

        public async Task<OfferSummaryStatusModel> GetOfferStatusForOfferSummary(int offerId, string CR)
        {
            var OfferSummaryStatusModel = await _offerQueries.FindOfferSummaryStatusModelById(offerId, CR);
            if (OfferSummaryStatusModel == null)
            {
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);
            }
            if (OfferSummaryStatusModel.StatusId == (int)Enums.OfferStatus.Received && OfferSummaryStatusModel.CommercialNumber == CR)
            {
                throw new BusinessRuleException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);
            }
            if (OfferSummaryStatusModel.StatusId == (int)Enums.OfferStatus.Canceled)
            {
                throw new BusinessRuleException("تم سحب العرض");
            }
            Check.ArgumentNotNull(nameof(OfferSummaryStatusModel), OfferSummaryStatusModel);
            return OfferSummaryStatusModel;
        }

        public async Task<OfferSummaryStatusModel> SendOfferToApprove(int offerId, string cr)
        {
            OfferSummaryStatusModel offerSummaryStatus = new OfferSummaryStatusModel();
            var offer = await _offerQueries.GetOfferWithTenderDetailsById(offerId);
            offerSummaryStatus.OfferStringId = Util.Encrypt(offerId);
            var tender = await _tenderQueries.FindTenderWithOffer(offer.TenderId);
            bool hasQuantityTable = tender.TenderTypeId != (int)Enums.TenderType.ReverseBidding && tender.TenderTypeId != (int)Enums.TenderType.FirstStageTender && tender.TenderTypeId != (int)Enums.TenderType.Competition;
            decimal amountWithDiscount = 0;
            decimal amountWithoutDiscount = 0;



            bool silidarityCheck = true;
            List<string> OtherCRs = new List<string>();

            var tenderSolidarities = await _offerQueries.GetOtherOfferSolidarity(offer.OfferId, offer.TenderId);
            var tenderofferCRs = tender.Offers.Where(e => e.IsActive == true && e.OfferStatusId == (int)Enums.OfferStatus.Received && e.OfferId != offer.OfferId).Select(d => d.CommericalRegisterNo).ToList();
            if (tenderofferCRs != null && tenderofferCRs.Any())
                OtherCRs.AddRange(tenderofferCRs);
            if (tenderSolidarities != null && tenderSolidarities.Any())
                OtherCRs.AddRange(tenderSolidarities.Select(d => d.CRNumber).ToList());
            var currentofferSolidirtyCRs = offer.Combined.Select(d => d.CRNumber);
            silidarityCheck = (OtherCRs.Any(f => f == offer.CommericalRegisterNo) && OtherCRs.Any() && OtherCRs != null) || (currentofferSolidirtyCRs.Any() && currentofferSolidirtyCRs != null && OtherCRs.Any(r => currentofferSolidirtyCRs.Any(f => f == r)));
            if (silidarityCheck)
            {
                offerSummaryStatus.ValidationSummary.Add("لقد تم إستلام عرض مسبق من احد اعضاء التضامن او ( احد اعضاء التضامن او مقدم العرض نفسة تضامن فى عرض اخر على نفس المنافسة )");
            }
            CheckAttachmentValidation(offerSummaryStatus, offer, tender);
            if (hasQuantityTable)
            {
                var Qitems = await _offerQueries.GetSupplierQTableItems(offerId, tender.TenderId);
                FormConfigurationDTO DTOModel = new FormConfigurationDTO()
                {
                    TenderId = offerId,
                    ActivityId = 1,
                    QuantityItemDtos = Qitems,
                    YearsCount = tender.TemplateYears ?? 0
                };
                ApiResponse<TotalCostDTO> obj = await _qantityTemplatesProxy.CalculateOfferFinalPricebyItems(DTOModel);
                if (obj.StatusCode != 200)
                {
                    throw new BusinessRuleException("خطأ فى جلب البيانات");
                }
                amountWithoutDiscount = obj.Data.TotalCostWithOutdiscount;
                amountWithDiscount = obj.Data.TotalCostWithdiscount;
                if (amountWithDiscount <= 0 && obj.Data.AlternativeTotalCostWithdiscount <= 0)
                {
                    offerSummaryStatus.ValidationSummary.Add("قيمة العرض الكلية لا يمكن ان تكون اصغر من أو تساوى  صفر ");
                }
                else if (amountWithDiscount > decimal.Parse("9999999999999999.98") || obj.Data.AlternativeTotalCostWithdiscount > decimal.Parse("9999999999999999.98"))
                {
                    offerSummaryStatus.ValidationSummary.Add("قيمة العرض الكلية كبيرة جدا   ");
                }
            }
            if (offer.IsSolidarity && (offer.Combined.Count == 1))
            {
                offerSummaryStatus.ValidationSummary.Add("العرض  بالتضامن ولا يوجد متضامنين");
            }
            offerSummaryStatus.StatusId = offer.OfferStatusId;
            offerSummaryStatus.StatusName = offer.Status.NameAr;
            if (offerSummaryStatus.ValidationSummary.Any())
            {
                return offerSummaryStatus;
            }
            _offerDomainService.IsValidToSend(offer, tender);
            var localContentConfigSettings = await _localContentConfigurationSettings.getLocalContentSettingsDate();
            if (tender.IsValidToApplyOfferLocalContentChanges(localContentConfigSettings.Date.Value))
            {
                await UpdateCorporationData(offerId, cr, offer, tender);
            }
            offer.UpdateStatus(Enums.OfferStatus.Received);
            offer.sendSolidarityInvitations(Enums.SupplierSolidarityStatus.ToBeSent);
            offer.UpdateFinalPriceAfterDiscount(amountWithDiscount);
            offer.UpdateFinalPriceBeforeDiscount(amountWithoutDiscount);
            offer.AddActionToOfferHistory(tender.TenderStatusId, offer.OfferStatusId, TenderActions.SubmitOffer, _httpContextAccessor.HttpContext.User.UserId(), cr);
            await _offerCommands.UpdateAsync(offer);
            #region [Send To Offer Owner]
            NotificationArguments acceptvendoroffer = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.TenderName, tender.TenderName, tender.TenderNumber, tender.Purpose, tender.LastEnqueriesDate, tender.LastOfferPresentationDate, tender.OffersOpeningDate, tender.OffersOpeningDate?.Hour, "Monafasat" },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { tender.TenderNumber },
                SMSArgs = new object[] { tender.TenderNumber }
            };
            MainNotificationTemplateModel templetModel = new MainNotificationTemplateModel(acceptvendoroffer,
                $"Tender/Details?STenderId={Util.Encrypt(tender.TenderId)}",
                 NotificationEntityType.Tender,
                  tender.TenderId.ToString(),
               tender.BranchId);
            List<string> Crs = new List<string>();
            Crs.AddRange(offer.Combined.Where(w => (w.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader)).Select(c => c.CRNumber).ToList());
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.acceptvendoroffer, Crs, templetModel);
            if (tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.EtimadOfficer.OperationsOnTheTender.vendorsubmitoffer, tender.BranchId, templetModel);
            else
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.vendorsubmitoffer, tender.BranchId, templetModel);
            #endregion
            #region [Send to Suppliers Solidarity]
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { _httpContextAccessor.HttpContext.User.SupplierName(), tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { _httpContextAccessor.HttpContext.User.SupplierName(), tender.ReferenceNumber },
                SMSArgs = new object[] { _httpContextAccessor.HttpContext.User.SupplierName(), tender.ReferenceNumber }
            };
            var RegisteredCRs = offer.Combined.Where(w => (w.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.Existed)).Select(c => c.CRNumber).ToList();
            MainNotificationTemplateModel inviteSupplierModel = new MainNotificationTemplateModel(NotificationArguments,
                $"Tender/DetailsForSupplier?STenderId={Util.Encrypt(offer.TenderId)}",
                NotificationEntityType.Offer, tender.TenderId.ToString());
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.Solidarity.SendSolidarityInvitation, RegisteredCRs, inviteSupplierModel);
            #endregion
            #region [Send To Unregistered]
            var UnregisteredSuppliers = offer.Combined.Where(w => w.SolidarityTypeId != (int)Enums.UnRegisteredSuppliersInvitationType.Existed && w.SolidarityTypeId != (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader).Select(c => c).ToList();
            foreach (var sup in UnregisteredSuppliers)
            {
                if (!string.IsNullOrEmpty(sup.Email))
                {
                    await _notificationAppService.SendSolidarityInvitationByEmail(new List<string> { sup.Email }, tender, _httpContextAccessor.HttpContext.User.SupplierName());
                }
                if (!string.IsNullOrEmpty(sup.Mobile))
                {
                    await _notificationAppService.SendSolidarityInvitationBySms(new List<string> { sup.Mobile }, tender, _httpContextAccessor.HttpContext.User.SupplierName());
                }
            }
            #endregion
            offer = await _offerQueries.FindOfferById(offerId);
            offerSummaryStatus.StatusId = offer.OfferStatusId;
            offerSummaryStatus.StatusName = offer.Status.NameAr;
            return offerSummaryStatus;
        }

        private static void CheckAttachmentValidation(OfferSummaryStatusModel offerSummaryStatus, Offer offer, Tender tender)
        {
            if (offer.IsSolidarity && !offer.Attachment.Any(w => w is SupplierCombinedAttachment))
            {
                offerSummaryStatus.ValidationSummary.Add(Resources.OfferResources.ErrorMessages.CombinationLetter);
            }
            if (DateTime.Now > tender.LastOfferPresentationDate)
            {
                offerSummaryStatus.ValidationSummary.Add(Resources.OfferResources.DisplayInputs.OfferPresentationDateEnded);
            }
            if ((tender.TenderTypeId == (int)Enums.TenderType.ReverseBidding || tender.TenderTypeId == (int)Enums.TenderType.FirstStageTender || tender.TenderTypeId == (int)Enums.TenderType.Competition) && !offer.Attachment.Any(d => d is SupplierTechnicalProposalAttachment))
            {
                offerSummaryStatus.ValidationSummary.Add(Resources.OfferResources.DisplayInputs.OfferAttachement);
            }
            if ((tender.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.OneFile
                && !(tender.TenderTypeId == (int)Enums.TenderType.ReverseBidding || tender.TenderTypeId == (int)Enums.TenderType.FirstStageTender || tender.TenderTypeId == (int)Enums.TenderType.Competition)
                && !offer.Attachment.Any(d => d is SupplierFinancialandTechnicalProposalAttachment))
                || (tender.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles && (!offer.Attachment.Any(d => d is SupplierFinancialProposalAttachment) || !offer.Attachment.Any(d => d is SupplierTechnicalProposalAttachment))))
            {
                offerSummaryStatus.ValidationSummary.Add(Resources.OfferResources.DisplayInputs.OfferAttachement);
            }
        }

        public async Task<SupplierCombinedDetail> AddOfferDetails(OfferDetailModel m)
        {
            SupplierCombinedDetail details = await _offerQueries.GetCombinedOfferDetail(Util.Decrypt(m.CombinedIdString));
            if (details == null)
            {
                details = new SupplierCombinedDetail
                {
                    Combined = await _offerQueries.GetCombinedbyId(Util.Decrypt(m.CombinedIdString))
                };
            }
            var offer = details.Combined.Offer;
            List<SupplierBankGuaranteeDetail> GuaranteesLst = new List<SupplierBankGuaranteeDetail>();
            List<SupplierSpecificationDetail> SpecificationsLst = new List<SupplierSpecificationDetail>();
            foreach (var item in m.SpecificationsFiles)
            {
                var Specification = new SupplierSpecificationDetail(item.SpecificationId, Util.Decrypt(m.CombinedIdString), item.IsForApplier, item.Degree, item.ConstructionWorkId, item.MaintenanceRunningWorkId);
                SpecificationsLst.Add(Specification);
            }
            if (details.Combined.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader)
            {
                foreach (var item in m.BankGuaranteeFiles)
                {
                    var BankGuarantee = new SupplierBankGuaranteeDetail(item.BankGuaranteeId, offer.OfferId, item.IsBankGuaranteeValid, item.GuaranteeNumber, item.BankId.Value, item.Amount, item.GuaranteeStartDate, item.GuaranteeEndDate, item.GuaranteeDays);
                    GuaranteesLst.Add(BankGuarantee);
                }
                offer.UpdateOfferAttachments(m.IsOfferCopyAttached, m.IsOfferLetterAttached, m.OfferLetterNumber, m.OfferLetterDate, m.IsVisitationAttached, m.IsPurchaseBillAttached, m.IsBankGuaranteeAttached);
                offer.updateBankGurnteeList(GuaranteesLst);
            }
            details.UpdateAttachmentDetails(details.CombinedDetailId, Util.Decrypt(m.CombinedIdString), m.IsChamberJoiningAttached, m.IsChamberJoiningValid,
                m.IsCommercialRegisterAttached, m.IsCommercialRegisterValid, m.IsSaudizationAttached, m.IsSaudizationValidDate, m.IsSocialInsuranceAttached,
                m.IsSocialInsuranceValidDate, m.IsZakatAttached, m.IsZakatValidDate, m.IsSpecificationAttached, m.IsSpecificationValidDate);
            details.updateAttachmentsList(SpecificationsLst);
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                SubmitAction = "",
                TenderId = offer.TenderId,
                HasAlternative = offer.Tender.HasAlternativeOffer ?? false,
                CanEditAlternative = offer.Tender.HasAlternativeOffer ?? false,
                AllowEdit = false,
                ActivityId = 1,
                EncryptedOfferId = Util.Encrypt(offer.OfferId),
                EncryptedTenderId = Util.Encrypt(offer.TenderId),
                QuantityItemDtos = offer.SupplierTenderQuantityTables.Where(c => c.QuantitiyItemsJson != null && c.IsActive == true).SelectMany(q => q.QuantitiyItemsJson.SupplierTenderQuantityTableItems).Select(qq => new TenderQuantityItemDTO
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
                    Id = qq.Id
                }).ToList(),
                YearsCount = offer.Tender.TemplateYears ?? 0
            };
            ApiResponse<TotalCostDTO> obj;
            obj = await _qantityTemplatesProxy.GetTotalCostForChecking(DTOModel);
            if (obj.StatusCode == 200)
            {
                offer.SetFinalPrice(obj.Data.TotalCostWithdiscount);
            }
            await _offerCommands.UpdateAsync(offer);
            details = await _offerCommands.UpdateCombinedDetailAsync(details);
            return details;
        }

        public async Task<Offer> SaveDiscountNotes(DiscountNotesModel m)
        {
            Offer offer = await _offerQueries.GetOfferByOfferId(Util.Decrypt(m.OfferIdString));
            offer.UpdateOpeningDiscountNotes(m.Discount, m.DiscountNotes);
            offer = await _offerCommands.UpdateAsync(offer);
            return offer;
        }

        public async Task<SupplierCombinedDetail> AddOfferAttachmentsDetails(OfferAttachmentsModel m, string cr)
        {
            Offer offer = await _offerQueries.GetOfferById(Util.Decrypt(m.OfferIDString));
            SupplierCombinedDetail details = await _offerQueries.GetCombinedOfferDetail(Util.Decrypt(m.CombinedIdString));
            if (details == null)
            {
                details = new SupplierCombinedDetail();
            }
            List<SupplierBankGuaranteeDetail> GuaranteesLst = new List<SupplierBankGuaranteeDetail>();
            List<SupplierSpecificationDetail> SpecificationsLst = new List<SupplierSpecificationDetail>();
            foreach (var item in m.BankGuaranteeFiles)
            {
                var BankGuarantee = new SupplierBankGuaranteeDetail(item.BankGuaranteeId, offer.OfferId, item.IsBankGuaranteeValid, item.GuaranteeNumber, item.BankId.Value, item.Amount, item.GuaranteeStartDate, item.GuaranteeEndDate, item.GuaranteeDays);
                GuaranteesLst.Add(BankGuarantee);
            }
            foreach (var item in m.SpecificationsFiles)
            {
                var Specification = new SupplierSpecificationDetail(item.SpecificationId, Util.Decrypt(m.CombinedIdString), item.IsForApplier, item.Degree, item.ConstructionWorkId, item.MaintenanceRunningWorkId);
                SpecificationsLst.Add(Specification);
            }
            offer.UpdateOfferAttachments(m.IsOfferCopyAttached, m.IsOfferLetterAttached, m.OfferLetterNumber, m.OfferLetterDate, m.IsVisitationAttached, m.IsPurchaseBillAttached, m.IsBankGuaranteeAttached);
            details.UpdateAttachmentDetails(details.CombinedDetailId, Util.Decrypt(m.CombinedIdString), m.IsChamberJoiningAttached, m.IsChamberJoiningValid,
                m.IsCommercialRegisterAttached, m.IsCommercialRegisterValid, m.IsSaudizationAttached, m.IsSaudizationValidDate, m.IsSocialInsuranceAttached,
                m.IsSocialInsuranceValidDate, m.IsZakatAttached, m.IsZakatValidDate, m.IsSpecificationAttached, m.IsSpecificationValidDate);
            details.updateAttachmentsList(SpecificationsLst);
            offer.UpdateBankGurnteesDetails(GuaranteesLst);
            await _offerCommands.UpdateAsync(offer);
            details = await _offerCommands.UpdateCombinedDetailAsync(details);
            return details;
        }

        public async Task<SupplierSpecificationAttachment> AddClassificationAttachmentsAsync(SupplierSpecificationAttachment Attachments)
        {
            var offer = await _offerQueries.FindOfferByIdWithAttaches(Attachments.OfferId);
            var tender = await _tenderQueries.FindTenderWithOffer(offer.TenderId);
            _offerDomainService.IsValidAndSent(offer, tender);
            offer.AddAttachment(Attachments);
            await _offerCommands.UpdateAsync(offer);
            return Attachments;
        }

        #region QuantityTable

        public async Task<Offer> ConfirmReceivedOfferAsync(int offerId)
        {
            Offer _offer = (await _offerQueries.FindOfferById(offerId)) ?? new Offer("10101010", 1555, true);
            _offer.UpdateStatus(Enums.OfferStatus.Received);
            await _offerCommands.UpdateAsync(_offer);
            return _offer;
        }

        #region New Apply Offer

        #endregion

        public async Task<Offer> WithdrawOffer(int offerId, string suplierCR)
        {
            Offer offer = await _offerQueries.FindOfferById(offerId);
            List<string> Suppliers = new List<string> { offer.CommericalRegisterNo };
            offer.UpdateStatus(OfferStatus.Canceled);
            foreach (var supplier in offer.Combined.Where(d => d.SolidarityTypeId != (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader))
            {
                supplier.UpdateStatus(SupplierSolidarityStatus.New);
            }
            offer.AddActionToOfferHistory(offer.Tender.TenderStatusId, (int)OfferStatus.Canceled, TenderActions.WithdrowOffer, _httpContextAccessor.HttpContext.User.UserId(), suplierCR);
            await _offerCommands.UpdateAsync(offer);
            await _offerCommands.SaveAsync();
            NotificationArguments WithdrawOffer = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", offer.Tender.TenderName, offer.Tender.TenderName, offer.Tender.TenderNumber, offer.Tender.Purpose, offer.Tender.LastEnqueriesDate, offer.Tender.LastOfferPresentationDate, offer.Tender.OffersOpeningDate, offer.Tender.OffersOpeningDate?.Hour },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { offer.Tender.TenderNumber },
                SMSArgs = new object[] { offer.Tender.TenderNumber }
            };
            MainNotificationTemplateModel templetModel = new MainNotificationTemplateModel(WithdrawOffer, $"Tender/Details?STenderId={Util.Encrypt(offer.Tender.TenderId)}", NotificationEntityType.Tender, offer.Tender.TenderId.ToString(), offer.Tender.BranchId);
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.deletevendoroffer, Suppliers, templetModel);
            return offer;
        }

        public async Task<OfferModel> GeOfferByTenderIdAndOfferIdForOpening(int tenderId, int offerId)
        {
            OfferModel result = await _offerQueries.GeOfferByTenderIdAndOfferIdForOpening(tenderId, offerId);
            if (_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersOppeningSecretary) && result.TenderStatusId == (int)Enums.TenderStatus.OffersOppening)
            {
                result.QuantityTableStepDTO = await FindOfferQuantityItemsVROById(tenderId, offerId, _httpContextAccessor.HttpContext.User.UserBranch(), false);
            }
            else
            {
                result.QuantityTableStepDTO = await FindOfferQuantityItemsVROById(tenderId, offerId, _httpContextAccessor.HttpContext.User.UserBranch(), true);
            }
            if (result.OfferStatusId == (int)OfferStatus.Received && _httpContextAccessor.HttpContext.User.UserRole() == RoleNames.supplier)
                throw new UnHandledAccessException();
            return result;
        }

        public async Task<OfferModel> GeOfferByTenderId(int tenderId)
        {
            var result = await _offerQueries.GeOfferByTenderId(tenderId);
            if (result.OfferStatusId == (int)Enums.OfferStatus.Received && _httpContextAccessor.HttpContext.User.UserRole() == RoleNames.supplier)
                throw new UnHandledAccessException();
            return result;
        }

        public async Task<OfferDetailModel> GetTenderStatus(int tenderId)
        {
            return await _offerQueries.GetTenderStatus(tenderId);
        }

        public async Task<OfferModel> GeOfferByTenderIdAndOfferIdForChecking(int offerId)
        {
            Offer offer = await _offerQueries.GeOfferByTenderIdAndOfferIdForChecking(offerId);
            var result = await GetOfferModel(offer);
            if ((result.CheckingDateSet || result.FinancialCheckingDateSet || result.TenderTypeId == (int)Enums.TenderType.CurrentTender || result.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase)
                && ((result.OfferPresentationWayId == (int)OfferPresentationWayId.OneFile &&
                (result.TenderStatusId == (int)Enums.TenderStatus.Approved || result.TenderStatusId == (int)Enums.TenderStatus.OffersChecking || result.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed || result.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageApproved))
                   || (result.OfferPresentationWayId == (int)OfferPresentationWayId.TwoSepratedFiles
                   && (result.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking || result.TenderStatusId == (int)Enums.TenderStatus.OffersOpenFinancialStageApproved))))
            {
                result.QuantityTableStepDTO = await FindOfferQuantityItemsById(offer, false);
            }
            else
            {
                result.QuantityTableStepDTO = await FindOfferQuantityItemsById(offer, true);
            }
            if (result.OfferStatusId == (int)OfferStatus.Received && _httpContextAccessor.HttpContext.User.UserRole() == RoleNames.supplier)
                throw new UnHandledAccessException();
            return result;
        }
        private QuantitiesTablesForAwardingModel FillQuantitiesTablesForAwardingModel(Offer offer)
        {
            return new QuantitiesTablesForAwardingModel
            {
                FinalPrice = offer.FinalPriceAfterDiscount,
                OfferId = offer.OfferId,
                TenderTypeId = offer.Tender.TenderTypeId,
                DiscountNotes = offer.DiscountNotes,
                Discount = offer.Discount,
                OfferStatusId = offer.OfferStatusId,
                HasAlternativeOffer = offer.Tender.HasAlternativeOffer ?? false,
                TenderId = offer.TenderId,
                TenderIdString = Util.Encrypt(offer.TenderId),
                CommericalRegisterNo = offer.CommericalRegisterNo,
                TenderStatusId = offer.Tender.TenderStatusId,
                IsSolidarity = offer.IsSolidarity,
                OfferPresentationWayId = offer.Tender.OfferPresentationWayId ?? 0,
                IsSupplierCombinedLeader = offer.Combined.Any(),
                CombinedOwner = offer.Combined.Any(),
                OfferAcceptanceStatusId = offer.OfferAcceptanceStatusId,
                OfferTechnicalEvaluationStatusId = offer.OfferTechnicalEvaluationStatusId,
                FinantialOfferStatusId = offer.OfferAcceptanceStatusId,
                FinantialRejectionReason = offer.FinantialRejectionReason,
                FinantialNotes = offer.FinantialNotes,
                Notes = offer.Notes,
                FinantialOfferStatusString = offer.OfferAcceptanceStatusId == (int)OfferAcceptanceStatusId.AcceptedOffer ? Resources.OfferResources.DisplayInputs.AccepteOffer : Resources.OfferResources.DisplayInputs.RejectedOffer,
                TechnicalOfferStatusId = offer.OfferTechnicalEvaluationStatusId,
                TechnicalOfferStatusString = offer.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.OfferResources.DisplayInputs.Matching : Resources.OfferResources.DisplayInputs.NotMatching,
                RejectionReason = offer.RejectionReason,
                CombinedIdString = Util.Encrypt(offer.Combined.Where(c => c.SolidarityTypeId == (int)UnRegisteredSuppliersInvitationType.SolidarityLeader).Select(x => x.Id).FirstOrDefault()),
                OffersCheckingDateTime = offer.Tender.OffersCheckingDate,
                CheckingDateSet = offer.Tender.CheckingDateSet.HasValue && offer.Tender.CheckingDateSet == true,
                TechniciansReportAttachments = offer.TechnicianReportAttachments.Select(a => new TechniciansReportAttachmentModel
                {
                    AttachmentId = a.AttachmentId,
                    Name = a.Name,
                    FileNetReferenceId = a.FileNetReferenceId,
                    OfferId = a.OfferId,
                    AttachmentTypeId = a.AttachmentTypeId
                }).ToList(),
                TechniciansReportAttachmentsRef = string.Join(",", offer.TechnicianReportAttachments.Select(at =>
                        "/Upload/GetFile/" + at.FileNetReferenceId + ":" + at.Name)),
                Attachment = offer.Attachment.Select(a => new SupplierAttachmentModel
                {
                    AttachmentId = a.AttachmentId,
                    FileName = a.FileName,
                    FileNetReferenceId = a.FileNetReferenceId,
                    OfferId = a.OfferId
                }).ToList(),
                CombinedSupplierModel = offer.Combined != null && offer.Combined.Count > 0 ? offer.Combined.Select(y => new CombinedSupplierModel
                {
                    SupplierName = y.Supplier?.SelectedCrName,
                }).ToList() : new List<CombinedSupplierModel>(),
            };
        }

        private async Task<OfferModel> GetOfferModel(Offer offer)
        {
            var result = new OfferModel
            {
                FinalPrice = offer.FinalPriceAfterDiscount,
                OfferId = offer.OfferId,
                TenderTypeId = offer.Tender.TenderTypeId,
                DiscountNotes = offer.DiscountNotes,
                Discount = offer.Discount,
                OfferStatusId = offer.OfferStatusId,
                HasAlternativeOffer = offer.Tender.HasAlternativeOffer ?? false,
                TenderId = offer.TenderId,
                TenderIdString = Util.Encrypt(offer.TenderId),
                CommericalRegisterNo = offer.CommericalRegisterNo,
                TenderStatusId = offer.Tender.TenderStatusId,
                IsSolidarity = offer.IsSolidarity,
                OfferPresentationWayId = offer.Tender.OfferPresentationWayId ?? 0,
                IsSupplierCombinedLeader = offer.Combined.Any(),
                CombinedOwner = offer.Combined.Any(),
                OfferAcceptanceStatusId = offer.OfferAcceptanceStatusId,
                OfferTechnicalEvaluationStatusId = offer.OfferTechnicalEvaluationStatusId,
                FinantialOfferStatusId = offer.OfferAcceptanceStatusId,
                FinantialRejectionReason = offer.FinantialRejectionReason,
                FinantialNotes = offer.FinantialNotes,
                Notes = offer.Notes,
                FinantialOfferStatusString = offer.OfferAcceptanceStatusId == (int)OfferAcceptanceStatusId.AcceptedOffer ? Resources.OfferResources.DisplayInputs.AccepteOffer : Resources.OfferResources.DisplayInputs.RejectedOffer,
                TechnicalOfferStatusId = offer.OfferTechnicalEvaluationStatusId,
                TechnicalOfferStatusString = offer.OfferTechnicalEvaluationStatusId == (int)OfferTechnicalEvaluationStatusId.IdenticalOffer ? Resources.OfferResources.DisplayInputs.Matching : Resources.OfferResources.DisplayInputs.NotMatching,
                RejectionReason = offer.RejectionReason,
                CombinedIdString = Util.Encrypt(offer.Combined.Where(c => c.SolidarityTypeId == (int)UnRegisteredSuppliersInvitationType.SolidarityLeader).Select(x => x.Id).FirstOrDefault()),
                OffersCheckingDateTime = offer.Tender.OffersCheckingDate,
                isOldFlow = offer.Tender.IsOldFlow(_configuration.OldFlow.EndDate.ToDateTime()),
                CheckingDateSet = offer.Tender.CheckingDateSet.HasValue && offer.Tender.CheckingDateSet == true,
                FinancialCheckingDateSet = offer.Tender.FinancialCheckingDateSet.HasValue && offer.Tender.FinancialCheckingDateSet == true,

                TechniciansReportAttachments = offer.TechnicianReportAttachments.Select(a => new TechniciansReportAttachmentModel
                {
                    AttachmentId = a.AttachmentId,
                    Name = a.Name,
                    FileNetReferenceId = a.FileNetReferenceId,
                    OfferId = a.OfferId,
                    AttachmentTypeId = a.AttachmentTypeId
                }).ToList(),
                TechniciansReportAttachmentsRef = string.Join(",", offer.TechnicianReportAttachments.Select(at =>
                        "/Upload/GetFile/" + at.FileNetReferenceId + ":" + at.Name)),
                Attachment = offer.Attachment.Select(a => new SupplierAttachmentModel
                {
                    AttachmentId = a.AttachmentId,
                    FileName = a.FileName,
                    FileNetReferenceId = a.FileNetReferenceId,
                    OfferId = a.OfferId
                }).ToList(),
                CombinedSupplierModel = offer.Combined != null && offer.Combined.Count > 0 ? offer.Combined.Select(y => new CombinedSupplierModel
                {
                    SupplierName = y.Supplier?.SelectedCrName,
                }).ToList() : new List<CombinedSupplierModel>()
            };

            var localContentSetting = await _offerQueries.GetLocalContentSettingsDate();
            result.IsValidToApplyOfferLocalContentChanges = offer.Tender.CreatedAt >= localContentSetting.Date && offer.OfferlocalContentDetails != null;

            if (offer.Tender.IsValidToApplyOfferLocalContentChanges(localContentSetting.Date.Value) && offer.OfferlocalContentDetails != null)
            {
                result.TechnicalOfferStatusId = (!offer.OfferTechnicalEvaluationStatusId.HasValue && (!offer.OfferlocalContentDetails.IsBindedToTheLowestBaseLine /*|| offer.OfferlocalContentDetails.IsBindedToMandatoryList*/ || !offer.OfferlocalContentDetails.IsBindedToTheLowestLocalContent)) ? (int)OfferTechnicalEvaluationStatusId.NotIdenticalOffer : offer.OfferTechnicalEvaluationStatusId;
                result.IsSupplierBindedToBaseLine = offer.OfferlocalContentDetails.IsBindedToTheLowestBaseLine;
                result.IsSupplierBindedToLocalContent = offer.OfferlocalContentDetails.IsBindedToTheLowestLocalContent;
                result.IsBindedToMandatoryList = offer.OfferlocalContentDetails != null ? offer.OfferlocalContentDetails.IsBindedToMandatoryList : true;
                if (result.RejectionReason == null)
                {
                    if (!result.IsSupplierBindedToBaseLine)
                    {
                        result.RejectionReason += "عدم تحقيق المورد للحد الادني لخط الاساس";
                    }
                    if (!result.IsSupplierBindedToLocalContent)
                    {
                        result.RejectionReason += "   عدم تحقيق الحد الادني المطلوب للمحتوي المحلي ";

                    }
                }
            }

            return result;
        }

        public async Task<QuantityTableStepDTO> FindOfferQuantityItemsById(Offer offer, bool isReadOnly)
        {
            List<long> formIds = offer.SupplierTenderQuantityTables.Where(a => a.QuantitiyItemsJson != null && a.TenderQuantityTable != null && a.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any() && a.IsActive == true).Select(a => long.Parse(a.TenderQuantityTable.FormId.ToString())).ToList();
            ApiResponse<List<TemplateFormDTO>> obj = await _qantityTemplatesProxy.GenerateTemplatesByFormIds(formIds);
            QuantityTableStepDTO quantityTableStepDTO = new QuantityTableStepDTO
            {
                TemplateYears = offer.Tender.TemplateYears ?? 0,
                TenderID = offer.Tender.TenderId,
                TenderCreatedByTypeId = offer.Tender.CreatedByTypeId ?? 0,
                TenderIdString = Util.Encrypt(offer.Tender.TenderId),
                PreQualificationId = offer.Tender.PreQualificationId ?? 0,
                InvitationTypeId = offer.Tender.InvitationTypeId ?? 0,
                HasAlternativeOffer = offer.Tender.HasAlternativeOffer ?? false,
                TenderName = offer.Tender.TenderName,
                TenderStatusId = offer.Tender.TenderStatusId,
                ReferenceNumber = offer.Tender.ReferenceNumber,
                TenderNumber = offer.Tender.TenderNumber,
                TenderTypeId = offer.Tender.TenderTypeId,
                OfferPresentationWayId = offer.Tender.OfferPresentationWayId ?? 0,
                TemplateFormDTOs = obj.Data
            };
            quantityTableStepDTO.TemplateFormDTOs.ForEach(a =>
            {
                a.FormDTOs.ForEach(f =>
                {
                    f.Tables = offer.SupplierTenderQuantityTables.Where(q => q.IsActive == true && q.QuantitiyItemsJson != null && q.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any() && q.TenderQuantityTable?.FormId == f.FormId)
                    .Select(t => new TableDTO { TableId = t.Id, TableName = t.Name }).ToList();
                });
            });
            quantityTableStepDTO.IsReadOnly = isReadOnly;
            return quantityTableStepDTO;
        }

        public async Task<QuantitiesTemplateModel> FindApplyOfferQuantityItemsById(int tenderId, int offerId, int branchId, bool isReadOnly)
        {
            QuantitiesTemplateModel lst = await _offerQueries.GetOfferQuantityItemsForApplyOffer(offerId);
            if (lst == null)
                lst = new QuantitiesTemplateModel();
            if (lst.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || lst.TenderTypeId == (int)Enums.TenderType.CurrentTender || lst.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                lst.ActivityTemplates = new List<int> { (int)Enums.TenderActivityTamplate.OldSystemAndVRO };
            }
            FormConfigurationDTO DTOModel = new FormConfigurationDTO() { TenderId = tenderId, ActivityIds = lst.ActivityTemplates, QuantityItemDtos = lst.QuantitiesItems.ToList(), YearsCount = lst.TemplateYears == null ? 0 : lst.TemplateYears.Value };
            ApiResponse<List<HtmlTemplateDto>> obj = await _qantityTemplatesProxy.GetMonafasatGOV(DTOModel);
            if (obj?.Data != null)
            {
                lst.grid.AddRange(obj.Data.Where(a => a.TableId != "0").Select(a => a.FormHtml).ToList());
                lst.grids.AddRange(obj.Data.Where(a => a.TableId != "0").GroupBy(o => new { o.FormId, o.FormName, o.TemplateName, o.FormExcellTemplate }).Select(o => new HTMLObject
                {
                    FormId = o.Key.FormId,
                    FormName = o.Key.FormName,
                    TemplateName = o.Key.TemplateName,
                    FormExcellTemplate = _configuration.APIConfiguration.QuantityTemplates + "/api/QuantityTable/" + o.Key.FormExcellTemplate,
                    Javascript = o.FirstOrDefault().JsScript,
                    gridTables = o.Select(u => new TableModel { TableHtml = u.FormHtml, TableId = u.TableId, TableName = u.TableName, TenderId = u.TenderId.Value, FormId = u.FormId }).ToList()
                }).ToList());
            }
            lst.IsReadOnly = isReadOnly;
            return lst;
        }

        private async Task<QuantitiesTemplateModel> FindApplyOfferForms(int tenderId, int offerId, bool isReadOnly)
        {
            var offer = await _offerQueries.GetOfferWithQTById(offerId);
            QuantitiesTemplateModel lst = await _offerQueries.GetOfferQuantityItemsForApplyOffer(offerId);

            if (lst == null)
                lst = new QuantitiesTemplateModel();
            if (lst.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || lst.TenderTypeId == (int)Enums.TenderType.CurrentTender || lst.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                lst.ActivityTemplates = new List<int> { (int)Enums.TenderActivityTamplate.OldSystemAndVRO };
            }
            FormConfigurationDTO DTOModel = new FormConfigurationDTO() { TenderId = tenderId, FormIds = lst.FormIds, QuantityItemDtos = lst.QuantitiesItems?.ToList(), YearsCount = lst.TemplateYears == null ? 0 : lst.TemplateYears.Value };
            ApiResponse<List<HtmlTemplateDto>> obj = await _qantityTemplatesProxy.GetSupplierTemplatesToApplyOffer(DTOModel);
            if (obj.Data != null)
            {
                lst.grids.AddRange(obj.Data.GroupBy(o => new { o.FormId, o.FormName, o.TemplateName, o.FormExcellTemplate }).Select(o => new HTMLObject
                {
                    FormId = o.Key.FormId,
                    FormName = o.Key.FormName,
                    TemplateName = o.Key.TemplateName,
                    gridTables = offer.SupplierTenderQuantityTables.Where(q => q.IsActive == true && q.TenderQuantityTable.IsActive == true && q.QuantitiyItemsJson != null && q.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any() && q.TenderQuantityTable?.FormId == o.Key.FormId)
                    .Select(u => new TableModel { TableId = u.Id.ToString(), TableName = u.Name, TenderId = tenderId, FormId = u.TenderQuantityTable.FormId }).ToList()
                }).ToList());
            }
            lst.IsReadOnly = isReadOnly;
            return lst;
        }

        public async Task<QueryResult<TableModel>> GetOfferTableQuantityItems(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            var cellsCount = 0;
            Offer offer = await _offerQueries.GeOfferByTenderIdAndOfferIdForChecking(quantityTableSearchCretriaModel.OfferId);
            cellsCount = _offerQueries.GetOfferTableQuantityItems(offer, quantityTableSearchCretriaModel.QuantityTableId);
            quantityTableSearchCretriaModel.CellsCount = cellsCount;
            var quantityItems = await _offerQueries.GetSupplierQTableItemsForChecking(quantityTableSearchCretriaModel);
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                SubmitAction = "/Offer/SaveCheckingQuantityItem",
                TenderId = quantityTableSearchCretriaModel.TenderId,
                HasAlternative = offer.Tender.HasAlternativeOffer ?? false,
                ApplySelected = offer.Tender.HasAlternativeOffer ?? false,
                CanEditAlternative = !quantityTableSearchCretriaModel.IsReadOnly,
                AllowEdit = false,
                ActivityId = 1,
                EncryptedOfferId = Util.Encrypt(quantityTableSearchCretriaModel.OfferId),
                EncryptedTenderId = Util.Encrypt(quantityTableSearchCretriaModel.TenderId),
                QuantityItemDtos = quantityItems.Items.Where(i => i.TableId == quantityTableSearchCretriaModel.QuantityTableId).OrderBy(a => a.ItemNumber).ToList(),
                YearsCount = (offer.Tender.TemplateYears ?? 0)
            };
            ApiResponse<List<HtmlTemplateDto>> resultList;
            List<HTMLObject> hTMLObjects = new List<HTMLObject>();
            if (!quantityTableSearchCretriaModel.IsReadOnly)
            {
                resultList = await _qantityTemplatesProxy.GetMonafasatSupplierForChecking(DTOModel);
            }
            else
            {
                resultList = await _qantityTemplatesProxy.GenerateSupplierReadOnlyTemplate(DTOModel);
            }
            ApiResponse<List<HtmlTemplateDto>> obj = new ApiResponse<List<HtmlTemplateDto>> { Data = resultList?.Data };
            if (obj.Data != null && obj.Data.Count > 0 && obj.Data[0] != null)
            {
                hTMLObjects.AddRange(obj.Data.GroupBy(o => new { o.FormId, o.FormName, o.TemplateName, o.FormExcellTemplate }).Select(o => new HTMLObject { FormId = o.Key.FormId, FormName = o.Key.FormName, TemplateName = o.Key.TemplateName, FormExcellTemplate = _configuration.APIConfiguration.QuantityTemplates + "/api/QuantityTable/" + o.Key.FormExcellTemplate, Javascript = o.FirstOrDefault().JsScript, gridTables = o.Select(u => new TableModel { TableHtml = u.FormHtml, TableId = quantityTableSearchCretriaModel.QuantityTableId.ToString(), TableName = !string.IsNullOrEmpty(u.TableName) ? u.TableName : "اسم الجدول", FormId = u.FormId, DeleteFormHtml = u.DeleteFormHtml, EditFormHtml = u.EditFormHtml, Javascript = u.JsScript, IsTableHasAlternative = u.IsTableHasAlternative }).ToList() }).ToList());
            }
            return new QueryResult<TableModel>(hTMLObjects.Count > 0 ? hTMLObjects[0].gridTables.ToList() : new List<TableModel>(), quantityItems.TotalCount, quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * cellsCount);
        }

        public async Task<TableModel> GetCoastsTablForDirectPurchaseAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            var offer = await _offerQueries.FindOfferWithStatusById(quantityTableSearchCretriaModel.OfferId);
            var reuslt = offer.SupplierTenderQuantityTables.Where(x => x.IsActive == true && x.QuantitiyItemsJson != null)
                .SelectMany(s => s.QuantitiyItemsJson.SupplierTenderQuantityTableItems, (parent, child) => new TenderQuantityItemDTO
                {
                    ColumnId = child.ColumnId,
                    ItemNumber = child.ItemNumber,
                    ColumnName = "",
                    TableName = parent.Name,
                    TableId = offer.SupplierTenderQuantityTables.FirstOrDefault(a => a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any(i => i.Id == child.Id)).Id,
                    TemplateId = child.ActivityTemplateId,
                    TenderId = offer.TenderId,
                    TenderFormHeaderId = child.TenderFormHeaderId,
                    Value = child.Value,
                    AlternativeValue = child.AlternativeValue,
                    IsDefault = child.IsDefault,
                    Id = child.Id
                }).ToList();
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                SubmitAction = "/Offer/SaveCheckingQuantityItem",
                TenderId = quantityTableSearchCretriaModel.TenderId,
                HasAlternative = offer.Tender.HasAlternativeOffer ?? false,
                ApplySelected = offer.Tender.HasAlternativeOffer ?? false,
                ActivityId = 1,
                EncryptedOfferId = Util.Encrypt(quantityTableSearchCretriaModel.OfferId),
                EncryptedTenderId = Util.Encrypt(quantityTableSearchCretriaModel.TenderId),
                QuantityItemDtos = reuslt,
                YearsCount = offer.Tender.TemplateYears ?? 0
            };
            QuantitiesTemplateModel lst = new QuantitiesTemplateModel();
            ApiResponse<List<HtmlTemplateDto>> resultList;
            resultList = await _qantityTemplatesProxy.GenerateCostTable(DTOModel);
            ApiResponse<List<HtmlTemplateDto>> obj = new ApiResponse<List<HtmlTemplateDto>> { Data = resultList.Data };
            lst.grids.AddRange(obj.Data.GroupBy(o => new { o.FormId, o.FormName, o.TemplateName, o.FormExcellTemplate }).Select(o => new HTMLObject { FormId = o.Key.FormId, FormName = o.Key.FormName, TemplateName = o.Key.TemplateName, FormExcellTemplate = _configuration.APIConfiguration.QuantityTemplates + "/api/QuantityTable/" + o.Key.FormExcellTemplate, Javascript = o.FirstOrDefault().JsScript, gridTables = o.Select(u => new TableModel { TableHtml = u.FormHtml, TableId = u.TableId, TableName = !string.IsNullOrEmpty(u.TableName) ? u.TableName : "اسم الجدول", FormId = u.FormId, DeleteFormHtml = u.DeleteFormHtml, EditFormHtml = u.EditFormHtml, Javascript = u.JsScript }).ToList() }).ToList());
            lst.QuantitiesItems = reuslt;
            lst.IsReadOnly = quantityTableSearchCretriaModel.IsReadOnly;
            return lst.grids[0].gridTables.FirstOrDefault();
        }

        public async Task<TableModel> GetCoastsTablForApplyOfferAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            var offer = await _offerQueries.FindOfferWithQTJSONId(quantityTableSearchCretriaModel.OfferId);
            var reuslt = offer.SupplierTenderQuantityTables.Where(c => c.IsActive == true && c.QuantitiyItemsJson != null)
                .SelectMany(s => s.QuantitiyItemsJson.SupplierTenderQuantityTableItems, (parent, child) => new TenderQuantityItemDTO
                {
                    ColumnId = child.ColumnId,
                    ItemNumber = child.ItemNumber,
                    ColumnName = "",
                    TableName = parent.Name,
                    TableId = offer.SupplierTenderQuantityTables.FirstOrDefault(a => a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any(i => i.Id == child.Id)).Id,
                    TemplateId = child.ActivityTemplateId,
                    TenderId = offer.TenderId,
                    TenderFormHeaderId = child.TenderFormHeaderId,
                    Value = child.Value,
                    AlternativeValue = child.AlternativeValue,
                    IsDefault = child.IsDefault,
                    Id = child.Id
                }).ToList();
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                SubmitAction = "/Offer/SaveCheckingQuantityItem",
                TenderId = quantityTableSearchCretriaModel.TenderId,
                HasAlternative = offer.Tender.HasAlternativeOffer ?? false,
                ApplySelected = false,
                ActivityId = 1,
                EncryptedOfferId = Util.Encrypt(quantityTableSearchCretriaModel.OfferId),
                EncryptedTenderId = Util.Encrypt(quantityTableSearchCretriaModel.TenderId),
                QuantityItemDtos = reuslt,
                YearsCount = offer.Tender.TemplateYears ?? 0
            };
            QuantitiesTemplateModel lst = new QuantitiesTemplateModel();
            ApiResponse<List<HtmlTemplateDto>> resultList;
            resultList = await _qantityTemplatesProxy.GenerateCostTable(DTOModel);
            ApiResponse<List<HtmlTemplateDto>> obj = new ApiResponse<List<HtmlTemplateDto>> { Data = resultList.Data };
            lst.grids.AddRange(obj.Data.GroupBy(o => new { o.FormId, o.FormName, o.TemplateName, o.FormExcellTemplate }).Select(o => new HTMLObject { FormId = o.Key.FormId, FormName = o.Key.FormName, TemplateName = o.Key.TemplateName, FormExcellTemplate = _configuration.APIConfiguration.QuantityTemplates + "/api/QuantityTable/" + o.Key.FormExcellTemplate, Javascript = o.FirstOrDefault().JsScript, gridTables = o.Select(u => new TableModel { TableHtml = u.FormHtml, TableId = u.TableId, TableName = !string.IsNullOrEmpty(u.TableName) ? u.TableName : "اسم الجدول", FormId = u.FormId, DeleteFormHtml = u.DeleteFormHtml, EditFormHtml = u.EditFormHtml, Javascript = u.JsScript }).ToList() }).ToList());
            lst.QuantitiesItems = reuslt;
            lst.IsReadOnly = quantityTableSearchCretriaModel.IsReadOnly;
            return lst.grids[0].gridTables.FirstOrDefault();
        }
        public async Task<TableModel> GetCoastsTablForOpenDetails(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            var offer = await _offerQueries.FindOfferWithStatusById(quantityTableSearchCretriaModel.OfferId);
            var reuslt = offer.SupplierTenderQuantityTables
                .SelectMany(s => s.QuantitiyItemsJson.SupplierTenderQuantityTableItems, (parent, child) => new TenderQuantityItemDTO
                {
                    ColumnId = child.ColumnId,
                    ItemNumber = child.ItemNumber,
                    ColumnName = "",
                    TableName = parent.Name,
                    TableId = offer.SupplierTenderQuantityTables.FirstOrDefault(a => a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any(i => i.Id == child.Id)).Id,
                    TemplateId = child.ActivityTemplateId,
                    TenderId = offer.TenderId,
                    TenderFormHeaderId = child.TenderFormHeaderId,
                    Value = child.Value,
                    AlternativeValue = child.AlternativeValue,
                    IsDefault = child.IsDefault,
                    Id = child.Id
                }).ToList();
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                SubmitAction = "/Offer/SaveCheckingQuantityItem",
                TenderId = quantityTableSearchCretriaModel.TenderId,
                HasAlternative = offer.Tender.HasAlternativeOffer ?? false,
                ApplySelected = false,
                ActivityId = 1,
                EncryptedOfferId = Util.Encrypt(quantityTableSearchCretriaModel.OfferId),
                EncryptedTenderId = Util.Encrypt(quantityTableSearchCretriaModel.TenderId),
                QuantityItemDtos = reuslt,
                YearsCount = offer.Tender.TemplateYears ?? 0
            };
            QuantitiesTemplateModel lst = new QuantitiesTemplateModel();
            ApiResponse<List<HtmlTemplateDto>> resultList;
            resultList = await _qantityTemplatesProxy.GenerateCostTable(DTOModel);
            ApiResponse<List<HtmlTemplateDto>> obj = new ApiResponse<List<HtmlTemplateDto>> { Data = resultList.Data };
            lst.grids.AddRange(obj.Data.GroupBy(o => new { o.FormId, o.FormName, o.TemplateName, o.FormExcellTemplate }).Select(o => new HTMLObject { FormId = o.Key.FormId, FormName = o.Key.FormName, TemplateName = o.Key.TemplateName, FormExcellTemplate = _configuration.APIConfiguration.QuantityTemplates + "/api/QuantityTable/" + o.Key.FormExcellTemplate, Javascript = o.FirstOrDefault().JsScript, gridTables = o.Select(u => new TableModel { TableHtml = u.FormHtml, TableId = u.TableId, TableName = !string.IsNullOrEmpty(u.TableName) ? u.TableName : "اسم الجدول", FormId = u.FormId, DeleteFormHtml = u.DeleteFormHtml, EditFormHtml = u.EditFormHtml, Javascript = u.JsScript }).ToList() }).ToList());
            lst.QuantitiesItems = reuslt;
            lst.IsReadOnly = quantityTableSearchCretriaModel.IsReadOnly;
            return lst.grids[0].gridTables.FirstOrDefault();
        }

        public async Task<QueryResult<TableModel>> GetBayanTableAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            var cellsCount = 0;
            quantityTableSearchCretriaModel.FormId = int.Parse(_configuration.BayanFormIdConfiguration.BayanFormId);
            Offer offer = await _offerQueries.GetOfferWithQuantitiesTablesByOfferId(quantityTableSearchCretriaModel.OfferId);
            if (offer.SupplierTenderQuantityTables.Any(a => a.TenederQuantityId == null && a.IsActive == true &&
            (a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any())))
                quantityTableSearchCretriaModel.QuantityTableId = offer.SupplierTenderQuantityTables.FirstOrDefault(a => a.TenederQuantityId == null && a.IsActive == true && a.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any()).Id;
            if (quantityTableSearchCretriaModel.QuantityTableId == 0)
            {
                offer.CreateVROOfferQuantityTables(offer.OfferId, "اجمالى البيانات");
                offer = await _offerCommands.UpdateAsync(offer);
                quantityTableSearchCretriaModel.QuantityTableId = offer.SupplierTenderQuantityTables.FirstOrDefault(a => a.TenederQuantityId == null && a.IsActive == true).Id;
            }
            else
                cellsCount = await _offerQueries.GetOfferTableQuantityItems(quantityTableSearchCretriaModel.QuantityTableId);
            quantityTableSearchCretriaModel.CellsCount = cellsCount;
            var quantityItems = await _offerQueries.GetSupplierQTableItemsForChecking(quantityTableSearchCretriaModel);
            QuantitiesTemplateModel lst = await _offerQueries.GetOfferQuantityItems(quantityTableSearchCretriaModel.OfferId, quantityTableSearchCretriaModel.QuantityTableId);
            lst.QuantitiesItems = quantityItems.Items.ToList();
            if (lst == null)
                lst = new QuantitiesTemplateModel();
            lst.grid = new List<string>();
            FormConfigurationDTO DTOModel = new FormConfigurationDTO() { TenderId = quantityTableSearchCretriaModel.TenderId, ActivityId = (int)TenderActivityTamplate.BayanTemplate, QuantityItemDtos = lst.QuantitiesItems, YearsCount = lst.TemplateYears == null ? 0 : lst.TemplateYears.Value };
            ApiResponse<List<HtmlTemplateDto>> resultList = new ApiResponse<List<HtmlTemplateDto>>();
            ApiResponse<HtmlTemplateDto> resultItem = new ApiResponse<HtmlTemplateDto>();
            if ((_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersOpeningAndCheckSecretary) || _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersOppeningSecretary))
            && ((offer.Tender.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.OneFile && (offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersChecking || offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppening))
            || (offer.Tender.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles && offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking)
            || (offer.Tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects && (offer.Tender.TenderStatusId == (int)Enums.TenderStatus.VROFinancialCheckingOpening || offer.Tender.TenderStatusId == (int)Enums.TenderStatus.VROOffersFinancialChecking))))
            {
                if (DTOModel.QuantityItemDtos.Count != 0)
                    resultList = quantityTableSearchCretriaModel.IsReadOnly ? await _qantityTemplatesProxy.GenerateCommitteeReadOnlyTemplate(DTOModel) : await _qantityTemplatesProxy.GetBayanMonafasatGOV(DTOModel);
                else
                    resultItem = await _qantityTemplatesProxy.GetBayanTemplateFormHtml(quantityTableSearchCretriaModel.TenderId, quantityTableSearchCretriaModel.FormId, lst.TemplateYears == null ? 0 : lst.TemplateYears.Value, quantityTableSearchCretriaModel.QuantityTableId);
            }
            else
            {
                resultList = await _qantityTemplatesProxy.GenerateCommitteeReadOnlyTemplate(DTOModel);
            }
            HTMLObject obje = new HTMLObject();
            ApiResponse<List<HtmlTemplateDto>> obj = new ApiResponse<List<HtmlTemplateDto>> { Data = quantityTableSearchCretriaModel.CellsCount != 0 ? resultList.Data : new List<HtmlTemplateDto> { resultItem.Data } };
            if (obj.Data != null && obj.Data.Count > 0 && obj.Data[0] != null)
            {
                if (obj.Data[0].ColumnsCount != 0)
                    cellsCount = obj.Data[0].ColumnsCount;
                lst.grid.AddRange(obj.Data.Select(a => a.FormHtml).ToList());
                obje.grid.AddRange(obj.Data.Select(a => a.FormHtml).ToList());
                lst.grids.AddRange(obj.Data.GroupBy(o => new { o.FormId, o.FormName, o.TemplateName, o.FormExcellTemplate })
                    .Select(o => new HTMLObject
                    {
                        FormId = o.Key.FormId,
                        FormName = o.Key.FormName,
                        TemplateName = o.Key.TemplateName,
                        Javascript = o.FirstOrDefault().JsScript,
                        gridTables = o.Select(u => new TableModel
                        {
                            TableHtml = u.FormHtml,
                            TableId = u.TableId,
                            TableName = !string.IsNullOrEmpty(u.TableName) ? u.TableName : "اجمالى البيانات",
                            FormId = u.FormId,
                            DeleteFormHtml = u.DeleteFormHtml,
                            EditFormHtml = u.EditFormHtml,
                            Javascript = u.JsScript,
                            IsReadOnly = quantityTableSearchCretriaModel.IsReadOnly,
                            TemplateYears = lst.TemplateYears
                        }).ToList()
                    }).ToList());
                lst.grids.ForEach(x => x.gridTables.ForEach(a => a.TableId = quantityTableSearchCretriaModel.QuantityTableId.ToString()));
                lst.grids.ForEach(x => x.gridTables.ForEach(a => a.TenderId = quantityTableSearchCretriaModel.TenderId));
            }
            lst.IsReadOnly = quantityTableSearchCretriaModel.IsReadOnly;
            if (lst.grids.Any(a => a.gridTables.Any(b => b.TableId == quantityTableSearchCretriaModel.QuantityTableId.ToString())))
                return new QueryResult<TableModel>(lst.grids.FirstOrDefault(a => a.FormId == quantityTableSearchCretriaModel.FormId).gridTables.ToList(), quantityItems.TotalCount, quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * cellsCount);
            else if (lst.grids.Count == 0)
            {
                return new QueryResult<TableModel>(new List<TableModel>(), 0, quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * cellsCount);
            }
            else
            {
                lst.grids.FirstOrDefault().gridTables.FirstOrDefault().TenderId = quantityTableSearchCretriaModel.TenderId;
                lst.grids.FirstOrDefault().gridTables.FirstOrDefault().TableId = quantityTableSearchCretriaModel.QuantityTableId.ToString();
                return new QueryResult<TableModel>(lst.grids.FirstOrDefault(a => a.FormId == quantityTableSearchCretriaModel.FormId).gridTables.ToList(), cellsCount, quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * cellsCount);
            }
        }

        public async Task<QueryResult<TableModel>> GetBayanTableReadOnlyAsync(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            var cellsCount = 0;
            quantityTableSearchCretriaModel.FormId = int.Parse(_configuration.BayanFormIdConfiguration.BayanFormId);
            Offer offer = await _offerQueries.GetOfferWithQuantitiesTablesByOfferId(quantityTableSearchCretriaModel.OfferId);
            if (offer.SupplierTenderQuantityTables.Any(a => a.TenederQuantityId == null && a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any()))
                quantityTableSearchCretriaModel.QuantityTableId = offer.SupplierTenderQuantityTables.FirstOrDefault(a => a.TenederQuantityId == null && a.IsActive == true && a.QuantitiyItemsJson != null && a.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Any()).Id;

            cellsCount = await _offerQueries.GetOfferTableQuantityItems(quantityTableSearchCretriaModel.QuantityTableId);
            quantityTableSearchCretriaModel.CellsCount = cellsCount;
            var quantityItems = await _offerQueries.GetSupplierQTableItemsForChecking(quantityTableSearchCretriaModel);
            QuantitiesTemplateModel lst = await _offerQueries.GetOfferQuantityItems(quantityTableSearchCretriaModel.OfferId, quantityTableSearchCretriaModel.QuantityTableId);
            lst.QuantitiesItems = quantityItems.Items.ToList();
            if (lst == null)
                lst = new QuantitiesTemplateModel();
            lst.grid = new List<string>();
            FormConfigurationDTO DTOModel = new FormConfigurationDTO() { TenderId = quantityTableSearchCretriaModel.TenderId, ActivityId = (int)TenderActivityTamplate.BayanTemplate, QuantityItemDtos = lst.QuantitiesItems, YearsCount = lst.TemplateYears == null ? 0 : lst.TemplateYears.Value };
            ApiResponse<List<HtmlTemplateDto>> resultList;
            ApiResponse<HtmlTemplateDto> resultItem = new ApiResponse<HtmlTemplateDto>();
            resultList = await _qantityTemplatesProxy.GenerateCommitteeReadOnlyTemplate(DTOModel);
            HTMLObject obje = new HTMLObject();
            ApiResponse<List<HtmlTemplateDto>> obj = new ApiResponse<List<HtmlTemplateDto>> { Data = quantityTableSearchCretriaModel.CellsCount != 0 ? resultList.Data : new List<HtmlTemplateDto> { resultItem.Data } };
            if (obj.Data != null && obj.Data.Count > 0 && obj.Data[0] != null)
            {
                if (obj.Data[0].ColumnsCount != 0)
                    cellsCount = obj.Data[0].ColumnsCount;
                lst.grid.AddRange(obj.Data.Select(a => a.FormHtml).ToList());
                obje.grid.AddRange(obj.Data.Select(a => a.FormHtml).ToList());
                lst.grids.AddRange(obj.Data.GroupBy(o => new { o.FormId, o.FormName, o.TemplateName, o.FormExcellTemplate })
                    .Select(o => new HTMLObject
                    {
                        FormId = o.Key.FormId,
                        FormName = o.Key.FormName,
                        TemplateName = o.Key.TemplateName,
                        Javascript = o.FirstOrDefault().JsScript,
                        gridTables = o.Select(u => new TableModel
                        {
                            TableHtml = u.FormHtml,
                            TableId = u.TableId,
                            TableName = !string.IsNullOrEmpty(u.TableName) ? u.TableName : "اجمالى البيانات",
                            FormId = u.FormId,
                            DeleteFormHtml = u.DeleteFormHtml,
                            EditFormHtml = u.EditFormHtml,
                            Javascript = u.JsScript,
                            IsReadOnly = quantityTableSearchCretriaModel.IsReadOnly,
                            TemplateYears = lst.TemplateYears
                        }).ToList()
                    }).ToList());
                lst.grids.ForEach(x => x.gridTables.ForEach(a => a.TableId = quantityTableSearchCretriaModel.QuantityTableId.ToString()));
                lst.grids.ForEach(x => x.gridTables.ForEach(a => a.TenderId = quantityTableSearchCretriaModel.TenderId));
            }
            lst.IsReadOnly = quantityTableSearchCretriaModel.IsReadOnly;
            if (lst.grids.Any(a => a.gridTables.Any(b => b.TableId == quantityTableSearchCretriaModel.QuantityTableId.ToString())))
                return new QueryResult<TableModel>(lst.grids.FirstOrDefault(a => a.FormId == quantityTableSearchCretriaModel.FormId).gridTables.ToList(), quantityItems.TotalCount, quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * cellsCount);
            else
            {
                lst.grids.FirstOrDefault().gridTables.FirstOrDefault().TenderId = quantityTableSearchCretriaModel.TenderId;
                lst.grids.FirstOrDefault().gridTables.FirstOrDefault().TableId = quantityTableSearchCretriaModel.QuantityTableId.ToString();
                return new QueryResult<TableModel>(lst.grids.FirstOrDefault(a => a.FormId == quantityTableSearchCretriaModel.FormId).gridTables.ToList(), cellsCount, quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * cellsCount);
            }
        }

        public async Task<QueryResult<TableModel>> GetApplyOfferTableQuantityItems(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            var cellsCount = 0;
            var offer = await _offerQueries.GetofferWithTenderAndSupplierQT(quantityTableSearchCretriaModel.OfferId);
            if (quantityTableSearchCretriaModel.QuantityTableId == 0)
            {
                quantityTableSearchCretriaModel.QuantityTableId = offer.SupplierTenderQuantityTables.FirstOrDefault(o => o.TenderQuantityTable.FormId == quantityTableSearchCretriaModel.FormId).Id;
            }
            cellsCount = await _offerQueries.GetOfferTableQuantityItems(quantityTableSearchCretriaModel.QuantityTableId);
            quantityTableSearchCretriaModel.CellsCount = cellsCount;
            var quantityItems = await _offerQueries.GetSupplierQTableItemsForApplyOffer(quantityTableSearchCretriaModel);
            QuantitiesTemplateModel lst = await _offerQueries.GetOfferQuantityTableTemplateForApplyOffer(quantityTableSearchCretriaModel.OfferId);
            lst.QuantitiesItems = quantityItems.Items.ToList();
            if (lst == null)
                lst = new QuantitiesTemplateModel();
            lst.grid = new List<string>();
            if (lst.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || lst.TenderTypeId == (int)Enums.TenderType.CurrentTender || lst.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                lst.ActivityTemplates = new List<int> { (int)TenderActivityTamplate.OldSystemAndVRO };
            }
            foreach (var item in lst.ActivityTemplates)
            {
                FormConfigurationDTO DTOModel = new FormConfigurationDTO()
                {
                    SubmitAction = "/Offer/SaveCheckingQuantityItem",
                    TenderId = quantityTableSearchCretriaModel.TenderId,
                    HasAlternative = offer.Tender.HasAlternativeOffer ?? false,
                    ApplySelected = false,
                    CanEditAlternative = false,
                    AllowEdit = false,
                    ActivityId = 1,
                    EncryptedOfferId = Util.Encrypt(quantityTableSearchCretriaModel.OfferId),
                    EncryptedTenderId = Util.Encrypt(quantityTableSearchCretriaModel.TenderId),
                    QuantityItemDtos = lst.QuantitiesItems.ToList(),
                    YearsCount = offer.Tender.TemplateYears ?? 0
                };
                ApiResponse<List<HtmlTemplateDto>> resultList;
                resultList = quantityTableSearchCretriaModel.IsReadOnly ? await _qantityTemplatesProxy.GenerateSupplierReadOnlyTemplate(DTOModel) : await _qantityTemplatesProxy.GenerateSupplierTemplate(DTOModel);
                ApiResponse<List<HtmlTemplateDto>> obj = new ApiResponse<List<HtmlTemplateDto>> { Data = resultList.Data };
                if (obj.Data != null && obj.Data.Count > 0 && obj.Data[0] != null)
                {
                    lst.grid.AddRange(obj.Data.Select(a => a.FormHtml).ToList());
                    lst.grids.AddRange(obj.Data.GroupBy(o => new { o.FormId, o.FormName, o.TemplateName, o.FormExcellTemplate }).Select(o => new HTMLObject { FormId = o.Key.FormId, FormName = o.Key.FormName, TemplateName = o.Key.TemplateName, FormExcellTemplate = _configuration.APIConfiguration.QuantityTemplates + "/api/QuantityTable/" + o.Key.FormExcellTemplate, Javascript = o.FirstOrDefault().JsScript, gridTables = o.Select(u => new TableModel { TableHtml = u.FormHtml, TableId = u.TableId, TableName = !string.IsNullOrEmpty(u.TableName) ? u.TableName : "اسم الجدول", FormId = u.FormId, DeleteFormHtml = u.DeleteFormHtml, EditFormHtml = u.EditFormHtml, Javascript = u.JsScript, IsTableHasAlternative = u.IsTableHasAlternative }).ToList() }).ToList());
                }
            }
            lst.IsReadOnly = quantityTableSearchCretriaModel.IsReadOnly;
            return new QueryResult<TableModel>(lst.grids[0].gridTables.ToList(), quantityItems.TotalCount, quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * cellsCount);
        }
        public async Task<QueryResult<TableModel>> GetTableQuantityItemsOpenDetails(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            var cellsCount = 0;
            var offer = await _offerQueries.GetOfferWithQTByIdForOpenningDetails(quantityTableSearchCretriaModel.OfferId);
            if (quantityTableSearchCretriaModel.QuantityTableId == 0)
            {
                quantityTableSearchCretriaModel.QuantityTableId = offer.SupplierTenderQuantityTables.FirstOrDefault(o => o.TenderQuantityTable.FormId == quantityTableSearchCretriaModel.FormId).Id;
            }
            cellsCount = await _offerQueries.GetOfferTableQuantityItems(quantityTableSearchCretriaModel.QuantityTableId);
            quantityTableSearchCretriaModel.CellsCount = cellsCount;
            var quantityItems = await _offerQueries.GetSupplierQTableItemsForApplyOffer(quantityTableSearchCretriaModel);
            QuantitiesTemplateModel lst = await _offerQueries.GetOfferQuantityTableTemplateForApplyOffer(quantityTableSearchCretriaModel.OfferId);
            lst.QuantitiesItems = quantityItems.Items.ToList();
            if (lst == null)
                lst = new QuantitiesTemplateModel();
            lst.grid = new List<string>();
            if (lst.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || lst.TenderTypeId == (int)Enums.TenderType.CurrentTender || lst.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                lst.ActivityTemplates = new List<int> { (int)TenderActivityTamplate.OldSystemAndVRO };
            }
            foreach (var item in lst.ActivityTemplates)
            {
                FormConfigurationDTO DTOModel = new FormConfigurationDTO()
                {
                    SubmitAction = "/Offer/SaveCheckingQuantityItem",
                    TenderId = quantityTableSearchCretriaModel.TenderId,
                    HasAlternative = offer.Tender.HasAlternativeOffer ?? false,
                    ApplySelected = false,
                    CanEditAlternative = false,
                    AllowEdit = false,
                    ActivityId = 1,
                    EncryptedOfferId = Util.Encrypt(quantityTableSearchCretriaModel.OfferId),
                    EncryptedTenderId = Util.Encrypt(quantityTableSearchCretriaModel.TenderId),
                    QuantityItemDtos = lst.QuantitiesItems.ToList(),
                    YearsCount = offer.Tender.TemplateYears ?? 0
                };
                ApiResponse<List<HtmlTemplateDto>> resultList;
                resultList = quantityTableSearchCretriaModel.IsReadOnly ? await _qantityTemplatesProxy.GenerateSupplierReadOnlyTemplate(DTOModel) : await _qantityTemplatesProxy.GenerateSupplierTemplate(DTOModel);
                ApiResponse<List<HtmlTemplateDto>> obj = new ApiResponse<List<HtmlTemplateDto>> { Data = resultList.Data };
                if (obj.Data != null && obj.Data.Count > 0 && obj.Data[0] != null)
                {
                    lst.grid.AddRange(obj.Data.Select(a => a.FormHtml).ToList());
                    lst.grids.AddRange(obj.Data.GroupBy(o => new { o.FormId, o.FormName, o.TemplateName, o.FormExcellTemplate }).Select(o => new HTMLObject { FormId = o.Key.FormId, FormName = o.Key.FormName, TemplateName = o.Key.TemplateName, FormExcellTemplate = _configuration.APIConfiguration.QuantityTemplates + "/api/QuantityTable/" + o.Key.FormExcellTemplate, Javascript = o.FirstOrDefault().JsScript, gridTables = o.Select(u => new TableModel { TableHtml = u.FormHtml, TableId = u.TableId, TableName = !string.IsNullOrEmpty(u.TableName) ? u.TableName : "اسم الجدول", FormId = u.FormId, DeleteFormHtml = u.DeleteFormHtml, EditFormHtml = u.EditFormHtml, Javascript = u.JsScript, IsTableHasAlternative = u.IsTableHasAlternative }).ToList() }).ToList());
                }
            }
            lst.IsReadOnly = quantityTableSearchCretriaModel.IsReadOnly;
            return new QueryResult<TableModel>(lst.grids[0].gridTables.ToList(), quantityItems.TotalCount, quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * cellsCount);
        }

        public async Task<QueryResult<TableModel>> GetOfferTableQuantityItemsForDirectPurchase(QuantityTableSearchCretriaModel quantityTableSearchCretriaModel)
        {
            var cellsCount = 0;
            cellsCount = await _offerQueries.GetOfferTableQuantityItems(quantityTableSearchCretriaModel.QuantityTableId);
            quantityTableSearchCretriaModel.CellsCount = cellsCount;
            var offer = await _offerQueries.FindOfferWithStatusById(quantityTableSearchCretriaModel.OfferId);
            var quantityItems = await _offerQueries.GetSupplierQTableItemsForChecking(quantityTableSearchCretriaModel);
            QuantitiesTemplateModel lst = await _offerQueries.GetOfferQuantityItems(quantityTableSearchCretriaModel.OfferId, quantityTableSearchCretriaModel.QuantityTableId);
            lst.QuantitiesItems = quantityItems.Items.ToList();
            if (lst == null)
                lst = new QuantitiesTemplateModel();
            lst.grid = new List<string>();
            if (lst.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || lst.TenderTypeId == (int)Enums.TenderType.CurrentTender || lst.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                lst.ActivityTemplates = new List<int> { (int)TenderActivityTamplate.OldSystemAndVRO };
            }
            foreach (var item in lst.ActivityTemplates)
            {
                FormConfigurationDTO DTOModel = new FormConfigurationDTO()
                {
                    SubmitAction = "/Offer/SaveCheckingQuantityItem",
                    TenderId = quantityTableSearchCretriaModel.TenderId,
                    HasAlternative = offer.Tender.HasAlternativeOffer ?? false,
                    ApplySelected = offer.Tender.HasAlternativeOffer ?? false,
                    CanEditAlternative = _httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersCheckSecretary) && ((offer.OfferPresentationWayId == (int)OfferPresentationWayId.OneFile
                    && (offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersChecking)) || (offer.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles
                    && (offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed || offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking))),
                    AllowEdit = false,
                    ActivityId = 1,
                    EncryptedOfferId = Util.Encrypt(quantityTableSearchCretriaModel.OfferId),
                    EncryptedTenderId = Util.Encrypt(quantityTableSearchCretriaModel.TenderId),
                    QuantityItemDtos = lst.QuantitiesItems.ToList(),
                    YearsCount = offer.Tender.TemplateYears ?? 0
                };
                ApiResponse<List<HtmlTemplateDto>> resultList;
                if ((_httpContextAccessor.HttpContext.User.IsInRole(RoleNames.OffersCheckSecretary)) &&
                    ((offer.OfferPresentationWayId == (int)OfferPresentationWayId.OneFile && (offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersChecking))
                    || (offer.OfferPresentationWayId == (int)OfferPresentationWayId.TwoSepratedFiles && (offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed
                    || offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking))))
                {
                    resultList = await _qantityTemplatesProxy.GetMonafasatSupplierForChecking(DTOModel);
                }
                else
                {
                    resultList = await _qantityTemplatesProxy.GenerateSupplierReadOnlyTemplate(DTOModel);
                }
                HTMLObject obje = new HTMLObject();
                ApiResponse<List<HtmlTemplateDto>> obj = new ApiResponse<List<HtmlTemplateDto>> { Data = resultList.Data };
                if (obj.Data != null && obj.Data.Count > 0 && obj.Data[0] != null)
                {
                    lst.grid.AddRange(obj.Data.Select(a => a.FormHtml).ToList());
                    obje.grid.AddRange(obj.Data.Select(a => a.FormHtml).ToList());
                    lst.grids.AddRange(obj.Data.GroupBy(o => new { o.FormId, o.FormName, o.TemplateName, o.FormExcellTemplate }).Select(o => new HTMLObject { FormId = o.Key.FormId, FormName = o.Key.FormName, TemplateName = o.Key.TemplateName, FormExcellTemplate = _configuration.APIConfiguration.QuantityTemplates + "/api/QuantityTable/" + o.Key.FormExcellTemplate, Javascript = o.FirstOrDefault().JsScript, gridTables = o.Select(u => new TableModel { TableHtml = u.FormHtml, TableId = u.TableId, TableName = !string.IsNullOrEmpty(u.TableName) ? u.TableName : "اسم الجدول", FormId = u.FormId, DeleteFormHtml = u.DeleteFormHtml, EditFormHtml = u.EditFormHtml, Javascript = u.JsScript, IsTableHasAlternative = u.IsTableHasAlternative }).ToList() }).ToList());
                }
            }
            lst.IsReadOnly = quantityTableSearchCretriaModel.IsReadOnly;
            return new QueryResult<TableModel>(lst.grids[0].gridTables.ToList(), quantityItems.TotalCount, quantityTableSearchCretriaModel.PageNumber, quantityTableSearchCretriaModel.PageSize * cellsCount);
        }

        public async Task<OfferFinancialDetailsModel> OfferFinancialDetails(int offerId)
        {
            var result = await _offerQueries.OfferFinancialDetails(offerId);
            return result;
        }
        public async Task<OfferLocalContentDetailsModel> GetOfferLocalContentDetails(int offerId)
        {
            var result = await _offerQueries.GetOfferLocalContentDetails(offerId);
            return result;
        }

        public async Task<OfferAttachmentsModel> GetOfferAttachmentsDetails(int offerId, int combinedId, int userId)
        {
            OfferSolidarity combined = await _offerQueries.FindOfferByCombinedSupplierId(combinedId);
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(combined.Offer.TenderId);
            OfferAttachmentsModel result = await _offerQueries.GetOfferAttachmentsDetails(offerId, combinedId);
            if (result == null)
            {
                result = new OfferAttachmentsModel
                {
                    TenderID = tender.TenderId,
                    OfferId = combined.OfferId,
                    CombinedId = combinedId,
                    OfferPresentationWayId = tender.OfferPresentationWayId.Value,
                    TenderIDString = Util.Encrypt(tender.TenderId)
                };
            }
            result.SupplierName = combined.Supplier.SelectedCrName;
            result.TenderStatusId = tender.TenderStatusId;
            result.CombinationLetter = combined.Offer.Attachment.Where(x => x is SupplierCombinedAttachment)
                    .Select(x => new AttachmentModel
                    {
                        FileName = x.FileName,
                        FileNetId = x.FileNetReferenceId
                    }).FirstOrDefault();
            return result;
        }

        public async Task<OfferDetailModel> GetOfferDetails(int combinedId, int userId)
        {
            OfferSolidarity combined = await _offerQueries.FindOfferByCombinedSupplierId(combinedId);
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(combined.Offer.TenderId);
            if (tender == null)
            {
                throw new UnHandledAccessException();
            }
            OfferDetailModel result = await _offerQueries.GetOfferDetails(combinedId);
            if (result == null)
            {
                result = new OfferDetailModel
                {
                    TenderID = tender.TenderId,
                    OfferId = combined.OfferId,
                    CombinedId = combinedId,
                    OfferIdString = Util.Encrypt(combined.OfferId),
                    OfferPresentationWayId = tender.OfferPresentationWayId.Value,
                    TenderIDString = Util.Encrypt(tender.TenderId)
                };
            }
            result.CombinedOwner = combined.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader;
            result.SupplierName = combined.Supplier.SelectedCrName;
            result.TenderStatusId = tender.TenderStatusId;
            result.CombinationLetter = combined.Offer.Attachment.Where(x => x is SupplierCombinedAttachment)
                    .Select(x => new AttachmentModel
                    {
                        FileName = x.FileName,
                        FileNetId = x.FileNetReferenceId
                    }).FirstOrDefault();
            return result;
        }

        public async Task<OfferDetailModel> GetOfferDetailsForOpening(int combinedId, int userId)
        {
            OfferSolidarity combined = await _offerQueries.FindOfferByCombinedSupplierId(combinedId);
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(combined.Offer.TenderId);
            if (tender == null)
            {
                throw new UnHandledAccessException();
            }
            OfferDetailModel result = await _offerQueries.GetOfferDetails(combinedId);
            if (result == null)
            {
                result = new OfferDetailModel
                {
                    TenderID = tender.TenderId,
                    OfferId = combined.OfferId,
                    CombinedId = combinedId,
                    OfferIdString = Util.Encrypt(combined.OfferId),
                    OfferPresentationWayId = tender.OfferPresentationWayId.Value,
                    TenderIDString = Util.Encrypt(tender.TenderId)
                };
            }
            result.CombinedOwner = combined.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader;
            result.SupplierName = combined.Supplier == null ? "" : combined.Supplier.SelectedCrName;
            result.TenderStatusId = tender.TenderStatusId;
            result.CombinationLetter = combined.Offer.Attachment.Where(x => x is SupplierCombinedAttachment)
                    .Select(x => new AttachmentModel
                    {
                        FileName = x.FileName,
                        FileNetId = x.FileNetReferenceId
                    }).FirstOrDefault();
            return result;
        }

        public async Task<CheckOfferModel> GetOpenOfferDataForCheckStage(int offerid)
        {
            return await _offerQueries.GetOpenOfferDataForCheckStage(offerid);
        }

        public async Task<OfferDetailModel> GetOfferDetailsDisplayOnly(int combinedId, int userId)
        {
            OfferSolidarity combined = await _offerQueries.FindOfferByCombinedSupplierId(combinedId);
            Tender tender = await _tenderQueries.FindTenderOfferDetailsByTenderIdDisplayOnly(combined.Offer.TenderId);
            if (tender == null)
            {
                throw new UnHandledAccessException();
            }
            OfferDetailModel result = await _offerQueries.GetOfferDetails(combinedId);
            if (result == null)
            {
                result = new OfferDetailModel
                {
                    TenderID = tender.TenderId,
                    OfferId = combined.OfferId,
                    CombinedId = combinedId,
                    OfferIdString = Util.Encrypt(combined.OfferId),
                    OfferPresentationWayId = tender.OfferPresentationWayId.Value,
                    TenderIDString = Util.Encrypt(tender.TenderId)
                };
            }
            result.CombinedOwner = combined.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader;
            result.SupplierName = combined.Supplier == null ? "" : combined.Supplier.SelectedCrName;
            result.TenderStatusId = tender.TenderStatusId;
            result.CombinationLetter = combined.Offer.Attachment.Where(x => x is SupplierCombinedAttachment)
                    .Select(x => new AttachmentModel
                    {
                        FileName = x.FileName,
                        FileNetId = x.FileNetReferenceId
                    }).FirstOrDefault();
            return result;
        }

        public async Task<QueryResult<OfferFinancialDetailsModel>> OfferFinancialDetailsForTender(int tenderId)
        {
            QueryResult<OfferFinancialDetailsModel> result = await _offerQueries.OfferFinancialDetailsForTender(tenderId);
            return result;
        }

        public async Task<QueryResult<OfferFinancialDetailsModel>> GetOffersForAwardingByTenderId(int tenderId, string crsuppliers)
        {
            QueryResult<OfferFinancialDetailsModel> result = await _offerQueries.GetOffersForAwardingByTenderId(tenderId, crsuppliers);
            return result;
        }

        public async Task<QueryResult<OfferFinancialDetailsModel>> OfferAwardeFinancialDetailsForTender(int tenderId)
        {
            QueryResult<OfferFinancialDetailsModel> result = await _offerQueries.OfferAwardeFinancialDetailsForTender(tenderId);
            return result;
        }

        public async Task<Offer> UpdateOfferCheckingStatus(OfferCheckingDetailsModel offerModel)
        {
            Check.ArgumentNotNull(nameof(offerModel), offerModel);
            if (((offerModel.TenderTypeId != (int)Enums.TenderType.FirstStageTender && offerModel.TenderTypeId != (int)Enums.TenderType.ReverseBidding && offerModel.TenderTypeId != (int)Enums.TenderType.Competition) && offerModel.OfferAcceptanceStatusId == null) || offerModel.OfferTechnicalEvaluationStatusId == null)
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.OfferNotTechnicallyEvaluated);
            Offer offer = await FindById(offerModel.OfferId);
            if (offer.Tender != null && (offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed
                || (offer.Tender.TenderStatusId == (int)Enums.TenderStatus.Approved && (offer.Tender.OffersCheckingDate != null && offer.Tender.OffersCheckingDate <= DateTime.Now)))
                )
            {
                offer.Tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking);
            }
            List<TechnicianReportAttachment> technicianReportAttachments = _mapper.Map<List<TechnicianReportAttachment>>(offerModel.TechniciansReportAttachments);
            offer.UpdateTechnicianReportAttachments(technicianReportAttachments);
            offer.UpdateOfferCheckingStatus(offerModel.OfferAcceptanceStatusId, offerModel.OfferTechnicalEvaluationStatusId, offerModel.Notes, offerModel.RejectionReason);
            var result = await _offerCommands.UpdateAsync(offer);
            return result;
        }

        public async Task<OfferModel> OfferCheckingDetailsForAwarding(int tenderId, int offerId)
        {
            OfferModel result = await _offerQueries.OfferCheckingDetailsForAwarding(tenderId, offerId);
            return result;
        }

        public async Task SaveOfferAwardingValues(OfferAwardingModel offerAwardingObj)
        {

            await _offerDomainService.UpdateTenderAwardingTypeAndStoppingPeriodAndAwardingReport(offerAwardingObj.TenderId, offerAwardingObj.TenderAwardingType, offerAwardingObj.AwardingStoppingPeriod, offerAwardingObj.AwardingReportFileName,
                                    offerAwardingObj.AwardingReportFileNameId, offerAwardingObj.HasGuarantee, offerAwardingObj.AwardingDiscountPercentage, offerAwardingObj.AwardingMonths, offerAwardingObj.FinalGuaranteeDeliveryAddress);
            Offer offer = null;
            List<Offer> offers = new List<Offer>();
            List<Offer> tenderOffers = await _offerQueries.GetIdenticalOffersByTenderId(offerAwardingObj.TenderId);
            foreach (var tenderOffer in tenderOffers)
            {
                if (offerAwardingObj.OfferAwardingList == null || !offerAwardingObj.OfferAwardingList.Any(a => a.OfferId == tenderOffer.OfferId))
                {
                    tenderOffer.DeleteOfferAwardingValues(tenderOffer.OfferId);
                    await _offerCommands.UpdateAsync(tenderOffer);
                }
                else
                {
                    tenderOffer.DeleteOfferAwardingValues(tenderOffer.OfferId);
                }
            }
            if (offerAwardingObj.OfferAwardingList != null)
            {
                foreach (var item in offerAwardingObj.OfferAwardingList)
                {
                    offer = await _offerQueries.FindOfferById(item.OfferId);
                    if (offerAwardingObj.TenderAwardingType == true)
                        offer.SaveOfferAwardingValues(item.OfferId, item.AwardingValue, null, item.JustificationOfRecommendation);
                    if (offerAwardingObj.TenderAwardingType == false)
                        offer.SaveOfferAwardingValues(item.OfferId, null, item.AwardingValue, item.JustificationOfRecommendation);
                    offers.Add(offer);
                }
            }
            await _offerCommands.UpdateRangAsync(offers);
            await _offerCommands.SaveAsync();
        }


        public async Task AddExclusionReason(ExclusionReasonAwardingModel offerAwardingObj)
        {
            Offer offer = await _offerQueries.FindOfferById(offerAwardingObj.OfferId);
            await IsValidToAddExclusionReasonForTender(offer.Tender.TenderId);
            offer.AddExclusionReasonForSuppliers(offerAwardingObj.ExclusionReason);
            await _offerCommands.SaveAsync();
        }

        private async Task IsValidToAddExclusionReasonForTender(int tenderId)
        {
            var result = await _offerQueries.CanAddExclusionReasonIfTenderHasExtendOfferValidity(tenderId);
            if (result)
            {
                throw new BusinessRuleException("لايمكن اضافة سبب استبعاد لهذا المورد لأن المنافسة عليها طلب تمديد سريان عروض ساري");
            }
        }
        public async Task RemoveExclusionReason(ExclusionReasonAwardingModel offerAwardingObj)
        {
            Offer offer = await _offerQueries.FindOfferById(offerAwardingObj.OfferId);
            offer.RemoveExclusionReasonForSuppliers();
            await _offerCommands.SaveAsync();
        }


        public async Task<List<OfferDeatilsReportForTenderModel>> OfferDetailsReportForTender(int tenderId)
        {
            var result = await _offerQueries.OfferDetailsReportForTender(tenderId);
            return result;
        }

        public async Task<QueryResult<CombinedListModel>> GetAllCombinedInvitationForSupplier(CombinedSearchCriteria model)
        {
            return await _offerQueries.GetAllCombinedInvitationForSupplier(model);
        }

        public async Task<CombinedInvitationDetailsModel> GetCombinedInvitationDetails(int offerId)
        {
            var cR = _httpContextAccessor.HttpContext.User.SupplierNumber();
            var offer = await _offerQueries.GetCombinedInvitationDetailsByOfferIdAndCr(offerId, cR);
            CheckInvitationStatus(offer);
            return offer;
        }

        private static void CheckInvitationStatus(CombinedInvitationDetailsModel offer)
        {
            if (offer == null)
                throw new BusinessRuleException("تم سحب دعوة التضامن ");
            if (offer.OfferStatusId == (int)Enums.OfferStatus.Canceled)
                throw new BusinessRuleException("لقد تم سحب العرض من قبل قائد التضامن على هذه المنافسة ");
            if (offer.OfferStatusId != (int)Enums.OfferStatus.Received)
                throw new BusinessRuleException("العرض ليس مرسل ");
        }

        public async Task<QueryResult<CombinedSuppliersListModel>> GetAllCombinedSuppliers(CombinedSearchCretriaModel model)
        {
            return await _offerQueries.GetAllCombinedSuppliers(model);
        }

        public async Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliersForCombinedSuppliersCheckingStageAsync(int offerid, int userId)
        {
            QueryResult<CombinedSupplierModel> CombinedSuppliers = await _offerQueries.GetCombinedSuppliersForCombinedSuppliersCheckingStageAsync(offerid, userId);
            if (!CombinedSuppliers.Items.Any())
            {
                throw new UnHandledAccessException();
            }
            return CombinedSuppliers;
        }

        public async Task<OfferDetailsForAcceptingSolidarityInviationsModel> GetOfferDetailsByCombinedId(int combinedId, int userId)
        {
            await _offerQueries.GetCombinedWithOfferandTenderById(combinedId);
            return await _offerQueries.GetOfferDetailsByCombinedId(combinedId, userId);
        }

        #region OffersChecking
        public async Task<OfferDetailsForCheckingModel> GetOfferDetailsForFinancialChecking(string offerIdString, int userId)
        {
            var result = await _offerQueries.GetOfferDetailsForFinancialCheckingByOfferId(Util.Decrypt(offerIdString));
            if (result != null)
            {
                Tender tender = await _tenderQueries.FindTenderOfferDetailsByTenderIdForDirectPurchaseCheckStage(result.TenderID, userId);
                result = new OfferDetailsForCheckingModel
                {
                    TenderID = tender.TenderId,
                    TenderStatusId = tender.TenderStatusId,
                    OfferPresentationWayId = tender.OfferPresentationWayId.Value,
                    TenderIdString = Util.Encrypt(tender.TenderId)
                };
                result.TenderStatusId = tender.TenderStatusId;
                result.IsSupplierCombinedLeader = true;
            }
            return result;
        }

        public async Task<OfferDetailsForCheckingModel> GetOfferDetailsForTenderChecking(int combinedId, int userId)
        {
            OfferSolidarity combined = await _offerQueries.FindOfferByCombinedSupplierId(combinedId);
            Tender tender = await _tenderQueries.GetTenderByIdWithoutAnyIncluds(combined.Offer.TenderId);
            bool isLowBudget = tender.IsLowBudgetDirectPurchase == true;
            bool isUserisDirectPurchaseAssignedMember = userId == tender.DirectPurchaseMemberId;
            var result = await _offerQueries.GetOfferDetailsForDirectPurchaseChecking(combinedId);
            if (result == null)
            {
                result = new OfferDetailsForCheckingModel
                {
                    TenderID = tender.TenderId,
                    TenderTypeId = tender.TenderTypeId,
                    TenderStatusId = tender.TenderStatusId,
                    OfferIdString = Util.Encrypt(combined.OfferId),
                    CombinedId = combinedId,
                    OfferPresentationWayId = tender.OfferPresentationWayId.Value,
                    TenderIdString = Util.Encrypt(tender.TenderId)
                };

                result.TenderStatusId = tender.TenderStatusId;
                result.CombinedOwner = combined.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader;
                result.CombinationLetter = combined.Offer.Attachment.Where(x => x is SupplierCombinedAttachment)
                    .Select(x => new AttachmentModel
                    {
                        FileName = x.FileName,
                        FileNetId = x.FileNetReferenceId
                    }).FirstOrDefault();
                result.IsSpecificationAttached = true;
                result.IsSpecificationValidDate = true;
                result.IsBankGuaranteeAttached = true;
            }
            result.isLowBudgetFlow = isLowBudget;
            result.isUserisDirectPurchaseAssignedMember = isUserisDirectPurchaseAssignedMember;
            result.tenderHasGuarantee = !string.IsNullOrEmpty(tender.InitialGuaranteeAddress);
            result.OfferPresentationWayId = tender.OfferPresentationWayId ?? 0;
            result.TenderStatusId = tender.TenderStatusId;
            result.CombinedOwner = combined.SolidarityTypeId == (int)UnRegisteredSuppliersInvitationType.SolidarityLeader;
            result.SupplierName = combined.Supplier?.SelectedCrName;
            result.isOldFlow = tender.IsOldFlow(_configuration.OldFlow.EndDate.ToDateTime());
            return result;
        }

        public async Task AddTwoFilesFinancialDetails(TenderTowFilesFinancialDetailsDetails model)
        {
            Check.ArgumentNotNull(nameof(model), model);
            var offer = await _offerQueries.FindOfferById(Util.Decrypt(model.OfferIdString));
            _offerDomainService.IsValidToUpdateTwoFilesFinancialDetails(offer.Tender, offer, model);
            List<SupplierBankGuaranteeDetail> GuaranteesLst = new List<SupplierBankGuaranteeDetail>();
            foreach (var item in model.BankGuaranteeFiles)
            {
                var BankGuarantee = new SupplierBankGuaranteeDetail(item.BankGuaranteeId, Util.Decrypt(model.OfferIdString), item.IsBankGuaranteeValid, item.GuaranteeNumber, item.BankId.Value, item.Amount, item.GuaranteeStartDate, item.GuaranteeEndDate, item.GuaranteeDays);
                GuaranteesLst.Add(BankGuarantee);
            }
            offer.UpdateOfferFinantialCheckingDetails(model.IsFinaintialOfferLetterAttached, model.FinantialOfferLetterNumber, model.FinantialOfferLetterDate, model.IsFinantialOfferLetterCopyAttached, model.IsBankGuaranteeAttached);
            offer.UpdateBankGurnteesDetails(GuaranteesLst);
            if (offer.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersOpenFinancialStage)
            {
                offer.Tender.UpdateTenderStatus(Enums.TenderStatus.OffersOpenFinancialStage, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.FinancialCheckingStarted);
            }
            await _offerCommands.UpdateAsync(offer);
        }

        public async Task AddCheckingFinancialDetails(OfferDetailModel model)
        {
            Check.ArgumentNotNull(nameof(model), model);
            SupplierCombinedDetail details = await _offerQueries.GetCombinedOfferDetail(Util.Decrypt(model.CombinedIdString));
            if (details == null)
            {
                details = new SupplierCombinedDetail
                {
                    Combined = await _offerQueries.GetCombinedbyId(Util.Decrypt(model.CombinedIdString))
                };
            }
            if (details.Combined.Offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed)
            {
                details.Combined.Offer.Tender.UpdateTenderStatus(Enums.TenderStatus.OffersOpenFinancialStage, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.FinancialCheckingStarted);
            }
            List<SupplierBankGuaranteeDetail> GuaranteesLst = new List<SupplierBankGuaranteeDetail>();
            foreach (var item in model.BankGuaranteeFiles)
            {
                var BankGuarantee = new SupplierBankGuaranteeDetail(item.BankGuaranteeId, Util.Decrypt(model.OfferIdString), item.IsBankGuaranteeValid, item.GuaranteeNumber, item.BankId.Value, item.Amount, item.GuaranteeStartDate, item.GuaranteeEndDate, item.GuaranteeDays);
                GuaranteesLst.Add(BankGuarantee);
            }
            details.Combined.Offer.UpdateOfferFinantialCheckingDetails(model.IsFinaintialOfferLetterAttached, model.FinantialOfferLetterNumber, model.FinantialOfferLetterDate, model.IsFinantialOfferLetterCopyAttached, model.IsBankGuaranteeAttached);
            details.Combined.Offer.UpdateBankGurnteesDetails(GuaranteesLst);
            await _offerCommands.UpdateCombinedDetailAsync(details);
        }

        public async Task<Offer> AddTwoFilesFinancialCheck(OfferDetailsForCheckingModel m)
        {
            Offer offer = await _offerQueries.GetOfferByIdForFinancialChecking(Util.Decrypt(m.OfferIdString));
            SupplierCombinedDetail details = await _offerQueries.GetCombinedOfferDetail(Util.Decrypt(m.CombinedIdString));
            if (offer.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersOpenFinancialStage)
            {
                offer.Tender.UpdateTenderStatus(Enums.TenderStatus.OffersOpenFinancialStage, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.FinancialCheckingStarted);
            }
            if (details == null)
            {
                details = new SupplierCombinedDetail
                {
                    Combined = await _offerQueries.GetCombinedbyId(Util.Decrypt(m.CombinedIdString))
                };
            }
            List<SupplierBankGuaranteeDetail> GuaranteesLst = new List<SupplierBankGuaranteeDetail>();
            List<SupplierSpecificationDetail> SpecificationsLst = new List<SupplierSpecificationDetail>();
            foreach (var item in m.SpecificationsFiles)
            {
                var Specification = new SupplierSpecificationDetail(item.SpecificationId, Util.Decrypt(m.CombinedIdString), item.IsForApplier, item.Degree, item.ConstructionWorkId, item.MaintenanceRunningWorkId);
                SpecificationsLst.Add(Specification);
            }
            if (details.Combined?.SolidarityTypeId == (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader)
            {
                foreach (var item in m.BankGuaranteeFiles)
                {
                    var BankGuarantee = new SupplierBankGuaranteeDetail(item.BankGuaranteeId, offer.OfferId, item.IsBankGuaranteeValid, item.GuaranteeNumber, item.BankId.Value, item.Amount, item.GuaranteeStartDate, item.GuaranteeEndDate, item.GuaranteeDays);
                    GuaranteesLst.Add(BankGuarantee);
                }
                offer.UpdateOfferAttachments(m.IsOfferCopyAttached, m.IsOfferLetterAttached, m.OfferLetterNumber, m.OfferLetterDate, m.IsVisitationAttached, m.IsPurchaseBillAttached, m.IsBankGuaranteeAttached);
                offer.updateBankGurnteeList(GuaranteesLst);
            }
            offer.UpdateOfferFinancialWeight(m.FinancialWeight);
            offer.UpdateOpeningDiscountNotes(m.Discount, m.DiscountNotes);
            details.UpdateAttachmentDetails(details.CombinedDetailId, Util.Decrypt(m.CombinedIdString), m.IsChamberJoiningAttached, m.IsChamberJoiningValid,
                m.IsCommercialRegisterAttached, m.IsCommercialRegisterValid, m.IsSaudizationAttached, m.IsSaudizationValidDate, m.IsSocialInsuranceAttached,
                m.IsSocialInsuranceValidDate, m.IsZakatAttached, m.IsZakatValidDate, m.IsSpecificationAttached, m.IsSpecificationValidDate);
            details.updateAttachmentsList(SpecificationsLst);
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                TenderId = offer.TenderId,
                HasAlternative = offer.Tender.HasAlternativeOffer ?? false,
                CanEditAlternative = ((offer.OfferPresentationWayId == (int)OfferPresentationWayId.OneFile
                    && (offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersChecking || offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed))
                    || (offer.OfferPresentationWayId == (int)OfferPresentationWayId.TwoSepratedFiles
                    && (offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed || offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking))),
                ApplySelected = offer.Tender.HasAlternativeOffer ?? false,
                AllowEdit = false,
                ActivityId = 1,
                EncryptedOfferId = Util.Encrypt(offer.OfferId),
                EncryptedTenderId = Util.Encrypt(offer.TenderId),
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
                    Id = qq.Id
                }).ToList(),
                YearsCount = offer.Tender.TemplateYears ?? 0
            };
            ApiResponse<TotalCostDTO> obj;
            obj = await _qantityTemplatesProxy.GetTotalCostForChecking(DTOModel);
            if (obj.StatusCode == 200)
            {
                offer.SetFinalPrice(obj.Data.TotalCostWithdiscount);
            }
            await _offerCommands.UpdateAsync(offer);
            if (m.CombinedIdString != null)
                await _offerCommands.UpdateCombinedDetailAsync(details);
            return offer;
        }

        public async Task<Offer> AddFinantialCheckingResult(CheckOfferResultModel m)
        {
            var localContentSetting = await _offerQueries.GetLocalContentSettingsDate();
            Offer offer = await _offerQueries.GetOfferByIdForFinancialChecking(Util.Decrypt(m.OfferIdString));
            if (offer.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersFinancialChecking)
            {
                offer.Tender.UpdateTenderStatus(Enums.TenderStatus.OffersFinancialChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.FinancialCheckingStarted);
            }
            offer.UpdateOfferFinantialCheckingStatus(m.FinantialOfferStatusId, m.FinantialRejectionReason, m.FinantialNotes);
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                TenderId = offer.TenderId,
                HasAlternative = offer.Tender.HasAlternativeOffer ?? false,
                CanEditAlternative = (offer.OfferPresentationWayId == (int)OfferPresentationWayId.OneFile
                    && (offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersChecking || offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed))
                    || (offer.OfferPresentationWayId == (int)OfferPresentationWayId.TwoSepratedFiles
                    && (offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed || offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking)),
                ApplySelected = offer.Tender.HasAlternativeOffer ?? false,
                AllowEdit = false,
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
                    Id = qq.Id
                }).ToList(),
                NPpercentage = offer.Tender.NationalProductPercentage / 100 ?? 0,
                YearsCount = offer.Tender.TemplateYears ?? 0
            };
            ApiResponse<TotalCostDTO> obj;
            obj = await _qantityTemplatesProxy.GetTotalCostForChecking(DTOModel);
            bool isContainTawreed =
                 DTOModel.IsTenderContainsTawreed ||
                 offer.Tender.TenderActivities.Any(a => a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)TenderActivityTamplate.GeneralMaterials || t.TemplateId == (int)TenderActivityTamplate.GeneralSupply));

            if (offer.Tender?.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase
           && isContainTawreed)
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
            if (offer.Tender.IsValidToApplyOfferLocalContentChanges(localContentSetting.Date.Value) && offer.OfferlocalContentDetails != null)
            {
                offer.ResetOfferLocalContentPreference();
                offer.OfferlocalContentDetails.SetIsBindedToMandatoryList(m.IsBindedToMandatoryList);
            }
            offer = await _offerCommands.UpdateAsync(offer);
            return offer;
        }
        private void CheckIfUserCanAccessLowBudgetTender(bool? isTenderLowBudget, int? tenderDirectPurchasememberId, int userId)
        {
            if (isTenderLowBudget == true && tenderDirectPurchasememberId != userId)
            {
                throw new AuthorizationException();
            }

        }
        public async Task<OfferModel> GeOfferByTenderIdAndOfferIdForDirectPurchaseChecking(int tenderId, int offerId)
        {
            var userid = _httpContextAccessor.HttpContext.User.UserId();
            var tender = await _tenderQueries.FindTenderByTenderId(tenderId);
            CheckIfUserCanAccessLowBudgetTender(tender.IsLowBudgetDirectPurchase, tender.DirectPurchaseMemberId, userid);
            Offer offer = await _offerQueries.GeOfferByTenderIdAndOfferIdForChecking(offerId);
            var result = await GetOfferModel(offer);
            bool isLowBudget = tender.IsLowBudgetDirectPurchase == true;
            bool isUserisDirectPurchaseAssignedMember = userid == tender.DirectPurchaseMemberId;
            result.isLowBudgetFlow = isLowBudget;
            result.isUserisDirectPurchaseAssignedMember = isUserisDirectPurchaseAssignedMember;
            bool canEdit = (
                (result.OfferPresentationWayId == (int)OfferPresentationWayId.OneFile
                    && result.TenderStatusId == (int)Enums.TenderStatus.OffersChecking)
                    ||
                    (result.OfferPresentationWayId == (int)OfferPresentationWayId.TwoSepratedFiles
                    && (result.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed
                        || result.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking))

                        && _httpContextAccessor.HttpContext.User.UserRole() == RoleNames.OffersPurchaseSecretary
                       && !isLowBudget
                       )
                        ;
            canEdit = canEdit || (isLowBudget && isUserisDirectPurchaseAssignedMember && result.TenderStatusId == (int)Enums.TenderStatus.OffersChecking);
            result.QuantityTableStepDTO = await FindOfferQuantityItemsById(offer, !canEdit);
            if (result.OfferStatusId == (int)OfferStatus.Received && _httpContextAccessor.HttpContext.User.UserRole() == RoleNames.supplier)
                throw new UnHandledAccessException();
            return result;
        }

        public async Task<QuantitiesTablesForAwardingModel> GetOfferQuantityTableForAwarding(int tenderId, int offerId)
        {
            Offer offer = await _offerQueries.GetOfferForQuantityTableForAwarding(offerId);
            QuantitiesTablesForAwardingModel result = FillQuantitiesTablesForAwardingModel(offer);

            result.QuantityTableStepDTO = await FindOfferQuantityItemsById(offer, true);

            if (result.OfferStatusId == (int)OfferStatus.Received && _httpContextAccessor.HttpContext.User.UserRole() == RoleNames.supplier)
                throw new UnHandledAccessException();
            return result;
        }


        public async Task AddTechnicalOfferResultForTwoFilesTender(OfferDetailsForCheckingTwoFilesModel model)
        {
            var offer = await _offerQueries.FindOfferById(Util.Decrypt(model.OfferIdString));
            _offerDomainService.IsValidToUpdateTwoFilesTechnicalCheck(offer.Tender, offer, model);
            offer.UpdateOfferTecknicalCheckingStatus(model.TechnicalOfferStatusId, model.RejectionReason, model.Notes);
            if (offer.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersChecking)
            {
                offer.Tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.TechnicalEvaluation);
            }
            await _offerCommands.UpdateAsync(offer);
        }

        public async Task<QueryResult<TableModel>> ValidateandSaveCheckingQuantitiesTable(Dictionary<string, string> AuthorList)
        {
            int offerId = Util.Decrypt(AuthorList["EncryptedOfferId"]);
            int formId = int.Parse(AuthorList["FormId"]);
            int tableId = int.Parse(AuthorList["TableId"]);
            var offer = await _offerQueries.FindOfferWithStatusById(offerId);
            var tender = offer.Tender;
            CheckIfUserCanAccessLowBudgetTender(tender.IsLowBudgetDirectPurchase, tender.DirectPurchaseMemberId, _httpContextAccessor.HttpContext.User.UserId());

            var Qitems = await _offerQueries.GetSupplierQTableItemsByTableId(offerId, tableId);
            foreach (KeyValuePair<string, string> entry in AuthorList)
            {
                if (!entry.Key.Contains("IsDefault"))
                    continue;
                var _itemNumber = int.Parse(entry.Key.Split('_')[2]);
                var SQTItem = Qitems.Where(d => d.ItemNumber == _itemNumber).Select(d => d.Id).ToList();
                bool isDefualt = bool.Parse(entry.Value.ToString());
                Dictionary<long, bool> keyValues = SQTItem.Select(x => new KeyValuePair<long, bool>(x, isDefualt)).ToDictionary(x => x.Key, x => x.Value);
                offer.UpdateOfferSupplierQItemsDefault(keyValues, tableId);
            }
            foreach (KeyValuePair<string, string> entry in AuthorList)
            {
                if (!entry.Key.Contains("EditIsVerifiedMandatory_"))
                    continue;
                var _Id = long.Parse(entry.Key.Split('_')[1]);
                offer.SupplierTenderQuantityTables.FirstOrDefault(a => a.Id == tableId).QuantitiyItemsJson.SupplierTenderQuantityTableItems.FirstOrDefault(a => a.Id == _Id).AlternativeValue = entry.Value;
            }
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                SubmitAction = "/Offer/SaveCheckingQuantityItem",
                TenderId = offer.TenderId,
                HasAlternative = offer.Tender.HasAlternativeOffer ?? false,
                CanEditAlternative = (offer.OfferPresentationWayId == (int)OfferPresentationWayId.OneFile
                    && (offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersChecking || offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed))
                    || (offer.OfferPresentationWayId == (int)OfferPresentationWayId.TwoSepratedFiles
                    && (offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed || offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking)),
                ApplySelected = offer.Tender.HasAlternativeOffer ?? false,
                AllowEdit = false,
                ActivityId = 1,
                EncryptedOfferId = Util.Encrypt(offer.OfferId),
                EncryptedTenderId = Util.Encrypt(offer.TenderId),
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
                    Id = qq.Id
                }).ToList(),
                YearsCount = (offer.Tender.TemplateYears ?? 0)
            };
            ApiResponse<List<TableTemplateDTO>> obj;
            obj = await _qantityTemplatesProxy.ValidateCheckingData(DTOModel);
            if (obj.StatusCode == 200)
            {
                var items = obj.Data.FirstOrDefault().QuantityItemDtos.Select(item => new SupplierTenderQuantityTableItem()
                {
                    ColumnId = item.ColumnId,
                    Id = item.Id,
                    TenderFormHeaderId = item.TenderFormHeaderId,
                    ActivityTemplateId = item.TemplateId,
                    Value = item.Value,
                    AlternativeValue = item.AlternativeValue,
                    IsDefault = item.IsDefault,
                    ItemNumber = item.ItemNumber
                }).ToList();
                offer.UpdateOfferSupplierQItems(items, tableId);
                ApiResponse<TotalCostDTO> calcObj = await _qantityTemplatesProxy.GetTotalCostForChecking(DTOModel);
                if (obj.StatusCode == 200)
                {
                    offer.UpdateFinalPriceAfterDiscount(calcObj.Data.TotalCostWithdiscount);
                }
                await _offerCommands.UpdateAsync(offer);
                await _offerCommands.SaveAsync();
            }
            return await GetOfferTableQuantityItems(new QuantityTableSearchCretriaModel { QuantityTableId = tableId, OfferId = offerId, FormId = formId });
        }

        #endregion

        #endregion QuantityTable

        #region Bidding Round
        public async Task AddOfferBid(string tenderIdString, string biddingIdString, decimal biddingValue, string cR)
        {
            int tenderId = Util.Decrypt(tenderIdString);
            int biddingRoundId = Util.Decrypt(biddingIdString);
            var tender = await _tenderQueries.FindTenderForAddingBiddingOfferByTenderId(tenderId);
            var offers = await _tenderQueries.FindTenderOffersForBiddingRound(tenderId);
            _offerDomainService.IsValidAddBidding(tender, offers, biddingRoundId, biddingValue, cR);
            tender.AddBiddingRoundOfffer(tender.BiddingRounds.FirstOrDefault(r => r.BiddingRoundStatusId == (int)Enums.BiddingRoundStatus.Started &&
                        r.StartDate <= DateTime.Now && DateTime.Now < r.EndDate).BiddingRoundId,
                        offers.FirstOrDefault(o => o.CommericalRegisterNo == cR && o.IsActive == true).OfferId, biddingValue);
            await _tenderCommands.UpdateAsync(tender);
        }
        #endregion Bidding Round

        #endregion

        #region [Alternative Offer]

        public async Task<AlternativeItemResponseModel> DeleteAlternativeItem(int alternativeItemId, int tableId)
        {
            var table = await _offerQueries.GetSupplierQTableByTableId(tableId);
            table.RemoveAlternative();
            AlternativeItemResponseModel alternativeItemResponseModel;

            await _offerCommands.UpdateQuantityTableAsync(table);
            await _offerCommands.SaveAsync();
            alternativeItemResponseModel = new AlternativeItemResponseModel { IsSuccess = true, ResponseMessage = Resources.SharedResources.Messages.DataSaved };
            return alternativeItemResponseModel;
        }

        #endregion

        public async Task<OfferFullDetailsModel> GetOfferFullDetails(int offerId)
        {
            var offer = await _offerQueries.FindOfferFullDetailsModelbyOfferId(offerId, _httpContextAccessor.HttpContext.User.SupplierNumber());
            var Qitems = await _offerQueries.GetSupplierQTableItems(offerId, Util.Decrypt(offer.TenderIdString));
            FormConfigurationDTO DTOModel = new FormConfigurationDTO() { TenderId = Util.Decrypt(offer.TenderIdString), ActivityId = 1, QuantityItemDtos = Qitems, YearsCount = offer.yearsCount };
            ApiResponse<TotalCostDTO> obj = await _qantityTemplatesProxy.CalculateOfferFinalPricebyItems(DTOModel);
            if (obj.StatusCode == 200)
            {
                offer.FinalPrice = obj.Data.TotalCostWithdiscount == 0 && obj.Data.TotalCostWithOutdiscount > 0 ? obj.Data.TotalCostWithOutdiscount : obj.Data.TotalCostWithdiscount;
                offer.FinalAlternativePrice = obj.Data.AlternativeTotalCostWithdiscount;
                if (offer.FinalPrice > 0)
                {
                    try
                    {
                        offer.FinalPriceText = new ConvertNumberToText(offer.FinalPrice, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia)).ConvertToArabic();
                    }
                    catch
                    {
                        offer.FinalPriceText = offer.FinalPrice.ToString();
                    }
                }
                if (offer.FinalAlternativePrice > 0)
                {
                    try
                    {
                        offer.FinalAlternativePriceText = new ConvertNumberToText(offer.FinalAlternativePrice, new CurrencyInfo(CurrencyInfo.Currencies.SaudiArabia)).ConvertToArabic();
                    }
                    catch
                    {
                        offer.FinalAlternativePriceText = offer.FinalAlternativePrice.ToString();
                    }
                }
            }
            offer.QuantitiesTemplateModel = await FindApplyOfferForms(Util.Decrypt(offer.TenderIdString), offerId, true);
            return offer;
        }

        public async Task AcceptCombinedInvitation(int combinedId)
        {
            var SupplierName = _httpContextAccessor.HttpContext.User.SupplierName();
            var combined = await _offerQueries.GetCombinedWithOfferAndTenderById(combinedId);
            Check.ArgumentNotNull(nameof(combined), combined);
            _offerDomainService.IsVaildToAcceptOrRejectCombinedInvitation(combined);
            combined.UpdateStatus(SupplierSolidarityStatus.Approved);
            await _offerCommands.UpdateCombinedAsync(combined);
            NotificationArguments RejectCombineRequest = new NotificationArguments
            {
                BodyEmailArgs = new object[] { combined.Offer.Supplier.SelectedCrName, SupplierName, combined.Offer.Tender.TenderName },
                SubjectEmailArgs = new object[] { combined.Offer.Supplier.SelectedCrName, SupplierName, combined.Offer.Tender.TenderName },
                PanelArgs = new object[] { combined.Offer.Supplier.SelectedCrName, SupplierName, combined.Offer.Tender.TenderName },
                SMSArgs = new object[] { combined.Offer.Supplier.SelectedCrName, SupplierName, combined.Offer.Tender.TenderName }
            };
            MainNotificationTemplateModel templetModel = new MainNotificationTemplateModel(RejectCombineRequest,
                $"Tender/Details?STenderId={Util.Encrypt(combined.Offer.Tender.TenderId)}",
                 NotificationEntityType.Tender,
                  combined.Offer.Tender.TenderId.ToString(),
               combined.Offer.Tender.BranchId);
            List<string> Crs = new List<string>
            {
                combined.Offer.CommericalRegisterNo
            };
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.Solidarity.AcceptSolidarityInvitation, Crs, templetModel);
        }

        public async Task RejectCombinedInvitation(int combinedId)
        {
            var SupplierName = _httpContextAccessor.HttpContext.User.SupplierName();
            var combined = await _offerQueries.GetCombinedWithOfferAndTenderById(combinedId);
            Check.ArgumentNotNull(nameof(combined), combined);
            _offerDomainService.IsVaildToAcceptOrRejectCombinedInvitation(combined);
            combined.UpdateStatus(Enums.SupplierSolidarityStatus.Rejected);
            await _offerCommands.UpdateCombinedAsync(combined);
            NotificationArguments RejectCombineRequest = new NotificationArguments
            {
                BodyEmailArgs = new object[] { combined.Offer.Supplier.SelectedCrName, SupplierName, combined.Offer.Tender.TenderName },
                SubjectEmailArgs = new object[] { combined.Offer.Supplier.SelectedCrName, SupplierName, combined.Offer.Tender.TenderName },
                PanelArgs = new object[] { combined.Offer.Supplier.SelectedCrName, SupplierName, combined.Offer.Tender.TenderName },
                SMSArgs = new object[] { combined.Offer.Supplier.SelectedCrName, SupplierName, combined.Offer.Tender.TenderName }
            };
            MainNotificationTemplateModel templetModel = new MainNotificationTemplateModel(RejectCombineRequest,
                $"Tender/Details?STenderId={Util.Encrypt(combined.Offer.Tender.TenderId)}",
                 NotificationEntityType.Tender,
                  combined.Offer.Tender.TenderId.ToString(),
               combined.Offer.Tender.BranchId);
            List<string> Crs = new List<string>
            {
                combined.Offer.CommericalRegisterNo,
                combined.Offer.CommericalRegisterNo
            };
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.Solidarity.RejectSolidarityInvitation, Crs, templetModel);
        }

        #region Offer New

        public async Task<OfferQuantityTableHtmlVM> GetQuantityTables(int offerId)
        {
            QuantitiesTemplateModel lst = new QuantitiesTemplateModel();
            var offer = await _offerQueries.FindOfferWithStatusById(offerId);
            var Qitems = await _offerQueries.GetSupplierQTableItems(offerId, offer.TenderId);
            lst.QuantitiesItems = Qitems;
            #region Test Obj
            #endregion
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                TenderId = offer.TenderId,
                HasAlternative = offer.Tender.HasAlternativeOffer ?? false,
                ActivityId = 1,
                EncryptedOfferId = Util.Encrypt(offer.OfferId),
                EncryptedTenderId = Util.Encrypt(offer.TenderId),
                QuantityItemDtos = lst.QuantitiesItems.ToList(),
                YearsCount = offer.Tender.TemplateYears ?? 0
            };
            ApiResponse<List<HtmlTemplateDto>> obj;
            if (offer.OfferStatusId == (int)OfferStatus.UnderEstablishing || offer.OfferStatusId == (int)OfferStatus.Established || offer.OfferStatusId == (int)OfferStatus.Canceled)
            {
                obj = await _qantityTemplatesProxy.GenerateSupplierTemplate(DTOModel);
                OfferQuantityTableHtmlVM offerQuantityTableHtmlVM = new OfferQuantityTableHtmlVM
                {
                    offerStatusModel = new OfferStatusModel { offerIdString = Util.Encrypt(offer.OfferId), OfferStatusId = offer.OfferStatusId, offerStatusName = offer.Status.NameAr }
                };
                if (obj.StatusCode == 200)
                {
                    foreach (var tbl in obj.Data)
                    {
                        offerQuantityTableHtmlVM.tableFormHtmls.Add(new TableFormHtml
                        {
                            TemplateName = tbl.TemplateName,
                            FormId = tbl.FormId,
                            TableName = tbl.FormName,
                            FormHtml = tbl.FormHtml,
                            tableId = tbl.FormId
                        });
                    }
                }
                return offerQuantityTableHtmlVM;
            }
            else
            {
                obj = await _qantityTemplatesProxy.GenerateSupplierReadOnlyTemplate(DTOModel);
                OfferQuantityTableHtmlVM offerQuantityTableHtmlVM = new OfferQuantityTableHtmlVM
                {
                    offerStatusModel = new OfferStatusModel { offerIdString = Util.Encrypt(offer.OfferId), OfferStatusId = offer.OfferStatusId, offerStatusName = offer.Status.NameAr }
                };
                if (obj.StatusCode == 200)
                {
                    foreach (var tbl in obj.Data)
                    {
                        offerQuantityTableHtmlVM.tableFormHtmls.Add(new TableFormHtml { TemplateName = tbl.TemplateName, FormId = tbl.FormId, TableName = tbl.FormName, FormHtml = tbl.FormHtml, tableId = tbl.FormId });
                    }
                }
                return offerQuantityTableHtmlVM;
            }
        }

        public async Task<OfferQuantityTableHtmlVM> GetQuantityTablesReadOnly(int offerId)
        {
            QuantitiesTemplateModel lst = new QuantitiesTemplateModel();
            var offer = await _offerQueries.FindOfferWithStatusById(offerId);
            var Qitems = await _offerQueries.GetSupplierQTableItems(offerId, offer.TenderId);
            lst.QuantitiesItems = Qitems;
            #region Test Obj
            #endregion
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                TenderId = offer.TenderId,
                ActivityId = 1,
                EnablePaging = true,
                HasAlternative = offer.Tender.HasAlternativeOffer ?? false,
                QuantityItemDtos = lst.QuantitiesItems.ToList(),
                YearsCount = offer.Tender.TemplateYears ?? 0
            };
            ApiResponse<List<HtmlTemplateDto>> obj = await _qantityTemplatesProxy.GenerateSupplierReadOnlyTemplate(DTOModel);
            OfferQuantityTableHtmlVM offerQuantityTableHtmlVM = new OfferQuantityTableHtmlVM
            {
                offerStatusModel = new OfferStatusModel { offerIdString = Util.Encrypt(offer.OfferId), OfferStatusId = offer.OfferStatusId, offerStatusName = offer.Status.NameAr }
            };
            if (obj.StatusCode == 200)
            {
                foreach (var tbl in obj.Data)
                {
                    offerQuantityTableHtmlVM.tableFormHtmls.Add(new TableFormHtml { TemplateName = tbl.TemplateName, FormId = tbl.FormId, TableName = tbl.FormName, FormHtml = tbl.FormHtml, tableId = tbl.FormId });
                }
            }
            return offerQuantityTableHtmlVM;
        }

        public async Task<OfferFileVModel> GetOfferFileVModel(int offerId)
        {
            var offer = await _offerQueries.GetOfferWithQuantityTablesForApplyOfferById(offerId);
            var tender = await _tenderAppService.FindTenderByTenderId(offer.TenderId);
            var cr = _httpContextAccessor.HttpContext.User.SupplierNumber();
            ValidateOfferAndTenderForApplyOfferSteps(offer, tender, cr);
            OfferFileVModel offerFileVModels = await _offerQueries.FindOfferFileVModelByOfferId(offerId);
            return offerFileVModels;
        }

        private static void ValidateOfferAndTenderForApplyOfferSteps(Offer offer, Tender tender, string cr)
        {
            if (offer.CommericalRegisterNo != cr)
            {
                throw new BusinessRuleException("غير مسموح عرض بيانات العرض");
            }
            if (offer.OfferStatusId == (int)Enums.OfferStatus.Received)
            {
                throw new BusinessRuleException("غير مسموح عرض بيانات العرض");
            }
            if (tender.TenderStatusId != (int)Enums.TenderStatus.Approved)
            {
                throw new BusinessRuleException("المنافسة ليست فى المرحلة الصحيحة لتقديم العرض");
            }
            if (tender.OffersOpeningDate <= DateTime.Now || tender.LastOfferPresentationDate < DateTime.Now)
            {
                throw new BusinessRuleException("لقت انتهى وقت تقديم العروض");
            }
        }

        public async Task<OfferMainVModel> GetOfferMainVModel(int offerId, int tenderId)
        {
            var cr = _httpContextAccessor.HttpContext.User.SupplierNumber();
            var tender = await _tenderAppService.FindTenderByTenderId(tenderId);
            CheckTenderStatusAvailabilty(tender);
            Offer offer = null;
            OfferMainVModel mainVModel;
            //1 - if supplier has offer( not sent) first check by offer id
            if (offerId != 0)
            {
                mainVModel = await _offerQueries.FindActiveOfferMainVModelByOfferId(offerId, cr);
                if (mainVModel == null) throw new BusinessRuleException("غير مسموح عرض بيانات العرض");
                if (mainVModel.OfferStatusId == (int)OfferStatus.Canceled) // if offer was withdraw
                {
                    offer = await _offerQueries.FindOfferWithCombinedById(offerId);
                    await RemoveSolidarityCrsWhoCombinedOtherOffers(offer, mainVModel);
                    offer.UpdateStatus(Enums.OfferStatus.Established);
                    await _offerCommands.UpdateAsync(offer);
                }
                return mainVModel;
            }
            else if (offerId == 0)
                offer = await _offerQueries.GetOfferByTenderIdAndCROfOfferOwner(tenderId, cr);
            if (offer != null)
            {
                // 2 - if supplier have  offer 
                offerId = offer.OfferId;
                mainVModel = new OfferMainVModel()
                {
                    offerOwner = offer.CommericalRegisterNo == cr,
                    hasOffer = true,
                    isSolidarity = false,
                    OfferStatusId = offer.OfferStatusId,
                    offerIdString = Util.Encrypt(offer.OfferId),
                    tenderIdString = Util.Encrypt(tenderId),
                    tenderTypeId = tender.TenderTypeId,
                    CR = cr
                };
                if (offer.OfferStatusId == (int)OfferStatus.Canceled)
                {
                    offer = await _offerQueries.FindOfferWithCombinedById(offerId);
                    await RemoveSolidarityCrsWhoCombinedOtherOffers(offer, mainVModel);
                    offer.UpdateStatus(OfferStatus.Established);
                    await _offerCommands.UpdateAsync(offer);
                }
                return mainVModel;
            }
            else
                offer = await _offerQueries.GetOfferByTenderIdAndSolidarityCR(tenderId, cr); // 3- Check if Combined 
            if (offer != null)
                throw new BusinessRuleException("غير مسموح بتقديم عرض لأنك متضامن مع مورد اخر فى نفس المنافسة");
            return new OfferMainVModel() { CR = cr, isSolidarity = false, OfferStatusId = 0, tenderIdString = Util.Encrypt(tenderId), tenderTypeId = tender.TenderTypeId };
        }

        private async Task RemoveSolidarityCrsWhoCombinedOtherOffers(Offer offer, OfferMainVModel mainVModel)
        {
            var solidarityCRs = offer.Combined.Where(d => d.SolidarityTypeId != (int)UnRegisteredSuppliersInvitationType.SolidarityLeader).ToList();
            if (solidarityCRs.Any())
            {
                var crsThatshouldbyIncluded = await _offerQueries.GetSolidaritybyCRs(solidarityCRs.Select(d => d.CRNumber).ToList(), offer.TenderId);
                foreach (var solidarity in crsThatshouldbyIncluded)
                {
                    offer.RemoveRegisteredSupplierCombined(solidarity);
                }
                if (crsThatshouldbyIncluded.Any())
                {
                    mainVModel.SolidarityMessage = " تم مسح   بعض المتضامنين لأنهم  تضامنوا فى عرض اخر على نفس المنافسة";
                }
            }
        }

        private static void CheckTenderStatusAvailabilty(Tender tender)
        {
            if (tender.TenderStatusId != (int)Enums.TenderStatus.Approved)
            {
                throw new BusinessRuleException("المنافسة ليست فى المرحلة الصحيحة لتقديم العرض");
            }
            if (tender.OffersOpeningDate <= DateTime.Now || tender.LastOfferPresentationDate < DateTime.Now)
            {
                throw new BusinessRuleException("لقت انتهى وقت تقديم العروض");
            }
        }

        public async Task<OfferMainVModel> AddOfferMainVModel(OfferMainVModel offerMainVModel)
        {
            OfferMainVModel offerFileVModels = offerMainVModel;
            var CR = _httpContextAccessor.HttpContext.User.SupplierNumber();
            var tender = await _tenderQueries.FindTenderByTenderId(Util.Decrypt(offerMainVModel.tenderIdString));
            if (tender.TenderStatusId != ((int)Enums.TenderStatus.Approved))
            {
                throw new BusinessRuleException("غير مسموح بتقديم العروض ");
            }
            if (string.IsNullOrEmpty(offerMainVModel.offerIdString))
            {
                var SupplierOffer = await _offerQueries.FindOfferWithQuantityTableByTenderIdAndCR(tender.TenderId, CR);
                if (SupplierOffer != null)
                {
                    throw new BusinessRuleException("لديك عرض بالفعل  ");
                }
                var supDataList = new List<SupplierTenderQuantityTable>();
                var QtableDTOs = await _tenderQueries.getTenderQuantitytableItems(Util.Decrypt(offerMainVModel.tenderIdString));
                FormConfigurationDTO DTOModel = new FormConfigurationDTO()
                {
                    TenderId = tender.TenderId,
                    ActivityId = 1,
                    QuantityItemDtos = new List<TenderQuantityItemDTO>(),
                    YearsCount = tender.TemplateYears ?? 1
                };
                Dictionary<long, List<SupplierTenderQuantityTableItem>> tablesItemsJson = new Dictionary<long, List<SupplierTenderQuantityTableItem>>();
                foreach (var table in QtableDTOs.Where(s => s.SupplierQItems.Any()).ToList())
                {
                    var supplierItems = table.SupplierQItems.Select(item => new TenderQuantityItemDTO
                    {
                        ColumnId = item.ColumnId,
                        ItemNumber = item.ItemNumber,
                        TableId = table.TableId,
                        TemplateId = 1,
                        TableName = table.TableName,
                        TenderFormHeaderId = item.TenderFormHeaderId,
                        TenderId = table.TenderId,
                        Value = item.Value
                    });
                    #region LOOP
                    #endregion
                    DTOModel.QuantityItemDtos = new List<TenderQuantityItemDTO>();
                    DTOModel.QuantityItemDtos.AddRange(supplierItems);
                    ApiResponse<FormConfigurationDTO> allColumns = await _qantityTemplatesProxy.GetMonafasatSupplierColumns(DTOModel);
                    var dataColumns = new SupplierTenderQuantityTable(table.TableName, table.TableId, null);


                    var items = allColumns.Data?.QuantityItemDtos.Select(item => new SupplierTenderQuantityTableItem()
                    {
                        Id = BitConverter.ToInt64(Guid.NewGuid().ToByteArray(), 0),
                        ColumnId = item.ColumnId,
                        TenderFormHeaderId = item.TenderFormHeaderId,
                        ActivityTemplateId = item.TemplateId,
                        Value = item.Value,
                        ItemNumber = item.ItemNumber,
                        IsDefault = true
                    }).ToList();

                    tablesItemsJson.Add(table.TableId, items);
                    supDataList.Add(dataColumns);
                }
                Offer offer = new Offer(Util.Decrypt(offerMainVModel.tenderIdString), CR, supDataList, tender.OfferPresentationWayId, offerMainVModel.isSolidarity);
                await _offerCommands.CreateAsync(offer);
                foreach (var table in tablesItemsJson)
                {
                    await _offerQueries.addOfferQTItemsJson(table.Value, table.Key, offer.OfferId);
                }
                offerFileVModels = await _offerQueries.FindActiveOfferMainVModelByOfferId(offer.OfferId, CR);
            }
            else
            {
                var offer = await _offerQueries.GetOfferWithCombinedById(Util.Decrypt(offerMainVModel.offerIdString));
                if (offer.CommericalRegisterNo != _httpContextAccessor.HttpContext.User.SupplierNumber())
                {
                    throw new BusinessRuleException(" غير مسموح  عرض بيانات العرض");
                }
                if (!offerMainVModel.isSolidarity && offer.Combined.Any(s => s.SolidarityTypeId != (int)UnRegisteredSuppliersInvitationType.SolidarityLeader))
                {
                    foreach (var solid in offer.Combined.Where(s => s.SolidarityTypeId != (int)UnRegisteredSuppliersInvitationType.SolidarityLeader))
                    {
                        offer.RemoveRegisteredSupplierCombined(solid);
                    }
                }
                offer.UpdateSolidarity(offerMainVModel.isSolidarity);
                await _offerCommands.UpdateAsync(offer);
            }
            return offerFileVModels;
        }

        public async Task<OfferFileVModel> Addofferfiles(OfferFileVModel offerFileVModel)
        {
            var offer = await _offerQueries.FindOfferById(Util.Decrypt(offerFileVModel.offerIdString));
            var SolidarityFiles = offerFileVModel.SolidarityFiles.Select(d => new SupplierCombinedAttachment(d.FileName, d.FileNetReferenceId));
            var FinantialAttachments = offerFileVModel.FinancialFiles.Select(d => new SupplierFinancialProposalAttachment(d.FileName, d.FileNetReferenceId));
            var TechnicalAttachments = offerFileVModel.TechnicalFiles.Select(d => new SupplierTechnicalProposalAttachment(d.FileName, d.FileNetReferenceId));
            var TechnicalandFinantialAttachments = offerFileVModel.TechnicalandFinancialFiles.Select(d => new SupplierFinancialandTechnicalProposalAttachment(d.FileName, d.FileNetReferenceId));
            offer.DeleteAttachment();
            offer.AddAttachment(TechnicalAttachments);
            offer.AddAttachment(FinantialAttachments);
            offer.AddAttachment(SolidarityFiles);
            offer.AddAttachment(TechnicalandFinantialAttachments);
            await _offerCommands.UpdateAsync(offer);
            var offermodel = await _offerQueries.FindOfferToCheckTenderTypeByOfferId(offer.OfferId);
            return offermodel;
        }

        public async Task<SupplierAttachmentPartialVModel> GetSupplierAllFiles(int OfferId)
        {
            var offer = await _offerQueries.FindOfferByIdWithAttachementsModel(OfferId);
            return offer;
        }

        public async Task DeleteAttachement(int Id, string suplierCR)
        {
            SupplierAttachment attachment = await _offerQueries.FindSupplierAttachmentById(Id);
            var offer = await _offerQueries.FindOfferById(attachment.OfferId);
            if (offer.CommericalRegisterNo != suplierCR)
            {
                throw new BusinessRuleException("Not Allowed");
            }
            offer.RemoveAttachment(attachment);
            await _offerCommands.UpdateAsync(offer);
            await _offerCommands.SaveAsync();
        }

        public async Task DeleteAttachement(string referenceNumber)
        {
            SupplierAttachment attachment = await _offerQueries.FindSupplierAttachmentById(referenceNumber);
            if (attachment != null)
            {
                var offer = await _offerQueries.FindOfferById(attachment.OfferId);
                offer.RemoveAttachment(attachment);
                await _offerCommands.UpdateAsync(offer);
                await _offerCommands.SaveAsync();
            }
        }

        public async Task<OfferQuantityTableModel> GetApplyOfferQuantityTableModel(int offerId)
        {
            var offer = await _offerQueries.FindOfferById(offerId);
            var tender = await _tenderAppService.FindTenderByTenderId(offer.TenderId);
            var cr = _httpContextAccessor.HttpContext.User.SupplierNumber();
            ValidateOfferAndTenderForApplyOfferSteps(offer, tender, cr);
            OfferQuantityTableModel offerQuantityTableModel = await _offerQueries.FindOfferQuantityTableModel(offerId);
            offerQuantityTableModel.QuantitiesTemplateModel = await FindApplyOfferQuantityItemsById(tender.TenderId, offerId, _httpContextAccessor.HttpContext.User.UserBranch(), false);
            return offerQuantityTableModel;
        }

        public async Task<OfferQuantityTableModel> GetApplyOfferQuantityTableStepModel(int offerId)
        {
            var offer = await _offerQueries.GetOfferWithTenderById(offerId);
            var cr = _httpContextAccessor.HttpContext.User.SupplierNumber();
            ValidateOfferAndTenderForApplyOfferSteps(offer, offer.Tender, cr);
            OfferQuantityTableModel offerQuantityTableModel = await _offerQueries.FindOfferQuantityTableModel(offerId);
            offerQuantityTableModel.QuantitiesTemplateModel = await FindApplyOfferForms(offer.Tender.TenderId, offerId, false);
            return offerQuantityTableModel;
        }

        public async Task<OfferQuantityTableModel> GetOfferQuantityTableModel(int offerId)
        {
            var offer = await _offerQueries.FindOfferById(offerId);
            var tender = await _tenderAppService.FindTenderByTenderId(offer.TenderId);
            var cr = _httpContextAccessor.HttpContext.User.SupplierNumber();
            ValidateOfferAndTenderForApplyOfferSteps(offer, tender, cr);
            OfferQuantityTableModel offerQuantityTableModel = await _offerQueries.FindOfferQuantityTableModel(offerId);
            return offerQuantityTableModel;
        }

        public async Task<string> ValidateandSaveQuantitiesTable(Dictionary<string, string> AuthorList)
        {
            int offerId = Util.Decrypt(AuthorList["EncryptedOfferId"]);
            int tableId = int.Parse(AuthorList["TableId"]);
            var offer = await _offerQueries.FindOfferWithSupplierQTItemsForApplyOfferByIdAndTableId(offerId, tableId);
            var QitemsModel = offer.SupplierTenderQuantityTables.ToList();

            List<TenderQuantityItemDTO> Qitems = new List<TenderQuantityItemDTO>();
            string tablename = "";
            int TenderId = 0;
            foreach (var tbl in QitemsModel)
            {
                TenderId = offer.TenderId;
                tablename = tbl.TenderQuantityTable.Name;
                var itemsNumbers = AuthorList["ItemNumber"].Split(',').Distinct().ToList();
                foreach (var s in tbl.QuantitiyItemsJson.SupplierTenderQuantityTableItems.Where(x => itemsNumbers.Contains(x.ItemNumber.ToString())).ToList())
                {
                    string colId = s.Id.ToString();
                    var supItems = AuthorList.Count(d => d.Key == colId);
                    Qitems.Add(new TenderQuantityItemDTO
                    {
                        ColumnId = s.ColumnId,
                        ItemNumber = s.ItemNumber,
                        ColumnName = "",
                        TableName = tablename,
                        TableId = tbl.Id,
                        TemplateId = s.ActivityTemplateId,
                        TenderId = TenderId,
                        TenderFormHeaderId = s.TenderFormHeaderId,
                        IsDefault = s.IsDefault,
                        Id = s.Id,
                        Value = (supItems == 0 ? s.Value : AuthorList.FirstOrDefault(d => d.Key == colId).Value),
                        AlternativeValue = s.AlternativeValue
                    });
                }
            }
            var HasAlternative = (offer.Tender.HasAlternativeOffer ?? false);
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                HasAlternative = HasAlternative,
                TenderId = offer.TenderId,
                ActivityId = 1,
                EncryptedOfferId = Util.Encrypt(offer.OfferId),
                EncryptedTenderId = Util.Encrypt(offer.TenderId),
                QuantityItemDtos = Qitems,
                YearsCount = (offer.Tender.TemplateYears ?? 0)
            };

            ApiResponse<List<TableTemplateDTO>> obj = await _qantityTemplatesProxy.ValidateSupplierData(DTOModel);
            if (obj.StatusCode == 200)
            {
                var items = obj.Data.FirstOrDefault().QuantityItemDtos.Select(item => new SupplierTenderQuantityTableItem()
                {
                    ColumnId = item.ColumnId,
                    Id = item.Id,
                    TenderFormHeaderId = item.TenderFormHeaderId,
                    ActivityTemplateId = item.TemplateId,
                    Value = item.Value,
                    ItemNumber = item.ItemNumber
                }).ToList();
                offer.UpdateOfferSupplierQItems(items, tableId);
                await _offerCommands.UpdateAsync(offer);
                await _offerCommands.SaveAsync();
            }
            else
            {
                throw new BusinessRuleException(obj.Message);
            }
            return obj.Data.FirstOrDefault().FormHtml;
        }

        public async Task<ValidationReturndTemplate> ValidateSupplierAlternativeItem(Dictionary<string, string> AuthorList)
        {
            int offerId = Util.Decrypt(AuthorList["EncryptedOfferId"]);
            int tableId = int.Parse(AuthorList["TableId"]);
            var offer = await _offerQueries.FindOfferWithStatusById(offerId);
            int yearsCount = offer.Tender.TemplateYears ?? 0;
            ApiResponse<HtmlTableRowTemplateDto> obj = await _qantityTemplatesProxy.ValidateAlternativeQuantityItem(yearsCount, AuthorList);
            if (obj.StatusCode == 200)
            {
                var items = obj.Data.QuantityItemDto.Select(item => new SupplierTenderQuantityTableItem()
                {
                    ColumnId = item.ColumnId,
                    Id = item.Id,
                    TenderFormHeaderId = item.TenderFormHeaderId,
                    ActivityTemplateId = item.TemplateId,
                    Value = item.Value,
                    AlternativeValue = item.AlternativeValue,
                    ItemNumber = item.ItemNumber
                }).ToList();
                offer.UpadteSupplierQTableItemsAlternativeValue(items, tableId);
                await _offerCommands
                    .UpdateAsync(offer);
                await _offerCommands.SaveAsync();
            }
            else
            {
                throw new BusinessRuleException(obj.Message);
            }
            return new ValidationReturndTemplate() { tableId = obj.Data.TableId, FormHtml = obj.Data.FormHtml };
        }

        public async Task<OfferSolidarityModel> GetOfferSolidarity(int offerId)
        {
            var offer = await _offerQueries.FindOfferById(offerId);
            var tender = await _tenderAppService.FindTenderByTenderId(offer.TenderId);
            if (tender.LastOfferPresentationDate < DateTime.Now)
            {
                throw new BusinessRuleException("غير مسموح عرض بيانات العرض");
            }
            var cr = _httpContextAccessor.HttpContext.User.SupplierNumber();
            if (offer.CommericalRegisterNo != cr)
            {
                throw new BusinessRuleException("غير مسموح عرض بيانات العرض");
            }
            var offerModel = await _offerQueries.FindOfferSolidarity(offerId);
            CheckSecurityAndBusinessValidations(offerModel);
            return offerModel;
        }

        private void CheckSecurityAndBusinessValidations(OfferSolidarityModel offer)
        {
            if (offer.CommericailNo != _httpContextAccessor.HttpContext.User.SupplierNumber())
            {
                throw new BusinessRuleException("غير مسموح ");
            }
            if (offer.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects || offer.TenderTypeId == (int)Enums.TenderType.CurrentTender || offer.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase)
            {
                throw new BusinessRuleException("لايوجد تضمان لهذا النوع من المنافسات");
            }
            if (!offer.IsSolidarity)
            {
                throw new BusinessRuleException("العرض ليس بالتضامن");
            }
        }

        #endregion

        #region Opening Offer New

        public async Task<QuantitiesTemplateModel> GetQuantityTablesForOpeningTest(int offerId, bool allowEdit)
        {
            var offer = await _offerQueries.GetOfferWithQTByIdForOpenningDetails(offerId);
            QuantitiesTemplateModel lst = await _offerQueries.GetOfferQuantityItemsForApplyOffer(offerId);
            if (lst == null)
                lst = new QuantitiesTemplateModel();
            if (lst.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase || lst.TenderTypeId == (int)Enums.TenderType.CurrentTender || lst.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects)
            {
                lst.ActivityTemplates = new List<int> { (int)Enums.TenderActivityTamplate.OldSystemAndVRO };
            }
            FormConfigurationDTO DTOModel = new FormConfigurationDTO() { TenderId = offer.TenderId, FormIds = lst.FormIds, QuantityItemDtos = lst.QuantitiesItems?.ToList(), YearsCount = lst.TemplateYears == null ? 0 : lst.TemplateYears.Value };
            ApiResponse<List<HtmlTemplateDto>> obj = await _qantityTemplatesProxy.GetSupplierTemplatesToApplyOffer(DTOModel);
            if (obj.Data != null)
            {
                lst.grids.AddRange(obj.Data.GroupBy(o => new { o.FormId, o.FormName, o.TemplateName, o.FormExcellTemplate }).Select(o => new HTMLObject
                {
                    FormId = o.Key.FormId,
                    FormName = o.Key.FormName,
                    TemplateName = o.Key.TemplateName,
                    gridTables = offer.SupplierTenderQuantityTables.Where(q => q.IsActive == true && q.TenderQuantityTable != null && q.TenderQuantityTable.IsActive == true && q.TenderQuantityTable?.FormId == o.Key.FormId)
                    .Select(u => new TableModel { TableId = u.Id.ToString(), TableName = u.Name, TenderId = offer.TenderId, FormId = u.TenderQuantityTable != null ? u.TenderQuantityTable.FormId : 0 }).ToList()
                }).ToList());
            }
            lst.IsReadOnly = true;
            return lst;
        }

        public async Task<OfferQuantityTableHtmlVM> GetQuantityTablesForOpening(int offerId, bool allowEdit)
        {
            QuantitiesTemplateModel lst = new QuantitiesTemplateModel();
            var Qitems = await _offerQueries.GetSupplierQTableItemsForOpening(offerId);
            lst.QuantitiesItems = Qitems;
            Offer offer = await _offerQueries.FindOfferById(offerId);
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                TenderId = offer.Tender.TenderId,
                HasAlternative = (offer.Tender.HasAlternativeOffer ?? false),
                ActivityId = 1,
                EncryptedOfferId = Util.Encrypt(offer.OfferId),
                EncryptedTenderId = Util.Encrypt(offer.TenderId),
                QuantityItemDtos = lst.QuantitiesItems.ToList(),
                YearsCount = (offer.Tender.TemplateYears ?? 0),
                SubmitAction = "/Offer/SaveOpeningQuantityItem",
                CanEditAlternative = false,
                AllowEdit = allowEdit,
                ApplySelected = false
            };
            if (offer.Tender.TenderTypeId != (int)Enums.TenderType.CurrentTender && offer.Tender.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase || !allowEdit)
            {
                ApiResponse<List<HtmlTemplateDto>> obj = await _qantityTemplatesProxy.GenerateCommitteeReadOnlyTemplate(DTOModel);
                OfferQuantityTableHtmlVM offerQuantityTableHtmlVM = new OfferQuantityTableHtmlVM();
                if (obj.StatusCode == 200)
                {
                    foreach (var tbl in obj.Data)
                    {
                        offerQuantityTableHtmlVM.tableFormHtmls.Add(new TableFormHtml
                        {
                            TemplateName = tbl.TemplateName,
                            FormId = tbl.FormId,
                            FormName = tbl.FormName,
                            TableName = tbl.TableName,
                            FormHtml = tbl.FormHtml,
                            tableId = long.Parse(tbl.TableId)
                        });
                    }
                }
                offerQuantityTableHtmlVM.offerId = offerId;
                offerQuantityTableHtmlVM.TenderStatusId = offer.Tender.TenderStatusId;
                offerQuantityTableHtmlVM.TenderTypeId = offer.Tender.TenderTypeId;
                offerQuantityTableHtmlVM.OfferPresentationWayId = offer.Tender.OfferPresentationWayId ?? 0;
                return offerQuantityTableHtmlVM;
            }
            else
            {
                ApiResponse<List<HtmlTemplateDto>> obj = await _qantityTemplatesProxy.GenerateCommitteeTemplate(DTOModel);
                OfferQuantityTableHtmlVM offerQuantityTableHtmlVM = new OfferQuantityTableHtmlVM
                {
                    offerStatusModel = new OfferStatusModel { offerIdString = Util.Encrypt(offer.OfferId), OfferStatusId = offer.OfferStatusId, offerStatusName = offer.Status.NameAr }
                };
                if (obj.StatusCode == 200)
                {
                    foreach (var tbl in obj.Data)
                    {
                        offerQuantityTableHtmlVM.tableFormHtmls.Add(new TableFormHtml { FormId = tbl.FormId, TableName = tbl.FormName, FormHtml = tbl.FormHtml, tableId = tbl.FormId });
                    }
                }
                offerQuantityTableHtmlVM.offerId = offerId;
                offerQuantityTableHtmlVM.TenderStatusId = offer.Tender.TenderStatusId;
                offerQuantityTableHtmlVM.TenderTypeId = offer.Tender.TenderTypeId;
                offerQuantityTableHtmlVM.OfferPresentationWayId = offer.Tender.OfferPresentationWayId ?? 0;
                return offerQuantityTableHtmlVM;
            }
        }

        public async Task<OfferQuantityTableHtmlVM> GetQuantityTablesDisplay(int offerId, bool allowEdit, bool isNegotiation = false)
        {
            QuantitiesTemplateModel lst = new QuantitiesTemplateModel();
            Offer offer = await _offerQueries.FindOfferWithTenderAndStatusAndNegotiationById(offerId);
            var offerNegotaion = offer.negotiations.OrderByDescending(d => d.NegotiationId).FirstOrDefault();
            List<TenderQuantityItemDTO> Qitems;
            if (isNegotiation && offerNegotaion != null && offerNegotaion.StatusId == (int)enNegotiationStatus.SupplierAgreed)
                Qitems = await _offerQueries.GetNegotiationQTableItems(offerId);
            else Qitems = await _offerQueries.GetSupplierQTableItemsForOpening(offerId);
            lst.QuantitiesItems = Qitems;
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                TenderId = offer.Tender.TenderId,
                HasAlternative = offer.Tender.HasAlternativeOffer ?? false,
                ActivityId = 1,
                EncryptedOfferId = Util.Encrypt(offer.OfferId),
                EncryptedTenderId = Util.Encrypt(offer.TenderId),
                QuantityItemDtos = lst.QuantitiesItems.ToList(),
                YearsCount = offer.Tender.TemplateYears ?? 0,
                SubmitAction = "",
                CanEditAlternative = false,
                AllowEdit = allowEdit,
                ApplySelected = true
            };
            if (offer.Tender.TenderTypeId != (int)Enums.TenderType.CurrentTender && offer.Tender.TenderTypeId != (int)Enums.TenderType.CurrentDirectPurchase || !allowEdit)
            {
                ApiResponse<List<HtmlTemplateDto>> obj = await _qantityTemplatesProxy.GenerateCommitteeReadOnlyTemplate(DTOModel);
                OfferQuantityTableHtmlVM offerQuantityTableHtmlVM = new OfferQuantityTableHtmlVM();
                if (obj.StatusCode == 200)
                {
                    foreach (var tbl in obj.Data)
                    {
                        offerQuantityTableHtmlVM.tableFormHtmls.Add(new TableFormHtml
                        {
                            TemplateName = tbl.TemplateName,
                            FormId = tbl.FormId,
                            TableName = tbl.FormName,
                            FormHtml = tbl.FormHtml,
                            tableId = tbl.FormId
                        });
                    }
                }
                return offerQuantityTableHtmlVM;
            }
            else
            {
                ApiResponse<List<HtmlTemplateDto>> obj = await _qantityTemplatesProxy.GenerateCommitteeTemplate(DTOModel);
                OfferQuantityTableHtmlVM offerQuantityTableHtmlVM = new OfferQuantityTableHtmlVM
                {
                    offerStatusModel = new OfferStatusModel { offerIdString = Util.Encrypt(offer.OfferId), OfferStatusId = offer.OfferStatusId, offerStatusName = offer.Status.NameAr }
                };
                if (obj.StatusCode == 200)
                {
                    foreach (var tbl in obj.Data)
                    {
                        offerQuantityTableHtmlVM.tableFormHtmls.Add(new TableFormHtml { FormId = tbl.FormId, TableName = tbl.FormName, FormHtml = tbl.FormHtml, tableId = tbl.FormId });
                    }
                }
                return offerQuantityTableHtmlVM;
            }
        }

        public async Task<string> ValidateandSaveQuantitiesTableForOpening(Dictionary<string, string> AuthorList)
        {
            int tenderId = Util.Decrypt(AuthorList["EncryptedTenderId"]);
            int offerId = Util.Decrypt(AuthorList["EncryptedOfferId"]);
            int formId = int.Parse(AuthorList["FormId"]);
            int tableId = int.Parse(AuthorList["TableId"]);
            var Qitems = await _offerQueries.GetSupplierQTableItemsByTableId(offerId, tableId);
            var offer = await _offerQueries.FindOfferWithStatusById(offerId);
            var HasAlternative = offer.Tender.HasAlternativeOffer ?? false;
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                HasAlternative = HasAlternative,
                TenderId = offer.TenderId,
                ActivityId = 1,
                EncryptedOfferId = Util.Encrypt(offer.OfferId),
                EncryptedTenderId = Util.Encrypt(offer.TenderId),
                QuantityItemDtos = new List<TenderQuantityItemDTO>(),
                YearsCount = offer.Tender.TemplateYears ?? 0
            };
            foreach (var item in Qitems)
            {
                string colId = item.Id.ToString();
                var supItems = AuthorList.Count(d => d.Key == colId);
                TenderQuantityItemDTO tenderQuantityItemDTO = new TenderQuantityItemDTO
                {
                    ColumnId = item.ColumnId,
                    Id = item.Id,
                    ItemNumber = item.ItemNumber,
                    TableId = tableId,
                    TemplateId = formId,
                    TableName = item.TableName,
                    TenderFormHeaderId = item.TenderFormHeaderId,
                    TenderId = tenderId,
                    Value = (supItems == 0 ? item.Value : AuthorList.FirstOrDefault(d => d.Key == colId).Value),
                    AlternativeValue = item.AlternativeValue
                };
                DTOModel.QuantityItemDtos.Add(tenderQuantityItemDTO);
            }
            ApiResponse<List<TableTemplateDTO>> obj = await _qantityTemplatesProxy.ValidateOpeningData(DTOModel);
            if (obj.StatusCode == 200)
            {
                var items = obj.Data.FirstOrDefault().QuantityItemDtos.Select(item => new SupplierTenderQuantityTableItem()
                {
                    ColumnId = item.ColumnId,
                    Id = item.Id,
                    TenderFormHeaderId = item.TenderFormHeaderId,
                    ActivityTemplateId = item.TemplateId,
                    Value = item.Value,
                    ItemNumber = item.ItemNumber
                }).ToList();
                offer.UpdateOfferSupplierQItems(items, tableId);
                await _offerCommands.UpdateAsync(offer);
                await _offerCommands.SaveAsync();
            }
            return obj.Data.FirstOrDefault().FormHtml;
        }

        public async Task<string> GetEmptyForm(int offerId, int tenderId)
        {
            Offer offer = await _offerQueries.GetOfferWithQuantityItems(offerId);
            offer.CreateOfferQuantityTables(0, "");
            await _offerCommands.UpdateAsync(offer);
            long createdTable = offer.SupplierTenderQuantityTables.OrderByDescending(a => a.Id).FirstOrDefault().Id;
            var result = await _qantityTemplatesProxy.GetTemplateFormHtml(tenderId, offerId, 54, 1, createdTable);
            result.Data[0].TableName = "بيان جديد";
            return result.Data[0].FormHtml;
        }

        public async Task<ValidationReturndTemplate> ValidateOfferQuantitiesData(Dictionary<string, string> AuthorList)
        {
            int tenderId = Util.Decrypt(AuthorList["EncryptedTenderId"]);
            int offerId = Util.Decrypt(AuthorList["EncryptedOfferId"]);
            int formId = int.Parse(AuthorList["FormId"]);
            Offer offer = await _offerQueries.GetOfferWithQuantityItems(offerId);
            int table = int.Parse(AuthorList["TableId"]);
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                HasAlternative = false,
                TenderId = offer.TenderId,
                ActivityId = 1,
                EncryptedOfferId = Util.Encrypt(offer.OfferId),
                EncryptedTenderId = Util.Encrypt(offer.TenderId),
                QuantityItemDtos = new List<TenderQuantityItemDTO>(),
                YearsCount = 0
            };
            foreach (KeyValuePair<string, string> entry in AuthorList)
            {
                if (!entry.Key.All(char.IsDigit))
                    continue;
                var _columnId = int.Parse(entry.Key);
                var _value = entry.Value;
                TenderQuantityItemDTO tenderQuantityItemDTO = new TenderQuantityItemDTO
                {
                    ColumnId = _columnId,
                    Id = 0,
                    ItemNumber = 1,
                    TableId = table,
                    TemplateId = formId,
                    TableName = "",
                    TenderFormHeaderId = 0,
                    TenderId = tenderId,
                    Value = _value
                };
                DTOModel.QuantityItemDtos.Add(tenderQuantityItemDTO);
            }
            ApiResponse<List<TableTemplateDTO>> item = await _qantityTemplatesProxy.ValidateOpeningData(DTOModel);
            if (item.StatusCode == 200)
            {
                var validatedItems = item.Data[0].QuantityItemDtos;
                offer.SaveOfferQuantityTables(table, validatedItems);
                await _offerCommands.SaveAsync();
            }
            return null;
        }

        public async Task<ValidationReturndTemplate> ValidateBayanQuantitiesData(Dictionary<string, string> AuthorList)
        {
            int offerId = int.Parse(AuthorList["OfferId"]);
            Offer offer = await _offerQueries.GetOfferWithQuantitiesTablesByOfferId(offerId);
            int table = int.Parse(AuthorList["TableId"]);
            ApiResponse<HtmlTableRowTemplateDto> lst = await _qantityTemplatesProxy.ValidateTenderQuantityItem(offer.Tender.TemplateYears == null ? 0 : offer.Tender.TemplateYears.Value, AuthorList);
            lst.Data.QuantityItemDto.ForEach(x => x.TableId = table);
            long.TryParse(AuthorList["ItemNumber"], out long itemNumber);
            offer.SaveSupplierTenderQuantityTables(lst.Data.QuantityItemDto, AuthorList["TableName"], itemNumber, out itemNumber);
            await _offerCommands.UpdateAsync(offer);
            return new ValidationReturndTemplate() { tableId = lst.Data.TableId.Replace('_' + table.ToString() + '_', '_' + table.ToString() + '_'), FormHtml = lst.Data.FormHtml, formTableId = table, itemNumber = itemNumber };
        }

        public async Task<ValidationReturndTemplate> DeleteOfferQuantityItem(Dictionary<string, string> AuthorList)
        {
            int offerId = int.Parse(AuthorList["OfferId"]);
            Offer offer = await _offerQueries.GetOfferWithQuantitiesTablesByOfferId(offerId);
            long table = long.Parse(AuthorList["TableId"]);
            int itemNumber = int.Parse(AuthorList["ItemNumber"]);
            int formId = int.Parse(AuthorList["FormId"]);
            offer.DeleteQuantityTableItems(table, itemNumber);
            await _offerCommands.UpdateAsync(offer);
            return new ValidationReturndTemplate() { tableId = "Table_" + table.ToString() + "_" + formId.ToString() };
        }

        #endregion

        #region Checking Offer New

        public async Task<Offer> SaveOfferChecking(SaveOpeningOfferFinancialDetails model, CheckOfferResultModel checkingResult)
        {
            var localContentSetting = await _offerQueries.GetLocalContentSettingsDate();
            Offer offer = await _offerQueries.FindOfferForCheckingById(Util.Decrypt(checkingResult.OfferIdString));
            if (offer.Tender != null && ((offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed)
                || (offer.Tender.TenderStatusId == (int)Enums.TenderStatus.Approved && (offer.Tender.OffersCheckingDate != null && offer.Tender.OffersCheckingDate <= DateTime.Now))))
            {
                offer.Tender.UpdateTenderStatus(Enums.TenderStatus.OffersChecking);
            }
            else if (offer.Tender != null && offer.Tender.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed)
            {
                offer.Tender.UpdateTenderStatus(Enums.TenderStatus.OffersTechnicalChecking);
            }
            if (offer.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed && offer.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersOppenedConfirmed && offer.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersChecking && offer.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersTechnicalOppeningConfirmed && offer.Tender.TenderStatusId != (int)Enums.TenderStatus.OffersTechnicalChecking)
            {
                throw new AuthorizationException(Resources.SharedResources.ErrorMessages.NotAddedAuthorized);
            }
            FormConfigurationDTO DTOModel = new FormConfigurationDTO()
            {
                TenderId = offer.TenderId,
                HasAlternative = offer.Tender.HasAlternativeOffer ?? false,
                CanEditAlternative = (offer.OfferPresentationWayId == (int)OfferPresentationWayId.OneFile
                    && (offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersChecking || offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersOppenedConfirmed))
                    || (offer.OfferPresentationWayId == (int)OfferPresentationWayId.TwoSepratedFiles
                    && (offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersTechnicalCheckingConfirmed || offer.Tender?.TenderStatusId == (int)Enums.TenderStatus.OffersFinancialChecking)),
                ApplySelected = offer.Tender.HasAlternativeOffer ?? false,
                AllowEdit = false,
                ActivityId = 1,
                EncryptedOfferId = Util.Encrypt(offer.OfferId),
                EncryptedTenderId = Util.Encrypt(offer.TenderId),
                IsTenderContainsTawreed = offer.Tender.IsTenderContainsTawreedTables.HasValue && offer.Tender.IsTenderContainsTawreedTables.Value,
                QuantityItemDtos = offer.SupplierTenderQuantityTables.Where(q => q.TenederQuantityId.HasValue).SelectMany(q => q.QuantitiyItemsJson.SupplierTenderQuantityTableItems).Select(qq => new TenderQuantityItemDTO
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
                    Id = qq.Id
                }).ToList(),
                NPpercentage = offer.Tender.NationalProductPercentage / 100 ?? 0,
                YearsCount = offer.Tender.TemplateYears ?? 0
            };
            if (offer.Tender.OfferPresentationWayId != (int)Enums.OfferPresentationWayId.TwoSepratedFiles)
            {
                ApiResponse<TotalCostDTO> obj = await _qantityTemplatesProxy.CalculateOfferFinalPricebyItems(DTOModel);
                if (obj.StatusCode == 200)
                {
                    offer.UpdateFinalPriceAfterDiscount(obj.Data.TotalCostWithdiscount);
                }
                if (offer.Tender?.TenderTypeId != (int)Enums.TenderType.CurrentTender &&

                  (DTOModel.IsTenderContainsTawreed ||
                    offer.Tender.TenderActivities.Any(a => a.Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)ConditionsTemplateCategory.GeneralSuppliesSupply || t.TemplateId == (int)TenderActivityTamplate.GeneralSupply))))
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
            offer.UpdateOfferTecknicalCheckingStatus(checkingResult.TechnicalOfferStatusId, checkingResult.RejectionReason, checkingResult.Notes);
            List<TechnicianReportAttachment> technicianReportAttachments = _mapper.Map<List<TechnicianReportAttachment>>(checkingResult.TechniciansReportAttachments);
            offer.UpdateTechnicianReportAttachments(technicianReportAttachments);
            offer.UpdateOfferFinantialCheckingStatus(checkingResult.FinantialOfferStatusId, checkingResult.FinantialRejectionReason, checkingResult.FinantialNotes);
            if (offer.Tender.IsValidToApplyOfferLocalContentChanges(localContentSetting.Date.Value) && offer.OfferlocalContentDetails != null)
            {
                offer.OfferlocalContentDetails = await _offerQueries.GetOfferLocalContentDetailsByOfferId(offer.OfferId);
                offer.ResetOfferLocalContentPreference();
                offer.OfferlocalContentDetails.SetIsBindedToMandatoryList(checkingResult.IsBindedToMandatoryList);
            }
            await _offerCommands.UpdateAsync(offer);
            return offer;
        }

        #endregion

        #region Vendors Invitations
        public async Task RemoveSolidaritySupplier(SolidarityRemoveInvitationModel solidarity)
        {
            Offer offer = await _offerQueries.FindOfferById(Util.Decrypt(solidarity.offerIdEncrypted));
            if (offer.CommericalRegisterNo != _httpContextAccessor.HttpContext.User.SupplierNumber())
            {
                throw new BusinessRuleException("غير مسموح ");
            }
            Tender tender = await _tenderQueries.FindTenderInvitationsWithById(offer.TenderId);
            if (tender.TenderStatusId != (int)Enums.TenderStatus.Approved)
            {
                throw new BusinessRuleException("المنافسة ليست فى حالة معتمدة");
            }
            offer.RemoveSolidaritySupplier(Util.Decrypt(solidarity.SolidarityIdString));
            await _offerCommands.UpdateAsync(offer);
        }

        public async Task AddSolidaritySupplier(SolidarityInvitationModel solidarity)
        {
            if (solidarity.SolidarityInvitationType == UnRegisteredSuppliersInvitationType.HasCR)
            {
                var RSupplier = await _idmAppService.GetSupplierInfoByCR(solidarity.CrNumber);
                if (RSupplier != null)
                {
                    throw new BusinessRuleException("  يجب دعوة المورد من الموردين المسجلين ");
                }
            }
            Offer offer = await _offerQueries.FindOfferById(Util.Decrypt(solidarity.offerIdEncrypted));
            Tender tender = await _tenderQueries.FindTenderInvitationsWithById(offer.TenderId);
            var checkBlockList = await _blockAppService.CheckifSupplierBlockedByCrNo(solidarity.CrNumber, tender.AgencyCode);
            if (checkBlockList)
                throw new BusinessRuleException(string.Format(Resources.BlockResources.Messages.ThisCrBlocked, solidarity.CrNumber));
            Enums.SupplierSolidarityStatus status = SupplierSolidarityStatus.New;
            await CheckIfSupplierExistinCurrentOffer(offer.Combined, solidarity, offer.TenderId);
            Check.ArgumentNotNull(nameof(tender), tender);
            var supplier = await _supplierQueries.FindSupplierByCRNumber(solidarity.CrNumber);
            if (supplier == null)
            {
                var supplierobj = new Supplier(solidarity.CrNumber, solidarity.SolidarityInvitationType == UnRegisteredSuppliersInvitationType.Existed ? solidarity.CrName
                                    : string.Concat("مورد غير مسجل", " - ", solidarity.CrNumber), await _notificationAppService.GetDefaultSettingByCr());
                await _supplierCommands.CreateSupplierAsync(supplierobj);
            }
            else
            {
                supplier.Update(solidarity.CrNumber, solidarity.CrName);
                await _supplierCommands.UpdateSupplierAsync(supplier);
            }
            switch (solidarity.SolidarityInvitationType)
            {
                case UnRegisteredSuppliersInvitationType.Existed:
                    if (offer.OfferStatusId == (int)Enums.OfferStatus.Received)
                        status = Enums.SupplierSolidarityStatus.ToBeSent;
                    List<string> checkedCommericalRegisterNo = new List<string> { solidarity.CrNumber };
                    List<OfferSolidarity> SolidarityRegisteredSuppliers = new List<OfferSolidarity>();
                    SolidarityRegisteredSuppliers.AddRange(checkedCommericalRegisterNo.Select(f => new OfferSolidarity(f, status, Enums.UnRegisteredSuppliersInvitationType.Existed)).ToList());
                    offer.AddRegisteredSolidaritySupplier(SolidarityRegisteredSuppliers);
                    if (offer.OfferStatusId == (int)Enums.OfferStatus.Received)
                    {
                        NotificationArguments NotificationArguments = new NotificationArguments
                        {
                            BodyEmailArgs = new object[] { "", tender.ReferenceNumber },
                            SubjectEmailArgs = new object[] { },
                            PanelArgs = new object[] { _httpContextAccessor.HttpContext.User.SupplierName(), tender.ReferenceNumber },
                            SMSArgs = new object[] { "", tender.ReferenceNumber }
                        };
                        var RegisteredCRs = offer.Combined.Where(w => w != null).Select(c => c.CRNumber).ToList();
                        MainNotificationTemplateModel inviteSupplierModel = new MainNotificationTemplateModel(NotificationArguments, $"Offer/CombinedInvitationDetails?offerIdString={Util.Encrypt(offer.OfferId)}", NotificationEntityType.Offer, tender.TenderId.ToString());
                        await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.Solidarity.SendSolidarityInvitation, RegisteredCRs, inviteSupplierModel);
                    }
                    break;
                case UnRegisteredSuppliersInvitationType.HasCR:
                case UnRegisteredSuppliersInvitationType.HasLicience:
                case UnRegisteredSuppliersInvitationType.Foriegn:
                    if (offer.OfferStatusId == (int)Enums.OfferStatus.Received)
                        status = Enums.SupplierSolidarityStatus.ToBeSent;
                    var unregistered = new OfferSolidarity(solidarity.EmailAddress, solidarity.MobileNumber, solidarity.SolidarityInvitationType, status, solidarity.CrNumber);
                    offer.AddUnRegisteredCombinedSupplier(unregistered);
                    if (offer.OfferStatusId == (int)Enums.OfferStatus.Received)
                    {
                        if (!string.IsNullOrEmpty(unregistered.Email))
                        {
                            await _notificationAppService.SendSolidarityInvitationByEmail(new List<string> { unregistered.Email }, tender, _httpContextAccessor.HttpContext.User.SupplierName());
                        }
                        if (!string.IsNullOrEmpty(unregistered.Mobile))
                        {
                            await _notificationAppService.SendSolidarityInvitationBySms(new List<string> { unregistered.Mobile }, tender, _httpContextAccessor.HttpContext.User.SupplierName());
                        }
                    }
                    break;
            }
            await _offerCommands.UpdateAsync(offer);
        }

        private async Task CheckIfSupplierExistinCurrentOffer(List<OfferSolidarity> offerSolidarities, SolidarityInvitationModel solidarity, int tenderId)
        {
            switch (solidarity.SolidarityInvitationType)
            {
                case UnRegisteredSuppliersInvitationType.Existed:
                    var Offer = await _offerQueries.findOfferbyCRandTenderID(tenderId, solidarity.CrNumber);
                    if (Offer != null && Offer.OfferStatusId == (int)OfferStatus.Received)
                    {
                        throw new BusinessRuleException("هذا المورد قدم عرض على نفس المنافسة");
                    }
                    Offer = await _offerQueries.GetOfferByTenderAndOwnerOrSolidarityCR(tenderId, solidarity.CrNumber);
                    if (Offer != null)
                    {
                        if (Offer.OfferId == Util.Decrypt(solidarity.offerIdEncrypted))
                        {
                            throw new BusinessRuleException(Resources.InvitationResources.DisplayInputs.SupplierAlreadyExsit);
                        }
                        else if (Offer.OfferStatusId == (int)OfferStatus.Received)
                        {
                            throw new BusinessRuleException("هذا المورد متضامن فى  عرض على نفس المنافسة");
                        }
                    }
                    var registeredList = offerSolidarities.Any(d => (d != null) && d.CRNumber == solidarity.CrNumber);
                    if (registeredList)
                    {
                        throw new BusinessRuleException("تم اضافتة مسبقا");
                    }
                    break;
                case UnRegisteredSuppliersInvitationType.Foriegn:
                case UnRegisteredSuppliersInvitationType.HasCR:
                case UnRegisteredSuppliersInvitationType.HasLicience:
                    var supplierHasLicience = offerSolidarities.Where(d => d.SolidarityTypeId != (int)Enums.UnRegisteredSuppliersInvitationType.Existed && d.SolidarityTypeId != (int)Enums.UnRegisteredSuppliersInvitationType.SolidarityLeader)
                                                .Select(f => f).ToList();
                    if (supplierHasLicience != null && supplierHasLicience.Any(d => d.CRNumber == solidarity.CrNumber))
                    {
                        throw new BusinessRuleException("تم اضافتة مسبقا");
                    }
                    break;
            }
        }

        public async Task SendInvitationsAsync(List<SolidarityInvitedRegisteredSupplierModel> suppliersModel)
        {
            var IDMSuppliers = await _idmAppService.GetContactOfficersByCR(suppliersModel.Select(s => s.CrNumber).ToList());
            foreach (var supplier in suppliersModel.Where(x => x.StatusId == 0 && x.IsChecked))
            {
                var checkBlockList = await _blockAppService.CheckifSupplierBlockedByCrNo(supplier.CrNumber);
                if (checkBlockList)
                    throw new BusinessRuleException(string.Format(Resources.BlockResources.Messages.ThisCrBlocked, supplier.CrNumber));
            }
            Offer offer = await _offerQueries.FindOfferById(Util.Decrypt(suppliersModel[0].OfferIdString));
            Tender tender = await _tenderQueries.FindTenderInvitationsWithById(suppliersModel[0].TenderId);
            Check.ArgumentNotNull(nameof(tender), tender);
            List<SolidarityInvitedRegisteredSupplierModel> checkedSuppliers = suppliersModel.Where(x => x.StatusId != (int)InvitationStatus.Approved && x.IsChecked).ToList();
            List<string> checkedCommericalRegisterNo = checkedSuppliers.Select(i => i.CrNumber).ToList();
            List<ContactOfficerModel> suppliers = IDMSuppliers.Where(x => checkedSuppliers.Where(s => s.StatusId == 0).Select(i => i.CrNumber).Contains(x.supplierNumber)).ToList();
            var invitedSuppliers = _mapper.Map<List<SupplierInvitationModel>>(suppliers);
            foreach (var item in checkedSuppliers)
            {
                var supplier = await _supplierQueries.FindSupplierByCRNumber(item.CrNumber);
                if (supplier == null)
                {
                    var supplierobj = new Supplier(item.CrNumber, item.CrName, await _notificationAppService.GetDefaultSettingByCr());
                    await _supplierCommands.CreateSupplierAsync(supplierobj);
                }
            }
            List<OfferSolidarity> SolidarityRegisteredSuppliers = new List<OfferSolidarity>();
            SolidarityRegisteredSuppliers.AddRange(checkedCommericalRegisterNo.Select(f => new OfferSolidarity(f, (int)UnRegisteredSuppliersInvitationType.Existed)).ToList());
            offer.AddRegisteredSolidaritySupplier(SolidarityRegisteredSuppliers);
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[] { "", tender.ReferenceNumber },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { _httpContextAccessor.HttpContext.User.SupplierName(), tender.ReferenceNumber },
                SMSArgs = new object[] { "", tender.ReferenceNumber }
            };
            MainNotificationTemplateModel inviteSupplierModel = new MainNotificationTemplateModel(NotificationArguments, $"Offer/CombinedInvitationsForSupplier", NotificationEntityType.Offer, tender.TenderId.ToString());
            await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.Solidarity.SendSolidarityInvitation, invitedSuppliers.Select(i => i.CommericalRegisterNo).ToList(), inviteSupplierModel);
            await _offerCommands.UpdateAsync(offer);
        }

        public async Task SendInvitationsByEmail(int tenderId, List<string> emails)
        {
            Tender tender = await _tenderQueries.FindTenderByTenderId(tenderId);
            Check.ArgumentNotNull(nameof(tender), tender);
            tender.RegisterSupplierEmails(emails);
            await _tenderCommands.UpdateAsync(tender);
            await _notificationAppService.SendInvitationByEmail(emails, tender);
        }

        public async Task SendInvitationBySms(int tenderId, List<string> mobilNoList)
        {
            Tender tender = await _tenderQueries.FindTenderByTenderId(tenderId);
            Check.ArgumentNotNull(nameof(tender), tender);
            tender.RegisterSupplierMobilNo(mobilNoList);
            await _tenderCommands.UpdateAsync(tender);
            await _notificationAppService.SendInvitationBySms(mobilNoList, tender);
        }

        #endregion

        #region Open Offers

        public async Task<OpenOfferModel> GetOpenOfferbyId(int offerid, int userId)
        {
            OpenOfferModel OpenOfferModel = await _offerQueries.GetOpenOfferbyId(offerid);
            if (OpenOfferModel == null)
            {
                throw new UnHandledAccessException();
            }
            return OpenOfferModel;
        }

        public async Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliersByOfferid(CombinedSupplierModel model)
        {
            QueryResult<CombinedSupplierModel> CombinedSuppliers = await _offerQueries.GetCombinedSuppliersByOfferid(model);
            return CombinedSuppliers;
        }

        public async Task<QueryResult<CombinedSupplierModel>> GetCombinedSuppliers(CombinedSupplierModel model, int userId)
        {
            QueryResult<CombinedSupplierModel> CombinedSuppliers = await _offerQueries.GetCombinedSuppliers(model, userId);
            return CombinedSuppliers;
        }

        public async Task<OfferDetailsDisplayModel> GetFinancialOfferDetailsForDisplay(int offerId)
        {
            OfferDetailsDisplayModel result = await _offerQueries.GetFinancialOfferDetailsForDisplay(offerId);
            return result;
        }

        #endregion

        public async Task<bool> RemoveOfferAwardingValue(int offerId)
        {
            bool result = false;
            Offer offer = await _offerQueries.FindOfferByOfferId(offerId);
            if (offer != null)
            {
                offer.RemoveOfferAwardingValue(offerId);
                await _offerCommands.UpdateAsync(offer);
                result = true;
            }
            return result;
        }

        public async Task<List<int>> GetCanIgnoreMandatoryValidationColumnsId()
        {
            try
            {
                var columnsIds = await _qantityTemplatesProxy.GetCanIgnoreMandatoryValidationColumnsId();
                return columnsIds != null ? columnsIds : new List<int>();
            }
            catch (Exception ex)
            {
                throw new WebServiceException(ex.Message);
            }
        }


        public async Task<string> UpdateCorporationSize(int offerId)
        {
            var offer = await _offerQueries.GetOfferWithLocalContentDetailsById(offerId);
            var IsSMModel = await _sMEASizeInqueryProxy.SMEASizeInquiry(offer.CommericalRegisterNo);
            if (IsSMModel == null || IsSMModel.Status == "Error")
                throw new BusinessRuleException("فشلت عملية تحديث البيانات");
            if (IsSMModel.isSMEA)
            {
                offer.OfferlocalContentDetails.SetCorporationSizeSmallOrMedium();
            }
            else
            {
                offer.OfferlocalContentDetails.SetCorporationSizeLarg();
            }

            offer.OfferlocalContentDetails.UpdateCorporationSizeDate();
            await _offerCommands.UpdateOfferlocalContentDetailsAsync(offer.OfferlocalContentDetails);
            offer.ResetOfferLocalContentPreference();
            await _offerCommands.UpdateAsync(offer);
            return IsSMModel.isSMEA ? "نعم" : "لا";

        }

        public async Task<string> UpdateIsCorporatioExistedInMoneyMarket(int offerId)
        {
            var offer = await _offerQueries.GetOfferWithLocalContentDetailsById(offerId);
            var supplierData = await _offerQueries.GetSupplierFromMonayMarketWithCr(offer.CommericalRegisterNo);
            if (supplierData == null)
                offer.OfferlocalContentDetails.SetIsIncludedInMoneyMarket(false);
            else
                offer.OfferlocalContentDetails.SetIsIncludedInMoneyMarket(true);

            offer.OfferlocalContentDetails.UpdateIsExistInMoneyMarketDate();
            offer.ResetOfferLocalContentPreference();
            await _offerCommands.UpdateAsync(offer);
            return supplierData == null? "لا":"نعم";
        }

        public async Task<string> UpdateLocalContentBaseLine(int offerId)
        {
            var offer = await _offerQueries.GetOfferWithLocalContentDetailsById(offerId);
            var baseline = await _localContentProxy.GetBaselineScoreInquiry(offer.CommericalRegisterNo);
            if (baseline.isSuccess)
            {
                if (baseline.content > offer.Tender.TenderLocalContent.MinimumBaseline)
                {
                    offer.OfferlocalContentDetails.SetIsSupplierBindedToBaseLine(true);
                }
                else
                {
                    offer.OfferlocalContentDetails.SetIsSupplierBindedToBaseLine(false);
                }
                offer.OfferlocalContentDetails.SetLocalContentBaseLinePercentage(baseline.content);
                offer.OfferlocalContentDetails.UpdateBaseLineDate();
                await _offerCommands.UpdateOfferlocalContentDetailsAsync(offer.OfferlocalContentDetails);
                offer.ResetOfferLocalContentPreference();
                await _offerCommands.UpdateAsync(offer);
            }
            else
                throw new BusinessRuleException("فشلت عملية تحديث البيانات");

            var result = offer.OfferlocalContentDetails.LocalContentPercentage == null ? "لا يوجد" : offer.OfferlocalContentDetails.LocalContentPercentage.ToString() + " % ";
            return result;
        }

        public async Task<string> UpdateLocalContentDesiredPercentage(int offerId)
        {
            var offer = await _offerQueries.GetOfferWithLocalContentDetailsById(offerId);
            var targetPlan = await _localContentProxy.GetTargetPlanScoreInquiry(offer.CommericalRegisterNo, offer.Tender.TenderId.ToString());
            if (targetPlan.isSuccess)
            {
                if (targetPlan.content > offer.Tender.TenderLocalContent.MinimumPercentageLocalContentTarget)
                {
                    offer.OfferlocalContentDetails.SetIsSupplierBindedToTheLowestLocalContent(true);
                }
                else
                {
                    offer.OfferlocalContentDetails.SetIsSupplierBindedToTheLowestLocalContent(false);
                }
                offer.OfferlocalContentDetails.UpdateLocalContentDesiredPercentageDate();
                offer.OfferlocalContentDetails.SetLocalContentDesiredPercentage(targetPlan.content);
                await _offerCommands.UpdateOfferlocalContentDetailsAsync(offer.OfferlocalContentDetails);
                offer.ResetOfferLocalContentPreference();
                await _offerCommands.UpdateAsync(offer);
            }
            else
                throw new BusinessRuleException("فشلت عملية تحديث البيانات");

            var result = offer.OfferlocalContentDetails.LocalContentDesiredPercentage == null ? "لا يوجد" : offer.OfferlocalContentDetails.LocalContentDesiredPercentage.ToString() + " % ";
            return result;
        }

        public async Task CalculateOfferFinancialPreferences(int tenderId)
        {
            try
            {
                var offers = await _offerQueries.GetIdenticalOffersWithLocalContentByTenderId(tenderId);
                var tenderLocalContent = await _tenderQueries.FindTenderWithLocalContentPreference(tenderId);
                var isHighValueTender = tenderLocalContent.TenderLocalContent.HighValueContractsAmmount <= tenderLocalContent.EstimatedValue;
                if (!isHighValueTender)
                {
                    if (offers.Any(x => x.OfferlocalContentDetails.IsSmallOrMediumCorporation == true))
                    {
                        await CalculateSMEAPreference(offers.Where(x => !x.OfferlocalContentDetails.IsSmallOrMediumCorporation.HasValue || x.OfferlocalContentDetails.IsSmallOrMediumCorporation == false).ToList(), tenderLocalContent.TenderId);

                        var smallCompaniesOffers = offers.Where(x => x.OfferlocalContentDetails.IsSmallOrMediumCorporation == true).ToList();
                    }
                }
                var lowestOfferPrice = offers.Min(x => x.OfferFinalPrice);
                if (isHighValueTender || tenderLocalContent.TenderLocalContent.MinimumBaseline.HasValue || tenderLocalContent.TenderLocalContent.MinimumPercentageLocalContentTarget.HasValue)
                {
                    await CalculateOffersFinancialEvaluation(offers, tenderLocalContent, lowestOfferPrice);
                }
                await CalculatePricePreference(offers, lowestOfferPrice.Value);
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public async Task CalculateSMEAPreference(List<Offer> LargeCompaniesOffers, int tenderId)
        {
            try
            {
                var tenderQuantityTables = await _tenderQueries.GetTenderWithQuantityTable(tenderId);

                var formsIds = tenderQuantityTables.TenderQuantityTables.Select(x => (long)x.FormId).ToList();

                var costColumns = await _qantityTemplatesProxy.GetCostColumnsIdByFormIdForNotSupply(formsIds);

                foreach (var offer in LargeCompaniesOffers)
                {
                    decimal totalNotSupplyTables = 0;
                    var quantityTablesItems = await _offerQueries.GetSupplierQTableItems(offer.OfferId, offer.TenderId);
                    foreach (var costColumn in costColumns)
                    {
                        var offerCostColumns = quantityTablesItems.Where(f => costColumn.Id == f.ColumnId).ToList();
                        foreach (var item in offerCostColumns)
                        {
                            totalNotSupplyTables += QTFormulaHelper.Calculate(costColumn.DataSource, quantityTablesItems.Where(data => data.ItemNumber == item.ItemNumber).ToList(), item.ItemNumber, item.TenderFormHeaderId, item.IsDefault);
                        }
                    }
                    var preferencePrice = offer.OfferWeightAfterCalcNPA + (totalNotSupplyTables * (decimal)0.1);
                    offer.UpdateOfferWeightAfterCalcSMEA(preferencePrice.Value);
                    await _offerCommands.UpdateAsync(offer);
                }
            }
            catch (Exception ex)
            {
                throw new BusinessRuleException("حدث خطأ فى عملية حساب تفضيل المنشأت الصغيرة");
            }


        }

        public async Task CalculatePricePreference(List<Offer> offers, decimal lowestOfferPrice)
        {
            if (lowestOfferPrice == 0)
            {
                throw new BusinessRuleException("اقل عرض لا يمكن ان يكون 0");
            }
            foreach (var offer in offers)
            {
                var percentage = (offer.OfferFinalPrice - lowestOfferPrice) / lowestOfferPrice;
                offer.OfferlocalContentDetails.SetPricePreferancePercentage(percentage);
                await _offerCommands.UpdateOfferlocalContentDetailsAsync(offer.OfferlocalContentDetails);
            }
        }
        public async Task CalculateOffersFinancialEvaluation(List<Offer> offers, Tender tenderLocalContent, decimal? lowestOfferPrice)
        {
            foreach (Offer offer in offers)
            {
                //offer.OfferlocalContentDetails = await _offerQueries.GetOfferLocalContentDetailsByOfferId(offer.OfferId);
                LocalContentPreferenceModel localContentModel = new LocalContentPreferenceModel()
                {
                    lowestOfferPrice = lowestOfferPrice.HasValue ? lowestOfferPrice.Value : 0,
                    currentOfferPrice = offer.OfferFinalPrice.Value,
                    supplierBaselinePercentage = offer.OfferlocalContentDetails.LocalContentPercentage.HasValue ? offer.OfferlocalContentDetails.LocalContentPercentage.Value : 0,
                    supplierLocalContentPercentage = offer.OfferlocalContentDetails.LocalContentDesiredPercentage.HasValue ? offer.OfferlocalContentDetails.LocalContentDesiredPercentage.Value : 0,

                    priceWeight = tenderLocalContent.TenderLocalContent.PriceWeightAfterAdjustment.HasValue ? tenderLocalContent.TenderLocalContent.PriceWeightAfterAdjustment.Value : 60,
                    baselineWeight = tenderLocalContent.TenderLocalContent.BaselineWeight.HasValue ? tenderLocalContent.TenderLocalContent.BaselineWeight.Value : 50,
                    localContentTargetWeight = tenderLocalContent.TenderLocalContent.LocalContentTargetWeight.HasValue ? tenderLocalContent.TenderLocalContent.LocalContentTargetWeight.Value : 50,
                    localContentWeight = tenderLocalContent.TenderLocalContent.LocalContentWeightAndFinancialMarket.HasValue ? tenderLocalContent.TenderLocalContent.LocalContentWeightAndFinancialMarket.Value : 40,
                    includedInMarketWeight = tenderLocalContent.TenderLocalContent.FinancialMarketListedWeight.HasValue ? tenderLocalContent.TenderLocalContent.FinancialMarketListedWeight.Value : 5,
                    isIncludedInMarket = offer.OfferlocalContentDetails.IsIncludedInMoneyMarket.HasValue && offer.OfferlocalContentDetails.IsIncludedInMoneyMarket.Value
                };
                offer.OfferlocalContentDetails.SetOfferFinancialWeight(_localContentPreferenceService.CalculateOfferLocalContentPreference(localContentModel));
                await _offerCommands.UpdateOfferlocalContentDetailsAsync(offer.OfferlocalContentDetails);
            }

        }
    }
}