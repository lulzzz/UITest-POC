using System;
namespace MOF.Etimad.Monafasat.Integration
{
    public class PaymentNotificationServiceModel
    {
        public PaymentNotificationServiceModel()
        {

        }
        public PaymentNotificationServiceModel(string agencyCode, string intermediatePaymentId, string billCategory, string billAccount, decimal? paymentAmount, DateTime? paymentDate,
            string paymentMethod, string paymentStatusCode, string paymentReferenceInfo, string sadadPaymentTranactionId,
            string bankId, string bankReversalTransactionID, string bankPaymentID, string paymentChannel, string poiNumber, string poiType)
        {
            AgencyCode = agencyCode;
            IntermediatePaymentId = intermediatePaymentId;
            BillCategory = billCategory;
            BillAccount = billAccount;
            PaymentAmount = paymentAmount;
            PaymentDate = paymentDate;
            PaymentMethod = paymentMethod;
            PaymentStatusCode = paymentStatusCode;
            PaymentReferenceInfo = paymentReferenceInfo;
            PaymentMethodDetail = new PaymentMethodDetailsServiceModel(bankId, bankPaymentID, bankReversalTransactionID, paymentChannel, sadadPaymentTranactionId);
            PayorPOIDetail = new PayorPOIServiceModel(poiNumber, poiType);
        }
        public string AgencyCode { get; private set; }
        public string IntermediatePaymentId { get; private set; }
        public string BillCategory { get; private set; }
        public string BillAccount { get; private set; }
        public decimal? PaymentAmount { get; private set; }
        public DateTime? PaymentDate { get; private set; }
        public string PaymentMethod { get; private set; }
        public string PaymentStatusCode { get; private set; }
        public string PaymentReferenceInfo { get; private set; }
        public PaymentMethodDetailsServiceModel PaymentMethodDetail { get; private set; }
        public PayorPOIServiceModel PayorPOIDetail { get; private set; }
    }
}
