using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using MOF.Etimad.Monafasat.ViewModel.Notification;
using System.Collections.Generic;
using System.Reflection;

namespace MOF.Etimad.Monafasat.TestsBuilders.NotificationDefaults
{
    public class NotificationsDefaults
    {
        public List<UserNotificationSetting> GetUserNotificationSettings()
        {
            List<UserNotificationSetting> userNotificationSetting = new List<UserNotificationSetting>();
            userNotificationSetting.Add(new UserNotificationSetting("123", "456", 1, false, false, 1));
            userNotificationSetting.Add(new UserNotificationSetting("123",1, 1, false, false, 1));

            return userNotificationSetting;
        }

        public List<UserNotificationSetting> GetUserNotificationSettingWithProfileId()
        {
            var generalSetting = new UserNotificationSetting("123", 1, 1, true, true, 1);
            List<UserNotificationSetting> userNotificationSetting = new List<UserNotificationSetting>();
            userNotificationSetting.Add(generalSetting);

            UserProfile user = new UserProfile(102, "User Name", "Full Name", "0505050500", "AA@aa.com", new List<UserNotificationSetting>(1));
            PropertyInfo userProp = generalSetting.GetType().GetProperty("User");
            userProp.SetValue(generalSetting, user);
            
            return userNotificationSetting;
        }


        public List<UserNotificatioSettingModel> GetUserNotificationSettingModel()
        {
            List<UserNotificatioSettingModel> userNotificationSettings = new List<UserNotificatioSettingModel>();

            UserNotificatioSettingModel userNotificationSetting = new UserNotificatioSettingModel()
            {

              OperationCode="1",
              SMSEnabled=true,
              EmailEnabled=true,
              Id= "69N3QwUl%208o0yRoVEsRsYA==",
              categoryName="categoryname"
            };

            userNotificationSettings.Add(userNotificationSetting);
            return userNotificationSettings;
        }


        

        public List<NotificationPanelModel> GetNotificationPanelModel()
        {
            List<NotificationPanelModel> notifications = new List<NotificationPanelModel>();

            NotificationPanelModel notificationPanelModel = new NotificationPanelModel()
            {
                Link="link",
            };

            notifications.Add(notificationPanelModel);
            return notifications;
        }



        public List<NotificationOperationCode> GetNotificationOperationCode()
        {
            List<NotificationOperationCode> notificationOperationCodes = new List<NotificationOperationCode>();

            NotificationOperationCode notificationOperation = new NotificationOperationCode("OpCode", "arName", "enName", "panelAr", "panelEn", "emailAr", "emailEn", "smsAr", "smsEn", 1, 1);
            notificationOperation.setNotificationOperationCodeIdForTest();
            notificationOperationCodes.Add(notificationOperation);

            return notificationOperationCodes;
        }


        public NotificationOperationCode GetNotificationOperationCodeData()
        {

            NotificationOperationCode notificationOperation = new NotificationOperationCode("OpCode", "arName", "enName", "panelAr", "panelEn", "emailAr", "emailEn", "smsAr", "smsEn", 1, 1);
            notificationOperation.setNotificationOperationCodeIdForTest();
            return notificationOperation;
        }

        public List<OperationCodeModel> GetNotificationOperationCodeModel()
        {
            List<OperationCodeModel> notificationOperationCodes = new List<OperationCodeModel>();

            OperationCodeModel notificationOperation = new OperationCodeModel() { 
            OperationCode= "OperationCode",
            OperationCodeArabic= "OperationCode"
            };

            notificationOperationCodes.Add(notificationOperation);

            return notificationOperationCodes;
        }

        

        public UserProfile ReturnUserProfileDefaults()
        {
            UserProfile userProfile = new UserProfile(1, "1087287072", "hamed", "0533286913", "hsawak@etimad.sa", null);
            userProfile.SetNotificationSettings(GetUserNotificationSettings());
            return userProfile;
        }


       public  List<BaseNotification> getBaseNotification()
        {
            NotificationEmail notificationEmail = new NotificationEmail(1,"hsawak@etimad.sa","testTitle","TestContent",1,"link","key");
            List<BaseNotification> baseNotifications = new List<BaseNotification>();

                baseNotifications.Add(notificationEmail);
           
            NotificationSMS notificationSMS = new NotificationSMS(1,"0533286913","content",1,"link","key");
            baseNotifications.Add(notificationEmail);
            baseNotifications.Add(notificationSMS);
            return baseNotifications;
        }
    }
}
