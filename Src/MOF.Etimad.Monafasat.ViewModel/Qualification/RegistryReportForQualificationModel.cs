using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class RegistryReportForQualificationModel
    {
        //معلومات التأهيل
        public string Id { get; set; }
        public string QualificationNumber { get; set; }
        public string QualificationName { get; set; }
        public string QualificationSubmitionDateString { get; set; }
        public string QualificationCheckingDate { get; set; }
        public string QualificationParticipantsNumber { get; set; }
        public string QualificationReqiredPoints { get; set; }
        public int QualificationTypeId { get; set; }

        //small
        public string PreviousExperienceYearPercentage { get; set; }
        public string ExistingContractualObligationsPercentage { get; set; }
        public string HumanResourcePercentage { get; set; }
        //medium
        public string QualityPercentage { get; set; }
        public string EnviromentAndHealthyPercentage { get; set; }
        //Large
        public string InsurancePercentage { get; set; }
        //Financial
        public string FinancialStatementsPercentage { get; set; }


        //بيانات المشاركين
        public List<QualificationSubCategoryWeights> PassedQualificationSubCategoryWeights { get; set; }
        public List<QualificationSubCategoryWeights> FailedQualificationSubCategoryWeights { get; set; }
    }

    public class QualificationSubCategoryWeights
    {
        public string QualificationResultValueString { get; set; }
        public string QualificationResultString { get; set; }
        public string SupplierName { get; set; }
        //small
        public string PreviousExperienceYearWeight { get; set; }
        public string ExistingContractualObligationsWeight { get; set; }
        public string HumanResourceWeight { get; set; }

        //medium
        public string QualityWeight { get; set; }
        public string EnviromentAndHealthyWeight { get; set; }

        //Large
        public string InsuranceWeight { get; set; }

        //financial
        public string FinancialStatementsWeight { get; set; }
    }
}
