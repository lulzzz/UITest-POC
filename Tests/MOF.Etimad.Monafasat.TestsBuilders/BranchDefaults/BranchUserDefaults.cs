using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.Integration;
using MOF.Etimad.Monafasat.ViewModel;
using System.Collections.Generic;
using System.Reflection;

namespace MOF.Etimad.Monafasat.TestsBuilders.BranchDefaults
{
    public class BranchUserDefaults
    {
        public readonly int _branchId = 1;
        public readonly int _userProfileId = 1;
        public readonly int _userRoleId = 1;
        public readonly int _agencyType = 1;        
        public readonly string _relatedAgencyCode = "022001000000";
        public readonly string _agencyCode = "022001000000";
        public readonly decimal? _estimatedValueFrom = 1;
        public readonly decimal? _estimatedValueTo = 1;
        public readonly string _nationalId = "1070814676";

        public List<UserNotificationSetting> GetUserNotificationSettings()
        {
            List<UserNotificationSetting> userNotificationSetting = new List<UserNotificationSetting>();
            userNotificationSetting.Add(new UserNotificationSetting("123", "456", 1, false, false, 1));
            return userNotificationSetting;
        }

        public List<UserNotificationSetting> GetUserWithoutNotificationSettings()
        {
            List<UserNotificationSetting> userNotificationSetting = new List<UserNotificationSetting>();
            userNotificationSetting.Add(new UserNotificationSetting("123", "456", 1, false, false, 2));
            return userNotificationSetting;
        }



        public UserNotificationSetting GetUserNotificationSetting()
        {
            UserNotificationSetting userNotificationSetting = new UserNotificationSetting("123", 1, 1, false, false, 1);
            UserProfile user = new UserProfile(1123, "User Name", "Full Name", "0505050500", "AA@aa.com", new List<UserNotificationSetting>(1));
            PropertyInfo userProp = userNotificationSetting.GetType().GetProperty("User");
            userProp.SetValue(userNotificationSetting, user);
            return userNotificationSetting;
        }

        public BranchUser ReturnBranchUserDefaults()
        {
            BranchUser generalBranchUser = new BranchUser(_branchId, _userProfileId, _userRoleId, _relatedAgencyCode, _estimatedValueFrom, _estimatedValueTo);
            generalBranchUser.CreateBranchForTest(new Branch(_agencyCode, "testbranch", new List<BranchAddress> ()));
            generalBranchUser.CreateUserProfile();
            generalBranchUser.CreateUserRoleForTest();
            generalBranchUser.UserProfile.Update("hamed", "1087287072", "hsawak@etimad.sa", "0533286913");
            return generalBranchUser;
        }

        public CommitteeUser GetCommitteeUserDataForUserProfile()
        {
            UserProfile userProfile = new UserProfile(1, "username", "fullname", "phone", "email", new List<UserNotificationSetting>());
            CommitteeUser generalCommittee = new CommitteeUser(1, 1, 1, _agencyCode);
            generalCommittee.Test_AddCommittee(new Committee(1, _agencyCode, "committeeName", "address", "0533286913", "fax", "hsawak@etimad.sa", "11", "11", true));
            generalCommittee.Test_AddUserProfile(userProfile);
            generalCommittee.CreateUserRoleForTest();
            return generalCommittee;
        }


        public UserProfile ReturnUserProfileDefaults()
        {
            List<BranchUser> branchUsers = new List<BranchUser>();
            List<CommitteeUser> committeeUser = new List<CommitteeUser>();
            UserProfile userProfile = new UserProfile(1, "1087287072", "hamed", "0533286913", "hsawak@etimad.sa", null);
            userProfile.SetNotificationSettings(GetUserNotificationSettings());
            branchUsers.Add(ReturnBranchUserDefaults());
            committeeUser.Add(GetCommitteeUserDataForUserProfile());
            userProfile.SetBranchuserForTest(branchUsers);
            userProfile.SetCommitteeUsersForTest(committeeUser);
            return userProfile;
        }

        public UserProfile ReturnUserProfileDefaultWithoutNotificationSettings()
        {
            UserProfile userProfile = new UserProfile(1, "1087287072", "hamed", "0533286913", "hsawak@etimad.sa", null);
            userProfile.SetNotificationSettings(GetUserWithoutNotificationSettings());
            return userProfile;
        }

        public UserProfile ReturnUserProfileData()
        {
            UserProfile userProfile = new UserProfile(1, "1087287072", "hamed", "0533286913", "hsawak@etimad.sa", null);
            userProfile.BranchUsers = new List<BranchUser>();
            userProfile.CommitteeUsers = new List<CommitteeUser>();
            userProfile.SetNotificationSettings(GetUserNotificationSettings());
            return userProfile;
        }





        public ManageUsersAssignationModel ReturnManageUsersAssignationDataModel()
        {
            List<RoleModel> roles = new List<RoleModel>();
            RoleModel roleModel = new RoleModel() { RoleName = "NewMonafasat_OffersCheckSecretary", RoleNameAr = "NewMonafasat_OffersCheckSecretary" };
            roles.Add(roleModel);

            ManageUsersAssignationModel manageUsersAssignationModel = new ManageUsersAssignationModel()
            {
                userId=102,
                nationalId=_nationalId,
                fullName="test", 
                mobileNumber="0533286913", 
                AgencyNames = "وزارة الصحة - الديوان العام",
                AllCommitteeRoles = roles,
                email="h@y.com",
                BranchRoles= roles,
                CommitteeAuditOfferRoles=roles
            };
            return manageUsersAssignationModel;
        }

        public List<IDMRolesModel> GetIDMRolesModel()
        {
            List<IDMRolesModel> iDMRolesModel = new List<IDMRolesModel>();
            iDMRolesModel.Add(new IDMRolesModel() { Name = "NewMonafasat_DataEntry", NormalizedName = "1" });
            return iDMRolesModel;
        }

        public NotificationOperationCode ReturnNotificationOperationCode()
        {
            return new NotificationOperationCode("100","","","","","","","","",1, _userRoleId);
        }

    }
}
