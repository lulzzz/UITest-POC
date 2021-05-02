namespace MOF.Etimad.Monafasat.ViewModel
{
    public class NegotiationQuantityTableItemModel
    {
        public string Id { get; set; }
        public string TenderQTItemId { get; set; }
        public string NegotiationQTItemId { get; set; }
        public string SupplierQTItemId { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; }
        public string Name { get; set; }
        public string AttachmentName { get; set; }
        public string AttachmentRefId { get; set; }
        public string Details { get; set; }
        public decimal Price { get; set; }
        #region [Navigation]
        public NegotiationQuantityTableModel NegotiationQuantityTable { get; set; }

        #endregion

        #region [Methods]
        public NegotiationQuantityTableItemModel()
        {

        }
        public NegotiationQuantityTableItemModel(string id, int quantity, string unit, string name)
        {
            Id = id;
            Quantity = quantity;
            Unit = unit;
            Name = name;
        }
        #endregion

    }

}
