using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
   public  class DataEntryNotificationSetting: BaseNotificationSetting
    {
        public DataEntryNotificationSetting()
        {
          
        }
        public DataEntryNotificationSetting(bool load)
        {
            OperationsOnTheTender = new NotificationBy() { Email = true, Mobile = true, };
            InquiriesAboutTender = new NotificationBy() { Email = true, Mobile = true, }; 
            EntityCreated();
        }
        public DataEntryNotificationSetting(NotificationBy inquiriesAboutTender,
              NotificationBy operationsOnTheTender)
        {

            InquiriesAboutTender = inquiriesAboutTender;
            OperationsOnTheTender = operationsOnTheTender;
          
          
           
            EntityCreated();
        }
    
        public NotificationBy OperationsOnTheTender { get; private set; }
      
        public NotificationBy InquiriesAboutTender { get; private set; }


        public void UpdateInquiriesAboutTender(bool email,bool mobile)
        {
            InquiriesAboutTender.Email = email;
            InquiriesAboutTender.Mobile = mobile;
            EntityUpdated();
        }

        public void UpdateOperationsOnTheTender(bool email,bool mobile)
        {
            OperationsOnTheTender.Email = email;
            OperationsOnTheTender.Mobile = mobile;
            EntityUpdated();
        }



      

    }
}
