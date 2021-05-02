using System;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.Integration
{
    public class ContractorInfoModel
    {
        public ContractorInfoModel()
        {
            ContractorContactInfo = new ContractorContactInfoModel();
            ContractorClassificationInfo = new List<ContractorClassificationInfoModel>();
        }
        public string ContractorNameAR { get; set; }
        public string ContractorNameEn { get; set; }
        public string ContractorCompany { get; set; }
        public DateTime ClassificationDt { get; set; }
        public string ClassificationDtHjr { get; set; }
        public ContractorContactInfoModel ContractorContactInfo { get; set; }
        public List<ContractorClassificationInfoModel> ContractorClassificationInfo { get; set; }
    }
}
