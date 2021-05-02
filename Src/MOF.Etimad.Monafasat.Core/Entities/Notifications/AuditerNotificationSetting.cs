using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public class AuditerNotificationSetting : BaseNotificationSetting   //مدقق بيانات
    {
        public AuditerNotificationSetting()
        {

        }
        public AuditerNotificationSetting(bool load)
        {

            OperationsOnTheTender = new NotificationBy() { Email = true, Mobile = true, };
           
            InquiriesAboutTender = new NotificationBy() { Email = true, Mobile = true, }; ;
            OperationsNeedToBeAccredited = new NotificationBy() { Email = true, Mobile = true, }; ;
            EntityCreated();
        }
        public AuditerNotificationSetting(NotificationBy inquiriesAboutTender, NotificationBy operationsNeedToBeAccredited
                , NotificationBy operationsOnTheTender)
        {

            InquiriesAboutTender = inquiriesAboutTender;
            OperationsOnTheTender = operationsOnTheTender;
         
            OperationsNeedToBeAccredited = operationsNeedToBeAccredited;
          
            EntityCreated();
        }
    
        public NotificationBy OperationsOnTheTender { get; private set; }
      
        public NotificationBy InquiriesAboutTender { get; private set; }
        public NotificationBy OperationsNeedToBeAccredited { get; private set; }


        public void UpdateInquiriesAboutTender(bool email, bool mobile)
        {
            InquiriesAboutTender.Email = email;
            InquiriesAboutTender.Mobile = mobile;
            EntityUpdated();
        }

        public void UpdateOperationsOnTheTender(bool email, bool mobile)
        {
            OperationsOnTheTender.Email = email;
            OperationsOnTheTender.Mobile = mobile;
            EntityUpdated();
        }


        public void UpdateOperationsNeedToBeAccredited(bool email, bool mobile)
        {
            OperationsNeedToBeAccredited.Email = email;
            OperationsNeedToBeAccredited.Mobile = mobile;
            EntityUpdated();
        }
      
    }
}
