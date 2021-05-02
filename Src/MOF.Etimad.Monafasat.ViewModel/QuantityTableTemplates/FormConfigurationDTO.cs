using MOF.Etimad.Monafasat.Resources.TenderResources;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class FormConfigurationDTO
    {
        public decimal NPpercentage { get; set; }
        public string SubmitAction { get; set; }
        public string EncryptedNegotiationId { get; set; }
        public bool HasAlternative { get; set; }
        public bool ApplySelected { get; set; }
        public bool CanEditAlternative { get; set; }
        public bool EnablePaging { get; set; }
        public bool AllowEdit { get; set; }
        public string FormHtml { get; set; }
        public string EncryptedOfferId { get; set; }
        /// <summary>
        /// اى دى النموزج
        /// </summary>
        public List<int> ActivityTemplatesIds { get; set; }
        /// <summary>
        /// عدد سنين المنافسة
        /// </summary>
        public int YearsCount { get; set; }
        /// <summary>
        /// البيانات من جدول "Qualification Quantity Item "
        /// </summary>
        [JsonProperty]
        public List<TenderQuantityItemDTO> QuantityItemDtos { get; set; }
        public int TenderId { get; set; }
        public string EncryptedTenderId { get; set; }
        public bool IsTenderContainsTawreed { get; set; }
        public int ActivityId { get; set; }
        public List<int> ActivityIds { get; set; }
        public List<int> FormIds { get; set; }
        public string TableName { get; set; }
    }

    public class TenderActivityDTO
    {
        public List<int> ActivityIdsList { get; set; }
        public int YearCount { get; set; }
        public int? VesionId { get; set; }
    }

    public class TemplateFormDTO
    {
        public int TemplateId { get; set; }
        public string TemplateName { get; set; }
        public List<FormDTO> FormDTOs { get; set; }
    }

    public class FormDTO
    {
        public int FormId { get; set; }
        public string FormName { get; set; }
        public string FormExcellTemplate { get; set; }
        public List<TableDTO> Tables { get; set; }
    }
    public class TableDTO
    {
        public long TableId { get; set; }

        //[StringLength(1000, ErrorMessageResourceType = (typeof(Resources.SharedResources.ErrorMessages)), ErrorMessageResourceName = "Less500")]
        public string TableName { get; set; }
        public bool IsTableDeleted { get; set; }
        public bool IsNewTable { get; set; }
    }
    public class QuantityTableStepDTO
    {
        public int? QuantityTableVersionId { get; set; }
        public int TemplateId { get; set; }
        public int TemplateYears { get; set; }
        public int TenderID { get; set; }
        public int TenderCreatedByTypeId { get; set; }
        public string TenderIdString { get; set; }
        public int PreQualificationId { get; set; }
        public int InvitationTypeId { get; set; }
        [Display(Name = "AlternativeOffer", ResourceType = typeof(DisplayInputs))]
        public bool HasAlternativeOffer { get; set; }
        public bool HasChangedAlternativeOffer { get; set; }
        public string TenderName { get; set; }
        public int TenderStatusId { get; set; }
        public string ReferenceNumber { get; set; }
        public string TenderNumber { get; set; }
        public int TenderTypeId { get; set; }
        public int OfferPresentationWayId { get; set; }
        public bool IsReadOnly { get; set; }
        public List<TemplateFormDTO> TemplateFormDTOs { get; set; }
        public int ChangeRequestTypeId { get; set; }
        public int ChangeRequestStatusId { get; set; }
        public string RejectionReason { get; set; }
        public int OfferId { get; set; }
        /// <summary>
        /// نموذج اعاشة
        /// </summary>
        public bool hasEasha { get; set; } = false;
    }



    public class TableTemplateDTO
    {
        [JsonProperty]
        public string FormHtml { get; set; }
        public string EncryptedOfferId { get; set; }

        /// <summary>
        /// عدد سنين المنافسة
        /// </summary>
        public int YearsCount { get; set; }
        /// <summary>
        /// البيانات من جدول "Qualification Quantity Item "
        /// </summary>
        [JsonProperty]
        public List<TenderQuantityItemDTO> QuantityItemDtos { get; set; }
        public int TenderId { get; set; }
        public string EncryptedTenderId { get; set; }
        public int ActivityId { get; set; }

        //public bool HasAlternativeOffer { set; get; }
    }


}
