using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.ViewModel
{
    public class SaveTableModel
    {
        public string TenderId { get; set; }
        public int OfferId { get; set; }
        public string Name { get; set; }
        public string tableQuantityName { get; set; }
        public int TableQuantityId { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal VAT { get; set; }
        public decimal OpeningTotalPrice { get; set; }
        public decimal OpeningDiscount { get; set; }
        public decimal OpeningVat { get; set; }
        public List<SaveitemModel> QuantityTableItem { get; set; }
    }

    public class SaveitemModel
    {
        public int Id { get; set; }

        //[Range(1, int.MaxValue, ErrorMessageResourceName = "CannotInsertZeroInPrice", ErrorMessageResourceType = typeof(Resources.OfferResources.ErrorMessages))]
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal VAT { get; set; }
        public decimal Quantity { get; set; }
        public int itemQuantity { get; set; }
        public string Unit { get; set; }
    }


}
