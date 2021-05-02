using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
   public  class OffersManagerNotificationSeting: BaseNotificationSetting // مدير العروض
    {
        public OffersManagerNotificationSeting()
        {
           
        }
        public OffersManagerNotificationSeting(bool load)
        {
         
            OperationsOnTheTender = new NotificationBy() { Email = true, Mobile = true, }; 
           
            EntityCreated();
        }
        public OffersManagerNotificationSeting(
 NotificationBy operationsOnTheTender)
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
