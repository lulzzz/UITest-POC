using MOF.Etimad.Monafasat.SharedKernal;

namespace MOF.Etimad.Monafasat.UnitTests
{
    public class RootConfigurationDefaults
    {
        public RootConfigurations DefaultRootConfigurationsForBilling()
        {
            BillingConfiguration billingConfiguration = new BillingConfiguration()
            {
                ClientKey = "111111",
                GFSCODEForConditionalBooklet = "1421901",
                GFSCODEForInvitations = "1421901",
                GFSCODEForAddedValue = "1421901"
            };
            EsbSettingsConfiguration esbSettingsConfiguration = new EsbSettingsConfiguration()
            {
                IsProduction = false,
                BillRefIdForBillingForSaddad = "006000000000000000",
                BillAgencyCodeForSaddad = "006000000000000000",
                BillAgencyCodeentifierForSaddad = "006000000000000000",
                BenAgencyCodeForInvitations = "006000000000000000",
                BenAgencyCodeForConditionsBooklet = "006000000000000000",
                ClientCertificateFindValue = "EtimadIDMProd",
            };

            var options = new RootConfigurations()
            {
                EsbSettingsConfiguration = esbSettingsConfiguration,
                BillingConfiguration = billingConfiguration
            };

            return options;
        }

        public RootConfigurations DefaultRootConfigurationsForOfferTimesConfiguration()
        {
            OfferTimesConfiguration offerTimesConfiguration = new OfferTimesConfiguration()
            {
                StartOfferTime = 7,
                EndOfferTime = 20,
                StartOfferVacationTime = 9,
                EndOfferVacationTime = 14
            };
            PlaintSettingConfiguration plaintSettingConfiguration = new PlaintSettingConfiguration()
            {
                PlaintReviewingPeriod = "15",
                PlaintPeriod = "3"
            };
            var options = new RootConfigurations()
            {
                OfferTimesConfiguration = offerTimesConfiguration,
                PlaintSettingConfiguration = plaintSettingConfiguration
            };

            return options;
        }

        public RootConfigurations DefaultRootConfigurationsForTender()
        {
            OldFlow oldFlow = new OldFlow()
            {
                isApplied = true,
                EndDate = "2020-04-24"
            };
            NewAwarding newAwarding = new NewAwarding()
            { 
                ReleaseDate = "2020-11-24"
            };

            UnitAgencyCodeConfiguration unitAgencyCodeConfiguration = new UnitAgencyCodeConfiguration()
            {
                UnitAgencyCode = "022001000000"
            };

            RootConfigurations rootConfigurations = new RootConfigurations()
            {
                OldFlow = oldFlow,
                NewAwarding = newAwarding,
                UnitAgencyCodeConfiguration = unitAgencyCodeConfiguration

            };

            return rootConfigurations;
        }

        public RootConfigurations DefaultRootConfigurationsForQualification()
        {

            QualificationSettingConfiguration qualificationSetting = new QualificationSettingConfiguration()
            {
                DefaultValue =  "0.01"
            };
            RootConfigurations rootConfigurations = new RootConfigurations()
            {
               QualificationSettingConfiguration = qualificationSetting

            };
            return rootConfigurations;
        }
        public RootConfigurations DefaultRootConfigurationsForCachingInformation()
        {

            ChachingConfiguration ChachingConfiguration = new ChachingConfiguration()
            {
               CachingDays = "1",
               CachingHours = "1",
               CachingMinutes = "1",
               CachingSeconds = "31104000",
            };

            MonafasatURLConfiguration monafasatURLConfiguration = new MonafasatURLConfiguration()
            {
                MonafasatURL= "https://monafasat.etimad.sa/"
            };

            InvitationEmailConfiguration invitationEmailConfiguration = new InvitationEmailConfiguration()
            {
                SendInvitationByEmail = new SendInvitationByEmail2Configuration() {subject="subject",body="body" } ,
                SendInvitationBySms="sms"
            };

            InvitationSolidarityEmailConfiguration invitationSolidarityEmailConfiguration = new InvitationSolidarityEmailConfiguration()
            {
                
                SendInvitationByEmail =new SendInvitationByEmailConfiguration() { subject = "subject", body = "body" },
                SendInvitationBySms = "sms"
            };

            RootConfigurations rootConfigurations = new RootConfigurations()
            {
                ChachingConfiguration = ChachingConfiguration,
                MonafasatURLConfiguration= monafasatURLConfiguration,
                InvitationEmailConfiguration= invitationEmailConfiguration,
                InvitationSolidarityEmailConfiguration= invitationSolidarityEmailConfiguration

            };
            return rootConfigurations;
        }

        public RootConfigurations DefaultRootConfigurationsFor()
        {
            OldFlow oldFlow = new OldFlow()
            {
                isApplied = true,
                EndDate = "2020-04-24"
            };
            RootConfigurations rootConfigurations = new RootConfigurations()
            {
                OldFlow = oldFlow
            };

            return rootConfigurations;
        }


    }
}
