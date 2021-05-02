using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.SharedKernel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MOF.Etimad.Monafasat.Core
{
    [Table("Supplier", Schema = "IDM")]
    public class Supplier : AuditableEntity
    {
        #region Properties
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [StringLength(20)]
        public string SelectedCr { get; private set; }
        [StringLength(200)]
        public string SelectedCrName { get; private set; }
        public List<SupplierUserProfile> SupplierUserProfiles { get; private set; }
        public List<Invitation> Invitations { get; private set; }
        public List<Offer> offers { get; private set; }
        public List<SupplierBlock> SupplierBlocks { get; private set; }
        public List<FavouriteSupplier> FavouriteSuppliers { get; private set; }
        public List<UserNotificationSetting> NotificationSetting { get; set; } = new List<UserNotificationSetting>();

        #endregion

        #region Constructors

        public Supplier()
        {
        }

        public Supplier(string selectedCr, string selectedCrName, List<UserNotificationSetting> userNotificationSettings)
        {
            SelectedCr = selectedCr;
            SelectedCrName = selectedCrName;
            NotificationSetting = userNotificationSettings;
            EntityCreated();
        }

        #endregion

        #region Operations

        public void Update(string selectedCr, string selectedCrName)
        {
            SelectedCr = selectedCr;
            SelectedCrName = selectedCrName;
            base.EntityUpdated();
        }

        public void AddNotificationSettings(List<UserNotificationSetting> setting)
        {
            foreach (var item in setting)
            {
                if (!NotificationSetting.Exists(x => x.NotificationCodeId == item.NotificationCodeId))
                {
                    NotificationSetting.Add(item);
                    EntityUpdated();
                }
            }
        }
        public bool UpdateNotificationSetting(int id, bool email, bool sms)
        {
            var set = NotificationSetting.FirstOrDefault(x => x.Id == id);
            set.UpdateNotificationSetting(email, sms, set.NotificationOperationCode.CanEditEmail, set.NotificationOperationCode.CanEditSMS);
            EntityUpdated();
            return true;
        }
        #endregion
    }
}
