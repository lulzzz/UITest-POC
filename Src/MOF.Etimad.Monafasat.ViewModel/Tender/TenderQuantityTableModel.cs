using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class TenderQuantityTableModel
    {
        public int TenderId { set; get; }
        public int QuantitiesTableId { get; set; }
        //[Required]
        [Display(ResourceType = typeof(Resources.TenderResources.DisplayInputs), Name = "TableName")]
        [StringLength(1024)]
        public string Name { get; set; }
        public int TenderStatusId { get; set; }
        //public int[] QuantityItemsIDs { get; set; }
        public List<TenderQuantityTableItemModel> TenderQuantitiesTableItems { get; set; } = new List<TenderQuantityTableItemModel>();
        //public List<int> QuantitiesTableItemsIds { get; set; } = new List<int>();
        public bool? IsActive { get; set; }
        public int? TenderQuantitiesTableId { get; set; }

        [StringLength(2048)]
        public string RejectionReason { get; set; }
        public int TableChangeStatusId { get; set; }
    }
}
