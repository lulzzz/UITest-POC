using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;

namespace MOF.Etimad.Monafasat.TestsBuilders.CommitteeDefaults
{
    public class CommitteeDefaults
    {
        public readonly string _agencyCode = "022001000000";
        public readonly List<BranchAddress> _branchAddresses = new List<BranchAddress>();
        public readonly string _committeeName = "committename";
        public readonly int addressTypeId = 1;
        public readonly string position = "position";
        public readonly string address = "address";
        public readonly string phone = "0533286913";
        public readonly string fax = "0533286913";
        public readonly string phone2 = "0533286913";
        public readonly string fax2 = "0533286913";
        public readonly string email = "hsawak@etimad.sa";
        public readonly string description = "description";
        public readonly string cityCode = "11111";
        public readonly string postalCode = "11111";
        public readonly string zipCode = "11111";
        public readonly bool? isActive = true;

        public Committee ReturnCommitteeDefaults()
        {
            Committee generalCommittee = new Committee(0, _agencyCode, _committeeName, address, phone, fax, email, postalCode, zipCode, isActive);
            return generalCommittee;
        }

        public UserProfile ReturnUserProfileDefaults()
        {
            UserProfile userProfile = new UserProfile(1, "1087287072", "hamed", "0533286913", "hsawak@etimad.sa", null);
            userProfile.SetNotificationSettings(GetUserNotificationSettings());
            return userProfile;
        }

        public UserProfile ReturnUserProfileDefaultWithoutNotificationSettings()
        {
            UserProfile userProfile = new UserProfile(1, "1087287072", "hamed", "0533286913", "hsawak@etimad.sa", null);
            userProfile.SetNotificationSettings(GetUserWithoutNotificationSettings());
            return userProfile;
        }
        public List<IDMRolesModel> GetIDMRolesModel()
        {
            List<IDMRolesModel> iDMRolesModel = new List<IDMRolesModel>();
            iDMRolesModel.Add(new IDMRolesModel() { Name = "NewMonafasat_DataEntry", NormalizedName = "1" });
            return iDMRolesModel;
        }

        public List<UserNotificationSetting> GetUserNotificationSettings()
        {
            var setting = new UserNotificationSetting("123", "456", 1, false, false, 1);
            List<UserNotificationSetting> userNotificationSetting = new List<UserNotificationSetting>();
            setting.SetIdForTest(100);
            setting.setNotificationOperationCodeForTest();
            userNotificationSetting.Add(setting);
            return userNotificationSetting;
        }

        public List<UserNotificationSetting> GetUserWithoutNotificationSettings()
        {
            List<UserNotificationSetting> userNotificationSetting = new List<UserNotificationSetting>();
            userNotificationSetting.Add(new UserNotificationSetting("123", "456", 1, false, false, 2));
            return userNotificationSetting;
        }

        public Committee GetCommitteeData()
        {
            Committee generalCommittee = new Committee(1, _agencyCode, _committeeName, address, phone, fax, email, postalCode, zipCode, isActive);
            generalCommittee.Test_AddCommitteeUsers(new List<CommitteeUser>());
            return generalCommittee;
        }



        public List<Committee> GetCommitteesData()
        {
            List<Committee> committees = new List<Committee>();
            Committee generalCommittee = new Committee(1, _agencyCode, _committeeName, address, phone, fax, email, postalCode, zipCode, isActive);
            generalCommittee.Test_AddCommitteeUsers(new List<CommitteeUser>());
            generalCommittee.Test_AddCommitteeTypeId(1);
            committees.Add(generalCommittee);
            return committees;
        }



        public CommitteeUser GetCommitteeUserData()
        {
            UserProfile userProfile = new UserProfile(1, "username", "fullname", phone, email, new List<UserNotificationSetting>());
            CommitteeUser generalCommittee = new CommitteeUser(1, 1, 1, _agencyCode);
            generalCommittee.Test_AddUserProfile(userProfile);
            return generalCommittee;
        }

        public CommitteeUserModel GetCommitteeUserModel()
        {

            return new CommitteeUserModel
            {
                RelatedAgencyCode = "022001000000",
                RoleName = "NewMonafasat_DataEntry",
                UserName = "1087287072",

            };
        }


        public CommitteeModel ReturnCommitteeModelData()
        {
            return new CommitteeModel()
            {
                AgencyCode = _agencyCode,
                CommitteeName = _committeeName,
                Address = address,
                CommitteeId = 1,
                Email = email,
                Fax = fax,
                Phone = phone,
                PostalCode = postalCode,
                ZipCode = zipCode,
                RelatedAgencyCode = _agencyCode
            };
        }

        public CommitteeUserAssignModel GetCommitteeUserAssignModel()
        {
            return new CommitteeUserAssignModel
            {
                CommitteeId=1,
                RelatedAgencyCode = "022001000000",
                RoleName = "NewMonafasat_DataEntry"
            };
        }


        public List<CommitteeUserAssignModel> GetAllCommitteeUserAssignModel()
        {
            List<CommitteeUserAssignModel> committeeUserAssignModels = new List<CommitteeUserAssignModel>();
            committeeUserAssignModels.Add(GetCommitteeUserAssignModel());
            return committeeUserAssignModels;
        }

        public List<CommitteeModel> ReturnCommitteeModels()
        {
            return new List<CommitteeModel>() {
                new CommitteeModel()
                {
                    AgencyCode = _agencyCode,
                    CommitteeName = _committeeName,
                    Address = address,
                    CommitteeId = 1,
                    Email = email,
                    Fax = fax,
                    Phone = phone,
                    PostalCode = postalCode,
                    ZipCode = zipCode,
                    RelatedAgencyCode = _agencyCode
                }
            };
        }
    }
}
