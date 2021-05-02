using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
   public  class OffersCheckSecretaryNotificationSetting: BaseNotificationSetting  //سكرتير لجنة فحص العروض
    {
        public OffersCheckSecretaryNotificationSetting()
        {
          
        }
        public OffersCheckSecretaryNotificationSetting(bool load)
        {
          
            OperationsOnTheTender = new NotificationBy() { Email = true, Mobile = true, }; 
          
            EntityCreated();
        }
        public OffersCheckSecretaryNotificationSetting(NotificationBy inquiriesAboutTender,NotificationBy operationsOnTheTender, NotificationBy accessCodeOnTheSystem)
        {
            OperationsOnTheTender = operationsOnTheTender;
          
            EntityCreated();
        }
   
        public NotificationBy OperationsOnTheTender { get; private set; }
      




        public void UpdateOperationsOnTheTender(bool email, bool mobile)
        {
            OperationsOnTheTender.Email = email;
            OperationsOnTheTender.Mobile = mobile;
            EntityUpdated();
        }

      
      
    

    }
}
