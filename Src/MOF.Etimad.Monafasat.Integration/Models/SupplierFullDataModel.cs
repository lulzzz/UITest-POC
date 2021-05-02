using System.Collections.Generic;
namespace MOF.Etimad.Monafasat.Integration
{

    public class SupplierFullDataModel
    {
        public string supplierName { get; set; }
        public string CRNameEN { get; set; }
        public int SupplierType { get; set; }
        public string supplierNumber { get; set; }
        public List<IsicActivityApiModel> IsicActivity { get; set; }
        public List<SupllierAddressesApiModel> Branches { get; set; }
        public List<Project> Projects { get; set; }
        public string MainActivity { get; set; }
        public string Email { get; set; }
        public string CityName { get; set; }
        public string PostalCode { get; set; }
        public string PostOffice { get; set; }
        public string Mobile { get; set; }
        public string Fax { get; set; }
        public string MainActivityDescription { get; set; }
        public List<string> Activities { get; set; }
        public string CompanyType { get; set; }
        public string BoardOfDirectories { get; set; }
        public YasserApiModel YasserInfo { get; set; }
        public SadadInfoApiModel SadadInfo { get; set; }
        public CityApiModel City { get; set; }
        public List<ContactOfficerApiModel> contactOfficers { get; set; }
        public List<SupllierAddressesApiModel> Addresses { get; set; }
        public List<Attachments> attachments { get; set; }
        public List<string> Attachments { get; set; }
        public string crExpiryDate { get; set; }
        public string ownerNationality { get; set; }
        public string ownerName { get; set; }
        public string capital { get; set; }
        public string creationDate { get; set; }
        public int? SupplierHasNoCRType { get; set; }
        public string OtherSupplierHasNoCRType { get; set; }
        public string crIssueDate { get; set; }
    }

    public class IsicActivityApiModel
    {
        public int IsicActivityID { get; set; }
        public string Description { get; set; }
    }

    public class CityApiModel
    {
        public int CityID { get; set; }
        public string CityName { get; set; }
        public string CityCode { get; set; }
    }

    public class ContactOfficerApiModel
    {
        #region Properties
        /// <summary>
        /// رقم الهوية
        /// </summary>
        public string NationalID { get; set; }
        /// <summary>
        /// الاسم
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// البريد الالكتروني
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// رقم الجوال
        /// </summary>
        public string Mobile { get; set; }
        /// <summary>
        /// الجنسية
        /// </summary>
        public string CiviRegistery { get; set; }

        public string Nationality { get; set; }
        /// <summary>
        /// كود الدولة لرقم الجوال
        /// </summary>
        public string MobileCountry { get; set; }
        /// <summary>
        /// المسمى الوظيفي
        /// </summary>
        public string JobTitle { get; set; }
        /// <summary>
        /// رقم الهاتف
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// كود الدولة لرقم الهاتف
        /// </summary>
        public string PhoneNumberCountry { get; set; }
        /// <summary>
        /// تحويلة
        /// </summary>
        public int? Extension { get; set; }
        /// <summary>
        /// هل ترغب بأن يكون هذا البريد ورقم الهاتف هو الافتراضي للتواصل 
        /// </summary>
        public bool IsPrimary { get; set; }

        public string supplierNumber { get; set; }
        #endregion
    }
    public class SadadInfoApiModel
    {
        #region Properties
        public string BenefecieryChapterNumber { get; set; }
        public string BenefecieryDepartmentNumber { get; set; }
        public string BenefecierySectionNumber { get; set; }
        public string BenefecierySequenceNumber { get; set; }
        public string BenefecieryManagementNumber { get; set; }
        public string BenefecierySectionsNumber { get; set; }
        #endregion
    }
    public class YasserApiModel
    {
        #region Properties
        public string SequenceFacilityNumberInMinistryOfLabor { get; set; }

        public string OfficeFacilityNumberInMinistryOfLabor { get; set; }

        public string InvestmentLicenseNumber { get; set; }

        public string SocialSecuritySubscriptionNumber { get; set; }

        public string MembershipCityCode { get; set; }
        public string MembershipCityName { get; set; }
        #endregion
    }
    public class SupllierAddressesApiModel
    {
        #region Properties
        public string City { get; set; }

        public string Location { get; set; }

        public string Email { get; set; }

        public string PostOffice { get; set; }

        public string PostalCode { get; set; }

        public string PhoneNumber { get; set; }

        public string PhoneExtension { get; set; }

        public string FaxNumber { get; set; }

        public string FaxExtension { get; set; }
        public bool IsPrimary { get; set; }
        #endregion
    }
    public class Project
    {
        public string ProjectName { get; set; }
        public decimal ContractAmount { get; set; }
        public string Agency { get; set; }
        public string ShortDescription { get; set; }
        public string Region { get; set; }
        public string City { get; set; }
        public string StartDate { get; set; }
        public string EndtDate { get; set; }

    }
}
