using MOF.Etimad.Monafasat.SharedKernel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("TenderConditionsTemplate", Schema = "Tender")]
    public class TenderConditionsTemplate : AuditableEntity
    {

        #region Fields====================================================
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TenderConditionsTemplateId { get; private set; }

        public Tender Tender { get; private set; }

        #region SecondSection

        [StringLength(2000)]
        public string AgencyDecalration { get; set; }

        [Phone]
        [StringLength(20)]
        public string AgentPhone { set; get; }

        [Phone]
        [StringLength(20)]
        public string AgentFax { set; get; }

        [StringLength(1000)]
        public string AgentJob { set; get; }

        [EmailAddress]
        [StringLength(500)]
        public string AgentEmail { set; get; }
        public string Description { set; get; }

        [StringLength(2000)]
        public string AgentName { set; get; }

        public string TenderFragmentation { get; set; }

        public bool HideTenderFragmentation { get; set; }

        public bool HideCerificatesIDs { get; set; }

        public List<ConditionsTemplateCertificate> TemplateCertificates { get; private set; }

        #endregion SecondSection

        #region ThirdSection 

        [StringLength(2000)]
        public string FinancialOfferDocuments { get; set; }

        public bool HideTechnicalDocumentSections { get; set; }

        [StringLength(2000)]
        public string TechnicalOfferDocuments { get; set; }

        public int? MaxTimeToAnswerQuestions { get; set; }

        [EmailAddress]
        [StringLength(500)]
        public string AlternativeEmailForCommuncation { get; set; }

        public string DocumentStyle { get; set; }

        public string WritePrice { get; set; }

        public string ConfirimJoiningTheTender { get; set; }

        #endregion ThirdSection

        #region Four

        public bool RemoveHowToApplyTechnicalAndFinancialOffersSectionWay { get; set; }
        public string ApplyOffersTemplateId { get; set; }

        #endregion

        #region Five

        public int? AllowancePeriodToAddOffersIfNotAddedBeofre { get; set; }

        #endregion

        #region Six

        public int? AllowedOfferSiningDays { get; set; }

        #endregion

        #region Seven

        public List<TenderConditionsTemplateTechnicalDeclration> TenderConditionsTemplateTechnicalDelrations { get; private set; }
        public List<TenderConditionsTemplateTechnicalOutput> TenderConditionsTemplateTechnicalOutputs { get; private set; }

        public string ProjectsScope { get; set; }

        public string WorksProgram { get; set; }

        public string WorkLocationDetails { get; set; }

        #endregion

        #region Eight

        public string WorkforceSpecifications { get; set; }

        public string SpecialConditions { get; set; }

        public string Attachments { get; set; }

        public string MaterialsSpecifications { get; set; }

        public string EquipmentsSpecifications { get; set; }

        [StringLength(1000)]
        public string ContractBasedOnPerformanceDetails { get; set; }

        [StringLength(2000)]
        public string ServicesAndWorkImplementationsMethod { get; set; }

        public string QualitySpecifications { get; set; }

        public string SafetySpecifications { get; set; }


        [StringLength(2000)]
        public string OffersEvaluationCriteria { get; set; }
        [StringLength(2000)]
        public string OffersChecking { get; set; }

        #endregion

        [ForeignKey(nameof(TenderConditionsTemplateMaterialInofmration))]
        public int? TenderConditionsTemplateMaterialInofmrationId { get; set; }
        public TenderConditionsTemplateMaterialInofmration TenderConditionsTemplateMaterialInofmration { get; set; }


        #endregion

        #region Constructors====================================================

        public TenderConditionsTemplate()
        {

        }

        #endregion

        #region Methods====================================================
        public void CreateCertificates(IList<int> certificatesIds)
        {
            if (certificatesIds != null)
            {
                if (TemplateCertificates == null)
                    TemplateCertificates = new List<ConditionsTemplateCertificate>();
                //will not cahnge
                var mutualAreas = TemplateCertificates.Where(x => certificatesIds.Contains(x.CerificateId)).ToList();
                var mutualIds = mutualAreas.Select(a => a.CerificateId).ToList<int>();
                //Will be deleted
                List<ConditionsTemplateCertificate> AreasToBeDeleted = TemplateCertificates.Where(x => !certificatesIds.Contains(x.CerificateId)).ToList();
                //Will be added
                List<int> AreasAddedIDs = certificatesIds.Where(x => !mutualIds.Contains(x)).ToList<int>();
                if (TemplateCertificates != null)
                {
                    foreach (ConditionsTemplateCertificate item in AreasToBeDeleted)
                    {
                        item.Delete();
                    }
                }
                if (certificatesIds != null)
                {
                    foreach (var item in AreasAddedIDs)
                    {
                        TemplateCertificates.Add(new ConditionsTemplateCertificate(item));
                    }
                }
            }
        }


        private void ManageTenderConditionsTemplateOutputs(IList<TenderConditionsTemplateTechnicalOutput> tenderConditionsTemplateTechnicalOutput)
        {
            if (tenderConditionsTemplateTechnicalOutput.Count > 0)
            {
                if (TenderConditionsTemplateTechnicalOutputs == null)
                    TenderConditionsTemplateTechnicalOutputs = new List<TenderConditionsTemplateTechnicalOutput>();
                //may need edit
                List<TenderConditionsTemplateTechnicalOutput> mutualDeclerations = TenderConditionsTemplateTechnicalOutputs.Where(x => tenderConditionsTemplateTechnicalOutput.Select(q => q.TenderConditionsTemplateTechnicalOutputId)
                .Contains(x.TenderConditionsTemplateTechnicalOutputId)).ToList();
                //Will be deleted
                List<TenderConditionsTemplateTechnicalOutput> delrationsToBeDeleted = TenderConditionsTemplateTechnicalOutputs.Where(x => !tenderConditionsTemplateTechnicalOutput.Select(q => q.TenderConditionsTemplateTechnicalOutputId)
                .Contains(x.TenderConditionsTemplateTechnicalOutputId)).ToList();
                //Will be added
                List<TenderConditionsTemplateTechnicalOutput> delrationsToBeAdded = tenderConditionsTemplateTechnicalOutput.Where(x => x.TenderConditionsTemplateTechnicalOutputId <= 0).ToList();
                if (mutualDeclerations != null)
                {
                    foreach (var item in mutualDeclerations)
                    {
                        item.Update(item.OutputStage, item.OutputName, item.OutputDescriptions, item.OutputDeliveryTime);
                    }
                }
                if (delrationsToBeDeleted != null)
                {
                    foreach (var item in delrationsToBeDeleted)
                    {
                        item.Delete();
                    }
                }
                if (delrationsToBeAdded != null)
                {
                    foreach (var item in delrationsToBeAdded)
                    {
                        TenderConditionsTemplateTechnicalOutputs.Add(new TenderConditionsTemplateTechnicalOutput(item.OutputStage, item.OutputName, item.OutputDescriptions, item.OutputDeliveryTime));
                    }
                }
            }
        }

        public void UpdateConditionsTemplateSecondStep(string agencyDecalration, string description, string agentName, string agentJob, string agentFax,
            string agentPhone, string tenderFragmentation, bool hideTenderFragmentation, string agentEmail, IList<int> certificatesIds, bool hideCerificatesIDs)
        {
            AgencyDecalration = agencyDecalration;
            Description = description;
            AgentName = agentName;
            AgentJob = agentJob;
            AgentFax = agentFax;
            AgentPhone = agentPhone;
            AgentEmail = agentEmail;
            HideTenderFragmentation = hideTenderFragmentation;
            HideCerificatesIDs = hideCerificatesIDs;
            TenderFragmentation = tenderFragmentation;
            CreateCertificates(certificatesIds);
            if (TenderConditionsTemplateId == 0)
                EntityCreated();
            else
                EntityUpdated();
        }

        public void UpdatePreparingOffersStep(string financialOfferDocuments, bool hideTechnicalDocumentSections, string technicalOfferDocuments,
            int? maxTimeToAnswerQuestions, string alternativeEmailForCommuncation, string documentStyle, int? allowancePeriodToAddOffersIfNotAddedBeofre, int? allowedOfferSiningDays, string writePrice, string confirimJoiningTheTender, string offersChecking, string offersEvaluationCriteria)
        {
            FinancialOfferDocuments = financialOfferDocuments;
            HideTechnicalDocumentSections = hideTechnicalDocumentSections;
            TechnicalOfferDocuments = technicalOfferDocuments;
            MaxTimeToAnswerQuestions = maxTimeToAnswerQuestions;
            AlternativeEmailForCommuncation = alternativeEmailForCommuncation;
            AllowedOfferSiningDays = allowedOfferSiningDays;
            AllowancePeriodToAddOffersIfNotAddedBeofre = allowancePeriodToAddOffersIfNotAddedBeofre;
            DocumentStyle = documentStyle;
            WritePrice = writePrice;
            ConfirimJoiningTheTender = confirimJoiningTheTender;
            OffersChecking = offersChecking;
            OffersEvaluationCriteria = offersEvaluationCriteria;
            EntityUpdated();
        }

        public void UpdateConditionsTemplateSeventhStep(IList<TenderConditionsTemplateTechnicalOutput> tenderConditionsTemplateTechnicalOutput, IList<TenderConditionsTemplateTechnicalDeclration> tenderConditionsTemplateTechnicalDelrations,
            string projectsScope, string worksProgram, string workLocationDetails, string servicesAndWorkImplementationsMethod)
        {
            ManageTenderConditionsTemplateTechnicalDeclrationChange(tenderConditionsTemplateTechnicalDelrations);

            ManageTenderConditionsTemplateOutputs(tenderConditionsTemplateTechnicalOutput);
            ProjectsScope = projectsScope;
            WorksProgram = worksProgram;
            WorkLocationDetails = workLocationDetails;
            ServicesAndWorkImplementationsMethod = workLocationDetails;
            EntityUpdated();
        }

        private void ManageTenderConditionsTemplateTechnicalDeclrationChange(IList<TenderConditionsTemplateTechnicalDeclration> tenderConditionsTemplateTechnicalDelrations)
        {
            if (tenderConditionsTemplateTechnicalDelrations != null)
            {
                if (TenderConditionsTemplateTechnicalDelrations == null)
                    TenderConditionsTemplateTechnicalDelrations = new List<TenderConditionsTemplateTechnicalDeclration>();
                //may need edit
                List<TenderConditionsTemplateTechnicalDeclration> mutualDeclerations = TenderConditionsTemplateTechnicalDelrations.Where(x => tenderConditionsTemplateTechnicalDelrations.Select(q => q.TenderConditionsTemplateTechnicalDeclrationId)
                .Contains(x.TenderConditionsTemplateTechnicalDeclrationId)).ToList();
                //Will be deleted
                List<TenderConditionsTemplateTechnicalDeclration> delrationsToBeDeleted = TenderConditionsTemplateTechnicalDelrations.Where(x => !tenderConditionsTemplateTechnicalDelrations.Select(q => q.TenderConditionsTemplateTechnicalDeclrationId)
                .Contains(x.TenderConditionsTemplateTechnicalDeclrationId)).ToList();
                //Will be added
                List<TenderConditionsTemplateTechnicalDeclration> delrationsToBeAdded = tenderConditionsTemplateTechnicalDelrations.Where(x => x.TenderConditionsTemplateTechnicalDeclrationId <= 0).ToList();
                if (mutualDeclerations != null)
                {
                    foreach (var item in mutualDeclerations)
                    {
                        item.Update(item.Term, item.Decleration);
                    }
                }
                if (delrationsToBeDeleted != null)
                {
                    foreach (var item in delrationsToBeDeleted)
                    {
                        item.DeActive();
                    }
                }
                if (delrationsToBeAdded != null)
                {
                    foreach (var item in delrationsToBeAdded)
                    {
                        TenderConditionsTemplateTechnicalDelrations.Add(new TenderConditionsTemplateTechnicalDeclration(item.Term, item.Decleration));
                    }
                }
            }
        }

        public void UpdateConditionsTemplateEighthStep(string workforceSpecifications, string materialsSpecifications, string specialConditions, string attachments, string equipmentsSpecifications,
            string contractBasedOnPerformanceDetails, string qualitySpecifications, string safetySpecifications, string basicInformation, string requiredDcoumentationBefore, string tests,
            string intilizationAndStartWork, string requiredDcoumentationAfter, string trainging, string guarantee, string maintanance, string machineGuarantee, string machineMaintanance, bool hasMaterialInformation)
        {
            WorkforceSpecifications = workforceSpecifications;
            MaterialsSpecifications = materialsSpecifications;
            SpecialConditions = specialConditions;
            Attachments = attachments;
            EquipmentsSpecifications = equipmentsSpecifications;
            ContractBasedOnPerformanceDetails = contractBasedOnPerformanceDetails;
            QualitySpecifications = qualitySpecifications;
            SafetySpecifications = safetySpecifications;

            if (TenderConditionsTemplateMaterialInofmration == null)
                TenderConditionsTemplateMaterialInofmration = new TenderConditionsTemplateMaterialInofmration(basicInformation, requiredDcoumentationBefore, tests,
             intilizationAndStartWork, requiredDcoumentationAfter, trainging, guarantee, maintanance, machineGuarantee,
             machineMaintanance);
            else
                TenderConditionsTemplateMaterialInofmration.Update(basicInformation, requiredDcoumentationBefore, tests,
                 intilizationAndStartWork, requiredDcoumentationAfter, trainging, guarantee, maintanance, machineGuarantee,
                 machineMaintanance);

            EntityUpdated();
        }

        public void SetAddedState()
        {
            TenderConditionsTemplateId = 0;
            EntityCreated();
        }

        #endregion
    }
}