using System;
using System.Collections.Generic;
namespace MOF.Etimad.Monafasat.Integration
{
    public class MCICRInfoModelRequest
    {
        public MCICRInfoModelRequest()
        {

        }
        public string CommercialRegistrationNumber { get; set; }
    }

    public class MCICRInfoModel
    {
        public MCICRInfoModel()
        {
        }
        public string Name { get; set; }
        public string CommercialRegistrationNameAr { get; set; }
        public string statusName { get; set; }
        public DateTime IssueDateHjri { get; set; }
        public DateTime ExpiryDateHjri { get; set; }
        public string CityNameAr { get; set; }
        public decimal Capital { get; set; }
        public string LegalTypeAr { get; set; }
        public string ResponseCode { get; set; }
        public string LegalTypeEn { get; set; }
        public CommercialRegistrationType CommercialRegistrationType { get; set; }
        public CommercialRegistrationStatusInfo CommercialRegistrationStatusInfo { get; set; }
        public string PhoneNumber { get; set; }
        public string FaxNumber { get; set; }
        public AddressDescription AddressDescription { get; set; }
        public CommercialRegistrationActivities CommercialRegistrationActivities { get; set; }
        public List<CROwner> CROwnersList { get; set; }
    }
}
