using System;
using System.Collections.Generic;
using System.Text;

namespace MOF.Etimad.Monafasat.Core.Entities
{
   public  class OffersOppeningSecretaryNotificationSetting: BaseNotificationSetting//سكرتير لجنة فتح العروض
    {
        public OffersOppeningSecretaryNotificationSetting()
        {
           
        }
        public OffersOppeningSecretaryNotificationSetting(bool load)
        {
          
            OperationsOnTheTender = new NotificationBy() { Email = true, Mobile = true, }; 
           
           //  InquiriesAboutTender= new NotificationBy() { Email = true, Mobile = true, }; 
            EntityCreated();
        }
        public OffersOppeningSecretaryNotificationSetting( NotificationBy operationsOnTheTender)
        {
            OperationsOnTheTender = operationsOnTheTender;
          
           
           // InquiriesAboutTender = inquiriesAboutTender;
            EntityCreated();
        }
    
        public NotificationBy OperationsOnTheTender { get; private set; }
       
       // public NotificationBy InquiriesAboutTender { get; private set; }


        //public void UpdateInquiriesAboutTender(bool email,bool mobile)
        //{
        //    InquiriesAboutTender.Email = email;
        //    InquiriesAboutTender.Mobile = mobile;
        //    EntityUpdated();
        //}
        public void UpdateOperationsOnTheTender(bool email,bool mobile)
        {
            OperationsOnTheTender.Email = email;
            OperationsOnTheTender.Mobile = mobile;
            EntityUpdated();
        }

     

       

    }
}
