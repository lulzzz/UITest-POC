using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Services;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Qualification;
using MOF.Etimad.Monafasat.WebApi.Infrastructure;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MOF.Etimad.Monafasat.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class QualificationController : BaseController
    {
        private readonly ISupplierService _supplierService;
        private readonly IQualificationAppService _qualificationService;
        private readonly ISupplierQualificationDocumentAppService _supplierqualificationService;
        private readonly IOfferAppService _offerAppService;
        private readonly ILookUpService _lookupAppService;
        private readonly ITenderAppService _tenderService;
        private readonly IVerificationService _verification;
        private readonly IIDMAppService _iDMAppService;
        private readonly Services.IAuthorizationService _authorizationService;
        private readonly ISupplierQualificationDocumentDomainService _supplierQualificationDocumentDomainService;
        private readonly IMapper _mapper;

        public QualificationController(ISupplierQualificationDocumentAppService supplierqualificationService, IQualificationAppService qualificationService, IMapper mapper,
             IVerificationService verification,
             IIDMAppService iDMAppService, Services.IAuthorizationService authorizationService, ISupplierService supplierService, IOfferAppService offerAppService, ILookUpService lookupAppService,
             ITenderAppService tenderService,
             ISupplierQualificationDocumentDomainService supplierQualificationDocumentDomainService, IOptionsSnapshot<RootConfigurations> rootConfiguration) : base(rootConfiguration)
        {
            _qualificationService = qualificationService;
            _offerAppService = offerAppService;
            _iDMAppService = iDMAppService;
            _authorizationService = authorizationService;
            _supplierService = supplierService;
            _lookupAppService = lookupAppService;
            _mapper = mapper;
            _supplierqualificationService = supplierqualificationService;
            _supplierQualificationDocumentDomainService = supplierQualificationDocumentDomainService;
            _verification = verification;
            _tenderService = tenderService;
        }


        #region [GET]

        [HttpGet]
        [Route("GetsupplierPreQualificationDocumentById/{SupplierPreQualificationDocumentId}/{SupplierId}")]
        public async Task<SupplierPreQualificationDocumentModel> GetsupplierPreQualificationDocumentById(int SupplierPreQualificationDocumentId, string SupplierId)
        {
            SupplierPreQualificationDocumentModel supplierPreQualificationDocument = await _supplierqualificationService.FindPreQualificationDocumentById(SupplierPreQualificationDocumentId, SupplierId);
            return supplierPreQualificationDocument;
        }


        [HttpGet("OpenQualificationDetailsReport/{tenderId}")]
        [AllowAnonymous]
        public async Task<TenderDetialsReportModel> OpenTenderDetailsReport(int tenderId)
        {
            return await _tenderService.OpenTenderDetailsReport(tenderId);
        }


        [HttpGet]
        [Route("GetPreQualificationDetails/{id}")]
        public async Task<PreQualificationDetailsModel> GetPreQualificationDetails(int id)
        {
            int branchId = User.UserBranch();
            PreQualificationDetailsModel detailsModel = await _qualificationService.GetPreQualificationDetailsModelById(id, branchId);
            return detailsModel;
        }


        #region [Supplier]

        [HttpGet]
        [Authorize(Roles = RoleNames.supplier + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager)]
        [Route("GetPreQualificationDetailsForSupplier/{id}")]
        public async Task<PreQualificationPartialDetailsModel> GetPreQualificationDetailsForSupplier(int id)
        {
            PreQualificationPartialDetailsModel tender = await _qualificationService.GetPrequalificationPartialDetails(id);
            Check.ArgumentNotNull(nameof(tender), tender);
            var model = _mapper.Map<PreQualificationPartialDetailsModel>(tender);
            return tender;
        }



        [HttpGet]
        [Authorize(Roles = RoleNames.supplier)]
        [Route("CheckSupplierDocuments/{PreQualificationId}/{editMode}")]
        public async Task<PreQualificationApplyDocumentsModel> CheckSupplierDocuments(int PreQualificationId, bool editMode)
        {
            string userIdFlag = User.SupplierNumber();

            if (!editMode)
                await _supplierQualificationDocumentDomainService.CheckApplyingDuplicationValidation(PreQualificationId, userIdFlag);

            SupplierPreQualificationDocument SupplierDocument = await _supplierqualificationService.CheckSuppierDocument(PreQualificationId, userIdFlag, editMode);
            var model = _mapper.Map<PreQualificationApplyDocumentsModel>(SupplierDocument);
            model = new PreQualificationApplyDocumentsModel
            {
                PreQualificationResult = 0,
                PreQualificationIdString = Util.Encrypt(PreQualificationId)
            };

            List<QualificationSupplierDataModel> lstQualificationSupplierDataModel = (editMode ? await _supplierqualificationService.GetQualificationSupplierDataForEdit(PreQualificationId, userIdFlag) :
                await _supplierqualificationService.GetQualificationSupplierData(PreQualificationId));

            model.lstQualificationSupplierTechDataModel = lstQualificationSupplierDataModel.Where(l => l.QualificationCategoryId == (int)Enums.QualificationItemCategory.Technical).ToList();
            model.lstQualificationSupplierFinancialDataModel = lstQualificationSupplierDataModel.Where(l => l.QualificationCategoryId == (int)Enums.QualificationItemCategory.Financial).ToList();
            model.lstQualificationSupplierDataYearlyViewModel = lstQualificationSupplierDataModel.FirstOrDefault().lstQualificationSupplierDataYearlyViewModel;
            model.lstQualificationSupplierProjectModel = lstQualificationSupplierDataModel.FirstOrDefault().lstQualificationSupplierProjectModel;


            // bind financialProperty for edit mode
            if (editMode)
            {
                model = await bindFinancialPropertyAsync(model);
                SupplierPreQualificationDocument supplierPreQualificationDocument = await _supplierqualificationService.GetSuppierDocument(PreQualificationId, userIdFlag);
                model.AttachmentRefrences = await GetEditModeAttahmentsData(supplierPreQualificationDocument);
                model.FileReferenceForQualityAssuranceStandards = model.lstQualificationSupplierTechDataModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.QualityAssuranceStandards).FirstOrDefault()?.FileReferenceForQualityAssuranceStandards;
                model.FileReferenceForEnvironmentalHealthSafetyStandards = model.lstQualificationSupplierTechDataModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.EnvironmentalHealthSafetyStandards).FirstOrDefault()?.FileReferenceForEnvironmentalHealthSafetyStandards;
                model.FileReferenceForInsurance = model.lstQualificationSupplierTechDataModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.InsuranceOfGeneralCommercialResponsibility ||
                a.QualificationItemId == (int)Enums.QualificationEvaluationItems.InsuranceOfProfessionalCompensation ||
                a.QualificationItemId == (int)Enums.QualificationEvaluationItems.LiabilityInsurance).FirstOrDefault()?.FileReferenceForInsurance;
            }



            if (lstQualificationSupplierDataModel != null && lstQualificationSupplierDataModel.Count > 0)
            {
                model.QualificationTypeId = lstQualificationSupplierDataModel.FirstOrDefault().QualificationTypeId;
                model.QualificationStatusId = lstQualificationSupplierDataModel.FirstOrDefault().QualificationStatusId;
                model.SupplierId = lstQualificationSupplierDataModel.FirstOrDefault().SupplierSelectedCr;
                model.OfferCheckingDate = lstQualificationSupplierDataModel.FirstOrDefault().OfferCheckingDate != null ? lstQualificationSupplierDataModel.FirstOrDefault().OfferCheckingDate : null;
                model.OffersCheckingDateHijri = lstQualificationSupplierDataModel.FirstOrDefault().OffersCheckingDateHijri;
            }

            return model;
        }


        private async Task<string> GetEditModeAttahmentsData(SupplierPreQualificationDocument model)
        {
            var list = model.supplierPreQualificationAttachments;
            string AttachmentReference = string.Empty;

            if (list != null)
            {
                foreach (var item in list)
                {
                    AttachmentReference += ',' + "/Upload/GetFile?id=" + item.FileNetReferenceId + ":" + item.FileName;
                }
            }
            return AttachmentReference;
        }

        /// <summary>
        /// Gets data for qualification registry report
        /// </summary>
        /// <param name="qualificationId"></param>
        /// <returns>RegistryReportForQualificationModel</returns>
        [HttpGet("QualificationOffersRegistryReport/{qualificationId}")]
        [Authorize(RoleNames.QualificationSecretaryAndManagerPolicy)]
        public async Task<RegistryReportForQualificationModel> QualificationOffersRegistryReport(int qualificationId)
        {
            return await _qualificationService.GetQualificationOffersRegistryReportData(qualificationId);
        }

        public async Task<PreQualificationApplyDocumentsModel> bindFinancialPropertyAsync(PreQualificationApplyDocumentsModel model)
        {

            if (model.lstQualificationSupplierFinancialDataModel.FirstOrDefault().QualificationTypeId == (int)Enums.PreQualificationType.Small)
            {
                model.CashEquivalents = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashEquivalents && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
                model.AccountsReceivable = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.AccountsReceivable && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
                model.CurrentLiabilities = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CurrentLiabilities && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
            }
            else if (model.lstQualificationSupplierFinancialDataModel.FirstOrDefault().QualificationTypeId == (int)Enums.PreQualificationType.Medium ||
                model.lstQualificationSupplierFinancialDataModel.FirstOrDefault().QualificationTypeId == (int)Enums.PreQualificationType.Large)
            {

                model.CurrentAssets = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CurrentAssets && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
                model.CashEquivalents = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashEquivalents && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
                model.AccountsReceivable = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.AccountsReceivable && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
                model.CurrentLiabilities = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CurrentLiabilities && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;

                model.CurrentAssets1 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CurrentAssets && a.QualificationYearId == (int)Enums.QualificationYear.SecondYear).FirstOrDefault().SupplierValue;
                model.CashEquivalents1 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashEquivalents && a.QualificationYearId == (int)Enums.QualificationYear.SecondYear).FirstOrDefault().SupplierValue;
                model.AccountsReceivable1 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.AccountsReceivable && a.QualificationYearId == (int)Enums.QualificationYear.SecondYear).FirstOrDefault().SupplierValue;
                model.CurrentLiabilities1 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CurrentLiabilities && a.QualificationYearId == (int)Enums.QualificationYear.SecondYear).FirstOrDefault().SupplierValue;



                model.CurrentAssets2 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CurrentAssets && a.QualificationYearId == (int)Enums.QualificationYear.ThirdYear).FirstOrDefault().SupplierValue;
                model.CashEquivalents2 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CashEquivalents && a.QualificationYearId == (int)Enums.QualificationYear.ThirdYear).FirstOrDefault().SupplierValue;
                model.AccountsReceivable2 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.AccountsReceivable && a.QualificationYearId == (int)Enums.QualificationYear.ThirdYear).FirstOrDefault().SupplierValue;
                model.CurrentLiabilities2 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.CurrentLiabilities && a.QualificationYearId == (int)Enums.QualificationYear.ThirdYear).FirstOrDefault().SupplierValue;


                if (model.lstQualificationSupplierFinancialDataModel.FirstOrDefault().QualificationTypeId == (int)Enums.PreQualificationType.Large)
                {
                    model.NetProfit = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.NetProfit && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
                    model.NetProfit1 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.NetProfit && a.QualificationYearId == (int)Enums.QualificationYear.SecondYear).FirstOrDefault().SupplierValue;
                    model.NetProfit2 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.NetProfit && a.QualificationYearId == (int)Enums.QualificationYear.ThirdYear).FirstOrDefault().SupplierValue;



                    model.TotalRevenue = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalRevenue && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
                    model.TotalRevenue1 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalRevenue && a.QualificationYearId == (int)Enums.QualificationYear.SecondYear).FirstOrDefault().SupplierValue;
                    model.TotalRevenue2 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalRevenue && a.QualificationYearId == (int)Enums.QualificationYear.ThirdYear).FirstOrDefault().SupplierValue;


                    model.TotalLiabilities = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalLiabilities && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
                    model.TotalLiabilities1 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalLiabilities && a.QualificationYearId == (int)Enums.QualificationYear.SecondYear).FirstOrDefault().SupplierValue;
                    model.TotalLiabilities2 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalLiabilities && a.QualificationYearId == (int)Enums.QualificationYear.ThirdYear).FirstOrDefault().SupplierValue;



                    model.TotalAssets = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalAssets && a.QualificationYearId == (int)Enums.QualificationYear.CurrentYear).FirstOrDefault().SupplierValue;
                    model.TotalAssets1 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalAssets && a.QualificationYearId == (int)Enums.QualificationYear.SecondYear).FirstOrDefault().SupplierValue;
                    model.TotalAssets2 = model.lstQualificationSupplierDataYearlyViewModel.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalAssets && a.QualificationYearId == (int)Enums.QualificationYear.ThirdYear).FirstOrDefault().SupplierValue;

                }
            }

            return model;
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.supplier)]
        [Route("GetSupplierDocuments/{PreQualificationId}")]
        public async Task<PreQualificationApplyDocumentsModel> GetSupplierDocuments(int PreQualificationId)
        {
            string userIdFlag = User.SupplierNumber();

            SupplierPreQualificationDocument SupplierDocument = await _supplierqualificationService.GetSuppierDocument(PreQualificationId, userIdFlag);
            var model = _mapper.Map<PreQualificationApplyDocumentsModel>(SupplierDocument);
            return model;
        }

        [HttpGet]
        //[Authorize(Roles = RoleNames.supplier + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.PreQualificationCommitteeManager + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary)]
        [Route("GetSupplierDataReviewModel/{PreQualificationId}/{SupplierId}")]
        public async Task<QualificationSupplierDataReviewViewModel> GetSupplierDataReviewModel(int PreQualificationId, string SupplierId)
        {
            QualificationSupplierDataReviewViewModel SupplierDataReviewViewModel = await _supplierqualificationService.GetSupplierDataReviewModel(PreQualificationId, SupplierId);
            return SupplierDataReviewViewModel;
        }



        [HttpGet]
        //[Authorize(Roles = RoleNames.supplier + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.PreQualificationCommitteeManager)]
        [Route("GetSupplierIDMInfo/{SupplierId}")]
        public async Task<SupplierFullDataViewModel> GetSupplierIDMInfo(string SupplierId)
        {


            var SupplierDataReviewViewModel = await _iDMAppService.GetSupplierInfoByCR(SupplierId);
            if (SupplierDataReviewViewModel == null)
            {
                return new SupplierFullDataViewModel();
            }
            SupplierFullDataViewModel supplierFullDataModel = _mapper.Map<SupplierFullDataViewModel>(SupplierDataReviewViewModel);
            return supplierFullDataModel;
        }


        [HttpGet]
        [Authorize(Roles = RoleNames.supplier + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.PreQualificationCommitteeManager + "," + RoleNames.DataEntry + "," + RoleNames.Auditer + "," + RoleNames.OffersOppeningManager + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.CustomerService)] // To Be Supplier
        [Route("GetPrequalificationStatus/{PreQualificationId}")]
        public async Task<int> GetPrequalificationStatus(int PreQualificationId)
        {
            string userIdFlag = User.SupplierNumber();
            int status = await _qualificationService.GetPreQualificationStatus(PreQualificationId);
            return status;
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.supplier + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.MonafasatAdmin + ","
            + RoleNames.MonafasatAccountManager + "," + RoleNames.PreQualificationCommitteeManager + "," + RoleNames.DataEntry + "," + RoleNames.Auditer + "," + RoleNames.OffersOppeningManager + "," +
            RoleNames.OffersOppeningSecretary + "," + RoleNames.CustomerService + "," + RoleNames.FinancialSupervisor + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)] // To Be Supplier
        [Route("GetPrequalificationDetailsData/{PreQualificationId}")]
        public async Task<QualificationStatusModel> GetPrequalificationDetailsData(int PreQualificationId)
        {
            if (User.IsInRole(RoleNames.supplier))
            {
                return await _qualificationService.GetPrequalificationDetailsForSupplier(PreQualificationId, User.SupplierNumber());
            }
            else
            {
                return await _qualificationService.GetPrequalificationDetails(PreQualificationId, User.UserAgency());
            }

        }


        [HttpGet]
        [Authorize(Roles = RoleNames.supplier + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.PreQualificationCommitteeManager + "," + RoleNames.DataEntry + "," + RoleNames.Auditer + "," + RoleNames.OffersOppeningManager + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.CustomerService)] // To Be Supplier
        [Route("GetSmallConfigQualificationDetails/{PreQualificationId}")]
        public async Task<QualificationSmallEvaluation> GetSmallConfigQualificationDetails(int PreQualificationId)
        {
            var details = await _qualificationService.GetSmallConfigQualificationDetails(PreQualificationId);
            return details;
        }
        [HttpGet]
        [Authorize(Roles = RoleNames.supplier + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.PreQualificationCommitteeManager + "," + RoleNames.DataEntry + "," + RoleNames.Auditer + "," + RoleNames.OffersOppeningManager + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.CustomerService)] // To Be Supplier
        [Route("GetMediumConfigQualificationDetails/{PreQualificationId}")]
        public async Task<QualificationMediumEvaluation> GetMediumConfigQualificationDetails(int PreQualificationId)
        {
            var details = await _qualificationService.GetMediumConfigQualificationDetails(PreQualificationId);
            return details;
        }
        [HttpGet]
        [Authorize(Roles = RoleNames.supplier + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.PreQualificationCommitteeManager + "," + RoleNames.DataEntry + "," + RoleNames.Auditer + "," + RoleNames.OffersOppeningManager + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.CustomerService)] // To Be Supplier
        [Route("GetLargeConfigQualificationDetails/{PreQualificationId}")]
        public async Task<QualificationLargeEvaluation> GetLargeConfigQualificationDetails(int PreQualificationId)
        {
            var details = await _qualificationService.GetLargeConfigQualificationDetails(PreQualificationId);
            return details;
        }

        [HttpGet]
        [Authorize(Roles = RoleNames.supplier)]
        [Route("GetSmallQualificationSupplierDetails/{PreQualificationId}")]
        public async Task<QualificationSmallEvaluation> GetSmallQualificationSupplierDetails(int PreQualificationId)
        {
            var details = await _qualificationService.GetSmallConfigQualificationDetails(PreQualificationId);
            return details;
        }
        [HttpGet]
        [Authorize(Roles = RoleNames.supplier)]
        [Route("GetMediumQualificationSupplierDetails/{PreQualificationId}")]
        public async Task<QualificationMediumEvaluation> GetMediumQualificationSupplierDetails(int PreQualificationId)
        {
            var details = await _qualificationService.GetMediumConfigQualificationDetails(PreQualificationId);
            return details;
        }
        [HttpGet]
        [Authorize(Roles = RoleNames.supplier)]
        [Route("GetLargeQualificationSupplierDetails/{PreQualificationId}")]
        public async Task<QualificationLargeEvaluation> GetLargeQualificationSupplierDetails(int PreQualificationId)
        {
            var details = await _qualificationService.GetLargeConfigQualificationDetails(PreQualificationId);
            return details;
        }

        [HttpPost]
        [Route("ApplyDocsforSupplier")]
        [Authorize(Roles = RoleNames.supplier)]
        public async Task<PreQualificationApplyDocumentsModel> ApplyDocsforSupplier([FromBody] PreQualificationApplyDocumentsModel _preQSupplierDocument)
        {
            Check.ArgumentNotNull(nameof(_preQSupplierDocument), _preQSupplierDocument);
            _preQSupplierDocument.SupplierId = User.SupplierNumber();
            var supplierDoc = await _supplierqualificationService.ApplyDocsforSupplier(_preQSupplierDocument);
            var model = _mapper.Map<PreQualificationApplyDocumentsModel>(supplierDoc);
            return model;
        }





        [HttpGet]
        [Route("GetAttachmentsByPreQSupId/{id}")]
        public async Task<List<PreQualificationSupplierAttachmentModel>> GetAttachmentsByPreQSupId(int id)
        {
            SupplierPreQualificationDocument preqAttachment = await _supplierqualificationService.FindTenderAttachmentsById(id);

            var attachmentlst = _mapper.Map<List<PreQualificationSupplierAttachmentModel>>(preqAttachment.supplierPreQualificationAttachments);

            return attachmentlst;
        }

        [HttpGet("ProcessAttachmentsChanges")]
        public async Task<PreQualificationSupplierAttachmentModel> ProcessAttachmentsChanges(SupplierPreQualificationDocument tender, bool isEdit)
        {
            var tenderAttachments = new PreQualificationSupplierAttachmentModel();

            var attachments = tender.supplierPreQualificationAttachments.ToList();
            return tenderAttachments;
        }




        #endregion


        #endregion

        #region Qualification grids

        [Authorize(PolicyNames.QualificationPagingPolicy)]
        [HttpGet]
        [Route("GetPreQualificationsBySearchCriteria")]
        public async Task<QueryResult<PreQualificationCardModel>> GetPreQualificationsBySearchCriteria(PreQualificationSearchCriteriaModel preQualificationSearchCriteriaModel)
        {
            if (!User.IsInRole(RoleNames.MonafasatAccountManager) && !User.IsInRole(RoleNames.CustomerService))
            {
                preQualificationSearchCriteriaModel.AgencyCode = User.UserAgency();
                preQualificationSearchCriteriaModel.UserId = User.UserId();
                if (!User.IsInRole(RoleNames.MonafasatAdmin) && !User.IsInRole(RoleNames.FinancialSupervisor))
                    preQualificationSearchCriteriaModel.BranchId = User.UserBranch();

                if (User.IsInRole(RoleNames.OffersPurchaseSecretary) || User.IsInRole(RoleNames.OffersPurchaseManager))
                    preQualificationSearchCriteriaModel.DirectPurchaseCommitteeId = User.SelectedCommittee();

                if (User.IsInRole(RoleNames.OffersCheckSecretary) || User.IsInRole(RoleNames.OffersCheckManager))
                    preQualificationSearchCriteriaModel.OffersCheckingCommitteeId = User.SelectedCommittee();

                if (User.IsInRole(RoleNames.PreQualificationCommitteeManager) || User.IsInRole(RoleNames.PreQualificationCommitteeSecretary))
                    preQualificationSearchCriteriaModel.CommitteeId = User.SelectedCommittee();

            }

            var tenderList = await _qualificationService.FindPreQualificationsModelBySearchCriteria(preQualificationSearchCriteriaModel);
            return tenderList;
        }



        [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.Auditer + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.MonafasatAdmin + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.OffersOppeningManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.CustomerService + "," + RoleNames.supplier
          + "," + RoleNames.TechnicalCommitteeUser + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.PreQualificationCommitteeManager + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
        [HttpGet]
        [Route("GetSupplierProjectbySearchCriteria")]
        public async Task<QueryResult<QualificationSupplierProjectModel>> GetSupplierProjectbySearchCriteria(PreQualificationSearchCriteriaModel preQualificationSearchCriteriaModel)
        {

            var supplierProject = await _qualificationService.FindSupplierProjectModelBySearchCriteria(preQualificationSearchCriteriaModel);
            return supplierProject;
        }


        [Authorize(Roles = RoleNames.DataEntry + "," + RoleNames.Auditer)]
        [HttpGet]
        [Route("GetPreQualificationForUnderOperationsStageIndex")]
        public async Task<QueryResult<PreQualificationCardModel>> GetPreQualificationForUnderOperationsStageIndex(PreQualificationSearchCriteriaModel preQualificationSearchCriteria)
        {
            bool statusFlag = await GetStatusFlag();
            int userIdFlag = User.UserId();
            preQualificationSearchCriteria.AgencyCode = User.UserAgency();
            var tenderList = await _qualificationService.FindPreQualificationByCriteriaForUnderOperationsStage(preQualificationSearchCriteria);
            return tenderList;
        }

        [Authorize(Roles = RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager)]
        [HttpGet]
        [Route("GetPreQualificationForCheckingStageIndex")]
        public async Task<QueryResult<PreQualificationCardModel>> GetPreQualificationForCheckingStageIndex(PreQualificationSearchCriteriaModel preQualificationSearchCriteria)
        {
            preQualificationSearchCriteria.UserId = User.UserId();
            preQualificationSearchCriteria.AgencyCode = User.UserAgency();
            preQualificationSearchCriteria.BranchId = User.UserBranch();
            var preQualificationListModel = await _qualificationService.FindPreQualificationForCheckingStageBySearchCriteria(preQualificationSearchCriteria);
            return preQualificationListModel;
        }

        [Authorize(Roles = RoleNames.supplier)]
        [HttpGet("FindCheckedPreQualificationsBySearchCriteria")]
        public async Task<QueryResult<PreQualificationCardModel>> FindCheckedPreQualificationsBySearchCriteria(PreQualificationSearchCriteriaModel criteria)
        {
            criteria.cr = User.SupplierNumber();
            bool statusFlag = true;
            if (User.IsInRole(RoleNames.DataEntry) || User.IsInRole(RoleNames.Auditer))
                statusFlag = false;
            var tenderList = await _qualificationService.FindCheckedPreQualificationsBySearchCriteria(criteria);
            var tenderListModel = _mapper.Map<QueryResult<PreQualificationCardModel>>(tenderList, opt => opt.Items["statusFlag"] = statusFlag);
            return tenderListModel;
        }



        #endregion


        #region Supplier Grids

        [Authorize(Roles = RoleNames.supplier)]
        [HttpGet("GetSupplierQualificationsList")]
        public async Task<QueryResult<PreQualificationCardModel>> GetSupplierQualificationsList(PreQualificationSearchCriteriaModel criteria)
        {
            criteria.cr = User.SupplierNumber();
            var qualifications = await _qualificationService.GetSupplierAllQualificationsList(criteria);
            return qualifications;
        }

        [HttpGet("GetQualificationsListForVisitor")]
        public async Task<QueryResult<PreQualificationCardModel>> GetQualificationsListForVisitor(PreQualificationSearchCriteriaModel criteria)
        {
            criteria.cr = User.SupplierNumber();
            var qualifications = await _qualificationService.GetQualificationListForVisitor(criteria);
            return qualifications;
        }

        #endregion Supplier Grids

        #region Supplier Actions

        [Authorize(Roles = RoleNames.supplier)]
        [HttpGet("AddQualificationToSupplierFavouriteList/{tenderId}")]
        public async Task<bool> AddQualificationToSupplierFavouriteList(int tenderId)
        {
            var cr = User.SupplierNumber();
            var tenders = await _qualificationService.AddTenderToSupplierTendersFavouriteList(tenderId, cr);
            return tenders;
        }

        #endregion
        //user story 7203 - 7204
        [HttpGet("ProcessAttachments")]
        public async Task<List<TenderAttachmentModel>> ProcessAttachments(Tender tender, bool isEdit)
        {
            if (tender.AgencyCode != User.UserAgency())
            {
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            }
            var attachments = tender.Attachments.ToList();
            var _tender = _mapper.Map<List<TenderAttachmentModel>>(attachments.Where(x => x.IsActive == true));
            if (_tender != null) _tender.ForEach(x => { x.TenderStatusId = tender.TenderStatusId; });
            return _tender;
        }


        [HttpGet]
        [Route("GetQualifiacrionDatesByQualificationId/{id}")]
        public async Task<TenderDatesModel> GetQualifiacrionDatesByQualificationId(int id)
        {
            TenderDatesModel tenderDatesModel = await _qualificationService.FindQualificationDatesByQualificationId(id);
            return tenderDatesModel;
        }

        #region Create Verification Code

        [Authorize(Roles = RoleNames.OffersPurchaseSecretary + "," + RoleNames.Auditer + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.OffersOppeningManager + "," + RoleNames.PreQualificationCommitteeManager + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersPurchaseManager)]
        [HttpPost]
        [Route("CreateVerificationCode/{PreQualificationID}")]
        public async Task CreateVerificationCode(int PreQualificationID)
        {
            int typeId = (int)Enums.VerificationType.Tender;
            var userEmail = User.Email();
            var userMobile = User.Mobile();
            await _verification.CreateVerificationCode(PreQualificationID, userMobile, userEmail, User.UserId(), typeId);
        }
        #endregion
        #region lookUps
        [Authorize(Roles = RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.PreQualificationCommitteeManager + "," + RoleNames.DataEntry + "," + RoleNames.Auditer + "," + RoleNames.MonafasatAccountManager
        + "," + RoleNames.MonafasatAdmin + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager + "," + RoleNames.OffersOppeningManager + "," + RoleNames.FinancialSupervisor
        + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.TechnicalCommitteeUser + "," + RoleNames.CustomerService + "," + RoleNames.EtimadOfficer + "," + RoleNames.PurshaseSpecialist)]
        [HttpGet]
        [Route("GetStatus")]
        public async Task<List<LookupModel>> GetStatus()
        {
            var tenderStatusList = await _lookupAppService.GetQualificationStatusLookup();
            return tenderStatusList;
        }

        [HttpGet("GetStatusFlag")]
        public async Task<bool> GetStatusFlag()
        {
            bool statusFlag = true;
            if (User.IsInRole(RoleNames.DataEntry) || User.IsInRole(RoleNames.Auditer))
                statusFlag = false;
            return statusFlag;
        }


        [HttpPost]
        [Route("EditPreQualificationCommittes")]
        public async Task<EditeCommitteesModel> EditPreQualificationCommittes([FromBody] EditeCommitteesModel committeesModel)
        {
            Tender tender = await _qualificationService.EditQualificationCommittees(committeesModel);
            EditeCommitteesModel model = _mapper.Map<EditeCommitteesModel>(tender);
            return model;
        }


        #endregion

        #region Create-Qualification

        //user story 7203 - 7204
        [HttpGet]
        [Route("GetPreQualificationEditingData/{id}")]
        [Authorize(Policy = PolicyNames.AddPreQualificationPolicy)]
        public async Task<PreQualificationSavingModel> GetPreQualificationEditingData(int id)
        {
            PreQualificationSavingModel qualification = await _qualificationService.GetPreQualificationEditingData(id);
            Check.ArgumentNotNull(nameof(qualification), qualification);
            if ((qualification.TenderStatusId != (int)Enums.TenderStatus.Established && qualification.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing && qualification.TenderStatusId != (int)Enums.TenderStatus.QualificationUnderEstablishingFromCommittee) || qualification.AgencyCode != User.UserAgency())
            {
                throw new AuthorizationException();
            }
            return qualification;
        }

        [Authorize(Policy = PolicyNames.AddPreQualificationPolicy)]
        [HttpPost("CreatePreQualifcation")]
        public async Task<PreQualificationSavingModel> CreatePreQualifcation([FromBody] PreQualificationSavingModel model)
        {
            if (model.TenderStatusId != 0 && model.TenderStatusId != (int)Enums.TenderStatus.Established && model.TenderStatusId != (int)Enums.TenderStatus.QualificationUnderEstablishingFromCommittee && model.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing)
            {
                throw new AuthorizationException();
            }
            int userId = User.UserId();
            Tender tender = await _qualificationService.CreatePreQualification(model, userId, User.UserAgency(), User.UserBranch());
            PreQualificationSavingModel tenderBasicDataModel = _mapper.Map<PreQualificationSavingModel>(tender);
            return tenderBasicDataModel;
        }

        [Authorize(Policy = PolicyNames.AddPreQualificationPolicy)]
        [HttpPost("SaveDraft")]
        public async Task<PreQualificationSavingModel> SaveDraft([FromBody] PreQualificationSavingModel model)
        {
            if (model.TenderStatusId != 0 && model.TenderStatusId != (int)Enums.TenderStatus.Established && model.TenderStatusId != (int)Enums.TenderStatus.UnderEstablishing)
            {
                throw new AuthorizationException();
            }
            int userId = User.UserId();
            Tender tender = await _qualificationService.SaveDraft(model, userId, User.UserAgency(), User.UserBranch());
            PreQualificationSavingModel tenderBasicDataModel = _mapper.Map<PreQualificationSavingModel>(tender);

            return tenderBasicDataModel;
        }
        #endregion

        #region Qualification-Approval-Actions   
        [Authorize(Policy = PolicyNames.PreQualificationApprovalPolicy)]
        [HttpGet]
        [Route("GetPreQualificationForApproval/{preQualificationId}")]
        public async Task<PreQulaificationApprovalModel> GetPreQualificationForApproval(int preQualificationId)
        {
            string agencyCode = User.UserAgency();
            PreQulaificationApprovalModel preQualfication = await _qualificationService.GetPreQualificationDetailsByPreQualificationId(preQualificationId, User.UserAgency());
            //PreQulaificationApprovalModel approvalModel = _mapper.Map<PreQulaificationApprovalModel>(preQualfication);
            return preQualfication;
        }

        [Authorize(Policy = PolicyNames.SendQualificationForApprovePolicy)]
        [HttpPost]
        [Route("SendPreQualificationToApprove/{tenderId}")]
        public async Task SendPreQualificationToApprove(int tenderId)
        {
            await _qualificationService.SendPreQualificationToApprove(tenderId, User.UserAgency());
        }

        [Authorize(Policy = PolicyNames.SendQualificationForApproveToCommitteeManagerPolicy)]
        [HttpPost]
        [Route("SendQualificationToCommitteeApprove/{tenderId}")]
        public async Task SendQualificationToCommitteeApprove(int tenderId)
        {
            await _qualificationService.SendQualificationToCommitteeApprove(tenderId, User.UserAgency());
        }
        [Authorize(Policy = PolicyNames.SendQualificationForApproveToCommitteeManagerPolicy)]
        [HttpPost]
        [Route("ApproveQualificationFromQualificationSecritary/{tenderId}")]
        public async Task ApproveQualificationFromQualificationSecritary(int tenderId)
        {
            await _qualificationService.ApproveQualificationFromQualificationSecritary(tenderId, User.UserAgency());
        }

        [Authorize(Policy = PolicyNames.ApproveQualificationPolicy)]
        [HttpPost]
        [Route("ApprvePreQualification/{id}/{verficationCode}")]
        public async Task ApprvePreQualification(int id, string verficationCode)
        {
            int typeId = (int)Enums.VerificationType.Tender;
            await _qualificationService.ApprovePreQualification(id, verficationCode, typeId, User.UserAgency());
        }

        [Authorize(Policy = PolicyNames.ApproveQualificationFromCommitteeManagerPolicy)]
        [HttpPost]
        [Route("ApprovePreQualificationFromCommitteeManager/{id}/{verficationCode}")]
        public async Task ApprovePreQualificationFromCommitteeManager(int id, string verficationCode)
        {
            int typeId = (int)Enums.VerificationType.Tender;
            await _qualificationService.ApprovePreQualificationFromCommitteeManager(id, verficationCode, typeId, User.UserAgency());
        }

        [Authorize(Policy = PolicyNames.RejectApproveQualificationPolicy)]
        [HttpPost]
        [Route("RejectApprvePreQualification")]
        public async Task RejectApprvePreQualification([FromBody] RejectionModel rejectionModel)
        {
            await _qualificationService.RejectApprovePreQualification(rejectionModel.TenderId, rejectionModel.RejectionReason, User.UserAgency());
        }

        [Authorize(Policy = PolicyNames.RejectApproveQualificationFromCommitteManagerPolicy)]
        [HttpPost]
        [Route("RejectApprovePreQualificationFromCommitteeManager")]
        public async Task RejectApprovePreQualificationFromCommitteeManager([FromBody] RejectionModel rejectionModel)
        {
            await _qualificationService.RejectApprovePreQualificationFromCommitteeManager(rejectionModel.TenderId, rejectionModel.RejectionReason, User.UserAgency());
        }

        [Authorize(Policy = PolicyNames.ReopenQualificationPolicy)]
        [HttpPost]
        [Route("ReopenPreQualification/{id}")]
        public async Task ReopenPreQualification(int id)
        {
            await _qualificationService.ReopenPreQualification(id, User.UserAgency());
        }

        [Authorize(Policy = PolicyNames.ReopenQualificationFromCommitteeSecrtraryPolicy)]
        [HttpPost]
        [Route("ReopenPreQualificationFromCommitteeSecrtary/{id}")]
        public async Task ReopenPreQualificationFromCommitteeSecrtary(int id)
        {
            await _qualificationService.ReopenPreQualificationFromCommitteeSecrtary(id, User.UserAgency());
        }

        #endregion

        #region Qualififcation-Check

        [Authorize(Roles = RoleNames.PreQualificationCommitteeManager + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersPurchaseSecretary + "," + RoleNames.OffersPurchaseManager)]
        [HttpGet]
        [Route("GetPreQualificationsRequestsForChecking")]
        public async Task<QueryResult<PreQualificationResultForChecking>> GetPreQualificationsRequestsForChecking(QualificationIdWithSearchCriteria qualificationSearch)
        {
            qualificationSearch.TenderId = Util.Decrypt(qualificationSearch.TenderIdString);
            var prequalificationsRequests = await _qualificationService.GetSupplierPreQualificationRequestByQualificationIdAsync(qualificationSearch);
            return prequalificationsRequests;
        }


        [Authorize(Roles = RoleNames.PreQualificationCommitteeManager + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary)]
        [HttpGet]
        [Route("GetPrequalificationTechnicalExamination/{PreQualificationId}")]
        public async Task<PrequalificationTechnicalExaminationModel> GetPrequalificationTechnicalExamination(int PreQualificationId)
        {
            var prequalificationTechnicalExamination = await _qualificationService.GetPrequalificationTechnicalExamination(PreQualificationId);
            return prequalificationTechnicalExamination;
        }

        [Authorize(Roles = RoleNames.PreQualificationCommitteeManager + "," + RoleNames.PreQualificationCommitteeSecretary)]
        [HttpGet]
        [Route("GetPreQualificationDetailsForChecking/{tenderId}")]
        public async Task<PreQualificationBasicDetailsModel> GetPreQualificationDetailsForChecking(int tenderId)
        {
            var prequalificationsRequests = await _qualificationService.GetPreQualificationDetailsByIdForChecking(tenderId);
            Check.ArgumentNotNull(nameof(PreQualificationBasicDetailsModel), prequalificationsRequests);
            var PreQualificationDetails = _mapper.Map<PreQualificationBasicDetailsModel>(prequalificationsRequests);
            return PreQualificationDetails;
        }

        [Authorize(Roles = RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.PreQualificationCommitteeManager)]
        [HttpPost]
        [Route("PreQualificationCheckingStatus")]
        public async Task<ActionResult> PreQualificationCheckingStatus([FromBody] SupplierPreQualificationDocumentModel SupplierPreQualificationDocumentModel)
        {
            var offer = await _supplierqualificationService.UpdateSupplierPreQualificationDocumentStatus(SupplierPreQualificationDocumentModel);
            var result = _mapper.Map<SupplierPreQualificationDocumentModel>(SupplierPreQualificationDocumentModel);
            return Ok(result);
        }

        [Authorize(Roles = RoleNames.PreQualificationCommitteeSecretary)]
        [HttpPost]
        [Route("ChooseQualificationResult")]
        public async Task ChooseQualificationResult([FromBody] ChooseQualificationResultModel chooseQualificationResultModel)
        {
            await _supplierqualificationService.ChooseQualificationResult(chooseQualificationResultModel);
        }
        #endregion

        #region Update Qualification Status
        [Authorize(Roles = RoleNames.PreQualificationCommitteeManager + "," + RoleNames.OffersPurchaseManager + "" + RoleNames.OffersCheckManager)]
        [HttpPost]
        [Route("StartPreQualificationCheckingOffersAsync/{id}")]
        public async Task StartPreQualificationCheckingOffersAsync(int id)
        {
            await _qualificationService.StartPreQualificationCheckingOffersAsync(id);
        }

        [Authorize(Roles = RoleNames.PreQualificationCommitteeSecretary)]
        [HttpPost]
        [Route("ReopenPreQualificationChecking/{id}")]
        public async Task ReopenTenderChecking(int id)
        {
            await _qualificationService.ReopenPreQualificationChecking(id, User.UserAgency());
        }

        [Authorize(Roles = RoleNames.PreQualificationCommitteeSecretary)]
        [HttpPost]
        [Route("SendPreQualificationToApproveChecking/{id}")]
        public async Task SendPreQualificationToApproveChecking(int id)
        {
            await _qualificationService.SendPreQualificationToApproveChecking(id, User.UserAgency());
        }


        [Authorize(Roles = RoleNames.PreQualificationCommitteeManager)]
        [HttpPost]
        [Route("ApprovePreQualificationCheckingWithVerification/{id}/{verficationCode}")]
        public async Task ApprovePreQualificationCheckingWithVerification(int id, string verficationCode)
        {
            int typeId = (int)Enums.VerificationType.Tender;
            await _qualificationService.ApprovePreQualificationChecking(id, verficationCode, typeId, User.UserAgency(), null);
        }

        [Authorize(Roles = RoleNames.PreQualificationCommitteeManager)]
        [HttpPost]
        [Route("RejectCheckPreQualificationOffersReportAsync")]
        public async Task RejectCheckPreQualificationOffersReportAsync([FromBody] RejectionModel rejectionModel)
        {
            await _qualificationService.RejectCheckPreQualificationOffers(rejectionModel.TenderId, rejectionModel.RejectionReason);
        }
        [HttpGet("getcountriesync")]
        public async Task<List<CountryModel>> getcountriesync()
        {
            var result = await _lookupAppService.FindCountries();
            return result;
        }
        [HttpGet("GetAcceptedPreQualificationDocuments/{qualificationId}")]
        [Authorize(Roles = RoleNames.supplier + "," + RoleNames.PreQualificationCommitteeManager + "," + RoleNames.MonafasatAdmin + "," + RoleNames.MonafasatAccountManager + "," + RoleNames.PreQualificationCommitteeSecretary + "," + RoleNames.DataEntry + "," + RoleNames.Auditer + "," + RoleNames.OffersCheckManager + "," + RoleNames.OffersCheckSecretary + "," + RoleNames.OffersOppeningManager + "," + RoleNames.OffersOppeningSecretary + "," + RoleNames.CustomerService + "," + RoleNames.FinancialSupervisor)] // To Be Supplier
        public async Task<AwardingDetailsModel> GetAcceptedPreQualificationDocuments(int qualificationId)
        {
            AwardingDetailsModel model = new AwardingDetailsModel();
            var result = await _qualificationService.GetSubscriptionAwardingInformation(qualificationId);
            model.SupplierNames = result;
            return model;
        }

        #endregion

        #region Create-Post-Qualification

        [HttpGet]
        [Route("GetPostQualificationData/{strCr}/{id}")]
        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        public async Task<PostQualificationApplyDocumentsModel> GetPostQualificationData(string strCr, int id)
        {
            List<string> crs = strCr.Split(',').ToList();
            await _qualificationService.checkPostQualificationSupplierBlock(crs);
            PostQualificationApplyDocumentsModel qualification = await _qualificationService.GetPostQualificationData(id);
            Check.ArgumentNotNull(nameof(qualification), qualification);
            return qualification;
        }

        [HttpGet]
        [Route("getQualificationDataToCreatePostQualification/{strCr}/{tenderId}/{qualificationId}")]
        [Authorize(Policy = PolicyNames.CreatePostQualificationPolicy)]
        public async Task<PostQualificationApplyDocumentsModel> getQualificationDataToCreatePostQualification(string strCr, string tenderId, string qualificationId)
        {
            List<string> crs = strCr.Split(',').ToList();
            await _qualificationService.CanAddPostqualificationIfTenderHasExtendOfferValidity(Util.Decrypt(tenderId));
            await _qualificationService.checkPostQualificationSupplierBlock(crs);
            await _qualificationService.CheckIfSupplierHavePostQualificationBefore(crs, Util.Decrypt(tenderId));
            PostQualificationApplyDocumentsModel qualification = await _qualificationService.getQualificationDataToCreatePostQualification(tenderId, null);
            Check.ArgumentNotNull(nameof(qualification), qualification);
            return qualification;
        }
        [HttpGet]
        [Route("GetQualificationDataToEditPostQualification/{qualificationId}")]
        [Authorize(Policy = PolicyNames.CreatePostQualificationPolicy)]
        public async Task<PostQualificationApplyDocumentsModel> GetQualificationDataToEditPostQualification(string qualificationId)
        {
            PostQualificationApplyDocumentsModel qualification = await _qualificationService.getQualificationDataToCreatePostQualification(null, qualificationId);
            Check.ArgumentNotNull(nameof(qualification), qualification);
            return qualification;
        }

        [Authorize(Policy = PolicyNames.CreatePostQualificationPolicy)]
        [HttpPost("CreatePostQualification")]
        public async Task<PreQualificationSavingModel> CreatePostQualification([FromBody] PostQualificationApplyDocumentsModel model)
        {

            if (User.IsInRole(RoleNames.OffersPurchaseSecretary))
                model.OffersDirectPurchaseCommitteeId = User.SelectedCommittee();
            else
                model.OffersCheckingCommitteeId = User.SelectedCommittee();

            int userId = User.UserId();
            model.branchId = User.UserBranch();
            Tender tender = await _qualificationService.CreatePoatQualification(model, userId, User.UserAgency());
            PreQualificationSavingModel tenderBasicDataModel = _mapper.Map<PreQualificationSavingModel>(tender);
            return tenderBasicDataModel;
        }
        #endregion

        #region Post-Qualification Approval Actions   
        [Authorize(Policy = PolicyNames.PostQualificationApprovalPolicy)]
        [HttpGet]
        [Route("GetPostQualificationForApproval/{preQualificationId}")]
        public async Task<PreQulaificationApprovalModel> GetPostQualificationForApproval(int preQualificationId)
        {
            PreQulaificationApprovalModel preQualfication = await _qualificationService.GetPostQulaificationByQualificationId(preQualificationId);
            //if ((preQualfication.PreQualificationStatusId != (int)Enums.TenderStatus.Rejected && preQualfication.PreQualificationStatusId != (int)Enums.TenderStatus.Established && preQualfication.PreQualificationStatusId != (int)Enums.TenderStatus.Pending) || preQualfication.CommitteeId != User.SelectedCommittee())
            //{
            //    throw new UnHandledAccessException();
            //}
            //PreQulaificationApprovalModel approvalModel = _mapper.Map<PreQulaificationApprovalModel>(preQualfication);
            return preQualfication;
        }
        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpPost]
        [Route("SendPostQualificationToApprove/{tenderId}")]
        public async Task SendPostQualificationToApprove(int tenderId)
        {
            await _qualificationService.SendPostQualificationToApprove(tenderId);
        }
        [Authorize(RoleNames.OffersCheckManagerPolicy)]
        [HttpPost]
        [Route("ApprvePostQualification/{id}/{verificationCode}")]
        public async Task ApprvePostQualification(int id, string verificationCode)
        {

            await _qualificationService.ApprovePostQualification(id, User.UserAgency(), verificationCode, User.UserRoles());
        }

        [Authorize(RoleNames.OffersCheckManagerPolicy)]
        [HttpPost]
        [Route("RejectApprvePostQualification")]
        public async Task RejectApprvePostQualification([FromBody] RejectionModel rejectionModel)
        {
            await _qualificationService.RejectApprovePostQualification(rejectionModel.TenderId, rejectionModel.RejectionReason, User.UserAgency(), User.UserRoles());
        }

        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpPost]
        [Route("ReopenPostQualification/{id}")]
        public async Task ReopenPostQualification(int id)
        {
            await _qualificationService.ReopenPostQualification(id, User.UserAgency());
        }
        #endregion

        #region Apply-Post-QualificationDocuments
        //[HttpGet]
        //[Authorize(RoleNames.SupplierPolicy)]
        //[Route("GetPostQualificationSuppierDocument/{PostQualificationId}")]
        //public async Task<PreQualificationApplyDocumentsModel> GetPostQualificationSuppierDocument(int PostQualificationId)
        //{
        //    string userIdFlag = User.SupplierNumber();

        //    await _supplierQualificationDocumentDomainService.CheckApplyingDuplicationValidation(PostQualificationId, userIdFlag);

        //    SupplierPreQualificationDocument SupplierDocument = await _supplierqualificationService.GetPostQualificationSuppierDocument(PostQualificationId, userIdFlag);

        //    var model = _mapper.Map<PreQualificationApplyDocumentsModel>(SupplierDocument);
        //    model = new PreQualificationApplyDocumentsModel
        //    {
        //        PreQualificationResult = 0,
        //        PreQualificationIdString = Util.Encrypt(PostQualificationId)
        //    };
        //    List<QualificationSupplierDataModel> lstQualificationSupplierDataModel = await _supplierqualificationService.GetQualificationSupplierData(PostQualificationId);
        //    model.lstQualificationSupplierTechDataModel = lstQualificationSupplierDataModel.Where(l => l.QualificationCategoryId == (int)Enums.QualificationItemCategory.Technical).ToList();
        //    model.lstQualificationSupplierFinancialDataModel = lstQualificationSupplierDataModel.Where(l => l.QualificationCategoryId == (int)Enums.QualificationItemCategory.Financial).ToList();
        //    if (lstQualificationSupplierDataModel != null && lstQualificationSupplierDataModel.Count > 0)
        //    {
        //        model.SupplierId = lstQualificationSupplierDataModel.FirstOrDefault().SupplierSelectedCr;
        //        model.QualificationTypeId = lstQualificationSupplierDataModel.FirstOrDefault().QualificationTypeId;
        //    }

        //    return model;
        //}

        //[HttpPost]
        //[Route("ApplyPostQualificationDocsforSupplier")]
        //[Authorize(RoleNames.SupplierPolicy)]
        //public async Task<PreQualificationApplyDocumentsModel> ApplyPostQualificationDocsforSupplier([FromBody] PreQualificationApplyDocumentsModel _preQSupplierDocument)
        //{
        //    string supplierCr = User.SupplierNumber();
        //    Check.ArgumentNotNull(nameof(_preQSupplierDocument), _preQSupplierDocument);
        //    _preQSupplierDocument.PreQualificationResult = (int)Enums.OfferStatus.Received;

        //    var supplierDoc = await _supplierqualificationService.ApplyPostQualificationDocsforSupplier(_preQSupplierDocument, supplierCr);
        //    var model = _mapper.Map<PreQualificationApplyDocumentsModel>(supplierDoc);
        //    return model;
        //}
        #endregion

        #region Check-Post-Qualification
        [Authorize(RoleNames.OffersCheckSecretaryAndManagerPolicy)]
        [HttpGet]
        [Route("GetPostQualificationDetailsByIdForChecking/{tenderId}")]
        public async Task<PreQualificationBasicDetailsModel> GetPostQualificationDetailsByIdForChecking(int tenderId)
        {
            int userId = User.UserId();
            var prequalificationsRequests = await _qualificationService.GetPostQualificationDetailsByIdForChecking(tenderId, userId, User.UserRoles());
            Check.ArgumentNotNull(nameof(PreQualificationBasicDetailsModel), prequalificationsRequests);
            return prequalificationsRequests;
        }
        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpPost]
        [Route("PostQualificationChecking")]
        public async Task<ActionResult> PostQualificationChecking([FromBody] SupplierPreQualificationDocumentModel SupplierPreQualificationDocumentModel)
        {
            var offer = await _supplierqualificationService.PostQualificationChecking(SupplierPreQualificationDocumentModel);
            var result = _mapper.Map<SupplierPreQualificationDocumentModel>(SupplierPreQualificationDocumentModel);
            return Ok(result);
        }
        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpPost]
        [Route("SendPostQualificationToApproveChecking/{id}")]
        public async Task SendPostQualificationToApproveChecking(int id)
        {
            await _qualificationService.SendPostQualificationToApproveChecking(id, User.UserRoles());
        }
        [Authorize(RoleNames.OffersCheckManagerPolicy)]
        [HttpPost]
        [Route("ApprovePostQualificationChecking/{id}")]
        public async Task ApprovePostQualificationChecking(int id)
        {
            //bool check = await _qualificationService.CheckForVerificationCode(id, verificationCode);
            await _qualificationService.ApprovePostQualificationChecking(id, User.UserRoles());
        }
        [Authorize(RoleNames.OffersCheckManagerPolicy)]
        [HttpPost]
        [Route("RejectCheckPostQualificationOffersReportAsync")]
        public async Task<BasicTenderModel> RejectCheckPostQualificationOffersReportAsync([FromBody] RejectionModel rejectionModel)
        {
            bool statusFlag = await GetStatusFlag();
            Tender tender = await _qualificationService.RejectCheckPostQualificationOffers(rejectionModel.TenderId, rejectionModel.RejectionReason, User.UserRoles());
            var model = _mapper.Map<BasicTenderModel>(tender, opt => opt.Items["statusFlag"] = statusFlag);
            return model;
        }
        [Authorize(RoleNames.OffersCheckSecretaryPolicy)]
        [HttpPost]
        [Route("ReopenPostQualificationChecking/{id}")]
        public async Task<BasicTenderModel> ReopenPostQualificationChecking(int id)
        {
            bool statusFlag = await GetStatusFlag();
            Tender tender = await _qualificationService.ReopenPostQualificationChecking(id);
            var model = _mapper.Map<BasicTenderModel>(tender, opt => opt.Items["statusFlag"] = statusFlag);
            return model;
        }
        #endregion


        [HttpGet]
        [Route("GetCurrentActivityVersion/{qualificationId}")]
        [Authorize(Policy = PolicyNames.CreatePostQualificationPolicy)]
        public async Task<int> GetCurrentActivityVersion(int qualificationId)
        {
            int ativityVersionId = await _tenderService.GetCurrentTenderActivityVersion(qualificationId);
            return ativityVersionId;
        }
    }
}
