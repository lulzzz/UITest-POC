using MOF.Etimad.Monafasat.Integration.Enums;

namespace MOF.Etimad.Monafasat.Integration
{
    public class CommercialRegistrationStatusInfo
    {
        public CommercialRegistrationStatusInfo()
        { }
        public CommercialRegistrationStatusInfo(int commercialRegistrationStatus)
        {
            CommercialRegistrationStatus = (CommercialRegistrationStatus)commercialRegistrationStatus;
        }
        public CommercialRegistrationStatus CommercialRegistrationStatus { get; set; }

    }
}
