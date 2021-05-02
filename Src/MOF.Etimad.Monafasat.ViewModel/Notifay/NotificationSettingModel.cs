namespace MOF.Etimad.Monafasat.ViewModel
{
    //[Serializable]
    public class NotificationSettingModel
    {
        public int Id { get; set; }

        public NotificationByModel OperationsOnTheTender { get; set; }
        public NotificationByModel InquiriesAboutTender { get; set; }
        public NotificationByModel OperationsNeedToBeAccredited { get; set; }
        public NotificationByModel OfferStatus { get; set; }
        public NotificationByModel OperationsOnYourAccount { get; set; }
        public NotificationByModel PurchaseInvoiceNumber { get; set; }
        public NotificationByModel TenderRelatedToYourActivity { get; set; }
        public NotificationByModel ReceiveTheAmountOfTheBooklet { get; set; }

    }
}
