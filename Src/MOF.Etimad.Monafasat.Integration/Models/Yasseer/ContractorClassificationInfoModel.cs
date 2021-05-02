using System;

namespace MOF.Etimad.Monafasat.Integration
{
    public class ContractorClassificationInfoModel
    {
        public ContractorClassificationInfoModel() { }
        public string ClassificationField { get; set; }
        public string ClassificationGrade { get; set; }
        public bool ClassificationDtSpecified { get; set; }
        public DateTime ClassificationDt { get; set; }
        public string ClassificationDtHjr { get; set; }
    }
}