using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Core.Entities.QualificationEvaluation;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Qualification;
using MOF.Etimad.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public class SupplierQualificationDocumentAppService : ISupplierQualificationDocumentAppService
    {
        private ISupplierQualificationDocumentDomainService _supplierqualificationdocumentdomain;
        private ISupplierQualificationDocumentQueries _supplierqualificationDocumentQueries;
        private readonly ISupplierQualificationDocumentCommands _supplierqualificationDocumentcommands;
        private readonly ISupplierQualificationDomainService _supplierQualificationDomainService;
        private readonly IQualificationQueries _qualificationQueries;
        private readonly INotificationAppService _notificationAppService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IQualificationCommands _qualificationCommands;
        private readonly RootConfigurations _configuration;
        private readonly decimal _defaultValue;



        public SupplierQualificationDocumentAppService(
            INotificationAppService notificationAppService,
            ISupplierQualificationDocumentDomainService supplierqualificationdocumentdomain,
            ISupplierQualificationDocumentCommands supplierqualificationDocumentcommands,
            ISupplierQualificationDocumentQueries supplierqualificationDocumentQueries, ISupplierQualificationDomainService supplierQualificationDomainService,
            IHttpContextAccessor httpContextAccessor,
            IQualificationQueries qualificationQueries,
            IQualificationCommands qualificationCommands,
        IOptionsSnapshot<RootConfigurations> configuration
            )
        {
            _supplierqualificationdocumentdomain = supplierqualificationdocumentdomain;
            _notificationAppService = notificationAppService;
            _supplierqualificationDocumentcommands = supplierqualificationDocumentcommands;
            _supplierqualificationDocumentQueries = supplierqualificationDocumentQueries;
            _supplierQualificationDomainService = supplierQualificationDomainService;
            _httpContextAccessor = httpContextAccessor;
            _configuration = configuration.Value;
            _qualificationQueries = qualificationQueries;
            _qualificationCommands = qualificationCommands;

            _defaultValue = (_configuration.QualificationSettingConfiguration.DefaultValue != null ? Convert.ToDecimal(_configuration.QualificationSettingConfiguration.DefaultValue) : 0);
        }


        public async Task<SupplierPreQualificationDocument> ApplyDocsforSupplier(PreQualificationApplyDocumentsModel _preQSupplier, bool? IsFromjob = null)
        {

            if (_preQSupplier.EditMode)
                await RemoveSupplierQualification(_preQSupplier, _preQSupplier.SupplierId);

            if (IsFromjob != true)
            {
                await _supplierqualificationdocumentdomain.CheckIfPreQualificationInCorrectStatus(Util.Decrypt(_preQSupplier.PreQualificationIdString), Enums.TenderStatus.Approved);
                await _supplierqualificationdocumentdomain.CheckPresentationDateValidation(Util.Decrypt(_preQSupplier.PreQualificationIdString), _preQSupplier.SupplierId);

                if (!_preQSupplier.EditMode)
                    await _supplierqualificationdocumentdomain.CheckApplyingDuplicationValidation(Util.Decrypt(_preQSupplier.PreQualificationIdString), _preQSupplier.SupplierId);
            }
            SupplierPreQualificationDocument _entity = new SupplierPreQualificationDocument(_preQSupplier.SupplierId, _preQSupplier.QualificationStatusId, Util.Decrypt(_preQSupplier.PreQualificationIdString), _preQSupplier.PreQualificationResult.Value);
            List<SupplierPreQualificationAttachment> attachments = new List<SupplierPreQualificationAttachment>(); foreach (var item in _preQSupplier.preQualificationSupplierAttachmentModels)
            {
                attachments.Add(new SupplierPreQualificationAttachment(item.FileName, item.FileNetReferenceId, _entity.SupplierPreQualificationDocumentId));
            }

            await EvaluateSupplierData(_preQSupplier);

            _entity.UpdateAttachments(attachments);
            SupplierPreQualificationDocument dbEntity = await _supplierqualificationDocumentcommands.CreateAsync<SupplierPreQualificationDocument>(_entity);


            await _supplierqualificationDocumentcommands.SaveAsync();


            if (IsFromjob != true && _preQSupplier.QualificationStatusId == (int)Enums.QualificationStatus.Received)
            {

                var preQ = await _supplierqualificationDocumentQueries.GetPreQualificationById(_entity.PreQualificationId);

                #region [Send Notification]
                NotificationArguments NotificationArguments = new NotificationArguments
                {
                    BodyEmailArgs = new object[] { preQ.ReferenceNumber },
                    SubjectEmailArgs = new object[] { },
                    PanelArgs = new object[] { preQ.ReferenceNumber },
                    SMSArgs = new object[] { preQ.ReferenceNumber }
                };
                #endregion

                MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
                              $"Qualification/PreQualificationSupplierDetails?qualificationId={Util.Encrypt(preQ.TenderId)}",
                              NotificationEntityType.Tender,
                              preQ.TenderId.ToString(),
                              preQ.BranchId);

                await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnTheTender.sendprequalificationdocs, new List<string> { _preQSupplier.SupplierId }, mainNotificationTemplateModel);
                await _notificationAppService.SendNotificationForBranchUsers(NotificationOperations.DataEntry.OperationsOnTheTender.sendprequalificationdocs, preQ.BranchId, mainNotificationTemplateModel);
            }
            return dbEntity;

        }



        private async Task RemoveSupplierQualification(PreQualificationApplyDocumentsModel _preQSupplier, string userId)
        {
            SupplierPreQualificationDocument supplierPreQualificationDocument = await _supplierqualificationDocumentQueries.GetSuppierDocument(Util.Decrypt(_preQSupplier.PreQualificationIdString), userId);
            if (supplierPreQualificationDocument != null)
            {
                supplierPreQualificationDocument.DeActive();
                _qualificationCommands.UpdateQualificationDocument(supplierPreQualificationDocument);


                if (supplierPreQualificationDocument.supplierPreQualificationAttachments != null)
                {
                    foreach (var item in supplierPreQualificationDocument.supplierPreQualificationAttachments)
                    {
                        item.DeActive();
                        _qualificationCommands.UpdateQualificationAttachment(item);
                    }
                }
            }



            List<QualificationSupplierDataYearly> qualificationSupplierDataYearlies = await _qualificationQueries.GetQualificationSupplierDataYear(Util.Decrypt(_preQSupplier.PreQualificationIdString), userId);
            if (qualificationSupplierDataYearlies != null && qualificationSupplierDataYearlies.Count > 0)
            {
                foreach (var item in qualificationSupplierDataYearlies)
                {
                    item.DeActive();
                    _qualificationCommands.DeactiveQualificationSupplierDataYearly(item);
                }
            }


            if (_preQSupplier.QualificationTypeId == (int)Enums.PreQualificationType.Large)
            {
                List<QualificationSupplierProject> qualificationSupplierProjects = await _qualificationQueries.GetQualificationSupplierProject(Util.Decrypt(_preQSupplier.PreQualificationIdString), userId);
                if (qualificationSupplierProjects != null && qualificationSupplierProjects.Count > 0)
                {
                    foreach (var item in qualificationSupplierProjects)
                    {
                        item.DeActive();
                        _qualificationCommands.DeactiveQualificationSupplierProject(item);
                    }
                }
            }


            List<QualificationSupplierData> qualificationSupplierDatas = await _qualificationQueries.GetQualificationSupplierData(Util.Decrypt(_preQSupplier.PreQualificationIdString), userId);
            if (qualificationSupplierDatas != null && qualificationSupplierDatas.Count > 0)
            {
                foreach (var item in qualificationSupplierDatas)
                {
                    item.DeActive();
                    _qualificationCommands.DeactiveQualificationSupplierData(item);

                    if (item.QualificationConfigurationAttachments != null && item.QualificationConfigurationAttachments.Count > 0)
                    {

                        foreach (var obj in item.QualificationConfigurationAttachments)
                        {
                            obj.DeActive();
                            _qualificationCommands.DeactivequalificationConfigurationAttachment(obj);
                        }
                    }
                }
            }



            QualificationFinalResult qualificationFinalResult = await _qualificationQueries.FindFinalResult(userId, Util.Decrypt(_preQSupplier.PreQualificationIdString));
            if (qualificationFinalResult != null)
            {
                qualificationFinalResult.DeActive();
                _qualificationCommands.UpdateFinalResult(qualificationFinalResult);
            }


        }


        public async Task EvaluateSupplierData(PreQualificationApplyDocumentsModel obj)
        {
            List<QualificationSupplierDataYearly> lstOfSupplierDataYearly = new List<QualificationSupplierDataYearly>();

            List<QualificationSupplierData> lstOfSupplierData = new List<QualificationSupplierData>();
            string SupplierId = obj.SupplierId;
            Tender tender = await _qualificationQueries.GetPreQualificationDetailsById(Util.Decrypt(obj.PreQualificationIdString));

            if (obj.QualificationTypeId == (int)Enums.PreQualificationType.Small)
            {
                lstOfSupplierData = await EvaluateSmallQualification(obj, tender);
            }
            else if (obj.QualificationTypeId == (int)Enums.PreQualificationType.Medium)
            {
                lstOfSupplierData = await EvaluateMediumQualification(obj, tender);
            }
            else if (obj.QualificationTypeId == (int)Enums.PreQualificationType.Large)
            {
                lstOfSupplierData = await EvaluateLargeQualification(obj, tender);
            }

            lstOfSupplierDataYearly = await InsertSupplierMainDataWithYear(obj, tender.TenderId, SupplierId);
            await _qualificationCommands.AddQualificationSupplierDataYearly(lstOfSupplierDataYearly);
            await _qualificationCommands.AddQualificationSupplierDataAsync(lstOfSupplierData);
            await CalculateSubCategoryResult(tender, lstOfSupplierData, SupplierId);

        }

        public async Task<List<QualificationSupplierData>> EvaluateSmallQualification(PreQualificationApplyDocumentsModel obj, Tender tender)
        {

            string SupplierId = obj.SupplierId;
            List<QualificationSupplierData> lstOfSupplierData = new List<QualificationSupplierData>();
            QualificationSupplierData qualificationSupplierData;
            if (obj.lstQualificationSupplierDataModel != null && obj.lstQualificationSupplierDataModel.Count > 0)
            {
                List<QualificationSupplierDataModel> _TechnicalqualificationSupplierDataModels = obj.lstQualificationSupplierDataModel.Where(a => a.QualificationCategoryId == (int)Enums.QualificationItemCategory.Technical).ToList();
                for (int i = 0; i < _TechnicalqualificationSupplierDataModels.Count; i++)
                {
                    decimal SupplierValue = 0;
                    decimal PointValue = 0;
                    decimal Min = _TechnicalqualificationSupplierDataModels[i].Min;
                    decimal Max = _TechnicalqualificationSupplierDataModels[i].Max;
                    decimal Value = _TechnicalqualificationSupplierDataModels[i].Value;
                    if (_TechnicalqualificationSupplierDataModels[i].IsConfigure)
                    {
                        if (_TechnicalqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees)
                        {
                            decimal TotalEmployee = _TechnicalqualificationSupplierDataModels.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfEmployees).FirstOrDefault().SupplierValue;
                            decimal SaudiEmployeeNo = _TechnicalqualificationSupplierDataModels.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfSaudiEmployees).FirstOrDefault().SupplierValue;
                            SupplierValue = (TotalEmployee == 0 ? 0 : (SaudiEmployeeNo / TotalEmployee));
                        }
                        else
                        {
                            SupplierValue = _TechnicalqualificationSupplierDataModels[i].SupplierValue;
                        }
                    }
                    else
                    {
                        SupplierValue = _TechnicalqualificationSupplierDataModels[i].SupplierValue;
                    }
                    PointValue = (!_TechnicalqualificationSupplierDataModels[i].IsConfigure ? 0 : await CalculatePointAsync(Min, Max, SupplierValue, _TechnicalqualificationSupplierDataModels[i].QualificationItemTypeId));
                    qualificationSupplierData = new QualificationSupplierData(0, _TechnicalqualificationSupplierDataModels[i].QualificationConfigurationId, tender.TenderId, SupplierValue, PointValue, _TechnicalqualificationSupplierDataModels[i].QualificationItemId,
                        _TechnicalqualificationSupplierDataModels[i].QualificationCategoryId, _TechnicalqualificationSupplierDataModels[i].Weight, SupplierId, _TechnicalqualificationSupplierDataModels[i].QualificationSubCategoryId);
                    lstOfSupplierData.Add(qualificationSupplierData);
                }
                List<QualificationSupplierDataModel> _FinancialqualificationSupplierDataModels = obj.lstQualificationSupplierDataModel.Where(a => a.QualificationCategoryId == (int)Enums.QualificationItemCategory.Financial).ToList();
                for (int i = 0; i < _FinancialqualificationSupplierDataModels.Count; i++)
                {
                    decimal Min = _FinancialqualificationSupplierDataModels[i].Min;
                    decimal Max = _FinancialqualificationSupplierDataModels[i].Max;
                    decimal Value = _FinancialqualificationSupplierDataModels[i].Value;
                    decimal SupplierValue = _FinancialqualificationSupplierDataModels[i].SupplierValue;
                    decimal PointValue = 0;
                    if (_FinancialqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.CashRate)
                    {
                        decimal CashRateRatio = CheckDefaultValue(obj.CashEquivalents) / CheckDefaultValue(obj.CurrentLiabilities);
                        SupplierValue = CashRateRatio;
                        PointValue = await CalculatePointAsync(Min, Max, Math.Round(CashRateRatio, 2, MidpointRounding.AwayFromZero), _FinancialqualificationSupplierDataModels[i].QualificationItemTypeId);
                    }
                    if (_FinancialqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.LiquidityRatio)
                    {
                        decimal LiquidityRatio = ((CheckDefaultValue(obj.CashEquivalents) + CheckDefaultValue(obj.AccountsReceivable)) / CheckDefaultValue(obj.CurrentLiabilities));
                        SupplierValue = LiquidityRatio;
                        PointValue = await CalculatePointAsync(Min, Max, Math.Round(LiquidityRatio, 2, MidpointRounding.AwayFromZero), _FinancialqualificationSupplierDataModels[i].QualificationItemTypeId);
                    }
                    qualificationSupplierData = new QualificationSupplierData(0, _FinancialqualificationSupplierDataModels[i].QualificationConfigurationId, tender.TenderId, SupplierValue, PointValue, _FinancialqualificationSupplierDataModels[i].QualificationItemId,
                        _FinancialqualificationSupplierDataModels[i].QualificationCategoryId, _FinancialqualificationSupplierDataModels[i].Weight, SupplierId, _FinancialqualificationSupplierDataModels[i].QualificationSubCategoryId);

                    lstOfSupplierData.Add(qualificationSupplierData);
                }
            }
            return lstOfSupplierData;
        }


        public async Task<List<QualificationSupplierData>> EvaluateMediumQualification(PreQualificationApplyDocumentsModel obj, Tender tender)
        {
            string SupplierId = obj.SupplierId;
            List<QualificationSupplierData> lstOfSupplierData = new List<QualificationSupplierData>();
            QualificationSupplierData qualificationSupplierData;
            List<QualificationConfigurationAttachment> attachments;
            if (obj.lstQualificationSupplierDataModel != null && obj.lstQualificationSupplierDataModel.Count > 0)
            {
                List<QualificationSupplierDataModel> _TechnicalqualificationSupplierDataModels = obj.lstQualificationSupplierDataModel.Where(a => a.QualificationCategoryId == (int)Enums.QualificationItemCategory.Technical).ToList();
                for (int i = 0; i < _TechnicalqualificationSupplierDataModels.Count; i++)
                {
                    decimal Min = _TechnicalqualificationSupplierDataModels[i].Min;
                    decimal Max = _TechnicalqualificationSupplierDataModels[i].Max;
                    decimal Value = _TechnicalqualificationSupplierDataModels[i].Value;
                    decimal SupplierValue = 0;
                    decimal SupplierSelectedPoint = 0;
                    int? SelectedId = 0;
                    int? QualificationLookupId = null;
                    decimal PointValue = 0;

                    if (_TechnicalqualificationSupplierDataModels[i].IsConfigure)
                    {
                        if (_TechnicalqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees)
                        {
                            decimal TotalEmployee = _TechnicalqualificationSupplierDataModels.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfEmployees).FirstOrDefault().SupplierValue;
                            decimal SaudiEmployeeNo = _TechnicalqualificationSupplierDataModels.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfSaudiEmployees).FirstOrDefault().SupplierValue;
                            SupplierValue = (TotalEmployee == 0 ? 0 : (SaudiEmployeeNo / TotalEmployee));
                        }
                        else
                        {
                            SupplierValue = _TechnicalqualificationSupplierDataModels[i].SupplierValue;
                        }
                        if (_TechnicalqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.QualityAssuranceStandards)
                        {
                            SelectedId = _TechnicalqualificationSupplierDataModels[i].QualificationLookupId;
                            SupplierSelectedPoint = await EvaluateQualityAssuranceStandards(SelectedId);
                        }

                        if (_TechnicalqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.EnvironmentalHealthSafetyStandards)
                        {
                            SelectedId = _TechnicalqualificationSupplierDataModels[i].QualificationLookupId;
                            SupplierSelectedPoint = await EvaluateEnvironmentalHealthSafetyStandards(SelectedId);
                        }

                        if (_TechnicalqualificationSupplierDataModels[i].QualificationItemTypeId == (int)Enums.QualificationItemType.Select)
                        {
                            QualificationLookupId = SelectedId;
                            PointValue = SupplierSelectedPoint;
                        }
                        else
                        {
                            PointValue = await CalculatePointAsync(Min, Max, SupplierValue, _TechnicalqualificationSupplierDataModels[i].QualificationItemTypeId);
                        }
                    }
                    else
                    {
                        PointValue = 0;
                        SupplierValue = _TechnicalqualificationSupplierDataModels[i].SupplierValue;
                    }
                    qualificationSupplierData = new QualificationSupplierData(0, _TechnicalqualificationSupplierDataModels[i].QualificationConfigurationId, tender.TenderId, SupplierValue, PointValue, _TechnicalqualificationSupplierDataModels[i].QualificationItemId,
                        _TechnicalqualificationSupplierDataModels[i].QualificationCategoryId, _TechnicalqualificationSupplierDataModels[i].Weight, SupplierId, _TechnicalqualificationSupplierDataModels[i].QualificationSubCategoryId, QualificationLookupId);
                    if (_TechnicalqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.QualityAssuranceStandards)
                    {
                        if (_TechnicalqualificationSupplierDataModels[i].QualificationLookupId != (int)Enums.QualificationQualityGuaranteeLookup.GuaranteeNotAvailible)
                        {

                            attachments = new List<QualificationConfigurationAttachment>();
                            foreach (var item in obj.QualityAssuranceStandardsAttachmentModels)
                            {
                                attachments.Add(new QualificationConfigurationAttachment(qualificationSupplierData.ID, item.FileName, item.FileNetReferenceId));
                            }
                            qualificationSupplierData.InsertQualificationConfigurationAttachments(attachments);
                        }
                    }
                    if (_TechnicalqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.EnvironmentalHealthSafetyStandards)
                    {
                        if (_TechnicalqualificationSupplierDataModels[i].QualificationLookupId != (int)Enums.QualificationEnvironmentStandardsLookup.EnvironmentGuideNotAvailible)
                        {
                            attachments = new List<QualificationConfigurationAttachment>();
                            foreach (var item in obj.EnvironmentalHealthSafetyStandardsAttachmentModels)
                            {
                                attachments.Add(new QualificationConfigurationAttachment(qualificationSupplierData.ID, item.FileName, item.FileNetReferenceId));
                            }
                            qualificationSupplierData.InsertQualificationConfigurationAttachments(attachments);
                        }
                    }

                    lstOfSupplierData.Add(qualificationSupplierData);
                }

                List<QualificationSupplierDataModel> _FinancialqualificationSupplierDataModels = obj.lstQualificationSupplierDataModel.Where(a => a.QualificationCategoryId == (int)Enums.QualificationItemCategory.Financial).ToList();
                for (int i = 0; i < _FinancialqualificationSupplierDataModels.Count; i++)
                {
                    decimal Min = _FinancialqualificationSupplierDataModels[i].Min;
                    decimal Max = _FinancialqualificationSupplierDataModels[i].Max;
                    decimal Value = _FinancialqualificationSupplierDataModels[i].Value;
                    decimal SupplierValue = _FinancialqualificationSupplierDataModels[i].SupplierValue;
                    decimal PointValue = 0;
                    int? QualificationLookupId = null;
                    if (_FinancialqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.CashRate)
                    {
                        decimal CashRateRatio = CheckDefaultValue(obj.CashEquivalents) / CheckDefaultValue(obj.CurrentLiabilities);
                        SupplierValue = CashRateRatio;
                        PointValue = await CalculatePointAsync(Min, Max, Math.Round(CashRateRatio, 2, MidpointRounding.AwayFromZero), _FinancialqualificationSupplierDataModels[i].QualificationItemTypeId);
                    }
                    else if (_FinancialqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.LiquidityRatio)
                    {

                        decimal LiquidityRatio = ((CheckDefaultValue(obj.CashEquivalents) + CheckDefaultValue(obj.AccountsReceivable)) / CheckDefaultValue(obj.CurrentLiabilities));
                        SupplierValue = LiquidityRatio;
                        PointValue = await CalculatePointAsync(Min, Max, Math.Round(LiquidityRatio, 2, MidpointRounding.AwayFromZero), _FinancialqualificationSupplierDataModels[i].QualificationItemTypeId);
                    }
                    else if (_FinancialqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.TradingRatio)
                    {
                        decimal TradingRatio = CheckDefaultValue(obj.CurrentAssets) / CheckDefaultValue(obj.CurrentLiabilities);
                        SupplierValue = TradingRatio;
                        PointValue = await CalculatePointAsync(Min, Max, Math.Round(TradingRatio, 2, MidpointRounding.AwayFromZero), _FinancialqualificationSupplierDataModels[i].QualificationItemTypeId);
                    }
                    qualificationSupplierData = new QualificationSupplierData(0, _FinancialqualificationSupplierDataModels[i].QualificationConfigurationId, tender.TenderId, SupplierValue, PointValue, _FinancialqualificationSupplierDataModels[i].QualificationItemId,
                        _FinancialqualificationSupplierDataModels[i].QualificationCategoryId, _FinancialqualificationSupplierDataModels[i].Weight, SupplierId, _FinancialqualificationSupplierDataModels[i].QualificationSubCategoryId, QualificationLookupId);
                    lstOfSupplierData.Add(qualificationSupplierData);
                }
            }
            return lstOfSupplierData;
        }

        public async Task<List<QualificationSupplierData>> EvaluateLargeQualification(PreQualificationApplyDocumentsModel obj, Tender tender)
        {
            string SupplierId = obj.SupplierId;
            List<QualificationSupplierData> lstOfSupplierData = new List<QualificationSupplierData>();
            QualificationSupplierData qualificationSupplierData;
            List<QualificationConfigurationAttachment> attachments;
            if (obj.lstQualificationSupplierDataModel != null && obj.lstQualificationSupplierDataModel.Count > 0)
            {
                bool InsuranceFileInsert = false;
                List<QualificationSupplierDataModel> _TechnicalqualificationSupplierDataModels = obj.lstQualificationSupplierDataModel.Where(a => a.QualificationCategoryId == (int)Enums.QualificationItemCategory.Technical).ToList();
                for (int i = 0; i < _TechnicalqualificationSupplierDataModels.Count; i++)
                {

                    decimal Min = _TechnicalqualificationSupplierDataModels[i].Min;
                    decimal Max = _TechnicalqualificationSupplierDataModels[i].Max;
                    decimal Value = _TechnicalqualificationSupplierDataModels[i].Value;
                    decimal SupplierValue = 0;
                    decimal SupplierSelectedPoint = 0;
                    int? SelectedId = 0;
                    int? QualificationLookupId = null;
                    decimal PointValue = 0;


                    if (_TechnicalqualificationSupplierDataModels[i].IsConfigure)
                    {
                        if (_TechnicalqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.PercentageOfSaudiEmployees)
                        {
                            decimal TotalEmployee = _TechnicalqualificationSupplierDataModels.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfEmployees).FirstOrDefault().SupplierValue;
                            decimal SaudiEmployeeNo = _TechnicalqualificationSupplierDataModels.Where(a => a.QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfSaudiEmployees).FirstOrDefault().SupplierValue;
                            SupplierValue = (TotalEmployee == 0 ? 0 : (SaudiEmployeeNo / TotalEmployee));
                        }
                        else if (_TechnicalqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears)
                        {
                            SupplierValue = (obj.lstQualificationSupplierProjectModel != null ? obj.lstQualificationSupplierProjectModel.Count : 0);
                        }

                        else if (_TechnicalqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.TotalValueProjectsLastThreeYears)
                        {
                            decimal TotalCost = (obj.lstQualificationSupplierProjectModel != null && obj.lstQualificationSupplierProjectModel.Count > 0 ? obj.lstQualificationSupplierProjectModel.Select(a => a.ContractValue).Sum() : 0);
                            SupplierValue = Math.Round(TotalCost, 2, MidpointRounding.AwayFromZero);
                        }

                        else if (_TechnicalqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.QualityAssuranceStandards)
                        {
                            SelectedId = _TechnicalqualificationSupplierDataModels[i].QualificationLookupId;
                            SupplierSelectedPoint = await EvaluateQualityAssuranceStandards(SelectedId);
                        }
                        else if (_TechnicalqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.EnvironmentalHealthSafetyStandards)
                        {
                            SelectedId = _TechnicalqualificationSupplierDataModels[i].QualificationLookupId;
                            SupplierSelectedPoint = await EvaluateEnvironmentalHealthSafetyStandards(SelectedId);
                        }


                        else if (_TechnicalqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.Insurance)
                        {
                            SelectedId = _TechnicalqualificationSupplierDataModels[i].QualificationLookupId;
                            SupplierSelectedPoint = await EvaluateInsuranceQualification(SelectedId);
                        }


                        else
                        {
                            SupplierValue = _TechnicalqualificationSupplierDataModels[i].SupplierValue;
                        }

                        if (_TechnicalqualificationSupplierDataModels[i].QualificationItemTypeId == (int)Enums.QualificationItemType.Select)
                        {
                            QualificationLookupId = SelectedId;
                            PointValue = SupplierSelectedPoint;
                        }
                        else
                        {
                            PointValue = await CalculatePointAsync(Min, Max, SupplierValue, _TechnicalqualificationSupplierDataModels[i].QualificationItemTypeId);
                        }
                    }
                    else
                    {
                        SupplierValue = _TechnicalqualificationSupplierDataModels[i].SupplierValue;
                        PointValue = 0;
                    }


                    qualificationSupplierData = new QualificationSupplierData(0, _TechnicalqualificationSupplierDataModels[i].QualificationConfigurationId, tender.TenderId, SupplierValue, PointValue, _TechnicalqualificationSupplierDataModels[i].QualificationItemId,
                     _TechnicalqualificationSupplierDataModels[i].QualificationCategoryId, _TechnicalqualificationSupplierDataModels[i].Weight, SupplierId, _TechnicalqualificationSupplierDataModels[i].QualificationSubCategoryId, QualificationLookupId,
                     _TechnicalqualificationSupplierDataModels[i].InsuranceCoverageEndDate, _TechnicalqualificationSupplierDataModels[i].InsuranceCoverage, _TechnicalqualificationSupplierDataModels[i].InsuranceProvider);

                    if (_TechnicalqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.QualityAssuranceStandards)
                    {
                        if (_TechnicalqualificationSupplierDataModels[i].QualificationLookupId != (int)Enums.QualificationQualityGuaranteeLookup.GuaranteeNotAvailible)
                        {
                            attachments = new List<QualificationConfigurationAttachment>();
                            foreach (var item in obj.QualityAssuranceStandardsAttachmentModels)
                            {
                                attachments.Add(new QualificationConfigurationAttachment(qualificationSupplierData.ID, item.FileName, item.FileNetReferenceId));
                            }
                            qualificationSupplierData.InsertQualificationConfigurationAttachments(attachments);
                        }
                    }
                    else if (_TechnicalqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.EnvironmentalHealthSafetyStandards)
                    {
                        if (_TechnicalqualificationSupplierDataModels[i].QualificationLookupId != (int)Enums.QualificationEnvironmentStandardsLookup.EnvironmentGuideNotAvailible)
                        {
                            attachments = new List<QualificationConfigurationAttachment>();
                            foreach (var item in obj.EnvironmentalHealthSafetyStandardsAttachmentModels)
                            {
                                attachments.Add(new QualificationConfigurationAttachment(qualificationSupplierData.ID, item.FileName, item.FileNetReferenceId));
                            }
                            qualificationSupplierData.InsertQualificationConfigurationAttachments(attachments);
                        }
                    }
                    else if (_TechnicalqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.NumberOfProjectsImplementedLastThreeYears)
                    {
                        if (obj.lstQualificationSupplierProjectModel != null && obj.lstQualificationSupplierProjectModel.Count > 0)
                        {
                            qualificationSupplierData.InsertSupplierProject(MapSupplierProjectModelToEntityAsync(obj.lstQualificationSupplierProjectModel, tender.TenderId, SupplierId));
                        }
                    }
                    lstOfSupplierData.Add(qualificationSupplierData);
                }
                List<QualificationSupplierDataModel> _FinancialqualificationSupplierDataModels = obj.lstQualificationSupplierDataModel.Where(a => a.QualificationCategoryId == (int)Enums.QualificationItemCategory.Financial).ToList();
                for (int i = 0; i < _FinancialqualificationSupplierDataModels.Count; i++)
                {
                    int? QualificationLookupId = null;
                    decimal PointValue = 0;

                    decimal Min = _FinancialqualificationSupplierDataModels[i].Min;
                    decimal Max = _FinancialqualificationSupplierDataModels[i].Max;
                    decimal Value = _FinancialqualificationSupplierDataModels[i].Value;
                    decimal SupplierValue = _FinancialqualificationSupplierDataModels[i].SupplierValue;

                    if (_FinancialqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.CashRate)
                    {
                        decimal CashRateRatio = CheckDefaultValue(obj.CashEquivalents) / CheckDefaultValue(obj.CurrentLiabilities);
                        SupplierValue = CashRateRatio;
                        PointValue = await CalculatePointAsync(Min, Max, Math.Round(CashRateRatio, 2, MidpointRounding.AwayFromZero), _FinancialqualificationSupplierDataModels[i].QualificationItemTypeId);
                    }

                    else if (_FinancialqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.LiquidityRatio)
                    {
                        decimal LiquidityRatio = ((CheckDefaultValue(obj.CashEquivalents) + CheckDefaultValue(obj.AccountsReceivable)) / CheckDefaultValue(obj.CurrentLiabilities));
                        SupplierValue = LiquidityRatio;
                        PointValue = await CalculatePointAsync(Min, Max, Math.Round(LiquidityRatio, 2, MidpointRounding.AwayFromZero), _FinancialqualificationSupplierDataModels[i].QualificationItemTypeId);
                    }
                    else if (_FinancialqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.TradingRatio)
                    {
                        decimal TradingRatio = CheckDefaultValue(obj.CurrentAssets) / CheckDefaultValue(obj.CurrentLiabilities);
                        SupplierValue = TradingRatio;
                        PointValue = await CalculatePointAsync(Min, Max, Math.Round(TradingRatio, 2, MidpointRounding.AwayFromZero), _FinancialqualificationSupplierDataModels[i].QualificationItemTypeId);
                    }

                    else if (_FinancialqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.RatioOfObligations)
                    {
                        decimal RatioOfObligations = CheckDefaultValue(obj.TotalLiabilities) / CheckDefaultValue(obj.TotalAssets);
                        SupplierValue = RatioOfObligations;
                        PointValue = await CalculatePointAsync(Min, Max, Math.Round(RatioOfObligations, 2, MidpointRounding.AwayFromZero), _FinancialqualificationSupplierDataModels[i].QualificationItemTypeId);
                    }
                    else if (_FinancialqualificationSupplierDataModels[i].QualificationItemId == (int)Enums.QualificationEvaluationItems.RateOfProfitability)
                    {
                        decimal ProfitCurrentYear = Math.Round(CheckDefaultValue(obj.NetProfit) / CheckDefaultValue(obj.TotalRevenue), 2, MidpointRounding.AwayFromZero);
                        decimal ProfitLastYear = Math.Round(CheckDefaultValue(obj.NetProfit2) / CheckDefaultValue(obj.TotalRevenue2), 2, MidpointRounding.AwayFromZero);


                        decimal _ProfieRate = Math.Round((ProfitCurrentYear / ProfitLastYear), 2, MidpointRounding.AwayFromZero);
                        double ProfieRate = Math.Round(Math.Pow(Convert.ToDouble(_ProfieRate), 0.3333333333333333), 2, MidpointRounding.AwayFromZero);


                        ProfieRate = Math.Round((ProfieRate - 1), 2, MidpointRounding.AwayFromZero);
                        SupplierValue = decimal.Parse(ProfieRate.ToString());
                        PointValue = await CalculatePointAsync(Min, Max, Math.Round((decimal)ProfieRate, 2, MidpointRounding.AwayFromZero), _FinancialqualificationSupplierDataModels[i].QualificationItemTypeId);
                    }


                    qualificationSupplierData = new QualificationSupplierData(0, _FinancialqualificationSupplierDataModels[i].QualificationConfigurationId, tender.TenderId, SupplierValue, PointValue, _FinancialqualificationSupplierDataModels[i].QualificationItemId,
                       _FinancialqualificationSupplierDataModels[i].QualificationCategoryId, _FinancialqualificationSupplierDataModels[i].Weight, SupplierId, _FinancialqualificationSupplierDataModels[i].QualificationSubCategoryId, QualificationLookupId);

                    lstOfSupplierData.Add(qualificationSupplierData);
                }
            }
            return lstOfSupplierData;
        }

        public async Task<int> EvaluateQualityAssuranceStandards(int? SelectedId)
        {
            switch (SelectedId)
            {
                case (int)Enums.QualificationQualityGuaranteeLookup.IsoCertificate:
                    return (int)Enums.Points.SevenPoint;
                case (int)Enums.QualificationQualityGuaranteeLookup.QualityGuarantee:
                    return (int)Enums.Points.ThreePoint;
                case (int)Enums.QualificationQualityGuaranteeLookup.GuaranteeNotAvailible:
                    return (int)Enums.Points.OnePoint;
                default:
                    return 0;
            }
        }

        public async Task<int> EvaluateEnvironmentalHealthSafetyStandards(int? SelectedId)
        {
            switch (SelectedId)
            {
                case (int)Enums.QualificationEnvironmentStandardsLookup.OSHA:
                    return (int)Enums.Points.SevenPoint;
                case (int)Enums.QualificationEnvironmentStandardsLookup.EnvironmentGuide:
                    return (int)Enums.Points.ThreePoint;
                case (int)Enums.QualificationEnvironmentStandardsLookup.EnvironmentGuideNotAvailible:
                    return (int)Enums.Points.OnePoint;
                default:
                    return 0;
            }
        }

        public async Task<int> EvaluateInsuranceQualification(int? SelectedId)
        {
            switch (SelectedId)
            {
                case (int)Enums.QualificationYesOrNoLookup.Yes:
                    return (int)Enums.Points.SevenPoint;
                case (int)Enums.QualificationYesOrNoLookup.No:
                    return (int)Enums.Points.OnePoint;
                default:
                    return 0;
            }
        }

        public async Task<List<QualificationSupplierDataYearly>> InsertSupplierMainDataWithYear(PreQualificationApplyDocumentsModel obj, int tenderId, string supplierId)
        {
            int[] ArrYears;
            List<QualificationSupplierDataYearly> lst = new List<QualificationSupplierDataYearly>();
            if (obj.QualificationTypeId == (int)Enums.PreQualificationType.Small)
            {
                ArrYears = new int[] { (int)Enums.QualificationYear.CurrentYear };

                for (int i = 0; i < ArrYears.Length; i++)
                {
                    int YearId = (int)Enums.QualificationYear.CurrentYear;
                    QualificationSupplierDataYearly CashEquivalentsObj = new QualificationSupplierDataYearly(0, (int)Enums.QualificationEvaluationItems.CashEquivalents, YearId, supplierId, obj.CashEquivalents, tenderId);
                    QualificationSupplierDataYearly AccountsReceivableObj = new QualificationSupplierDataYearly(0, (int)Enums.QualificationEvaluationItems.AccountsReceivable, YearId, supplierId, obj.AccountsReceivable, tenderId);
                    QualificationSupplierDataYearly CurrentAssetsObj = new QualificationSupplierDataYearly(0, (int)Enums.QualificationEvaluationItems.CurrentLiabilities, YearId, supplierId, obj.CurrentLiabilities, tenderId);

                    lst.Add(CashEquivalentsObj);
                    lst.Add(AccountsReceivableObj);
                    lst.Add(CurrentAssetsObj);
                }
            }

            if (obj.QualificationTypeId == (int)Enums.PreQualificationType.Medium)
            {
                ArrYears = new int[] { (int)Enums.QualificationYear.CurrentYear, (int)Enums.QualificationYear.SecondYear, (int)Enums.QualificationYear.ThirdYear };
                for (int i = 0; i < ArrYears.Length; i++)
                {

                    decimal CurrentAssets = (ArrYears[i] == 1 ? obj.CurrentAssets : ArrYears[i] == 2 ? obj.CurrentAssets1 : obj.CurrentAssets2);
                    decimal CashEquivalents = (ArrYears[i] == 1 ? obj.CashEquivalents : ArrYears[i] == 2 ? obj.CashEquivalents1 : obj.CashEquivalents2);
                    decimal AccountsReceivable = (ArrYears[i] == 1 ? obj.AccountsReceivable : ArrYears[i] == 2 ? obj.AccountsReceivable1 : obj.AccountsReceivable2);
                    decimal CurrentLiabilities = (ArrYears[i] == 1 ? obj.CurrentLiabilities : ArrYears[i] == 2 ? obj.CurrentLiabilities1 : obj.CurrentLiabilities2);

                    QualificationSupplierDataYearly CurrentAssetsObj = new QualificationSupplierDataYearly(0, (int)Enums.QualificationEvaluationItems.CurrentAssets, ArrYears[i], supplierId, CurrentAssets, tenderId);
                    QualificationSupplierDataYearly CashEquivalentsObj = new QualificationSupplierDataYearly(0, (int)Enums.QualificationEvaluationItems.CashEquivalents, ArrYears[i], supplierId, CashEquivalents, tenderId);
                    QualificationSupplierDataYearly AccountsReceivableObj = new QualificationSupplierDataYearly(0, (int)Enums.QualificationEvaluationItems.AccountsReceivable, ArrYears[i], supplierId, AccountsReceivable, tenderId);
                    QualificationSupplierDataYearly CurrentLiabilitiesObj = new QualificationSupplierDataYearly(0, (int)Enums.QualificationEvaluationItems.CurrentLiabilities, ArrYears[i], supplierId, CurrentLiabilities, tenderId);

                    lst.Add(CurrentAssetsObj);
                    lst.Add(CashEquivalentsObj);
                    lst.Add(AccountsReceivableObj);
                    lst.Add(CurrentLiabilitiesObj);

                }
            }


            if (obj.QualificationTypeId == (int)Enums.PreQualificationType.Large)
            {
                ArrYears = new int[] { (int)Enums.QualificationYear.CurrentYear, (int)Enums.QualificationYear.SecondYear, (int)Enums.QualificationYear.ThirdYear };
                for (int i = 0; i < ArrYears.Length; i++)
                {

                    decimal CurrentAssets = (ArrYears[i] == 1 ? obj.CurrentAssets : ArrYears[i] == 2 ? obj.CurrentAssets1 : obj.CurrentAssets2);
                    decimal CashEquivalents = (ArrYears[i] == 1 ? obj.CashEquivalents : ArrYears[i] == 2 ? obj.CashEquivalents1 : obj.CashEquivalents2);
                    decimal AccountsReceivable = (ArrYears[i] == 1 ? obj.AccountsReceivable : ArrYears[i] == 2 ? obj.AccountsReceivable1 : obj.AccountsReceivable2);
                    decimal CurrentLiabilities = (ArrYears[i] == 1 ? obj.CurrentLiabilities : ArrYears[i] == 2 ? obj.CurrentLiabilities1 : obj.CurrentLiabilities2);
                    decimal TotalAssets = (ArrYears[i] == 1 ? obj.TotalAssets : ArrYears[i] == 2 ? obj.TotalAssets1 : obj.TotalAssets2);
                    decimal TotalRevenue = (ArrYears[i] == 1 ? obj.TotalRevenue : ArrYears[i] == 2 ? obj.TotalRevenue1 : obj.TotalRevenue2);
                    decimal TotalLiabilities = (ArrYears[i] == 1 ? obj.TotalLiabilities : ArrYears[i] == 2 ? obj.TotalLiabilities1 : obj.TotalLiabilities2);
                    decimal NetProfit = (ArrYears[i] == 1 ? obj.NetProfit : ArrYears[i] == 2 ? obj.NetProfit1 : obj.NetProfit2);



                    QualificationSupplierDataYearly CurrentAssetsObj = new QualificationSupplierDataYearly(0, (int)Enums.QualificationEvaluationItems.CurrentAssets, ArrYears[i], supplierId, CurrentAssets, tenderId);
                    QualificationSupplierDataYearly CashEquivalentsObj = new QualificationSupplierDataYearly(0, (int)Enums.QualificationEvaluationItems.CashEquivalents, ArrYears[i], supplierId, CashEquivalents, tenderId);
                    QualificationSupplierDataYearly AccountsReceivableObj = new QualificationSupplierDataYearly(0, (int)Enums.QualificationEvaluationItems.AccountsReceivable, ArrYears[i], supplierId, AccountsReceivable, tenderId);
                    QualificationSupplierDataYearly CurrentLiabilitiesObj = new QualificationSupplierDataYearly(0, (int)Enums.QualificationEvaluationItems.CurrentLiabilities, ArrYears[i], supplierId, CurrentLiabilities, tenderId);


                    QualificationSupplierDataYearly TotalAssetsObj = new QualificationSupplierDataYearly(0, (int)Enums.QualificationEvaluationItems.TotalAssets, ArrYears[i], supplierId, TotalAssets, tenderId);
                    QualificationSupplierDataYearly TotalRevenueObj = new QualificationSupplierDataYearly(0, (int)Enums.QualificationEvaluationItems.TotalRevenue, ArrYears[i], supplierId, TotalRevenue, tenderId);
                    QualificationSupplierDataYearly TotalLiabilitiesObj = new QualificationSupplierDataYearly(0, (int)Enums.QualificationEvaluationItems.TotalLiabilities, ArrYears[i], supplierId, TotalLiabilities, tenderId);
                    QualificationSupplierDataYearly NetProfitObj = new QualificationSupplierDataYearly(0, (int)Enums.QualificationEvaluationItems.NetProfit, ArrYears[i], supplierId, NetProfit, tenderId);


                    lst.Add(CurrentAssetsObj);
                    lst.Add(CashEquivalentsObj);
                    lst.Add(AccountsReceivableObj);
                    lst.Add(CurrentLiabilitiesObj);
                    lst.Add(TotalAssetsObj);
                    lst.Add(TotalRevenueObj);
                    lst.Add(TotalLiabilitiesObj);
                    lst.Add(NetProfitObj);
                }
            }

            return lst;
        }


        public async Task<decimal> CalculatePointAsync(decimal Min, decimal Max, decimal Value, int ItemType)
        {
            decimal PointValue = 0;
            if (ItemType == (int)Enums.QualificationItemType.Percentage)
            {
                Value = Value * 100;
                if (Value > Max)
                {
                    PointValue = (int)Points.SevenPoint;
                }
                else if (Value < Min)
                {
                    PointValue = (int)Points.OnePoint;
                }
                else if (Value >= Min && Value <= Max)
                {
                    PointValue = (int)Points.ThreePoint;

                }
            }
            else if (ItemType == (int)Enums.QualificationItemType.Range)
            {
                if (Value > Max)
                {
                    PointValue = (int)Points.SevenPoint;
                }
                else if (Value < Min)
                {
                    PointValue = (int)Points.OnePoint;
                }
                else if (Value >= Min && Value <= Max)
                {
                    PointValue = (int)Points.ThreePoint;

                }
            }
            else if (ItemType == (int)Enums.QualificationItemType.Value)
            {
                if (Value > Max)
                {
                    PointValue = (int)Points.SevenPoint;
                }
                else if (Value < Min)
                {
                    PointValue = (int)Points.OnePoint;
                }
            }
            return PointValue;
        }

        public List<QualificationSupplierProject> MapSupplierProjectModelToEntityAsync(List<QualificationSupplierProjectModel> lst, int TenderId, string SupplierId)
        {
            List<QualificationSupplierProject> lstOfEntity = new List<QualificationSupplierProject>();
            foreach (var item in lst)
            {
                QualificationSupplierProject obj = new QualificationSupplierProject(0, TenderId, item.ContractValue, item.ContractName, item.Description, item.OwnerName, item.PhoneNumber, item.EmailAddress, SupplierId, item.StartDate, item.EndDate);
                lstOfEntity.Add(obj);
            }
            return lstOfEntity;
        }


        public async Task CalculateSubCategoryResult(Tender tender, List<QualificationSupplierData> lstOfQualificationSupplierData, string SupplierId)
        {
            List<QualificationSubCategoryResult> lstOfQualificationSubCategoryResult = new List<QualificationSubCategoryResult>();

            List<QualificationSubCategoryConfiguration> lstqualificationSubCategoryConfigurations = await _qualificationQueries.FindSubCategoryConfiguration(tender.TenderId);
            QualificationSubCategoryResult qualificationSubCategoryResult;

            if (tender.QualificationTypeId == (int)Enums.PreQualificationType.Small)
            {
                List<QualificationTypeCategory> lstqualificationTypeCategories = await _qualificationQueries.FindQualificationItems(tender.QualificationTypeId.Value);

                List<QualificationTypeCategory> lstqualificationTypeCategoriesTechnical = lstqualificationTypeCategories.Where(a => a.QualificationSubCategory.QualificationCategoryId == (int)Enums.QualificationItemCategory.Technical).ToList();
                for (int i = 0; i < lstqualificationTypeCategoriesTechnical.Count; i++)
                {
                    var xx = lstOfQualificationSupplierData.Where(a => a.QualificationSubCategoryId == lstqualificationTypeCategoriesTechnical[i].QualificationSubCategoryId);
                    var SubCategoryResult = lstOfQualificationSupplierData.Where(a => a.QualificationSubCategoryId == lstqualificationTypeCategoriesTechnical[i].QualificationSubCategoryId).Select(a => (a.Weight * a.PointValue) / 100).Sum();
                    qualificationSubCategoryResult = new QualificationSubCategoryResult(lstqualificationTypeCategoriesTechnical[i].QualificationSubCategoryId, lstqualificationTypeCategoriesTechnical[i].QualificationSubCategory.QualificationCategoryId, tender.TenderId,
                        SupplierId, Math.Round(Convert.ToDecimal(SubCategoryResult), 2, MidpointRounding.AwayFromZero), lstqualificationSubCategoryConfigurations.Where(a => a.QualificationSubCategoryId == lstqualificationTypeCategoriesTechnical[i].QualificationSubCategoryId).FirstOrDefault().Weight);

                    lstOfQualificationSubCategoryResult.Add(qualificationSubCategoryResult);
                }

                List<QualificationTypeCategory> lstqualificationTypeCategoriesFinancial = lstqualificationTypeCategories.Where(a => a.QualificationSubCategory.QualificationCategoryId == (int)Enums.QualificationItemCategory.Financial).ToList();
                for (int i = 0; i < lstqualificationTypeCategoriesFinancial.Count; i++)
                {
                    var SubCategoryResult = lstOfQualificationSupplierData.Where(a => a.QualificationSubCategoryId == lstqualificationTypeCategoriesFinancial[i].QualificationSubCategoryId).Select(a => (a.Weight * a.PointValue) / 100).Sum();

                    qualificationSubCategoryResult = new QualificationSubCategoryResult(lstqualificationTypeCategoriesFinancial[i].QualificationSubCategoryId, lstqualificationTypeCategoriesFinancial[i].QualificationSubCategory.QualificationCategoryId,
                        tender.TenderId, SupplierId, Math.Round(Convert.ToDecimal(SubCategoryResult), 2, MidpointRounding.AwayFromZero), lstqualificationSubCategoryConfigurations.Where(a => a.QualificationSubCategoryId == lstqualificationTypeCategoriesFinancial[i].QualificationSubCategoryId).FirstOrDefault().Weight);

                    lstOfQualificationSubCategoryResult.Add(qualificationSubCategoryResult);
                }
            }
            else if (tender.QualificationTypeId == (int)Enums.PreQualificationType.Medium)
            {
                List<QualificationTypeCategory> lstqualificationTypeCategories = await _qualificationQueries.FindQualificationItems(tender.QualificationTypeId.Value);

                List<QualificationTypeCategory> lstqualificationTypeCategoriesTechnical = lstqualificationTypeCategories.Where(a => a.QualificationSubCategory.QualificationCategoryId == (int)Enums.QualificationItemCategory.Technical).ToList();
                for (int i = 0; i < lstqualificationTypeCategoriesTechnical.Count; i++)
                {
                    var SubCategoryResult = lstOfQualificationSupplierData.Where(a => a.QualificationSubCategoryId == lstqualificationTypeCategoriesTechnical[i].QualificationSubCategoryId).Select(a => (a.Weight * a.PointValue) / 100).Sum();

                    qualificationSubCategoryResult = new QualificationSubCategoryResult(lstqualificationTypeCategoriesTechnical[i].QualificationSubCategoryId, lstqualificationTypeCategoriesTechnical[i].QualificationSubCategory.QualificationCategoryId, tender.TenderId,
                        SupplierId, Math.Round(Convert.ToDecimal(SubCategoryResult), 2, MidpointRounding.AwayFromZero), lstqualificationSubCategoryConfigurations.Where(a => a.QualificationSubCategoryId == lstqualificationTypeCategoriesTechnical[i].QualificationSubCategoryId).FirstOrDefault().Weight);


                    lstOfQualificationSubCategoryResult.Add(qualificationSubCategoryResult);
                }

                List<QualificationTypeCategory> lstqualificationTypeCategoriesFinancial = lstqualificationTypeCategories.Where(a => a.QualificationSubCategory.QualificationCategoryId == (int)Enums.QualificationItemCategory.Financial).ToList();
                for (int i = 0; i < lstqualificationTypeCategoriesFinancial.Count; i++)
                {
                    var SubCategoryResult = lstOfQualificationSupplierData.Where(a => a.QualificationSubCategoryId == lstqualificationTypeCategoriesFinancial[i].QualificationSubCategoryId).Select(a => (a.Weight * a.PointValue) / 100).Sum();

                    qualificationSubCategoryResult = new QualificationSubCategoryResult(lstqualificationTypeCategoriesFinancial[i].QualificationSubCategoryId, lstqualificationTypeCategoriesFinancial[i].QualificationSubCategory.QualificationCategoryId, tender.TenderId, SupplierId, Math.Round(Convert.ToDecimal(SubCategoryResult), 2, MidpointRounding.AwayFromZero),
                         lstqualificationSubCategoryConfigurations.Where(a => a.QualificationSubCategoryId == lstqualificationTypeCategoriesFinancial[i].QualificationSubCategoryId).FirstOrDefault().Weight);

                    lstOfQualificationSubCategoryResult.Add(qualificationSubCategoryResult);
                }
            }
            else if (tender.QualificationTypeId == (int)Enums.PreQualificationType.Large)
            {
                List<QualificationTypeCategory> lstqualificationTypeCategories = await _qualificationQueries.FindQualificationItems(tender.QualificationTypeId.Value);

                List<QualificationTypeCategory> lstqualificationTypeCategoriesTechnical = lstqualificationTypeCategories.Where(a => a.QualificationSubCategory.QualificationCategoryId == (int)Enums.QualificationItemCategory.Technical).ToList();
                for (int i = 0; i < lstqualificationTypeCategoriesTechnical.Count; i++)
                {

                    var SubCategoryResult = lstOfQualificationSupplierData.Where(a => a.QualificationSubCategoryId == lstqualificationTypeCategoriesTechnical[i].QualificationSubCategoryId).Select(a => (a.Weight * a.PointValue) / 100).Sum();
                    qualificationSubCategoryResult = new QualificationSubCategoryResult(lstqualificationTypeCategoriesTechnical[i].QualificationSubCategoryId, lstqualificationTypeCategoriesTechnical[i].QualificationSubCategory.QualificationCategoryId, tender.TenderId, SupplierId,
                       Math.Round(Convert.ToDecimal(SubCategoryResult), 2, MidpointRounding.AwayFromZero), lstqualificationSubCategoryConfigurations.Where(a => a.QualificationSubCategoryId == lstqualificationTypeCategoriesTechnical[i].QualificationSubCategoryId).FirstOrDefault().Weight);

                    lstOfQualificationSubCategoryResult.Add(qualificationSubCategoryResult);
                }

                List<QualificationTypeCategory> lstqualificationTypeCategoriesFinancial = lstqualificationTypeCategories.Where(a => a.QualificationSubCategory.QualificationCategoryId == (int)Enums.QualificationItemCategory.Financial).ToList();
                for (int i = 0; i < lstqualificationTypeCategoriesFinancial.Count; i++)
                {
                    var SubCategoryResult = lstOfQualificationSupplierData.Where(a => a.QualificationSubCategoryId == lstqualificationTypeCategoriesFinancial[i].QualificationSubCategoryId).Select(a => (a.Weight * a.PointValue) / 100).Sum();

                    qualificationSubCategoryResult = new QualificationSubCategoryResult(lstqualificationTypeCategoriesFinancial[i].QualificationSubCategoryId, lstqualificationTypeCategoriesFinancial[i].QualificationSubCategory.QualificationCategoryId, tender.TenderId, SupplierId,
                         Math.Round(Convert.ToDecimal(SubCategoryResult), 2, MidpointRounding.AwayFromZero), lstqualificationSubCategoryConfigurations.Where(a => a.QualificationSubCategoryId == lstqualificationTypeCategoriesFinancial[i].QualificationSubCategoryId).FirstOrDefault().Weight);
                    qualificationSubCategoryResult.Created();

                    lstOfQualificationSubCategoryResult.Add(qualificationSubCategoryResult);
                }
            }

            await _qualificationCommands.AddQualificationSubCategoryResult(lstOfQualificationSubCategoryResult);
            await CalculateQualificationCategoryResult(lstOfQualificationSubCategoryResult, tender, SupplierId, lstqualificationSubCategoryConfigurations);
        }


        public async Task CalculateQualificationCategoryResult(List<QualificationSubCategoryResult> lsfOfSubCategoryResult, Tender tender, string SupplierId, List<QualificationSubCategoryConfiguration> lstqualificationSubCategoryConfigurations)
        {


            decimal TotalSubCategoryTechnical = 0;
            decimal TotalSubCategoryFinancial = 0;
            List<QualificationCategoryResult> lstOfQualificationResult = new List<QualificationCategoryResult>();
            QualificationCategoryResult qualificationCategoryResult;
            if (tender.QualificationTypeId == (int)Enums.PreQualificationType.Small)
            {


                List<QualificationSubCategoryResult> lstOfTechnicalSubCategoryResult = lsfOfSubCategoryResult.Where(a => a.QualificationCategoryId == (int)Enums.QualificationItemCategory.Technical).ToList();
                for (int i = 0; i < lstOfTechnicalSubCategoryResult.Count; i++)
                {
                    var subCategoryCofiguration = lstqualificationSubCategoryConfigurations.Where(a => a.QualificationSubCategoryId == lstOfTechnicalSubCategoryResult[i].QualificationSubCategoryId).FirstOrDefault().Weight;
                    TotalSubCategoryTechnical += (lstOfTechnicalSubCategoryResult[i].ResultValue * Convert.ToDecimal(subCategoryCofiguration)) / 100;
                }

                qualificationCategoryResult = new QualificationCategoryResult(0, (int)Enums.QualificationItemCategory.Technical, tender.TenderId, SupplierId, Math.Round(TotalSubCategoryTechnical, 2, MidpointRounding.AwayFromZero), tender.TechnicalAdministrativeCapacity, tender.TechnicalAdministrativeCapacity);
                lstOfQualificationResult.Add(qualificationCategoryResult);

                List<QualificationSubCategoryResult> lstOfFinancialSubCategoryResult = lsfOfSubCategoryResult.Where(a => a.QualificationCategoryId == (int)Enums.QualificationItemCategory.Financial).ToList();
                for (int i = 0; i < lstOfFinancialSubCategoryResult.Count; i++)
                {
                    var subCategoryCofiguration = lstqualificationSubCategoryConfigurations.Where(a => a.QualificationSubCategoryId == lstOfFinancialSubCategoryResult[i].QualificationSubCategoryId).FirstOrDefault().Weight;

                    TotalSubCategoryFinancial += (lstOfFinancialSubCategoryResult[i].ResultValue * Convert.ToDecimal(subCategoryCofiguration)) / 100;
                }


                qualificationCategoryResult = new QualificationCategoryResult(0, (int)Enums.QualificationItemCategory.Financial, tender.TenderId, SupplierId, Math.Round(TotalSubCategoryFinancial, 2, MidpointRounding.AwayFromZero), tender.FinancialCapacity, tender.FinancialCapacity);
                lstOfQualificationResult.Add(qualificationCategoryResult);

            }

            else if (tender.QualificationTypeId == (int)Enums.PreQualificationType.Medium)
            {

                List<QualificationSubCategoryResult> lstOfTechnicalSubCategoryResult = lsfOfSubCategoryResult.Where(a => a.QualificationCategoryId == (int)Enums.QualificationItemCategory.Technical).ToList();
                for (int i = 0; i < lstOfTechnicalSubCategoryResult.Count; i++)
                {
                    var subCategoryCofiguration = lstqualificationSubCategoryConfigurations.Where(a => a.QualificationSubCategoryId == lstOfTechnicalSubCategoryResult[i].QualificationSubCategoryId).FirstOrDefault().Weight;

                    TotalSubCategoryTechnical += (lstOfTechnicalSubCategoryResult[i].ResultValue * Convert.ToDecimal(subCategoryCofiguration)) / 100;
                }


                qualificationCategoryResult = new QualificationCategoryResult(0, (int)Enums.QualificationItemCategory.Technical, tender.TenderId, SupplierId, Math.Round(TotalSubCategoryTechnical, 2, MidpointRounding.AwayFromZero), tender.TechnicalAdministrativeCapacity, tender.TechnicalAdministrativeCapacity);
                lstOfQualificationResult.Add(qualificationCategoryResult);


                List<QualificationSubCategoryResult> lstOfFinancialSubCategoryResult = lsfOfSubCategoryResult.Where(a => a.QualificationCategoryId == (int)Enums.QualificationItemCategory.Financial).ToList();
                for (int i = 0; i < lstOfFinancialSubCategoryResult.Count; i++)
                {
                    var subCategoryCofiguration = lstqualificationSubCategoryConfigurations.Where(a => a.QualificationSubCategoryId == lstOfFinancialSubCategoryResult[i].QualificationSubCategoryId).FirstOrDefault().Weight;

                    TotalSubCategoryFinancial += (lstOfFinancialSubCategoryResult[i].ResultValue * Convert.ToDecimal(subCategoryCofiguration)) / 100;
                }

                qualificationCategoryResult = new QualificationCategoryResult(0, (int)Enums.QualificationItemCategory.Financial, tender.TenderId, SupplierId, Math.Round(TotalSubCategoryFinancial, 2, MidpointRounding.AwayFromZero), tender.FinancialCapacity, tender.FinancialCapacity);
                lstOfQualificationResult.Add(qualificationCategoryResult);


            }

            else if (tender.QualificationTypeId == (int)Enums.PreQualificationType.Large)
            {
                List<QualificationSubCategoryResult> lstOfTechnicalSubCategoryResult = lsfOfSubCategoryResult.Where(a => a.QualificationCategoryId == (int)Enums.QualificationItemCategory.Technical).ToList();
                for (int i = 0; i < lstOfTechnicalSubCategoryResult.Count; i++)
                {
                    var subCategoryCofiguration = lstqualificationSubCategoryConfigurations.Where(a => a.QualificationSubCategoryId == lstOfTechnicalSubCategoryResult[i].QualificationSubCategoryId).FirstOrDefault().Weight;

                    TotalSubCategoryTechnical += (lstOfTechnicalSubCategoryResult[i].ResultValue * Convert.ToDecimal(subCategoryCofiguration)) / 100;
                }

                qualificationCategoryResult = new QualificationCategoryResult(0, (int)Enums.QualificationItemCategory.Technical, tender.TenderId, SupplierId, Math.Round(TotalSubCategoryTechnical, 2, MidpointRounding.AwayFromZero), tender.TechnicalAdministrativeCapacity, tender.TechnicalAdministrativeCapacity);
                lstOfQualificationResult.Add(qualificationCategoryResult);


                List<QualificationSubCategoryResult> lstOfFinancialSubCategoryResult = lsfOfSubCategoryResult.Where(a => a.QualificationCategoryId == (int)Enums.QualificationItemCategory.Financial).ToList();
                for (int i = 0; i < lstOfFinancialSubCategoryResult.Count; i++)
                {
                    var subCategoryCofiguration = lstqualificationSubCategoryConfigurations.Where(a => a.QualificationSubCategoryId == lstOfFinancialSubCategoryResult[i].QualificationSubCategoryId).FirstOrDefault().Weight;

                    TotalSubCategoryFinancial += (lstOfFinancialSubCategoryResult[i].ResultValue * Convert.ToDecimal(subCategoryCofiguration)) / 100;
                }

                qualificationCategoryResult = new QualificationCategoryResult(0, (int)Enums.QualificationItemCategory.Financial, tender.TenderId, SupplierId, Math.Round(TotalSubCategoryFinancial, 2, MidpointRounding.AwayFromZero), tender.FinancialCapacity, tender.FinancialCapacity);
                lstOfQualificationResult.Add(qualificationCategoryResult);

            }


            await _qualificationCommands.AddQualificationCategoryResult(lstOfQualificationResult);
            await CalculateQualificationFinalResult(lstOfQualificationResult, tender, SupplierId);
        }


        public async Task CalculateQualificationFinalResult(List<QualificationCategoryResult> LstOfQualificationCategoryResults, Tender tender, string SupplierId)
        {
            QualificationFinalResult qualificationFinalResult;
            decimal FinalResult = 0;


            for (int i = 0; i < LstOfQualificationCategoryResults.Count; i++)
            {
                if (LstOfQualificationCategoryResults[i].QualificationItemCategoryId != (int)Enums.QualificationItemCategory.Financial)
                {
                    FinalResult += (LstOfQualificationCategoryResults[i].Percentage * LstOfQualificationCategoryResults[i].ResultValue) / 100;
                }
                else
                {
                    FinalResult += LstOfQualificationCategoryResults[i].ResultValue;
                }
            }

            qualificationFinalResult = new QualificationFinalResult(tender.TenderId, SupplierId, Math.Round(FinalResult, 1, MidpointRounding.AwayFromZero), (FinalResult >= tender.TenderPointsToPass ? (int)Enums.QualificationResultLookup.Succeeded : (int)Enums.QualificationResultLookup.Failed));
            await _qualificationCommands.AddQualificationFinalResult(qualificationFinalResult);

        }

        public decimal CheckDefaultValue(decimal Value)
        {
            if (Value == 0 || Value == 0.0M || Value == 0.00M)
            {
                return _defaultValue;
            }
            return Value;
        }



        public async Task<SupplierPreQualificationDocument> FindTenderAttachmentsById(int preqSuplierId)
        {
            Check.ArgumentNotNullOrEmpty(nameof(preqSuplierId), preqSuplierId.ToString());
            return await _supplierqualificationDocumentQueries.GetSupplierDocumentByPreQualificationID(preqSuplierId);
        }
        public async Task<SupplierPreQualificationDocument> FindById(int SupplierPreQualificationDocumentId)
        {
            var qualification = await _supplierqualificationDocumentQueries.FindSupplierPreQualificationDocumentById(SupplierPreQualificationDocumentId);
            if (qualification.PreQualification.PreQualificationCommitteeId != _httpContextAccessor.HttpContext.User.SelectedCommittee() && qualification.PreQualification.OffersCheckingCommitteeId != _httpContextAccessor.HttpContext.User.SelectedCommittee())
                throw new AuthorizationException();
            return qualification;
        }


        public async Task<SupplierPreQualificationDocumentModel> FindPreQualificationDocumentById(int SupplierPreQualificationDocumentId, string SupplierId)
        {
            return await _supplierqualificationDocumentQueries.FindSupplierPreQualificationDocumentModel(SupplierPreQualificationDocumentId, SupplierId);
        }





        public async Task<SupplierPreQualificationDocument> CheckSuppierDocument(int PreQualificationId, string supplierId, bool editMode)
        {
            await _supplierqualificationdocumentdomain.CheckIfPreQualificationInCorrectStatus(PreQualificationId, Enums.TenderStatus.Approved);
            await _supplierqualificationdocumentdomain.CheckPresentationDateValidation(PreQualificationId, supplierId);

            if (!editMode)
                await _supplierqualificationdocumentdomain.CheckApplyingDuplicationValidation(PreQualificationId, supplierId);

            return null;
        }

        public async Task<List<QualificationSupplierDataModel>> GetQualificationSupplierData(int qualificationId)
        {
            Check.ArgumentNotNull(nameof(qualificationId), Convert.ToString(qualificationId));
            return await _supplierqualificationDocumentQueries.GetQualificationSupplierData(qualificationId);
        }

        public async Task<List<QualificationSupplierDataModel>> GetQualificationSupplierDataForEdit(int qualificationId, string supplierId)
        {
            Check.ArgumentNotNull(nameof(qualificationId), Convert.ToString(qualificationId));
            return await _supplierqualificationDocumentQueries.GetQualificationSupplierDataForEdit(qualificationId, supplierId);
        }


        public async Task<QualificationSupplierDataReviewViewModel> GetSupplierDataReviewModel(int qualificationId, string supplierId)
        {

            Check.ArgumentNotNull(nameof(qualificationId), Convert.ToString(qualificationId));
            return await _supplierqualificationDocumentQueries.GetSupplierDataReviewModel(qualificationId, supplierId);
        }


        public async Task<SupplierPreQualificationDocument> GetSuppierDocument(int PreQualificationId, string supplierId)
        {

            var supplierqualificationDocument = await _supplierqualificationDocumentQueries.GetSuppierDocument(PreQualificationId, supplierId);
            return supplierqualificationDocument;
        }





        public async Task<SupplierPreQualificationDocument> UpdateSupplierPreQualificationDocumentStatus(SupplierPreQualificationDocumentModel supplierPreQualificationDocumentModel)
        {
            Check.ArgumentNotNull(nameof(supplierPreQualificationDocumentModel), supplierPreQualificationDocumentModel);
            await _supplierQualificationDomainService.CheckPreQualificationDataValidation(supplierPreQualificationDocumentModel);
            SupplierPreQualificationDocument SupplierPreQualificationDocument = await FindById(supplierPreQualificationDocumentModel.SupplierPreQualificationDocumentId);
            SupplierPreQualificationDocument.UpdatePreQualificationDocumentStatus(supplierPreQualificationDocumentModel.PreQualificationResult, supplierPreQualificationDocumentModel.RejectionReason);
            var result = await _supplierqualificationDocumentcommands.UpdateAsync(SupplierPreQualificationDocument);
            return result;

        }

        public async Task ChooseQualificationResult(ChooseQualificationResultModel chooseQualificationResultModel)
        {
            QualificationFinalResult qualificationFinalResult = await _qualificationQueries.FindFinalResult(chooseQualificationResultModel.SupplierId, chooseQualificationResultModel.QualificationId);
            qualificationFinalResult.UpdateQualificationLookupId(chooseQualificationResultModel.FinalResultStatusId, chooseQualificationResultModel.FailingReason);
            await _qualificationCommands.UpdateFinalResultAsync(qualificationFinalResult);
        }



        #region Post-Qualification
        //public async Task<SupplierPreQualificationDocument> ApplyPostQualificationDocsforSupplier(PreQualificationApplyDocumentsModel PostQSupplier, string supplierCr)
        //{

        //    await _supplierqualificationdocumentdomain.CheckIfSupplierBlocked(supplierCr);
        //    await _supplierqualificationdocumentdomain.CheckIfPreQualificationInCorrectStatus(Util.Decrypt(PostQSupplier.PreQualificationIdString), Enums.TenderStatus.Approved);
        //    await _supplierqualificationdocumentdomain.CheckPresentationDateValidation(Util.Decrypt(PostQSupplier.PreQualificationIdString), supplierCr);
        //    await _supplierqualificationdocumentdomain.CheckApplyingDuplicationValidation(Util.Decrypt(PostQSupplier.PreQualificationIdString), supplierCr);
        //    SupplierPreQualificationDocument supplierprequalificationdocument = new SupplierPreQualificationDocument(supplierCr, PostQSupplier.QualificationStatusId, Util.Decrypt(PostQSupplier.PreQualificationIdString), PostQSupplier.PreQualificationResult.Value);
        //    List<SupplierPreQualificationAttachment> attachments = new List<SupplierPreQualificationAttachment>();
        //    foreach (var item in PostQSupplier.preQualificationSupplierAttachmentModels)
        //    {
        //        attachments.Add(new SupplierPreQualificationAttachment(item.FileName, item.FileNetReferenceId, supplierprequalificationdocument.SupplierPreQualificationDocumentId));
        //    }
        //    supplierprequalificationdocument.UpdateAttachments(attachments);

        //    await EvaluateSupplierData(PostQSupplier);


        //    SupplierPreQualificationDocument dbEntity = await _supplierqualificationDocumentcommands.CreateAsync<SupplierPreQualificationDocument>(supplierprequalificationdocument);
        //    await _supplierqualificationDocumentcommands.SaveAsync();
        //    var preQ = await _supplierqualificationDocumentQueries.GetPreQualificationById(supplierprequalificationdocument.PreQualificationId);
        //    await SendNotificationApplytPostQualificationDoc(preQ, supplierCr);
        //    return dbEntity;
        //}
        //private async Task SendNotificationApplytPostQualificationDoc(Tender postQualification, string cR)
        //{
        //    #region [Send Notification]
        //    NotificationArguments NotificationArguments = new NotificationArguments
        //    {
        //        BodyEmailArgs = new object[] { postQualification.ReferenceNumber },
        //        SubjectEmailArgs = new object[] { },
        //        PanelArgs = new object[] { postQualification.ReferenceNumber },
        //        SMSArgs = new object[] { postQualification.ReferenceNumber }
        //    };
        //    #endregion
        //    MainNotificationTemplateModel mainNotificationTemplateModel = new MainNotificationTemplateModel(NotificationArguments,
        //                  $"Qualification/PreQualificationSupplierDetails?qualificationId={Util.Encrypt(postQualification.TenderId)}",
        //                  NotificationEntityType.Tender,
        //                  postQualification.TenderId.ToString(),
        //                  postQualification.BranchId);

        //    await _notificationAppService.SendNotificationForSuppliers(NotificationOperations.Supplier.OperationsOnPostqualification.ApplyPostQualificationDocuments, new List<string> { cR }, mainNotificationTemplateModel);
        //}


        //public async Task<SupplierPreQualificationDocument> GetPostQualificationSuppierDocument(int PostQualificationId, string supplierId)
        //{
        //    await _supplierqualificationdocumentdomain.CheckIfPreQualificationInCorrectStatus(PostQualificationId, Enums.TenderStatus.Approved);
        //    await _supplierqualificationdocumentdomain.CheckPresentationDateValidation(PostQualificationId, supplierId);
        //    return await _supplierqualificationDocumentQueries.GetSuppierDocument(PostQualificationId, supplierId);
        //}




        #endregion


        public async Task<SupplierPreQualificationDocument> PostQualificationChecking(SupplierPreQualificationDocumentModel supplierPreQualificationDocumentModel)
        {
            Check.ArgumentNotNull(nameof(supplierPreQualificationDocumentModel), supplierPreQualificationDocumentModel);
            await _supplierQualificationDomainService.CheckPreQualificationDataValidation(supplierPreQualificationDocumentModel);
            SupplierPreQualificationDocument SupplierPreQualificationDocument = await FindById(supplierPreQualificationDocumentModel.SupplierPreQualificationDocumentId);
            SupplierPreQualificationDocument.UpdatePreQualificationDocumentStatus(supplierPreQualificationDocumentModel.PreQualificationResult, supplierPreQualificationDocumentModel.RejectionReason);
            if (SupplierPreQualificationDocument.PreQualification.TenderStatusId == (int)Enums.TenderStatus.Approved)
                SupplierPreQualificationDocument.PreQualification.UpdateTenderStatus(Enums.TenderStatus.DocumentChecking, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.SendPostQulaificationForApproveChecking);
            var result = await _supplierqualificationDocumentcommands.UpdateAsync(SupplierPreQualificationDocument);
            return result;

        }

    }
}
