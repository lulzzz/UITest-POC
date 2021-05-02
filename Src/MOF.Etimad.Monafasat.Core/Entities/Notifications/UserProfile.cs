using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    [Table("UserProfile", Schema = "IDM")]
    public class UserProfile : AuditableEntity
    {

        #region [ Constructors ]
        public UserProfile()
        {

        }

        public UserProfile(int userId, string userName, string fullName, string mobile, string email, List<UserNotificationSetting> setting)
        {
            Id = userId;
            FullName = fullName;
            UserName = userName;
            Mobile = mobile;
            Email = email;
            NotificationSetting = setting;
            EntityCreated();
        }

        #endregion

        #region [ Fields ]

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; private set; }
        [StringLength(20)]
        public string Mobile { get; private set; }
        [StringLength(100)]
        public string Email { get; private set; }
        [StringLength(20)]
        public virtual string UserName { get; set; }
        [StringLength(200)]
        public string FullName { get; protected set; }
        [StringLength(200)]
        public string DefaultUserRole { get; private set; }
        public List<UserNotificationSetting> NotificationSetting { get; set; } = new List<UserNotificationSetting>();
        public List<BaseNotification> Notifications { get; set; }
        public List<UserAgencyRole> UserAgencyRoles { get; set; }
        public List<SupplierUserProfile> SupplierUserProfiles { get; set; }
        public List<CommitteeUser> CommitteeUsers { get; set; }
        public List<BranchUser> BranchUsers { get; set; }
        public byte[] RowVersion { get; set; }

        #endregion 

        #region [ Methods ]
        public void UpdateDefaultUserRole(string defaultUserRole)
        {
            DefaultUserRole = defaultUserRole;
            EntityUpdated();
        }

        public List<UserNotificationSetting> AddNotificationSettings(List<UserNotificationSetting> setting)
        {

            foreach (var item in setting)
            {
                if (!NotificationSetting.Exists(x => x.NotificationCodeId == item.NotificationCodeId))
                {
                    NotificationSetting.Add(item);
                }
            }
            EntityUpdated();
            return setting;
        }
        public void AddNotificationSetting(UserNotificationSetting setting)
        {


            if (!NotificationSetting.Exists(x => x.NotificationCodeId == setting.NotificationCodeId))
            {
                NotificationSetting.Add(setting);
            }

            EntityUpdated();

        }

        public void AddMissingNotificationSettings(List<UserNotificationSetting> setting)
        {


            NotificationSetting?.AddRange(setting); EntityUpdated();




        }

        public bool UpdateNotificationSetting(int id, bool email, bool sms)
        {
            var set = NotificationSetting.FirstOrDefault(x => x.Id == id);
            set.UpdateNotificationSetting(email, sms, set.NotificationOperationCode.CanEditEmail, set.NotificationOperationCode.CanEditSMS);
            EntityUpdated();
            return true;
        }
        public UserProfile Update(string fullName, string userName, string email, string mobile)
        {
            FullName = fullName;
            UserName = userName;
            Email = email;
            Mobile = mobile;
            EntityUpdated();
            return this;
        }

        public void RestPermission()
        {
            this.BranchUsers.ForEach(x => x.DeActive());
            this.CommitteeUsers.ForEach(x => x.DeActive());
            EntityUpdated();
        }

        public void CheckExistRoleForUser(List<string> userRoles, string agencyCode)
        {
            foreach (var item in BranchUsers.Where(a => a.Branch.AgencyCode == agencyCode))
            {
                if (!userRoles.Contains(item.UserRole.Name))
                {
                    item.DeActive();
                }
            }
            foreach (var item in CommitteeUsers.Where(a => a.Committee.AgencyCode == agencyCode))
            {
                if (!userRoles.Contains(item.UserRole.Name))
                {
                    item.DeActive();
                }
            }
        }

        public void CheckExistRoleForUser(List<string> userRoles, string agencyCode, string relatedAgencyCode)
        {
            foreach (var item in BranchUsers.Where(a => a.Branch.AgencyCode == agencyCode && a.RelatedAgencyCode == relatedAgencyCode))
            {
                if (!userRoles.Contains(item.UserRole.Name))
                {
                    item.DeActive();
                }
            }
            foreach (var item in CommitteeUsers.Where(a => a.Committee.AgencyCode == agencyCode && a.RelatedAgencyCode == relatedAgencyCode))
            {
                if (!userRoles.Contains(item.UserRole.Name))
                {
                    item.DeActive();
                }
            }
        }

        public void SetNotificationSettings(List<UserNotificationSetting> setting)
        {
            NotificationSetting = new List<UserNotificationSetting>();
            NotificationSetting?.AddRange(setting); EntityUpdated();
        }

        public void SetBranchuserForTest(List<BranchUser> branchUsers)
        {
            BranchUsers = branchUsers;
        }
        public void SetCommitteeUsersForTest(List<CommitteeUser> committeeUsers)
        {
            CommitteeUsers = committeeUsers;
        }
        #endregion

    }
}
