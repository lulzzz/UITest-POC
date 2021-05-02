using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class QuantitiesTemplateModel
    {
        public int TenderID { set; get; }
        public int OfferId { set; get; }
        public int negotiationId { set; get; }
        public string TenderName { get; set; }
        public string TenderIdString { get; set; }
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "QuantityTables")]
        public List<TenderQuantityItemDTO> QuantitiesItems { get; set; } = new List<TenderQuantityItemDTO>();
        public List<TenderQuantityItemDTO> QuantitiesItemsChanges { get; set; } = new List<TenderQuantityItemDTO>();
        public List<TenderQuantityTableDTO> TenderQuantityTableDTOs { get; set; }
        public int TenderStatusId { get; set; }

        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "AlternativeOffer")]
        public bool HasAlternativeOffer { get; set; }
        public int? TenderTypeId { get; set; }
        public bool IsExceptedAgency { get; set; }
        public string Template { get; set; }
        public List<HTMLObject> grids { set; get; } = new List<HTMLObject>();
        public List<string> grid { set; get; } = new List<string>();
        public int? TenderCreatedByTypeId { get; set; }
        public int? PreQualificationId { get; set; }
        public int? InvitationTypeId { get; set; }
        public int? OfferPresentationWayId { get; set; }
        public List<int> ActivityTemplates { get; set; }
        public List<int> FormIds { get; set; }
        public string ReferenceNumber { get; set; }
        public string TenderNumber { get; set; }
        public int? TemplateYears { get; set; }
        public bool IsReadOnly { get; set; }
        public int ChangeRequestTypeId { get; set; }
        public int ChangeRequestStatusId { get; set; }
        public string RejectionReason { get; set; }

        public List<TableFormHtml> tableformhtml { get; set; } = new List<TableFormHtml>();
    }
    public class HTMLObject
    {
        public long FormId { get; set; }
        public int FormCategoryId { get; set; }
        public string FormName { get; set; }
        public string TemplateName { get; set; }
        public string FormExcellTemplate { get; set; }
        public string Javascript { get; set; }
        public string DeleteFormHtml { get; set; }
        public string EditFormHtml { get; set; }
        public List<string> grid { set; get; } = new List<string>();
        public List<TableModel> gridTables { set; get; } = new List<TableModel>();
    }
    public class TableModel
    {
        public long FormId { get; set; }
        public string TableId { get; set; }
        public int TenderId { get; set; }
        public int OfferId { get; set; }
        public int negotiationId { get; set; }
        public string TableName { get; set; }
        public string TableHtml { get; set; }
        public string EditFormHtml { get; set; }
        public string DeleteFormHtml { get; set; }
        public bool IsReadOnly { get; set; }
        public bool? IsTableDeleted { get; set; }
        public bool? IsNewTable { get; set; }
        public string Javascript { get; set; }
        public int? TemplateYears { get; set; }
        public string DescriptionTableHtml { get; set; }
        public bool IsTableHasAlternative { get; set; }
        public bool HasChangeRequest { get; set; }
        public bool CanUndoChange { get; set; }
    }
}
