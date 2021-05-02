using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
    public class OffersCheckManagerNotificationSetting : BaseNotificationSetting //رئيس لجنة فحص العروض
    {
        public OffersCheckManagerNotificationSetting()
        {

        }

        public OffersCheckManagerNotificationSetting(bool load)
        {
           
            OperationsOnTheTender = new NotificationBy() { Email = true, Mobile = true, }; 
          
            OperationsNeedToBeAccredited = new NotificationBy() { Email = true, Mobile = true, }; 
            EntityCreated();
        }

        public OffersCheckManagerNotificationSetting(NotificationBy operationsNeedToBeAccredited,
                  NotificationBy operationsOnTheTender)
        {


            OperationsOnTheTender = operationsOnTheTender;
          
            OperationsNeedToBeAccredited = operationsNeedToBeAccredited;
          
            EntityCreated();


        }

     
        public NotificationBy OperationsOnTheTender { get; private set; }
      
        public NotificationBy OperationsNeedToBeAccredited { get; private set; }

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
