using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ConditionTemplateBaseModel
    {
        public int TenderId { set; get; }
        public string EncryptedTenderId { set; get; }
    }

    public class GetConditionTemplateModel : ConditionTemplateBaseModel
    {
        [Display(Name = "StudyMinimumTargetFile", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string StudyMinimumTargetFileNetReferenceId { get; set; }
        public int? QuantityTableVersionId { get; set; }
        public int BranchId { get; set; }
        public int? TenderConditionsTemplateId { get; set; }
        public bool? IsTenderContainsTawreedTables { get; set; }
        public bool IsNotTawreed { get; set; }
        public bool IsTawreedActivity { get; set; }

        
        public int VersionId { get; set; }

        
                    public bool showLocalContentTab { get; set; }

        public bool HasMandatoryItems { get; set; } = false;
        public int TenderLocalContentId { get; set; }

        public List<LookupModel> LocalContentMechanism { set; get; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "DeterminePreferenceMechanismUsed")]
        public List<int> LocalContentMechanismIDs { set; get; }

        [Display(Name = "MinimumBaseline", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public int? MinimumBaseline { get; set; }

        [Display(Name = "MinimumPercentageLocalContentTarget", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public int? MinimumPercentageLocalContentTarget { get; set; }


        [Display(Name = "ApplyProvisionsMandatoryList", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public bool IsApplyProvisionsMandatoryList { get; set; }


        #region PrintedTemplate


        public bool IsTemplateHasOutput { get; set; }
        public List<int?> TemplateIds { get; set; }
        public string TemplateName { get; set; }
        public string MinistryName { get; set; }
        public string AgencyName { get; set; }
        public string AgencyLogo { get; set; }
        public string PurchaseDate { get; set; }
        public string TenderPurpose { get; set; }
        public decimal? TenderConditionsTemplatePriceNumbers { get; set; }
        public string TenderConditionsTemplatePriceText { get; set; }
        public string SendQuestionsDate { get; set; }
        public string LastSendQuestionsDate { get; set; }
        public string LastApplyOffersDate { get; set; }
        public string LastApplyOffersTime { get; set; }
        public string OpenOffersDate { get; set; }
        public string AwardingExpectedDate { get; set; }
        public string StartWorkingDate { get; set; }
        public string ParticipationConfirmationLetterDateString { get; set; }
        public string OpenOffersTime { get; set; }
        public string OpenOffersLocation { get; set; }
        #endregion 

        #region Checks
        public bool ShowGeneralOnly { get; set; }
        public bool ShowSectionNineAndTen { get; set; } = false;

        public bool ShowGeneralTemplates { get; set; } = false;
        public bool ShowServicesTemplates { get; set; } = false;

        public bool IsShowLocalContentConditionSection { get; set; } = false;
        

        public bool ShowWorkForce { get; set; }
        public bool ShowMaterials { get; set; }
        public bool ShowEquipments { get; set; }
        public bool ShowMaterialsAdvanced { get; set; }
        public bool ShowEquipmentsAdvanced { get; set; }
        public bool ShowImplementationMethod { get; set; }
        public bool ShowSafteyDescriptions { get; set; }
        public bool ShowQualityDescriptions { get; set; }
        public bool ShowContractBasedOnPerformance { get; set; }
        #endregion

        #region SecondSection

        [Display(Name = "AgencyDecalration", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string AgencyDecalration { get; set; }

        [Display(Name = "ShowTenderFragmentation", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public bool HideTenderFragmentation { get; set; }

        public bool HideCerificatesIDs { get; set; } = false;

        [Display(Name = "TenderFragmentation", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string TenderFragmentation { get; set; }

        [RegularExpression(@"^0[1?5]\d{3}\d{3}\d{2}$", ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "InvalidPhoneNumber")]
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        public string AgentPhone { set; get; }

        [Display(Name = "Fax", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        [RegularExpression(@"^0[1?5]\d{3}\d{3}\d{2}$", ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "InvalidFaxNumber")]
        public string AgentFax { set; get; }

        [Display(ResourceType = typeof(Resources.SharedResources.DisplayInputs), Name = "Job")]
        public string AgentJob { set; get; }

        [Display(Name = "Email", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        public string AgentEmail { set; get; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SystemCertificates")]
        public List<LookupModel> Certificates { set; get; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SystemCertificates")]
        public List<int> CerificatesIDs { set; get; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderDescription")]
        public string Description { set; get; }

        [Display(ResourceType = typeof(Resources.SharedResources.DisplayInputs), Name = "DisplayName")]
        public string AgentName { set; get; }

        public string RegulationsDate { get; set; }




        #endregion SecondSection

        #region ThirdSection
        [Display(Name = "FinancialOfferDocuments", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string FinancialOfferDocuments { get; set; }

        [Display(Name = "TechnicalOfferDocuments", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string TechnicalOfferDocuments { get; set; }

        [Display(Name = "MaxTimeToSendQuestions", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string MaxTimeToSendQuestions { get; set; }
        [Required(ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "MaxAnswerPeriod")]
        [Display(Name = "MaxTimeToAnswerQuestions", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public int? MaxTimeToAnswerQuestions { get; set; }

        [Display(Name = "AlternativeEmailForCommuncation", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string AlternativeEmailForCommuncation { get; set; }

        [Display(Name = "InitialGuaranteePercentage", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public decimal InitialGuaranteePercentage { get; set; }

        public decimal FinalGuaranteePercentage { get; set; }

        public int StoppingPeriod { get; set; }

        [Display(Name = "DocumentStyle", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string DocumentStyle { get; set; }

        [Display(Name = "AllowancePeriodToAddOffersIfNotAddedBeofre", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public int? AllowancePeriodToAddOffersIfNotAddedBeofre { get; set; }
        [Display(Name = "AllowedOfferSiningDays", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public int? AllowedOfferSiningDays { get; set; }

        [Display(Name = "WritePrice", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string WritePrice { get; set; }

        [Display(Name = "ConfirimJoiningTheTender", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string ConfirimJoiningTheTender { get; set; }

        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ParticipationConfirmationLetterDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ParticipationConfirmationLetterDate { get; set; }
        #endregion ThirdSection

        #region Five



        #endregion

        #region Six



        #endregion

        #region Seven

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderConditionsTemplateOutputs")]
        public List<TenderConditionsTemplateOutputsViewModel> TenderConditionsTemplateOutputs { set; get; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TechnicalDeclrations")]
        public List<TenderConditionsTemplateTechnicalDeclrationViewModel> TechnicalDeclrations { set; get; }

        [Display(Name = "ProjectsScope", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string ProjectsScope { get; set; }

        [Display(Name = "WorksProgram", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string WorksProgram { get; set; }

        [Display(Name = "WorkExcutionlocation", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string WorkLocationDetails { get; set; }

        [Display(Name = "HowToExcuteBusinessAndServices", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]

        public string HowToExcuteBusinessAndServices { get; set; }

        #endregion

        #region Eight

        [Display(Name = "WorkforceSpecifications", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string WorkforceSpecifications { get; set; }

        [Display(Name = "MaterialsSpecifications", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string MaterialsSpecifications { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "SpecialConditionsValidation")]
        [Display(Name = "SpecialConditions", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string SpecialConditions { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "AttachmentsValidation")]
        [Display(Name = "Attachments", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string Attachments { get; set; }

        [Display(Name = "EquipmentsSpecifications", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string EquipmentsSpecifications { get; set; }

        [Display(Name = "ContractBasedOnPerformanceDetails", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string ContractBasedOnPerformanceDetails { get; set; }

        [Display(Name = "ServicesAndWorkImplementationsMethod", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string ServicesAndWorkImplementationsMethod { get; set; }

        [Display(Name = "OffersEvaluationCriteria", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string OffersEvaluationCriteria { get; set; }

        [Display(Name = "OffersChecking", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string OffersChecking { get; set; }


        [Display(Name = "QualitySpecifications", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string QualitySpecifications { get; set; }

        [Display(Name = "SafetySpecifications", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string SafetySpecifications { get; set; }

        public string BasicInformation { get; set; }
        public string RequiredDcoumentationBefore { set; get; }
        public string Tests { set; get; }
        public string IntilizationAndStartWork { set; get; }
        public string RequiredDcoumentationAfter { set; get; }
        public string Trainging { set; get; }
        public string Guarantee { set; get; }
        public string Maintanance { set; get; }
        public string MachineGuarantee { set; get; }
        public string MachineMaintanance { set; get; }

        [Display(Name = "SupportingFiles", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string AttachmentReference { get; set; }
        public int? PreQualificationId { get; set; }
        public int? TenderCreatedByTypeId { get; set; }
        public int TenderTypeId { get; set; }
        public int TenderStatusId { get; set; }
        public int? InvitationTypeId { get; set; }
        public int? OfferPresentationWayId { get; set; }
        public string ReferenceNumber { get; set; }
        public string TenderName { get; set; }
        public string TenderNumber { get; set; }
        public int TablesCount { get; set; }
        public List<TenderAttachmentModel> TenderAttachments { get; set; }
        public int? ConditionTemplateStageStatusId { get; set; }
        public List<string> AttachmentReferenceLst { get; set; }

        public List<string> LocalContentAttachmentReferenceLst { get; set; }


        #endregion
        public Dictionary<int, List<string>> DescriptionQuantityTables { set; get; } = new Dictionary<int, List<string>>();
        public List<string> QuantityTables { set; get; } = new List<string>();
        public List<int> ListOfSections { get; set; } = new List<int>();
        public bool? SupplierNeedSample { get; set; }
        public string SamplesDeliveryAddress { get; set; }
        public string DeliveryDate { set; get; }
        public string SecondRegulationsDate { get; set; }
        public string AgencyCode { get; set; }

        public string BuildingName { get; set; }
        public string FloarNumber { get; set; }

        public string DepartmentName { get; set; }

        public string OfferBuildingName { get; set; }
        public string OfferFloarNumber { get; set; }

        public string OfferDepartmentName { get; set; }
        public string OffersDeliveryAddress { get; set; }
        public string OffersDeliveryDate { get; set; }

        public bool HasAlternativeOffer { get; set; }

        public decimal? EstimatedValue { get; set; }

    }

    public class LocalContentModel : ConditionTemplateBaseModel
    {
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "DeterminePreferenceMechanismUsed")]
        public List<int> LocalContentMechanismIDs { set; get; }

        //[Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SystemCertificates")]
        public List<LookupModel> LocalContentMechanism { set; get; }
     //   [Range(0, 100, ErrorMessage = "الرجاء ادخال قيمة الحد الأدنى لخط الأساس اكبر من او يساوي صفر و اقل من او يساوي مائة ")]

        [Display(Name = "MinimumBaseline", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public int? MinimumBaseline { get; set; }

        [Display(Name = "MinimumPercentageLocalContentTarget", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public int? MinimumPercentageLocalContentTarget { get; set; }


        [Display(Name = "ApplyProvisionsMandatoryList", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public bool IsApplyProvisionsMandatoryList { get; set; }

        public string StudyMinimumTargetFileNetReferenceId { get; set; }

        public List<TenderAttachmentModel> TenderAttachments { get; set; }



    }


    public class EditConditionTemplateSecondSectionModel : ConditionTemplateBaseModel
    {
        #region SecondSection

        [Display(Name = "AgencyDecalration", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string AgencyDecalration { get; set; }

        [Display(Name = "ShowTenderFragmentation", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public bool HideTenderFragmentation { get; set; }

        // [Display(Name = "Hide", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public bool HideCerificatesIDs { get; set; }


        [Display(Name = "TenderFragmentation", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string TenderFragmentation { get; set; }


        [RegularExpression(@"^0[1?5]\d{3}\d{3}\d{2}$", ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "InvalidPhoneNumber")]
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        public string AgentPhone { set; get; }

        [Display(Name = "Fax", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        [RegularExpression(@"^0[1?5]\d{3}\d{3}\d{2}$", ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "InvalidFaxNumber")]
        public string AgentFax { set; get; }

        [Display(ResourceType = typeof(Resources.SharedResources.DisplayInputs), Name = "Job")]
        public string AgentJob { set; get; }

        //[Required]
        [Display(Name = "Email", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        //[EmailAddress(ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "EmailInvalid")]
        [RegularExpression(@"\A(?:[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?)\Z",
        ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "EmailInvalid")]
        public string AgentEmail { set; get; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SystemCertificates")]
        public List<LookupModel> Certificates { set; get; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SystemCertificates")]
        public List<int> CerificatesIDs { set; get; }

        //[Required]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderDescription")]
        public string Description { set; get; }

        [Display(ResourceType = typeof(Resources.SharedResources.DisplayInputs), Name = "AgentName")]
        public string AgentName { set; get; }

        #endregion SecondSections
    }

    public class EditConditionTemplateThridSectionModel : ConditionTemplateBaseModel
    {
        #region ThirdSection

        //[Display(Name = "HideFinancialDocumentSections", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        //public bool HideFinancialDocumentSections { get; set; }

        [Display(Name = "FinancialOfferDocuments", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string FinancialOfferDocuments { get; set; }

        [Display(Name = "HideTechnicalDocumentSections", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public bool HideTechnicalDocumentSections { get; set; }

        [Display(Name = "TechnicalOfferDocuments", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string TechnicalOfferDocuments { get; set; }
         
        [Required(ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "MaxAnswerPeriod")]
        [Display(Name = "MaxTimeToAnswerQuestions", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public int? MaxTimeToAnswerQuestions { get; set; }
       
        [RegularExpression(@"\A(?:[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?)\Z",
        ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "EmailInvalid")]
        [Display(Name = "AlternativeEmailForCommuncation", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string AlternativeEmailForCommuncation { get; set; }
 
        [Display(Name = "DocumentStyle", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string DocumentStyle { get; set; }
        
        [Display(Name = "AllowancePeriodToAddOffersIfNotAddedBeofre", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        [Range(1, 10, ErrorMessageResourceType = typeof(Resources.PrePlanningResources.ErrorMessages), ErrorMessageResourceName = "ValueBetween")]
        [Required(ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "EnterCompletionValidityPeriod")]
        public int? AllowancePeriodToAddOffersIfNotAddedBeofre { get; set; }
       
        [Required(ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "EnterOfferSighningDays")]
        [Range(1, 999, ErrorMessageResourceType = typeof(Resources.PrePlanningResources.ErrorMessages), ErrorMessageResourceName = "ValueBetween")]
        [Display(Name = "AllowedOfferSiningDays", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public int? AllowedOfferSiningDays { get; set; }

        public string WritePrice { get; set; }

        public string ConfirimJoiningTheTender { get; set; }


        [ModelBinder(BinderType = typeof(DateTimeModelBinder))]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "ParticipationConfirmationLetterDate")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? ParticipationConfirmationLetterDate { get; set; }

        public string OffersEvaluationCriteria { get; set; }
        public string OffersChecking { get; set; }
        public int VersionId { get; set; }

        #endregion ThirdSection 
    }

    public class EditConditionTemplateSeventhSectionModel : ConditionTemplateBaseModel
    {
        #region Seven
        public bool IsTemplateHasOutput { get; set; }
        public bool ShowGeneralOnly { get; set; }
        public string ListOfSections { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TenderConditionsTemplateOutputs")]
        public List<TenderConditionsTemplateOutputsViewModel> TenderConditionsTemplateOutputs { set; get; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TechnicalDeclrations")]
        public List<TenderConditionsTemplateTechnicalDeclrationViewModel> TechnicalDeclrations { set; get; }

        [Display(Name = "ProjectsScope", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string ProjectsScope { get; set; }

        [Display(Name = "WorksProgram", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string WorksProgram { get; set; }

        [Display(Name = "WorkLocationDetails", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string WorkLocationDetails { get; set; }
       
        [Display(Name = "ServicesAndWorkImplementationsMethod", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string ServicesAndWorkImplementationsMethod { get; set; }

        public int VersionId { get; set; }

        #endregion
    }

    public class EditConditionTemplateEighthSectionModel : ConditionTemplateBaseModel
    {
        #region Eight

        public bool ShowGeneralOnly { get; set; }
        public string ListOfSections { get; set; }
        public string TemplateIds { get; set; }

        [Display(Name = "WorkforceSpecifications", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string WorkforceSpecifications { get; set; }

        [Display(Name = "MaterialsSpecifications", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string MaterialsSpecifications { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "SpecialConditionsValidation")]
        [Display(Name = "SpecialConditions", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string SpecialConditions { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.TenderResources.ErrorMessages), ErrorMessageResourceName = "AttachmentsValidation")]

        [Display(Name = "Attachments", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string Attachments { get; set; }


        [Display(Name = "EquipmentsSpecifications", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string EquipmentsSpecifications { get; set; }

        //[Required]
        [StringLength(1000)]
        [Display(Name = "ContractBasedOnPerformanceDetails", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string ContractBasedOnPerformanceDetails { get; set; }

       
        [Display(Name = "OffersEvaluationCriteria", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string OffersEvaluationCriteria { get; set; }

        [Display(Name = "OffersChecking", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string OffersChecking { get; set; }

        [Display(Name = "QualitySpecifications", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string QualitySpecifications { get; set; }

        [Display(Name = "SafetySpecifications", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string SafetySpecifications { get; set; }
        public bool IsTemplateHasOutput { get; set; }

        public string BasicInformation { get; set; }
        public string RequiredDcoumentationBefore { set; get; }
        public string Tests { set; get; }
        public string IntilizationAndStartWork { set; get; }
        public string RequiredDcoumentationAfter { set; get; }
        public string Trainging { set; get; }
        public string Guarantee { set; get; }
        public string Maintanance { set; get; }
        public string MachineGuarantee { set; get; }
        public string MachineMaintanance { set; get; }
        [Display(Name = "AttachmentsMainConditions", ResourceType = typeof(Resources.TenderResources.DisplayInputs))]
        public string AttachmentReference { get; set; }
        public int? PreQualificationId { get; set; }
        public int? TenderCreatedByTypeId { get; set; }
        public int TenderTypeId { get; set; }
        public int TenderStatusId { get; set; }
        public int? InvitationTypeId { get; set; }
        public int? OfferPresentationWayId { get; set; }
        public string ReferenceNumber { get; set; }
        public string TenderName { get; set; }
        public string TenderNumber { get; set; }
        public int TablesCount { get; set; }
        public List<TenderAttachmentModel> TenderAttachments { get; set; }

        #endregion
    }
}
