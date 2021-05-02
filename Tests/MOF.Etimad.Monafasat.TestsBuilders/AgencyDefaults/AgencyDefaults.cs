using MOF.Etimad.Monafasat.Core;
using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.SharedKernel;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.TestsBuilders.AgencyDefaults
{
    public class AgencyDefaults
    {
        public AgencyInfoModel GetAgencyDefaults()
        {
            return new AgencyInfoModel()
            {
                beneficiaryClassNo = "000",
                beneficiaryBranchNo = "111",
                beneficiarySectionNo = "222",
                beneficiarySerialNo = "333",
                managementNoForBeneficiary = "444",
                sectionNoForBeneficiary = "555"
            };
        }

        public List<GovAgency> GetGovAgencies()
        {
            GovAgency govAgency = new GovAgency("022001000000", "agency name", null, false, 1, "01222222");

            List<GovAgency> govAgencies = new List<GovAgency>() { govAgency };

            return govAgencies;
        }

        public GovAgency GetGovAgency()
        {
            GovAgency govAgency = new GovAgency("022001000000", "agency name", null, false, 1, "01222222");

            return govAgency;
        }

        public UserProfile GetUserProfileData()
        {
            UserNotificationSetting setting = new UserNotificationSetting("code", 1, 1, true, true, 1);
            List<UserNotificationSetting> settings = new List<UserNotificationSetting>() { setting };
            UserProfile userProfile = new UserProfile(1, "user name", "full name", "mobile", "email", settings);
            return userProfile;
        }

        public List<UserLookupModel> GetUserLookupModels()
        {
            List<UserLookupModel> userLookupModels = new List<UserLookupModel>()
            {
                new UserLookupModel(1, "name", "1111111111", new List<RoleModel>(){ new RoleModel() { RoleId = 1, RoleName = RoleNames.MonafasatAdmin, RoleNameAr = RoleNames.MonafasatAdmin } })
            };
            return userLookupModels;
        }

        public List<UserProfile> GetUserProfiles()
        {
            List<UserProfile> userProfiles = new List<UserProfile>() { GetUserProfileData() };
            return userProfiles;

        }
    }
}
