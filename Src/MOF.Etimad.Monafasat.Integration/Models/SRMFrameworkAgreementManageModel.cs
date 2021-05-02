using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.Integration
{
    public class SRMFrameworkAgreementManageModel
    {
        public string ReferenceNumber { get; set; }
        public string ContractName { get; set; }
        public SRMContractType ContractType { get; set; }
        public DateTime AwardDt { get; set; }
        public string CreatedBy { get; set; }
        public ValidityInfo ValidityInfo { get; set; }
        public DateTime ValidFrom { get; set; }
        public string Currency { get; set; }
        public decimal TotalAmt { get; set; }
        public string Note { get; set; }
        public List<VendorList> VendorsList { get; set; }
        public List<string> AgencyList { get; set; }
    }

    public enum SRMContractType
    {
        Open = 0,
        Close = 1
    }
    public class ValidityInfo
    {
        public int NumOfDays { get; set; }
        public int NumOfMonths { get; set; }
        public int NumOfYears { get; set; }
    }

    public class VendorList
    {
        public string VendorId { get; set; }
        public decimal AwardedAmt { get; set; }
        public string PurchaseCurrency { get; set; }
        public List<ProductList> ProductList { get; set; } = new List<ProductList>();
    }
    public class ProductList
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Quantity { get; set; }
        public string UnitOfMeasure { get; set; }
        public decimal VATAmt { get; set; }
        public decimal DiscountPercen { get; set; }
        public DeliveryDurationInfo DeliveryDurationInfo { get; set; }
        public string Desc { get; set; }
    }
    public class DeliveryDurationInfo
    {
        public int NumOfDays { get; set; }
        public int NumOfMonths { get; set; }
        public int NumOfYears { get; set; }
    }
    public class AgencyList
    {
        public string AgencyId { get; set; }
    }
}
