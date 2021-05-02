namespace MOF.Etimad.Monafasat.Integration
{
    public class PaymentMethodDetailsServiceModel
    {
        public PaymentMethodDetailsServiceModel()
        {

        }
        public PaymentMethodDetailsServiceModel(string bankId, string bankPaymentID, string bankReversalTransactionID, string paymentChannel, string sadadPaymentTranactionId)
        {
            BankId = bankId;
            BankPaymentID = bankPaymentID;
            BankReversalTransactionID = bankReversalTransactionID;
            PaymentChannel = paymentChannel;
            SadadPaymentTranactionId = sadadPaymentTranactionId;
        }
        public string SadadPaymentTranactionId { get; set; }
        public string BankId { get; set; }
        public string BankReversalTransactionID { get; set; }
        public string BankPaymentID { get; set; }
        public string PaymentChannel { get; set; }
    }
}
