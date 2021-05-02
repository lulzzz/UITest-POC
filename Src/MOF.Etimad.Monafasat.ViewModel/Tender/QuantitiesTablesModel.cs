using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class QuantitiesTablesModel
    {
        public int TenderID { set; get; }
        public string TenderName { get; set; }
        public string TenderNumber { get; set; }
        public string ReferenceNumber { get; set; }
        public string TenderIdString { get; set; }
        public byte[] TenderIdStringArray { get; set; }

        //[Display(Name = "جداول الكميات")]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "QuantityTables")]
        public IList<QuantityTableModel> QuantitiesTables { get; set; } = new List<QuantityTableModel>();
        public IList<QuantityTableModel> QuantitiesTablesChanges { get; set; } = new List<QuantityTableModel>();

        public int TenderStatusId { get; set; }

        public bool? HasAlternativeOffer { set; get; }
        public int? TenderTypeId { get; set; }
        public int? TenderCreatedByTypeId { get; set; }
        public bool IsExceptedAgency { get; set; }
        public int? OfferPresentationWayId { get; set; }

        public int? InvitationTypeId { get; set; }
        public int? PreQualificationId { get; set; }
        public int TenderChangeRequestId { get; set; }
        public int ChangeRequestStatusId { get; set; }
        public int ChangeRequestTypeId { get; set; }
        public string RejectionReason { get; set; }
        public string Template { get; set; }

        //[Display(Name = "جداول الكميات")]

        //public IList<int> Quantities { get; set; } = new List<int>();
    }
}
