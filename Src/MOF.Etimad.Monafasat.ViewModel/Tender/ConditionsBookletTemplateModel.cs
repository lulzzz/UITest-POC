using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class ConditionsBookletTemplateModel
    {
        public int TenderId { set; get; }
        public string EncryptedTenderId { set; get; }
        [Required]
        [Display(ResourceType = typeof(Resources.BranchResources.DisplayInputs), Name = "Description")]
        public string Description { set; get; }
        [Display(ResourceType = typeof(Resources.SharedResources.DisplayInputs), Name = "DisplayName")]
        [Required]
        public string AgentName { set; get; }
        [Display(ResourceType = typeof(Resources.SharedResources.DisplayInputs), Name = "Job")]
        public string AgentJob { set; get; }
        [Phone]
        [Required]
        [Display(Name = "Fax", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        [RegularExpression(@"^0[1?5]\d{3}\d{3}\d{2}$", ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "InvalidPhoneNumber")]
        public string AgentFax { set; get; }
        [Phone]
        [Required]
        [RegularExpression(@"^0[1?5]\d{3}\d{3}\d{2}$", ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "InvalidFaxNumber")]
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        public string AgentPhone { set; get; }
        [Required]
        [Display(Name = "Email", ResourceType = typeof(Resources.CommitteeResources.DisplayInputs))]
        [EmailAddress(ErrorMessageResourceType = (typeof(Resources.CommitteeResources.ErrorMessages)), ErrorMessageResourceName = "EmailInvalid")]
        public string AgentEmail { set; get; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SystemCertificates")]
        public List<int> CerificatesIDs { set; get; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "SystemCertificates")]
        public List<LookupModel> Certificates { set; get; }
        public string TenderName { get; set; }
        public string MinistryName { get; set; }
        public string AgencyName { get; set; }
        public string TednerNumber { get; set; }
        public DateTime? SubmitionDate { get; set; }
        public decimal? BookletPrice { get; set; }
        public string BookletPriceString { get; set; }
        public DateTime? EnqueriesDate { get; set; }
        public DateTime? LastEnqueriesDate { get; set; }
        public string LastOfferPresentationDate { get; set; }
        public string LastOfferPresentationTime { get; set; }
        public string OffersOpeningDate { get; set; }
        public string OffersOpeningTime { get; set; }
        public string OffersOpeningAddress { get; set; }
        public string AgreementDuration { get; set; }
        public string Beneficiaries { get; set; }
        public int TenderBranchId { get; set; }
        public string AgencyLogo { get; set; }
        public string AgencyCode { get; set; }
    }
}
