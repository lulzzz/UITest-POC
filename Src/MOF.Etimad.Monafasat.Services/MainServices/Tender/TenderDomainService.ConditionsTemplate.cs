using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernal.Exceptions;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System.Linq;

namespace MOF.Etimad.Monafasat.Services
{
    public partial class TenderDomainService
    {
        public void IsValidToUpdateConditionsTemplateSecondStep(Tender tender, EditConditionTemplateSecondSectionModel model, bool ValidatationFromNextStep = false)
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

        public void IsValidToUpdateConditionsTemplateThirdStep(Tender tender, EditConditionTemplateThridSectionModel model, bool ValidatationFromNextStep = false)
        {
            IsValidToUpdateConditionsTemplateSecondStep(tender, null, true);
            if (!(tender.ConditionTemplateStageStatusId == (int)Enums.TenderConditoinsStatus.PreparteOffers ||
                tender.ConditionTemplateStageStatusId == (int)Enums.TenderConditoinsStatus.EvaluateOffers ||
                tender.ConditionTemplateStageStatusId == (int)Enums.TenderConditoinsStatus.ContractingRequirments ||
                tender.ConditionTemplateStageStatusId == (int)Enums.TenderConditoinsStatus.TechnicalDeclerations ||
                tender.ConditionTemplateStageStatusId == (int)Enums.TenderConditoinsStatus.Specifications))
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


        public void IsValidToUpdateConditionsTemplateSeventhStep(Tender tender, EditConditionTemplateSeventhSectionModel model, bool ValidatationFromNextStep = false)
        {
            var ShowGeneralOnly = tender.TenderActivities.Where(a => a.IsActive == true).Select(a => a.Activity.Activitytemplate.ActivitytemplatId).Distinct().Count() > 1;

            IsValidToUpdateConditionsTemplateThirdStep(tender, null, true);

            var listOfSections = tender.TenderActivities.SelectMany(a => a.Activity.Activitytemplate.ConditionTemplateActivities).Select(a => a.ConditionsTemplateSectionId).Distinct().ToList();

            if (!(tender.ConditionTemplateStageStatusId == (int)Enums.TenderConditoinsStatus.EvaluateOffers ||
tender.ConditionTemplateStageStatusId == (int)Enums.TenderConditoinsStatus.ContractingRequirments ||
tender.ConditionTemplateStageStatusId == (int)Enums.TenderConditoinsStatus.Specifications))
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.MustFillPreviousInformation);
            }
            if (listOfSections.Contains((int)Enums.ConditionsTemplateSections.Outputs) && !ShowGeneralOnly)
            {
                if ((!ValidatationFromNextStep && ((model?.TenderConditionsTemplateOutputs?.Count ?? 0) == 0)) || (ValidatationFromNextStep && (tender.TenderConditionsTemplate?.TenderConditionsTemplateTechnicalOutputs?.Count ?? 0) == 0))
                {
                    var value = Resources.TenderResources.DisplayInputs.TenderConditionsTemplateOutputs;
                    throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
                }
            }
            if (listOfSections.Contains((int)Enums.ConditionsTemplateSections.ProjectWorkScope))
            {
                if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.ProjectsScope)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.ProjectsScope)))
                {
                    var value = Resources.TenderResources.DisplayInputs.ProjectsScope;
                    throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
                }
            }
            if (listOfSections.Contains((int)Enums.ConditionsTemplateSections.WorkProgram))
            {
                if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.WorksProgram)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.WorksProgram)))
                {
                    var value = Resources.TenderResources.DisplayInputs.WorksProgram;
                    throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
                }
            }
            if (listOfSections.Contains((int)Enums.ConditionsTemplateSections.WorkLocation))
            {
                if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.WorkLocationDetails)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.WorkLocationDetails)))
                {
                    var value = Resources.TenderResources.DisplayInputs.WorkLocationDetails;
                    throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
                }
            }
        }

        public void IsValidToUpdateConditionsTemplateEighthStep(Tender tender, EditConditionTemplateEighthSectionModel model, bool ValidatationFromNextStep = false)
        {
            var ShowGeneralOnly = tender.TenderActivities.Where(a => a.IsActive == true).Select(a => a.Activity.Activitytemplate.ActivitytemplatId).Distinct().Count() > 1;

            IsValidToUpdateConditionsTemplateSeventhStep(tender, null, true);

            var listOfSections = tender.TenderActivities.SelectMany(a => a.Activity.Activitytemplate.ConditionTemplateActivities).Select(a => a.ConditionsTemplateSectionId).Distinct().ToList();

            if (!(tender.ConditionTemplateStageStatusId == (int)Enums.TenderConditoinsStatus.Specifications))
            {
                throw new BusinessRuleException(Resources.TenderResources.ErrorMessages.MustFillPreviousInformation);
            }
            if (listOfSections.Contains((int)Enums.ConditionsTemplateSections.WorkForce))
            {
                if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.WorkforceSpecifications)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.WorkforceSpecifications)))
                {
                    var value = Resources.TenderResources.DisplayInputs.WorkforceSpecifications;
                    throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
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
            if (listOfSections.Contains((int)Enums.ConditionsTemplateSections.ImplementaionMethod))
            {
                if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.ServicesAndWorkImplementationsMethod)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.ServicesAndWorkImplementationsMethod)))
                {
                    var value = Resources.TenderResources.DisplayInputs.ServicesAndWorkImplementationsMethod;
                    throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
                }
            }

            if (listOfSections.Contains((int)Enums.ConditionsTemplateSections.QualityDescription))
            {
                if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.QualitySpecifications)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.QualitySpecifications)))
                {
                    var value = Resources.TenderResources.DisplayInputs.QualitySpecifications;
                    throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
                }
            }

            if (listOfSections.Contains((int)Enums.ConditionsTemplateSections.SafteyDescription))
            {
                if ((!ValidatationFromNextStep && string.IsNullOrEmpty(model?.SafetySpecifications)) || (ValidatationFromNextStep && string.IsNullOrEmpty(tender.TenderConditionsTemplate?.SafetySpecifications)))
                {
                    var value = Resources.TenderResources.DisplayInputs.SafetySpecifications;
                    throw new BusinessRuleException(Resources.TenderResources.DisplayInputs.PleaseEnterValueOf + value);
                }
            }
        }





    }
}
