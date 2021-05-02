using MOF.Etimad.Monafasat.Core.Entities;
using MOF.Etimad.Monafasat.ViewModel;
using static MOF.Etimad.Monafasat.SharedKernel.Enums;

namespace MOF.Etimad.Monafasat.TestsBuilders
{
    public class UserNotificationSettingBuilder
    {
        public UserNotificationSetting ReturnUserNotificationSettingDefaults()
        {
            return new UserNotificationSetting("123", "456", 1, false, false, 1); //("123", 1, 1, false, false, 1);
        }

        public MainNotificationTemplateModel ReturnMainNotificationTemplateModel()
        {
            NotificationArguments NotificationArguments = new NotificationArguments
            {
                BodyEmailArgs = new object[]{ "" ,"" , ""
                },
                SubjectEmailArgs = new object[] { },
                PanelArgs = new object[] { "" },
                SMSArgs = new object[] { "" }
            };
            return new MainNotificationTemplateModel(NotificationArguments, "", NotificationEntityType.Tender, "", 1);
        }
    }
}
