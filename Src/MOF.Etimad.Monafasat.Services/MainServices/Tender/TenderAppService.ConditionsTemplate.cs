using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.SharedKernel.Exceptions;
using MOF.Etimad.Monafasat.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.Services
{
    public partial class TenderAppService
    {
        #region ConditionsTemplate

        public async Task AddEditIntroductionTemplate(EditConditionTemplateSecondSectionModel model, int branchId)
        {
            Tender tender = await _tenderQueries.FindTenderWithConditionsTemplateIntroductionById(Util.Decrypt(model.EncryptedTenderId), branchId);
            if (tender == null)
            {
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            }
            if (tender.TenderConditionsTemplate == null)
                tender.CreateConditionsTemplate();
            IsValidToAddEditIntroductionTemplate(tender, model);
            UpdateTenderConditionTemplateStatusId(tender, Enums.TenderConditoinsStatus.GeneralStage);

            tender.TenderConditionsTemplate.UpdateConditionsTemplateSecondStep(model.AgencyDecalration, model.Description, model.AgentName, model.AgentJob, model.AgentFax, model.AgentPhone,
                model.TenderFragmentation, model.HideTenderFragmentation, model.AgentEmail, model.CerificatesIDs, model.HideCerificatesIDs);
            await _tenderCommands.UpdateAsync(tender);
        }
        public async Task AddEditLocalContentTemplate(LocalContentModel model, int branchId)
        {
            IsValidToAddEditLocalContentModel(model);
            Tender tender = await _tenderQueries.FindTenderWithConditionsTemplateById(Util.Decrypt(model.EncryptedTenderId), branchId);
            var templates = await _tenderQueries.FindTenderForLocalContentByTenderId(Util.Decrypt(model.EncryptedTenderId), branchId);
            if (tender == null)
            {
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            }
            var versionId = await _tenderQueries.GetCurrentTenderActivityVersion(tender.TenderId);

            if (tender.TenderLocalContent == null)
                tender.CreateTenderLocalContent();

            IsValidToAddEditLocalContent(tender, templates, model);
            var tenderAttachments = new List<TenderAttachment>();
            foreach (var item in model.TenderAttachments)
            {
                tenderAttachments.Add(new TenderAttachment(item.Name, item.FileNetReferenceId, item.AttachmentTypeId, null, null));
            }
            if (model.TenderAttachments.Any())
            {
                tender.UpdateLocalContentAttachments(tenderAttachments, _httpContextAccessor.HttpContext.User.UserId());
            }
            if (versionId >= (int)Enums.ActivityVersions.Sprint7Activities)
            {
                var hasInvitation = ((tender.PreQualificationId != null || tender.TenderTypeId == (int)Enums.TenderType.SecondStageTender || tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.ReverseBidding
                       || tender.TenderTypeId == (int)Enums.TenderType.LimitedTender || tender.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement || tender.TenderTypeId == (int)Enums.TenderType.Competition || tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects) && tender.InvitationTypeId == (int)Enums.InvitationType.Specific)
                       || (tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase && tender.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles);
                if (!hasInvitation)
                {
                    tender.UpdateTenderStatus(Enums.TenderStatus.Established, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.CreateTender);
                }
                else
                {
                    tender.UpdateTenderStatus(Enums.TenderStatus.UnderEstablishing);
                }

            }


            UpdateTenderConditionTemplateStatusId(tender, Enums.TenderConditoinsStatus.LocalContent);
            tender.TenderLocalContent.UpdateTenderLocalContent(tender.TenderId, model.LocalContentMechanismIDs, model.IsApplyProvisionsMandatoryList, model.MinimumBaseline, model.MinimumPercentageLocalContentTarget);
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToAddEditLocalContent(Tender tender, List<Template> templates, LocalContentModel model)
        {
            if ((!model.LocalContentMechanismIDs.Any(x => x == (int)Enums.LocalContentMechanism.LocalContentConditionsWeight) && !model.LocalContentMechanismIDs.Any(x => x == (int)Enums.LocalContentMechanism.MinimumBaseline)) && !tender.IsTenderContainsTawreedTables.Value && tender.EstimatedValue > 50000000 && !templates.Any(x => x.ActivitytemplatId == 22 || x.ActivitytemplatId == 13))
            {
                throw new BusinessRuleException("الرجاء اختيار اختيار آلية وزن المحتوى المحلي او الحد الادني المطلوب للمحتوي المحلي");
            }
        }

        private void IsValidToAddEditLocalContentModel(LocalContentModel model)
        {
            if (model.LocalContentMechanismIDs == null || !model.LocalContentMechanismIDs.Any())
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.LocalContentMechanismIMessage);
            }
            if (model.LocalContentMechanismIDs.Any(x => x == (int)Enums.LocalContentMechanism.LocalContentConditionsWeight) && model.LocalContentMechanismIDs.Any(x => x == (int)Enums.LocalContentMechanism.MinimumBaseline))
            {
                throw new BusinessRuleException("لا يمكن الجمع بين ألية الحد الادنى وآلية وزن المحتوى المحلي");
            }
            if (model.LocalContentMechanismIDs.Any(x => x == (int)Enums.LocalContentMechanism.LocalContentConditionsWeight) && (model.MinimumBaseline > 100 || model.MinimumBaseline < 0))
            {
                throw new BusinessRuleException("الرجاء ادخال قيمة الحد الأدنى لخط الأساس اكبر من او يساوي صفر و اقل من او يساوي مائة");
            }
            if (model.LocalContentMechanismIDs.Any(x => x == (int)Enums.LocalContentMechanism.MinimumBaseline) && (model.MinimumPercentageLocalContentTarget > 100 || model.MinimumPercentageLocalContentTarget < 0))
            {
                throw new BusinessRuleException("الرجاء ادخال قيمة الحد الادنى لنسبة المحتوى المحلي المستهدفة اكبر من او يساوي صفر و اقل من او يساوي مائة");
            }
            if (model.LocalContentMechanismIDs.Any(x => x == (int)Enums.LocalContentMechanism.MinimumBaseline) && string.IsNullOrEmpty(model.StudyMinimumTargetFileNetReferenceId))
            {
                throw new BusinessRuleException("الرجاء ارفاق ملف دراسة الحد الأدنى للمحتوى المحلي المستهدف");
            }
        }


        private void IsValidToAddEditIntroductionTemplate(Tender tender, EditConditionTemplateSecondSectionModel model, bool ValidatationFromNextStep = false)
        {
            if (tender == null)
                throw new AuthorizationException();
            if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.Description)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.Description)))
            {
                var value = Resources.TenderResources.DisplayInputs.TenderDescription;
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
            }
            if (model != null && !model.HideCerificatesIDs)
            {
                if ((!ValidatationFromNextStep && ((model?.CerificatesIDs?.Count ?? 0) == 0)) || (ValidatationFromNextStep && (tender.TenderConditionsTemplate?.TemplateCertificates?.Count ?? 0) == 0))
                {
                    var value = Resources.TenderResources.DisplayInputs.SystemCertificates;
                    throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseSelect + value);
                }
            }
            if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.AgentName)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.AgentName)))
            {
                var value = Resources.SharedResources.DisplayInputs.Name;
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
            }
            if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.AgentPhone)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.AgentPhone)))
            {
                throw new BusinessRuleException(Resources.CommitteeResources.ErrorMessages.InvalidPhoneNumber);
            }
            if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.AgentFax)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.AgentFax)))
            {
                throw new BusinessRuleException(Resources.CommitteeResources.ErrorMessages.InvalidFaxNumber);
            }
            if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.AgentJob)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.AgentJob)))
            {
                var value = Resources.SharedResources.DisplayInputs.Job;
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
            }
            if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.AgentEmail)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.AgentEmail)))
            {
                throw new BusinessRuleException(Resources.CommitteeResources.ErrorMessages.EmailInvalid);
            }
            if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.AgencyDecalration)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.AgencyDecalration)))
            {
                var value = Resources.TenderResources.DisplayInputs.AgencyDecalration;
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
            }
        }

        public async Task AddEditPreparingOffersTemplate(EditConditionTemplateThridSectionModel model, int branchId)
        {
            Tender tender = await _tenderQueries.FindTenderWithConditionsTemplateIntroductionById(Util.Decrypt(model.EncryptedTenderId), branchId);
            if (tender == null)
            {
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            }
            IsValidToAddEditPreparingOffersTemplate(tender, model);
            UpdateTenderConditionTemplateStatusId(tender, Enums.TenderConditoinsStatus.PreparteOffers);

            tender.TenderConditionsTemplate.UpdatePreparingOffersStep(model.FinancialOfferDocuments, model.HideTechnicalDocumentSections, model.TechnicalOfferDocuments, model.MaxTimeToAnswerQuestions,
                model.AlternativeEmailForCommuncation, model.DocumentStyle, model.AllowancePeriodToAddOffersIfNotAddedBeofre, model.AllowedOfferSiningDays, model.WritePrice, model.ConfirimJoiningTheTender, model.OffersChecking, model.OffersEvaluationCriteria);

            if (model.VersionId >= (int)Enums.ActivityVersions.Sprint7Activities)
            {
                tender.TenderDates.UpdateTenderConfirmationLetterDate(model?.ParticipationConfirmationLetterDate);
            }

            _genericCommandRepository.Update(tender);
            await _genericCommandRepository.SaveAsync();
        }

        private void IsValidToAddEditPreparingOffersTemplate(Tender tender, EditConditionTemplateThridSectionModel model, bool ValidatationFromNextStep = false)
        {
            if (!(tender.ConditionTemplateStageStatusId == (int)Enums.TenderConditoinsStatus.PreparteOffers ||

    tender.ConditionTemplateStageStatusId == (int)Enums.TenderConditoinsStatus.EvaluateOffers ||
    tender.ConditionTemplateStageStatusId == (int)Enums.TenderConditoinsStatus.ContractingRequirments ||
    tender.ConditionTemplateStageStatusId == (int)Enums.TenderConditoinsStatus.TechnicalDeclerations ||
    tender.ConditionTemplateStageStatusId == (int)Enums.TenderConditoinsStatus.Specifications ||
    tender.ConditionTemplateStageStatusId == (int)Enums.TenderConditoinsStatus.LocalContent
    ))
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.MustFillPreviousInformation);
            }


            if ((!ValidatationFromNextStep && (model?.MaxTimeToAnswerQuestions ?? 0) <= 0) || (ValidatationFromNextStep && (tender.TenderConditionsTemplate?.MaxTimeToAnswerQuestions ?? 0) <= 0))
            {
                var value = Resources.TenderResources.DisplayInputs.MaxTimeToAnswerQuestions;
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
            }
            if ((!ValidatationFromNextStep && !(model?.HideTechnicalDocumentSections ?? true)) || (ValidatationFromNextStep && !(tender.TenderConditionsTemplate?.HideTechnicalDocumentSections ?? true)))
            {
                if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.TechnicalOfferDocuments)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.TechnicalOfferDocuments)))
                {
                    var value = Resources.OfferResources.DisplayInputs.TechnicalDocuments;
                    throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
                }
            }
            // 63138 uncomment below if you want the finance documents be mandatory
            if (!ValidatationFromNextStep && tender.TenderTypeId != (int)Enums.TenderType.Competition && tender.TenderTypeId != (int)Enums.TenderType.FirstStageTender && tender.TenderTypeId != (int)Enums.TenderType.ReverseBidding)
            {
                if (string.IsNullOrEmpty(model?.FinancialOfferDocuments))
                {
                    var value = Resources.OfferResources.DisplayInputs.FinancialDocuments;
                    throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
                }
            }
            if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.AlternativeEmailForCommuncation)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.AlternativeEmailForCommuncation)))
            {
                var value = Resources.TenderResources.DisplayInputs.AlternativeEmailForCommuncation;
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.PleaseCheckTheValueOf + value);
            }
            if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.DocumentStyle)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.DocumentStyle)))
            {
                var value = Resources.TenderResources.DisplayInputs.DocumentStyle;
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
            }
        }

        public async Task AddEditTechnicalDeclerationsTemplate(EditConditionTemplateSeventhSectionModel model, int branchId)
        {
            Tender tender = await _tenderQueries.FindTenderWithConditionsTemplateTechnicalOutputs(Util.Decrypt(model.EncryptedTenderId), branchId);
            if (tender == null)
            {
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            }
            IsValidToAddEditTechnicalDeclerationsTemplate(tender, model);
            UpdateTenderConditionTemplateStatusId(tender, Enums.TenderConditoinsStatus.TechnicalDeclerations);
            tender.UpdateConditionsTemplateSeventhStep(
                 tenderConditionsTemplateTechnicalOutput: model.TenderConditionsTemplateOutputs != null ? model.TenderConditionsTemplateOutputs.Select(a => new TenderConditionsTemplateTechnicalOutput
                   (
                       tenderConditionsTemplateTechnicalOutputId: a.TenderConditionsTemplateTechnicalOutputId,
                       outState: a.OutputStage,
                       outputName: a.OutputName,
                       outDescriptions: a.OutputDescriptions,
                       outDeliveryTime: a.OutputDeliveryTime
                   )).ToList() : new List<TenderConditionsTemplateTechnicalOutput>(),
                 tenderConditionsTemplateTechnicalDelrations: model.TechnicalDeclrations != null ? model.TechnicalDeclrations.Select(a => new TenderConditionsTemplateTechnicalDeclration
                 (
                     tenderConditionsTemplateTechnicalDeclrationId: a.TenderConditionsTemplateTechnicalDeclrationId,
                     term: a.Term,
                     decleration: a.Decleration
                 )).ToList() : new List<TenderConditionsTemplateTechnicalDeclration>(),
                projectsScope: model.ProjectsScope,
                worksProgram: model.WorksProgram,
                workLocationDetails: model.WorkLocationDetails,
                servicesAndWorkImplementationsMethod: model.ServicesAndWorkImplementationsMethod
                );
            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToAddEditTechnicalDeclerationsTemplate(Tender tender, EditConditionTemplateSeventhSectionModel model)
        {
            List<int> listOfSections = Array.ConvertAll(model.ListOfSections.Split(','), int.Parse).ToList();

            if (!(tender.ConditionTemplateStageStatusId == (int)Enums.TenderConditoinsStatus.EvaluateOffers ||
                tender.ConditionTemplateStageStatusId == (int)Enums.TenderConditoinsStatus.ContractingRequirments ||
                tender.ConditionTemplateStageStatusId == (int)Enums.TenderConditoinsStatus.Specifications))
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.MustFillPreviousInformation);
            }
            if (model.VersionId >= (int)Enums.ActivityVersions.Sprint7Activities && model.TechnicalDeclrations == null)
            {
                var value = Resources.TenderResources.DisplayInputs.TermsAndDefinitions;
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
            }
            if (listOfSections.Contains((int)Enums.ConditionsTemplateSections.Outputs) && !model.ShowGeneralOnly && model.TenderConditionsTemplateOutputs == null)
            {
                var value = Resources.TenderResources.DisplayInputs.TenderConditionsTemplateOutputs;
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
            }
            if (string.IsNullOrEmpty(model?.ProjectsScope))
            {
                var value = Resources.TenderResources.DisplayInputs.ProjectsScope;
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
            }
            if (string.IsNullOrEmpty(model?.WorksProgram))
            {
                var value = Resources.TenderResources.DisplayInputs.WorksProgram;
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
            }
            if (string.IsNullOrEmpty(model?.WorkLocationDetails))
            {
                var value = Resources.TenderResources.DisplayInputs.WorkExcutionlocation;
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
            }
            if (listOfSections.Contains((int)Enums.ConditionsTemplateSections.ImplementaionMethod) && string.IsNullOrEmpty(model?.ServicesAndWorkImplementationsMethod))
            {
                var value = Resources.TenderResources.DisplayInputs.ServicesAndWorkImplementationsMethod;
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
            }
        }

        public async Task AddEditDescriptionAndConditionsTemplate(EditConditionTemplateEighthSectionModel model, int branchId)
        {
            Tender tender = await _tenderQueries.FindTenderWithConditionsTemplateById(Util.Decrypt(model.EncryptedTenderId), branchId);
            if (tender == null)
            {
                throw new UnHandledAccessException(Resources.SharedResources.ErrorMessages.YouHaveNoAccess);
            }
            var versionId = await _tenderQueries.GetCurrentTenderActivityVersion(tender.TenderId);
            IsValidToAddEditDescriptionAndConditionsTemplate(tender, model);

            if (versionId < (int)Enums.ActivityVersions.Sprint7Activities)
            {
                var hasInvitation = ((tender.PreQualificationId != null || tender.TenderTypeId == (int)Enums.TenderType.SecondStageTender || tender.TenderTypeId == (int)Enums.TenderType.NewDirectPurchase || tender.TenderTypeId == (int)Enums.TenderType.ReverseBidding
|| tender.TenderTypeId == (int)Enums.TenderType.LimitedTender || tender.TenderTypeId == (int)Enums.TenderType.FrameworkAgreement || tender.TenderTypeId == (int)Enums.TenderType.Competition || tender.TenderTypeId == (int)Enums.TenderType.NationalTransformationProjects) && tender.InvitationTypeId == (int)Enums.InvitationType.Specific)
|| (tender.TenderTypeId == (int)Enums.TenderType.CurrentDirectPurchase && tender.OfferPresentationWayId == (int)Enums.OfferPresentationWayId.TwoSepratedFiles);
                if (!hasInvitation)
                {
                    tender.UpdateTenderStatus(Enums.TenderStatus.Established, "", _httpContextAccessor.HttpContext.User.UserId(), TenderActions.CreateTender);
                }
                else
                {
                    tender.UpdateTenderStatus(Enums.TenderStatus.UnderEstablishing);
                }
            }

            var tenderAttachments = new List<TenderAttachment>();
            foreach (var item in model.TenderAttachments)
            {
                tenderAttachments.Add(new TenderAttachment(item.Name, item.FileNetReferenceId, item.AttachmentTypeId, null, null));
            }
            tender.UpdateAttachments(tenderAttachments, _httpContextAccessor.HttpContext.User.UserId());

            var hasMaterialInformation = tender.TenderActivities.Count == 1 && tender.TenderActivities.FirstOrDefault().Activity.ActivityTemplateVersions.Any(t => t.TemplateId == (int)Enums.TenderActivityTamplate.MedicalMaterials);
            tender.TenderConditionsTemplate.UpdateConditionsTemplateEighthStep(model.WorkforceSpecifications, model.MaterialsSpecifications, model.SpecialConditions, model.Attachments, model.EquipmentsSpecifications,
            model.ContractBasedOnPerformanceDetails, model.QualitySpecifications, model.SafetySpecifications, model.BasicInformation,
            model.RequiredDcoumentationBefore, model.Tests, model.IntilizationAndStartWork, model.RequiredDcoumentationAfter,
            model.Trainging, model.Guarantee, model.Maintanance, model.MachineGuarantee, model.MachineMaintanance, hasMaterialInformation
            );

            await _tenderCommands.UpdateAsync(tender);
        }

        private void IsValidToAddEditDescriptionAndConditionsTemplate(Tender tender, EditConditionTemplateEighthSectionModel model, bool ValidatationFromNextStep = false)
        {
            List<int> listOfSections = Array.ConvertAll(model.ListOfSections.Split(','), int.Parse).ToList();
            List<int> ListOfTemplateIds = Array.ConvertAll(model.TemplateIds.Split(','), int.Parse).ToList();

            if (tender.ConditionTemplateStageStatusId != (int)Enums.TenderConditoinsStatus.Specifications)
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.MustFillPreviousInformation);
            }
            if (listOfSections.Contains((int)Enums.ConditionsTemplateSections.WorkForce))
            {
                if (ListOfTemplateIds.Count == 1 && ListOfTemplateIds.FirstOrDefault() != (int)Enums.ConditionsTemplateCategory.GeneralSuppliesSupply)
                {
                    if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.WorkforceSpecifications)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.WorkforceSpecifications)))
                    {
                        var value = Resources.TenderResources.DisplayInputs.WorkforceSpecifications;
                        throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
                    }
                }
            }
            if (listOfSections.Contains((int)Enums.ConditionsTemplateSections.Materials))
            {
                if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.MaterialsSpecifications)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.MaterialsSpecifications)))
                {
                    var value = Resources.TenderResources.DisplayInputs.MaterialsSpecifications;
                    throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
                }
            }
            if (listOfSections.Contains((int)Enums.ConditionsTemplateSections.Equipments))
            {
                if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.EquipmentsSpecifications)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.EquipmentsSpecifications)))
                {
                    var value = Resources.TenderResources.DisplayInputs.EquipmentsSpecifications;
                    throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
                }
            }

            if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.QualitySpecifications)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.QualitySpecifications)))
            {
                var value = Resources.TenderResources.DisplayInputs.QualitySpecifications;
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
            }

            if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.SafetySpecifications)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.SafetySpecifications)))
            {
                var value = Resources.TenderResources.DisplayInputs.SafetySpecifications;
                throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
            }
        }

        #endregion ConditionsTemplate
        private void UpdateTenderConditionTemplateStatusId(Tender tender, Enums.TenderConditoinsStatus currentStage)
        {
            int currentConditionStatusId = tender.ConditionTemplateStageStatusId.HasValue ? tender.ConditionTemplateStageStatusId.Value : 1;
            switch (currentStage)
            {
                case Enums.TenderConditoinsStatus.GeneralStage:
                    {
                        if (!(currentConditionStatusId == (int)Enums.TenderConditoinsStatus.PreparteOffers ||
                            currentConditionStatusId == (int)Enums.TenderConditoinsStatus.EvaluateOffers ||
                            currentConditionStatusId == (int)Enums.TenderConditoinsStatus.ContractingRequirments ||
                            currentConditionStatusId == (int)Enums.TenderConditoinsStatus.TechnicalDeclerations ||
                            currentConditionStatusId == (int)Enums.TenderConditoinsStatus.Specifications))
                        {
                            tender.UpdateTenderConditoinsStatus(Enums.TenderConditoinsStatus.PreparteOffers);
                        }
                        break;
                    }
                case Enums.TenderConditoinsStatus.PreparteOffers:
                    {
                        if (!(currentConditionStatusId == (int)Enums.TenderConditoinsStatus.EvaluateOffers ||
                            currentConditionStatusId == (int)Enums.TenderConditoinsStatus.ContractingRequirments ||
                            currentConditionStatusId == (int)Enums.TenderConditoinsStatus.TechnicalDeclerations ||
                           currentConditionStatusId == (int)Enums.TenderConditoinsStatus.Specifications))
                        {
                            tender.UpdateTenderConditoinsStatus(Enums.TenderConditoinsStatus.EvaluateOffers);
                        }
                        break;
                    }
                case Enums.TenderConditoinsStatus.EvaluateOffers:
                    {
                        if (!(currentConditionStatusId == (int)Enums.TenderConditoinsStatus.ContractingRequirments ||
                            currentConditionStatusId == (int)Enums.TenderConditoinsStatus.TechnicalDeclerations ||
                            currentConditionStatusId == (int)Enums.TenderConditoinsStatus.Specifications))
                        {
                            tender.UpdateTenderConditoinsStatus(Enums.TenderConditoinsStatus.ContractingRequirments);
                        }
                        break;
                    }
                case Enums.TenderConditoinsStatus.ContractingRequirments:
                    {
                        if (!(currentConditionStatusId == (int)Enums.TenderConditoinsStatus.TechnicalDeclerations ||
                            currentConditionStatusId == (int)Enums.TenderConditoinsStatus.Specifications))
                        {
                            tender.UpdateTenderConditoinsStatus(Enums.TenderConditoinsStatus.TechnicalDeclerations);
                        }
                        break;
                    }
                case Enums.TenderConditoinsStatus.TechnicalDeclerations:
                    {
                        if (currentConditionStatusId != (int)Enums.TenderConditoinsStatus.Specifications)
                        {
                            tender.UpdateTenderConditoinsStatus(Enums.TenderConditoinsStatus.Specifications);
                        }
                        break;
                    }
                default:
                    break;
            }
        }
    }
}
