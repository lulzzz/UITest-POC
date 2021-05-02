using System;
namespace MOF.Etimad.Monafasat.PaymentCallbackAPI.Model
{
    public class PaymentNotificationResponseModel
    {
        public string StatusCode { get; set; }
        public string StatusDesc { get; set; }
        public string UpdatedPaymentNotificationList { get; set; }
    }
    public class PaymentNotificationModel
    {
        public string AgencyCode { get; set; }
        public string IntermediatePaymentId { get; set; }
        public string BillCategory { get; set; }
        public string BillAccount { get; set; }
        public decimal PaymentAmount { get; set; }
        public DateTime PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public string PaymentStatusCode { get; set; }
        public string PaymentReferenceInfo { get; set; }
        public PaymentMethodDetails PaymentMethodDetail { get; set; }
        public PayorPOI PayorPOI { get; set; }
    }

    public class PaymentMethodDetails
    {
        public string SADADPaymentTransactionId { get; set; }
        public string BankId { get; set; }
        public string BankReversalTransactionID { get; set; }
        public string BankPaymentId { get; set; }
        public string PaymentChannel { get; set; }
    }
    public class PayorPOI
    {
        public string POINumber { get; set; }
        public string POIType { get; set; }
    }
}